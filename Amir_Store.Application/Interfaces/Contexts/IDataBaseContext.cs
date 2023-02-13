using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Interfaces.Contexts
{
    internal interface IDataBaseContext
    {
         DbSet<User> Users { get; set; }
         DbSet<Role> Roles { get; set; }
         DbSet<UserInRole> UserInRoles { get; set; }
    }
}
