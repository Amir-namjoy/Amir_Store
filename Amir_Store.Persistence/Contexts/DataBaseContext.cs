using Amir_Store.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Persistence.Contexts
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions options):base(options)
        {

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
