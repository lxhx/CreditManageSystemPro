using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CreditManageSystemPro.Admin.Models
{
    /// <summary>
    /// 贷款申请信息表
    /// </summary>
    [Table("T_LoanInfo")]
    public class LoanInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int loanInfoId { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        [Required]
        public int customerId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public int userId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        [Required]
        public string contractNo { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public string customerType { get; set; }

        /// <summary>
        /// 贷款金额
        /// </summary>
        [Required]
        public int loanValue { get; set; }

        /// <summary>
        /// 贷款用途
        /// </summary>
        public string loanPurpose { get; set; }

        /// <summary>
        /// 贷款类型
        /// </summary>
        public string loanType { get; set; }

        /// <summary>
        /// 计算时间的方式
        /// </summary>
        public string calTimeType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// 还款期数
        /// </summary>
        public int periodNum { get; set; }

        /// <summary>
        /// 间隔天数
        /// </summary>
        public int intervalDays { get; set; }

        /// <summary>
        /// 月利率
        /// </summary>
        public int monthRate{ get; set; }

        /// <summary>
        /// 计息方法
        /// </summary>
        public string calInterestWay { get; set; }

        /// <summary>
        /// 逾期罚息
        /// </summary>
        public int penaltyRate { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string payWay { get; set; }

        /// <summary>
        /// 提前还款的违约比例
        /// </summary>
        public int breachScale { get; set; }

        /// <summary>
        /// 协议利息
        /// </summary>
        public int agreementInterest { get; set; }

        /// <summary>
        /// 预收利息
        /// </summary>
        public int advanceInterest { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public int poundage { get; set; }

        /// <summary>
        /// 担保人
        /// </summary>
        public string guarantee { get; set; }

        /// <summary>
        /// 担保方式
        /// </summary>
        public string guaranteeWay { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 贷款的状态
        /// </summary>
        public int loanState { get; set; }
    }

    [Serializable]
    public class AddLoanInfoModel
    {
        public int loanInfoId { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public int customerId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string contractNo { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public string customerType { get; set; }

        /// <summary>
        /// 贷款金额
        /// </summary>
        public int loanValue { get; set; }

        /// <summary>
        /// 贷款用途
        /// </summary>
        public string loanPurpose { get; set; }

        /// <summary>
        /// 贷款类型
        /// </summary>
        public string loanType { get; set; }

        /// <summary>
        /// 计算时间的方式
        /// </summary>
        public string calTimeType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// 还款期数
        /// </summary>
        public int periodNum { get; set; }

        /// <summary>
        /// 间隔天数
        /// </summary>
        public int intervalDays { get; set; }

        /// <summary>
        /// 月利率
        /// </summary>
        public int monthRate { get; set; }

        /// <summary>
        /// 计息方法
        /// </summary>
        public string calInterestWay { get; set; }

        /// <summary>
        /// 逾期罚息
        /// </summary>
        public int penaltyRate { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string payWay { get; set; }

        /// <summary>
        /// 提前还款的违约比例
        /// </summary>
        public int breachScale { get; set; }

        /// <summary>
        /// 协议利息
        /// </summary>
        public int agreementInterest { get; set; }

        /// <summary>
        /// 协议利息
        /// </summary>
        public int advanceInterest { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public int poundage { get; set; }

        /// <summary>
        /// 担保人
        /// </summary>
        public string guarantee { get; set; }

        /// <summary>
        /// 担保方式
        /// </summary>
        public string guaranteeWay { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 贷款的状态
        /// </summary>
        public int loanState { get; set; }

        /// <summary>
        /// 客户经理信息
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// 客户信息
        /// </summary>
        public Customer Customer { get; set; }
    }

    [Serializable]
    public class UpdateLoanInfoModel
    {
        public int loanInfoId { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public int customerId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string contractNo { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public string customerType { get; set; }

        /// <summary>
        /// 贷款金额
        /// </summary>
        public int loanValue { get; set; }

        /// <summary>
        /// 贷款用途
        /// </summary>
        public string loanPurpose { get; set; }

        /// <summary>
        /// 贷款类型
        /// </summary>
        public string loanType { get; set; }

        /// <summary>
        /// 计算时间的方式
        /// </summary>
        public string calTimeType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// 还款期数
        /// </summary>
        public int periodNum { get; set; }

        /// <summary>
        /// 间隔天数
        /// </summary>
        public int intervalDays { get; set; }

        /// <summary>
        /// 月利率
        /// </summary>
        public int monthRate { get; set; }

        /// <summary>
        /// 计息方法
        /// </summary>
        public string calInterestWay { get; set; }

        /// <summary>
        /// 逾期罚息
        /// </summary>
        public int penaltyRate { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string payWay { get; set; }

        /// <summary>
        /// 提前还款的违约比例
        /// </summary>
        public int breachScale { get; set; }

        /// <summary>
        /// 协议利息
        /// </summary>
        public int agreementInterest { get; set; }

        /// <summary>
        /// 协议利息
        /// </summary>
        public int advanceInterest { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public int poundage { get; set; }

        /// <summary>
        /// 担保人
        /// </summary>
        public string guarantee { get; set; }

        /// <summary>
        /// 担保方式
        /// </summary>
        public string guaranteeWay { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 贷款的状态
        /// </summary>
        public int loanState { get; set; }

        /// <summary>
        /// 客户经理信息
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// 客户信息
        /// </summary>
        public Customer Customer { get; set; }
    }
}