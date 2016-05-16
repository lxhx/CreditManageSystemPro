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
    /// 权限表
    /// </summary>
    [Table("T_Privilege")]
    public class Privilege
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int privilegeId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Required]
        public string privilegeName { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string privilegeNote { get; set; }
    }

    [Serializable]
    public class AddPrivilegeModel
    {
        public int privilegeId { get; set; }

        public int menuId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [Remote("ValidatePrivilegeName", "Privilege", ErrorMessage = "* 权限名称重复")]
        public string privilegeName { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string privilegeNote { get; set; }
    }

    [Serializable]
    public class UpdatePrivilegeModel
    {
        public int privilegeId { get; set; }

        public int menuId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Remote("ValidatePrivilegeName", "Privilege", ErrorMessage = "* 权限名称重复")]
        public string privilegeName { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string privilegeNote { get; set; }
    }
}