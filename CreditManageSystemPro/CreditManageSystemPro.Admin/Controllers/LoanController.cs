using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class LoanController : Controller
    {
        //
        // GET: /Loan/

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

        /// <summary>
        /// 审查列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanCheckList()
        {
            return View();
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
            return View();
        }

        /// <summary>
        /// 审议详情
        /// </summary>
        /// <returns></returns>
        public ActionResult LoanReviewDetail()
        {
            return View();
        }
    }
}
