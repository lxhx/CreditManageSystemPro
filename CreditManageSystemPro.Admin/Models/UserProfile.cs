using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditManageSystemPro.Admin.Models
{
    [Table("T_UserProfile")]
    public class UserProfile
    {
        public int userProfileId { get; set; }

        /// <summary>
        /// 外键
        /// </summary>
        public int userId { get; set; }
        public virtual User user { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public DateTime createDate { get; set; }

        /// <summary>
        /// 上次密码登录错误日期
        /// </summary>
        public DateTime? lastPasswordFailureDate { get; set; }

        /// <summary>
        /// 密码登录错误次数
        /// </summary>
        public int? passwordFailuresSinceLastSuccess { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string password { get; set; }

        /// <summary>
        /// 密码修改日期
        /// </summary>
        public DateTime? passwordChangedDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string userNote { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 6位随机字符串,加密用到
        /// </summary>
        public string salt { get; set; }
    }
}