using SSO.DataModel;
using SSO.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Service.Logic
{
    /// <summary>
    /// Class to handle the registration logic
    /// </summary>
    public class RegistrationManagement : LogicBase, IRegistrationManagement
    {
        #region Properties
        private readonly IAppUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly ICrypto _crypto;
        private AppUser _user;
        #endregion

        /// <summary>
        /// Constructor Injection for all required object
        /// </summary>
        /// <param name="userRepository">Repository to handle CURD for AppUser table</param>
        /// <param name="logger">Logger to log messages</param>
        /// <param name="crypto">Encryption for password</param>
        public RegistrationManagement(IAppUserRepository userRepository, ILogger logger, ICrypto crypto)
        {
            _userRepository = userRepository;
            _crypto = crypto;
            _logger = logger;
        }

        /// <summary>
        /// Registration implementation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RegistrationResposeModel> RegisterNewUser(RegistrationRequestModel request)
        {
            RegistrationResposeModel result = new RegistrationResposeModel();

            IsError = !await GenerateHashedPassword(request);

            if (IsError)
            {
                result.ErrorMessage = "Failed to generate hashed password";
                return result;
            }

            _user.Username = request.Username;
            _user.FullName = request.FullName;
            _user.UserLevel = request.UserLevel;

            try
            {
                result.Success = _userRepository.RegisterNewUser(_user);
            }
            catch (Exception e)
            {
                IsError = true;
                result.ErrorMessage = "Failed to save new user";
                _logger.Error(e, "Failed to save new user");
            }

            return result;
        }

        //Generate password salt and hased password
        internal async Task<bool> GenerateHashedPassword(RegistrationRequestModel request)
        {
            bool result = false;
            _user = new AppUser();

            try
            {
                var hashed = await _crypto.EncryptPassword(request.Password);
                if (hashed?.Count == 0)
                {
                    _logger.Warning("Hashed password is empty");
                    return result;
                }

                _user.PasswordSalt = Convert.ToBase64String(hashed.FirstOrDefault().Value);
                _user.HashedPassword = hashed.FirstOrDefault().Key;
                result = true;
            }
            catch (Exception e)
            {
                IsError = true;
                _logger.Error(e, "Failed to generate hashed password");
            }

            return result;
        }
    }
}
