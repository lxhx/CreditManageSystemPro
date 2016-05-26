using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreditManageSystemPro.Admin.Models;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class LoanController : Controller
    {
        //
        // GET: /Loan/
        private CreditManageContext db = new CreditManageContext();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 贷款申请
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanApply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoanApply(AddLoanInfoModel model)
        {
            return View();
        }

        public ActionResult UpdateLoanApply(int customerId)
        {
            var loanApplyInfo = db.LoanInfo.Where(m => m.customerId == customerId).FirstOrDefault();
            UpdateLoanInfoModel model = new UpdateLoanInfoModel();
            if (loanApplyInfo != null)
            {
                model.loanInfoId = loanApplyInfo.loanInfoId;
                model.customerId = loanApplyInfo.customerId;
                model.userId = loanApplyInfo.userId;
                model.contractNo = loanApplyInfo.contractNo;
                model.customerType = loanApplyInfo.customerType;
                model.loanValue = loanApplyInfo.loanValue;
                model.loanPurpose = loanApplyInfo.loanPurpose;
                model.loanType = loanApplyInfo.loanType;
                model.calTimeType = loanApplyInfo.calTimeType;
                model.startTime = loanApplyInfo.startTime;
                model.endTime = loanApplyInfo.endTime;
                model.periodNum = loanApplyInfo.periodNum;
                model.intervalDays = loanApplyInfo.intervalDays;
                model.monthRate = loanApplyInfo.monthRate;
                model.calInterestWay = loanApplyInfo.calInterestWay;
                model.penaltyRate = loanApplyInfo.penaltyRate;
                model.payWay = loanApplyInfo.payWay;
                model.breachScale = loanApplyInfo.breachScale;
                model.agreementInterest = loanApplyInfo.agreementInterest;
                model.advanceInterest = loanApplyInfo.advanceInterest;
                model.poundage = loanApplyInfo.poundage;
                model.guarantee = loanApplyInfo.guarantee;
                model.guaranteeWay = loanApplyInfo.guaranteeWay;
                model.remark = loanApplyInfo.remark;
            }
            else
            {
                Customer customer = db.Customer.Find(customerId);
                model.customerId = customerId;
                model.contractNo = Utility.Utils.GetRamCode();
                model.Customer = customer;
            }
            return View(model);
        }

        /// <summary>
        /// 修改贷款申请资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateLoanApply(UpdateLoanInfoModel model)
        {
            return View();
        }

        /// <summary>
        /// 审查列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanCheckList()
        {
            var customerList = db.Customer.ToList();
            ViewBag.CustomerCount = customerList.Count;
            return View(customerList);
        }

        /// <summary>
        /// 审查详情
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanCheckDetail()
        {
            return View();
        }

        /// <summary>
        /// 审议列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanReviewList()
        {
            var customerList = db.Customer.ToList();
            ViewBag.CustomerCount = customerList.Count;
            return View(customerList);
        }

        /// <summary>
        /// 审议详情
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanReviewDetail()
        {
            return View();
        }

        /// <summary>
        /// 审定列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanValidationList()
        {
            var customerList = db.Customer.ToList();
            ViewBag.CustomerCount = customerList.Count;
            return View(customerList);
        }

        /// <summary>
        /// 审定详情
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanValidationDetail()
        {
            return View();
        }
    }
}
