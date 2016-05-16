using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreditManageSystemPro.Admin.Models;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class UserController : Controller
    {
        string saveKey = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AuthSaveKey"]) ? "LoginedUser" : System.Configuration.ConfigurationManager.AppSettings["AuthSaveKey"];
        string _domain = System.Configuration.ConfigurationSettings.AppSettings["Domain"];
        private CreditManageContext db = new CreditManageContext();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        //
        // GET: /User/

        public ActionResult Index()
        {
            var model = db.User.ToList();
            var model1 = db.UserProfile.ToList();
            return View(db.User.ToList());
        }

        /// <summary>
        /// 获得单个用户的个人资料
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonData()
        {
            return View();
        }

        /// <summary>
        /// 获得所有的用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AllUser()
        {
            return View();
        }

        /// <summary>
        /// 获取所有的客户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ClientList()
        {
            return View();
        }

        public ActionResult AddClient()
        {
            return View();
        }
    }
}
