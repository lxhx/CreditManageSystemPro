using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using CreditManageSystemPro.Admin.Models;
using CreditManageSystemPro.Admin.Utility;

namespace CreditManageSystemPro.Admin.Controllers
{
    //[Authorization]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private CreditManageContext db = new CreditManageContext();
        StringBuilder sb = new StringBuilder();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexMenu()
        {
            return Content(get_navigation_list());
        }
        public ActionResult mIndex()
        {
            return View();
        }
        public ActionResult Index_2()
        {
            return View();
        }
        public ActionResult Index_3()
        {
            return View();
        }
        public ActionResult Index_4()
        {
            return View();
        }
        public ActionResult Index_5()
        {
            return View();
        }
        public ActionResult Index_6()
        {
            return View();
        }
        public ActionResult Index_7()
        {
            return View();
        }
        public ActionResult Index_8()
        {
            return View();
        }
        public ActionResult Index_9()
        {
            return View();
        }
        public ActionResult Index_10()
        {
            return View();
        }
        public ActionResult Index_11()
        {
            return View();
        }
        public ActionResult Index_12()
        {
            return View();
        }
        public ActionResult Index_13()
        {
            return View();
        }
        public ActionResult Index_14()
        {
            return View();
        }
        public ActionResult Index_15()
        {
            return View();
        }
        public ActionResult Index_16()
        {
            return View();
        }
        public ActionResult Index_17()
        {
            return View();
        }
        public ActionResult Index_18()
        {
            return View();
        }

        private string get_navigation_list()
        {
            using (CreditManageContext db = new CreditManageContext())
            {
                var menuQuery = db.Menu.ToList();
                //登录的用户
                UserProfile userProfile = (UserProfile)Session["LoginedUser"];
                //List<Menu> menuQuery = new List<Menu>();

                
                //if (userProfile != null)
                //{
                //    UserRole userRole = db.UserRole.Where(m => m.userId == userProfile.userId).FirstOrDefault();
                //    if (userRole != null)
                //    {
                //        RolePrivilege rolePrivilege = db.RolePrivilege.Find(userRole.roleId);
                //        string privilegeIds = rolePrivilege == null ? string.Empty : rolePrivilege.privilegeIds;
                //        if (!string.IsNullOrEmpty(privilegeIds))
                //        {
                //            string[] ids = privilegeIds.Split(',');
                //            for (int i = 0; i < ids.Length; i++)
                //            {
                //                MenuPrivilege menuPrivilege =
                //                    db.MenuPrivilege.Where(m => m.privilegeId == Convert.ToInt32(ids[i])).FirstOrDefault();
                //                if (menuPrivilege != null)
                //                {
                //                    Menu menu = db.Menu.Find(menuPrivilege.menuId);
                //                    menuQuery.Add(menu);
                //                }
                //            }
                //        }
                //    }
                //}
                return this.get_navigation_childs(menuQuery, 0,userProfile);
            }
        }

        private string get_navigation_childs(List<Menu> menus, int parent_id,UserProfile userProfile)
        {
            List<Menu> subMenus = (from item in menus where item.parentMenuId == parent_id select item).ToList();
            bool isWrite = false; //是否输出开始标签
            for (int i = 0; i < subMenus.Count; i++)
            {
                //如果是顶级导航
                if (parent_id == 0)
                {
                    if (subMenus[i].menuName.Equals("首页"))
                    {
                        sb.Append("<li>\n");
                        sb.Append("<a class=\"J_menuItem\" href=\"" + subMenus[i].menuUrl + "\"><i class=\"fa fa-home\"></i>\n");
                        sb.Append("<span class=\"nav-label\">" + subMenus[i].menuName + "</span>\n");
                        sb.Append("</a>\n");
                        sb.Append("</li>\n");
                    }
                    else if (subMenus[i].menuName.Equals("个人资料"))
                    {
                        sb.Append("<li>\n");
                        sb.Append("<a class=\"J_menuItem\"  data-index=\"1\" href=\"" + subMenus[i].menuUrl + "\"><i class=\"fa fa-street-view\"></i> <span class=\"nav-label\">" + subMenus[i].menuName + "</span></a>\n");
                        sb.Append("</li>\n");

                    }
                    else if (subMenus[i].menuName.Equals("贷后管理"))
                    {
                        sb.Append("<li>\n");
                        sb.Append("<a class=\"J_menuItem\" href=\"" + subMenus[i].menuUrl +
                                               "\"><i class=\"fa fa-building-o\"></i> <span class=\"nav-label\">" +
                                               subMenus[i].menuName + "</span></a>\n");
                        sb.Append("</li>\n");
                    }
                    else
                    {
                        sb.Append("<li>\n");
                        sb.Append("<a href=\"contacts.html\"><i class=\"fa fa-user\"></i> <span class=\"nav-label\">" +
                                               subMenus[i].menuName + "</span><span class=\"fa arrow\"></span></a>\n");
                        //调用自身迭代
                        this.get_navigation_childs(menus, subMenus[i].menuId, userProfile);
                        sb.Append("</li>\n");
                    }
                }
                else //下级导航
                {
                    if (!isWrite)
                    {
                        isWrite = true;
                        sb.Append("<ul class=\"nav nav-second-level\">\n");
                    }
                    sb.Append("<li><a class=\"J_menuItem\" href=\"" + subMenus[i].menuUrl +
                                               "\">" + subMenus[i].menuName + "</a>\n");
                    //调用自身迭代
                    this.get_navigation_childs(menus, subMenus[i].menuId, userProfile);
                    sb.Append("</li>\n");
                    if (i == (subMenus.Count - 1))
                    {
                        sb.Append("</ul>\n");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
