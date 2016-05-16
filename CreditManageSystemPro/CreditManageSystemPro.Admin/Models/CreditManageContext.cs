using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CreditManageSystemPro.Admin.Models
{
    public class CreditManageContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<Menu> Menu { get; set; }
    }
}