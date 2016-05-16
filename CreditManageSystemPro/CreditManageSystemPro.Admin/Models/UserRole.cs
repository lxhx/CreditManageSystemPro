using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CreditManageSystemPro.Admin.Models
{
    /// <summary>
    /// 用户角色关系表
    /// </summary>
    [Table("T_UserRole")]
    public class UserRole
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int userRoleId { get; set; }

        /// <summary>
        /// 外键-用户id
        /// </summary>
        [Required]
        public int userId { get; set; }
        public User user { get; set; }

        /// <summary>
        /// 外键-角色id
        /// </summary>
        [Required]
        public int roleId { get; set; }
        public Role role { get; set; }

    }
}