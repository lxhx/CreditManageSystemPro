using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class MailboxController : Controller
    {
        //
        // GET: /Mailbox/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Inbox()
        {
            return View();
        }

        /// <summary>
        /// 查看邮件
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewMail()
        {
            return View();
        }

        /// <summary>
        /// 写信
        /// </summary>
        /// <returns></returns>
        public ActionResult WriteMail()
        {
            return View();
        }
    }
}
