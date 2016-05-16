using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
}