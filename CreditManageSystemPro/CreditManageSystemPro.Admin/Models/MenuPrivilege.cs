using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CreditManageSystemPro.Admin.Models
{
    /// <summary>
    /// 菜单权限表
    /// </summary>
    [Table("T_MenuPrivilege")]
    public class MenuPrivilege
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int menuPrivilegeId { get; set; }

        /// <summary>
        /// 外键-菜单id
        /// </summary>
        [Required]
        public int menuId { get; set; }
        public Menu menu { get; set; }

        /// <summary>
        /// 外键-权限id
        /// </summary>
        [Required]
        public int privilegeId { get; set; }
        public Privilege privilege { get; set; }
    }
}