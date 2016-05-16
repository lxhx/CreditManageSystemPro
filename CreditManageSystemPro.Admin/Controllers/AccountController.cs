using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CreditManageSystemPro.Admin.Models;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class AccountController : Controller
    {
        private CreditManageContext db=new CreditManageContext();
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserProfile userProfileModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userProfile = db.UserProfile.SingleOrDefault(u => u.user.userName.Equals(userProfileModel.user.userName));
                if (userProfile != null && userProfile.password == userProfileModel.password)//登录成功
                {
                    FormsAuthentication.SetAuthCookie(userProfile.userId.ToString(),true);
                    Session["LoginedUser"] = userProfile;
                    Session["userName"] = userProfile.user.userName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("","用户名或者密码不正确！");
                    return View(userProfileModel);
                }
            }
            else
            {
                return View(userProfileModel);
            }
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
