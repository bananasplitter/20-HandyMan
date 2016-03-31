using HandyMan.Core.Domain;
using HandyMan.Core.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Infrastructure
{
    public interface IAuthorizationRepository : IDisposable
    {
        Task<User> FindUser(string username, string password);
        Task<IdentityResult> RegisterUser(RegistrationModel model);
    }
}
