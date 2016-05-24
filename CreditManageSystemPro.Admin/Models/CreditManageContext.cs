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

        public DbSet<Role> Role { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Privilege> Privilege { get; set; }

        public DbSet<RolePrivilege> RolePrivilege { get; set; }

        public DbSet<MenuPrivilege> MenuPrivilege { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<LoanInfo> LoanInfo { get; set; }
    }
}