using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public class Profile : UserModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Telephone { get; set; }
            public string Email { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string Zip { get; set; }
            public string State { get; set; }
            public int? Rate { get; set; }

        }
    }
}
