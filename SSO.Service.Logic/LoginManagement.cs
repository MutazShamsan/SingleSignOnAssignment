using SSO.DataModel;
using SSO.Interfaces;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("SSO.Test")]
namespace SSO.Service.Logic
{
    /// <summary>
    /// Class to handle the login logic
    /// </summary>
    public class LoginManagement : LogicBase, ILoginManagement
    {
        #region Properties
        private readonly IAppUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly ICrypto _crypto;
        private readonly ICacheManagement _cacheManagement;
        private AppUser _signedInUser;
        #endregion

        /// <summary>
        /// Constructor Injection for all required object
        /// </summary>
        /// <param name="userRepository">Repository to handle CURD for AppUser table</param>
        /// <param name="logger">Logger to log messages</param>
        /// <param name="crypto">Encryption for password</param>
        /// <param name="cacheManagement">Cache Management</param>
        public LoginManagement(IAppUserRepository userRepository, ILogger logger, ICrypto crypto, ICacheManagement cacheManagement)
        {
            _userRepository = userRepository;
            _logger = logger;
            _crypto = crypto;
            _cacheManagement = cacheManagement;
        }

        /// <summary>
        /// Login user implementation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginResposeModel> LoginUser(LoginRequsetModel request)
        {
            LoginResposeModel result = new LoginResposeModel();

            //Try to get from the cache first
            result = AuthorizeFromCache(request);

            //If not available, search from db
            if (!result.IsAuthorized)
                result = await AuthorizeFromDatabase(request);

            //Prepare response and cache it
            if (result.IsAuthorized && !IsError)
            {
                result.Success = true;
                result.LastActiveTime = DateTimeOffset.UtcNow;
                result.Username = request.Username;

                if (string.IsNullOrEmpty(result.AccessToken))
                {
                    result.AccessToken = CreateAccessToken(request.Username);
                    result.FirstActiveTime = DateTimeOffset.UtcNow;
                }

                _cacheManagement.SetOnly(request.Username, result);
            }

            return result;
        }

        /// <summary>
        /// Read from the cache if the user is still logged in
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        internal LoginResposeModel AuthorizeFromCache(LoginRequsetModel requset)
        {
            LoginResposeModel result = new LoginResposeModel();

            var cachedItem = _cacheManagement.GetOnly(requset.Username) as LoginResposeModel;

            if (cachedItem != null)
                result = cachedItem;

            return result;
        }

        /// <summary>
        /// Read from database if this user is registered and authorized to login
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        internal async Task<LoginResposeModel> AuthorizeFromDatabase(LoginRequsetModel requset)
        {
            LoginResposeModel result = new LoginResposeModel();

            //Since plain-text password is not saved in db, then it has to be hashed to find it in db
            var hashedPassword = await GetHashedPassword(requset.Username, requset.Password);

            IsError = string.IsNullOrEmpty(hashedPassword);
            try
            {
                if (!IsError)
                {
                    _signedInUser = _userRepository.GetUser(requset.Username, hashedPassword);

                    result.IsAuthorized = _signedInUser != null;
                    result.UserLevel = _signedInUser.UserLevel;
                }
            }
            catch (Exception e)
            {
                IsError = true;
                _logger.Error(e, "Failed to get user record");
            }

            return result;
        }

        /// <summary>
        /// Generate random access token for each successful login
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal string CreateAccessToken(string username)
        {
            return Common.TokenGenerator.GenerateToken(username);
        }

        /// <summary>
        /// Get the password salt used to hash the password from db
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal string GetPasswordSalt(string username)
        {
            string result = null;
            try
            {
                result = _userRepository.GetPasswordSalt(username);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to get user record");
            }

            return result;
        }

        /// <summary>
        /// Use Encryption to generate the hashed password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="plainPassword"></param>
        /// <returns></returns>
        internal async Task<string> GetHashedPassword(string username, string plainPassword)
        {
            string result = null;

            var salt = GetPasswordSalt(username);

            try
            {
                result = (await _crypto.EncryptPassword(plainPassword, Convert.FromBase64String(salt))).FirstOrDefault().Key;
            }
            catch (Exception e)
            {
                IsError = true;
                _logger.Error(e, "Failed to get hashed password");
            }

            return result;
        }

        /// <summary>
        /// When logout, Remove the logged in user from the cache
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<LoginResposeModel> LogoutUser(LoginRequsetModel request)
        {
            var result = new LoginResposeModel();

            try
            {
                _cacheManagement.Expire(request.Username);
                result.Success = true;
            }
            catch (Exception e)
            {
                IsError = true;
                _logger.Error(e, "Failed to logout user");
            }

            return Task.FromResult(result);
        }
    }

}
