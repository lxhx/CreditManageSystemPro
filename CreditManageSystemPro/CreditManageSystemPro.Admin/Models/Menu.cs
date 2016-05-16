using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace CreditManageSystemPro.Admin.Models
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table("T_Menu")]
    public class Menu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int menuId { get; set; }

        /// <summary>
        /// 父菜单id
        /// </summary>
        [DefaultValue(0)]
        [Required]
        public int parentMenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        public string menuName { get; set; }

        /// <summary>
        /// 菜单URL
        /// </summary>
        [Required]
        public string menuUrl { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public string menuNote { get; set; }

    }
    [Serializable]
    public class AddMenuModel
    {
        /// <summary>
        /// 父菜单名称
        /// </summary>
        [NotMapped]
        public int parentMenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Remote("ValidateMenuName", "Menu", ErrorMessage = "* 菜单名称重复")]
        public string menuName { get; set; }

        /// <summary>
        /// 菜单Url
        /// </summary>
        [Remote("ValidateMenuUrl", "Menu", ErrorMessage = "* 菜单Url重复")]
        public string menuUrl { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public string menuNote { get; set; }
    }
}