using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Domain
{
    public class Role
    {
        //Primary Key
        public int RoleId { get; set; }

        //Foreign Key NONE

        //Properties
        public string Name { get; set; }

        //Relations
        public virtual ICollection<User> Users { get; set; }
    }
}
