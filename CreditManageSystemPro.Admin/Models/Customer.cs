using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CreditManageSystemPro.Admin.Models
{
    /// <summary>
    /// 客户表
    /// </summary>
    [Table("T_Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int customerId { get; set; }

        /// <summary>
        /// 客户名
        /// </summary>
        [Required]
        public string customerName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Required]
        public string IdCard { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime birthday { get; set; }

        /// <summary>
        /// 身份证有效期
        /// </summary>
        public DateTime expireOfId { get; set; }

        /// <summary>
        /// 座机号码
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [Required]
        public string mobile { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string occupation { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string workUnit { get; set; }

        /// <summary>
        /// 年收入
        /// </summary>
        public int incomeOfYear { get; set; }

        /// <summary>
        /// 户籍所在地
        /// </summary>
        [Required]
        public string domicilePlace { get; set; }

        /// <summary>
        /// 受教育程度
        /// </summary>
        [Required]
        public string educationLevel { get; set; }

        /// <summary>
        /// 健康状况
        /// </summary>
        public string healthStatus { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string bankAccount { get; set; }

        /// <summary>
        /// 开户名
        /// </summary>
        [Required]
        public string accountName { get; set; }

        /// <summary>
        /// 现住房产权
        /// </summary>
        public string houseRight { get; set; }

        /// <summary>
        /// 不良贷款记录
        /// </summary>
        public string badRecord { get; set; }

        /// <summary>
        /// 自有房产估价
        /// </summary>
        public string houseEvaluation { get; set; }

        /// <summary>
        /// 负债余额
        /// </summary>
        public string debtValue { get; set; }

        /// <summary>
        /// 配偶姓名
        /// </summary>
        public string spouseName { get; set; }

        /// <summary>
        /// 配偶身份证
        /// </summary>
        public string spouseIdCard { get; set; }

        /// <summary>
        /// 客户近期照片
        /// </summary>
        public string recentPhoto { get; set; }

        /// <summary>
        /// 图片附件
        /// </summary>
        public string picAttachment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }
    }




    [Serializable]
    public class AddCustomerModel
    {
        public int customerId { get; set; }

        /// <summary>
        /// 客户名
        /// </summary>
        [Required]
        public string customerName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Required]
        public string IdCard { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime birthday { get; set; }

        /// <summary>
        /// 身份证有效期
        /// </summary>
        public DateTime expireOfId { get; set; }

        /// <summary>
        /// 座机号码
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [Required]
        public string mobile { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string occupation { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string workUnit { get; set; }

        /// <summary>
        /// 年收入
        /// </summary>
        public int incomeOfYear { get; set; }

        /// <summary>
        /// 户籍所在地
        /// </summary>
        [Required]
        public string domicilePlace { get; set; }

        /// <summary>
        /// 受教育程度
        /// </summary>
        [Required]
        public string educationLevel { get; set; }

        /// <summary>
        /// 健康状况
        /// </summary>
        public string healthStatus { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string bankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        [Required]
        public string accountName { get; set; }

        /// <summary>
        /// 现住房产权
        /// </summary>
        public string houseRight { get; set; }

        /// <summary>
        /// 不良贷款记录
        /// </summary>
        public string badRecord { get; set; }

        /// <summary>
        /// 自有房产估价
        /// </summary>
        public string houseEvaluation { get; set; }

        /// <summary>
        /// 负债余额
        /// </summary>
        public string debtValue { get; set; }

        /// <summary>
        /// 配偶姓名
        /// </summary>
        public string spouseName { get; set; }

        /// <summary>
        /// 配偶身份证
        /// </summary>
        public string spouseIdCard { get; set; }

        /// <summary>
        /// 客户近期照片
        /// </summary>
        public string recentPhoto { get; set; }

        /// <summary>
        /// 图片附件
        /// </summary>
        public string picAttachment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }
    }


    [Serializable]
    public class UpdateCustomerModel
    {
        public int customerId { get; set; }

        /// <summary>
        /// 客户名
        /// </summary>
        [Required]
        public string customerName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Required]
        public string IdCard { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime birthday { get; set; }

        /// <summary>
        /// 身份证有效期
        /// </summary>
        public DateTime expireOfId { get; set; }

        /// <summary>
        /// 座机号码
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [Required]
        public string mobile { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string occupation { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string workUnit { get; set; }

        /// <summary>
        /// 年收入
        /// </summary>
        public int incomeOfYear { get; set; }

        /// <summary>
        /// 户籍所在地
        /// </summary>
        [Required]
        public string domicilePlace { get; set; }

        /// <summary>
        /// 受教育程度
        /// </summary>
        [Required]
        public string educationLevel { get; set; }

        /// <summary>
        /// 健康状况
        /// </summary>
        public string healthStatus { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string bankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        [Required]
        public string accountName { get; set; }

        /// <summary>
        /// 现住房产权
        /// </summary>
        public string houseRight { get; set; }

        /// <summary>
        /// 不良贷款记录
        /// </summary>
        public string badRecord { get; set; }

        /// <summary>
        /// 自有房产估价
        /// </summary>
        public string houseEvaluation { get; set; }

        /// <summary>
        /// 负债余额
        /// </summary>
        public string debtValue { get; set; }

        /// <summary>
        /// 配偶姓名
        /// </summary>
        public string spouseName { get; set; }

        /// <summary>
        /// 配偶身份证
        /// </summary>
        public string spouseIdCard { get; set; }

        /// <summary>
        /// 客户近期照片
        /// </summary>
        public string recentPhoto { get; set; }

        /// <summary>
        /// 图片附件
        /// </summary>
        public string picAttachment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }
    }
}