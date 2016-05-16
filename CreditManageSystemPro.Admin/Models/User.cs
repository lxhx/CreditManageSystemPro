using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditManageSystemPro.Admin.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("T_User")]
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string userName { get; set; }
    }

    [Serializable]
    public class AddUserModel
    {
        public int userId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public int roleId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户简介
        /// </summary>
        public UserProfile UserProfile { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string RePassword { get; set; }
    }

    [Serializable]
    public class UpdateUserModel
    {
        public int userId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public int roleId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户简介
        /// </summary>
        public UserProfile UserProfile { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string RePassword { get; set; }
    }
}