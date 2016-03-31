using HandyMan.Core.Domain;
using HandyMan.Core.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HandyMan.Data.Infrastructure
{
    public class UserStore : Disposable,
        IUserPasswordStore<User, string>,
        IUserSecurityStampStore<User, string>
        //IUserRoleStore<User, string>
    {
        private readonly IDatabaseFactory _databaseFactory;

        private HandyManDataContext _dataContext;
        protected HandyManDataContext DataContext
        {
            get
            {
                return _dataContext ?? (_dataContext = _databaseFactory.GetDataContext());
            }
        }
        public UserStore(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        //  IUserPasswordStore
        public Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                user.Id = Guid.NewGuid().ToString();
                DataContext.Users.Add(user);
                DataContext.SaveChanges();
            });
        }

        public Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>{
                DataContext.Users.Remove(user);
                DataContext.SaveChanges();
            });
        }

        public Task<User> FindByIdAsync(string userId)
        {
            return Task.Factory.StartNew(() => {
                return DataContext.Users.FirstOrDefault(u => u.Id == userId);
            });
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() =>
            {
                return DataContext.Users.FirstOrDefault(u => u.UserName == userName);
            });
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.Factory.StartNew(() =>
            {
                return user.PasswordHash;
            });
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                DataContext.Users.Attach(user);
                DataContext.Entry(user).State = EntityState.Modified;

                DataContext.SaveChanges();
            });
        }
        // ISecurityStampStore

        public Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }
    }
}

        // IUserRoleStore I DO NOT HAVE A USER ROLE SO I MIGHT NOT NEED THIS
    //    public Task AddToRoleAsync(User user, string roleName)
    //    {
    //        if (user == null)
    //        {
    //            throw new ArgumentNullException(nameof(user));
    //        }
    //        if (string.IsNullOrEmpty(roleName))
    //        {
    //            throw new ArgumentException("Argument cannot be null or empty: roleName");
    //        }

    //        return Task.Factory.StartNew(() =>
    //        {
    //            if (!DataContext.Roles.Any(r => r.Name == roleName))
    //            {
    //                DataContext.Roles.Add(new Role
    //                {
    //                    RoleId = Guid.NewGuid().ToString(),
    //                    Name = roleName
    //                });
    //            }

    //            DataContext.Roles.Add(new Role
    //            {
    //                Roles = DataContext.Roles.FirstOrDefault(r => r.Name == roleName),
    //                User = user
    //            });

    //            DataContext.SaveChanges();
    //        });
    //    }
    //    public Task RemoveFromRoleAsync(User user, string roleName)
    //    {
    //        if (user == null)
    //        {
    //            throw new ArgumentNullException(nameof(user));
    //        }
    //        if (string.IsNullOrEmpty(roleName))
    //        {
    //            throw new ArgumentException("Argument cannot be null or empty: roleName");
    //        }

    //        return Task.Factory.StartNew(() =>
    //        {
    //            var userRole = user.Roles.FirstorDefault(ref => roleName.Role.Name == roleName);

    //            if (userRole == null)
    //            {
    //                throw new InvalidOperationException("User does not have that role");
    //            }
    //            DataContext.UserRoles.Remove(IUserRoleStore);

    //            DataContext.SaveChanges();

    //        });
    //    }
    //    public Task<IList<string>> GetRoleAsync(User user)
    //    {
    //        return Task.Factory.StartNew(() =>
    //        {
    //            return (IList<string>)user.Roles.Select(ur => ur.Role.Name);
    //        });

    //    }
    //}
