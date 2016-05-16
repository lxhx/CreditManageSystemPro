using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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

    [Serializable]
    public class AddRoleModel
    {
        public int roleId { get; set; }

        public string privilegeIds { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [Remote("ValidateRoleName", "Role", ErrorMessage = "* 角色名称重复")]
        public string roleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string roleNote { get; set; }
    }

    [Serializable]
    public class UpdateRoleModel
    {
        public int roleId { get; set; }

        public string privilegeIds { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [Remote("ValidateRoleName", "Role", ErrorMessage = "* 角色名称重复")]
        public string roleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string roleNote { get; set; }
    }
}