using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CreditManageSystemPro.Admin.Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("T_Role")]
    public class Role
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int roleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        public string roleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string roleNote { get; set; }
    }
}