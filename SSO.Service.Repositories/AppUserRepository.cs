using SSO.DataModel;
using SSO.Interfaces;
using System.Collections.Generic;

namespace SSO.Service.Repositories
{
    /// <summary>
    /// Repository to handle table CRUD
    /// </summary>
    public class AppUserRepository : RepositoryBase, IAppUserRepository
    {
        public AppUserRepository(IDataContext context)
        {
            AppContext = context;
        }

        public bool RegisterNewUser(AppUser user)
        {
            return InsertRecord(user) > 0;
        }

        /// <summary>
        /// Get user record by username and hashed password
        /// use the 'nameof' to take advantage of the C# stronge type and avoid typo error in sql querys
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        public AppUser GetUser(string username, string hashedPassword)
        {
            return GetSingleResult<AppUser>($"SELECT TOP(1) {nameof(AppUser.Id)}, {nameof(AppUser.Username)}, {nameof(AppUser.FullName)}, {nameof(AppUser.UserLevel)} FROM {nameof(AppUser)} WHERE {nameof(AppUser.Username)} = @{nameof(AppUser.Username)} AND {nameof(AppUser.HashedPassword)} = @{nameof(AppUser.HashedPassword)}", new Dictionary<string, object>() { { nameof(AppUser.Username), username }, { nameof(AppUser.HashedPassword), hashedPassword } });
        }

        /// <summary>
        /// Not Used -- Get user record by id
        /// use the 'nameof' to take advantage of the C# stronge type and avoid typo error in sql querys
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AppUser GetUser(int id)
        {
            return GetSingleResult<AppUser>($"SELECT TOP(1) {nameof(AppUser.Id)}, {nameof(AppUser.Username)}, {nameof(AppUser.FullName)}, {nameof(AppUser.UserLevel)} FROM {nameof(AppUser)} WHERE {nameof(AppUser.Id)} = @{nameof(AppUser.Id)}", new Dictionary<string, object>() { { nameof(AppUser.Id), id } });
        }

        /// <summary>
        /// Get user record password salt by username
        /// use the 'nameof' to take advantage of the C# stronge type and avoid typo error in sql querys
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPasswordSalt(string username)
        {
            return GetSingleResult<AppUser>(
                $"SELECT TOP(1) {nameof(AppUser.PasswordSalt)} FROM {nameof(AppUser)} WHERE {nameof(AppUser.Username)} = @{nameof(AppUser.Username)}",
                new Dictionary<string, object>() { { nameof(AppUser.Username), username } })?.PasswordSalt;
        }
    }
}
