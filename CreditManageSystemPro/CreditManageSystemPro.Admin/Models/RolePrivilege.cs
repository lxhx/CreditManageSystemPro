using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CreditManageSystemPro.Admin.Models
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    [Table("T_RolePrivilege")]
    public class RolePrivilege
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int rolePrivilegeId { get; set; }

        /// <summary>
        /// 外键-角色id
        /// </summary>
        [Required]
        public int roleId { get; set; }
        public virtual Role role { get; set; }

        /// <summary>
        /// 外键-权限id
        /// </summary>
        [Required]
        public int privilegeId { get; set; }
        public virtual Privilege privilege { get; set; }
    }
}