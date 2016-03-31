using HandyMan.Core.Domain;
using HandyMan.Core.Infrastructure;
using HandyMan.Core.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Data.Infrastructure
{
    public class AuthorizationRepository : IAuthorizationRepository, IDisposable
    {
        private readonly IUserStore<User, string> _userStore;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly UserManager<User, string> _userManager;

        private HandyManDataContext db;
        protected HandyManDataContext Db => db ?? (db = _databaseFactory.GetDataContext());

        public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<User, string> userStore)
        {
            _userStore = userStore;
            _databaseFactory = databaseFactory;
            _userManager = new UserManager<User, string>(userStore);
        }
        public async Task<IdentityResult> RegisterUser(RegistrationModel model)
        {
            var User = new User
            {
                UserName = model.Username,
                Email = model.EmailAddress
            };

            var result = await _userManager.CreateAsync(User, model.Password);

            return result;
        }

            public async Task<User> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
        
    }
}
