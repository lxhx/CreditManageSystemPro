using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using CreditManageSystemPro.Admin.Models;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
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
                //sb.Append("<li class=\"nav-header\">");
                //sb.Append("<div class=\"dropdown profile-element\">");
                //sb.Append("<span><img alt=\"image\" class=\"img-circle\" src=\"~/Images/default_profile_small.jpg\" /></span>");
                //sb.Append("<a data-toggle=\"dropdown\" class=\"dropdown-toggle\" href=\"#\">");
                //sb.Append("<span class=\"clear\">");
                //sb.Append("<span class=\"block m-t-xs\"><strong class=\"font-bold\">张三</strong></span>");
                //sb.Append("<span class=\"text-muted text-xs block\">超级管理员<b class=\"caret\"></b></span>");
                //sb.Append("</span></a>");
                //sb.Append("<ul class=\"dropdown-menu animated fadeInRight m-t-xs\">");
                //sb.Append("<li><a class=\"J_menuItem\" href=\"form_avatar.html\">修改头像</a></li>");
                //sb.Append("<li><a class=\"J_menuItem\" href=\"profile.html\">个人资料</a></li>");
                //sb.Append("<li><a class=\"J_menuItem\" href=\"contacts.html\">联系我们</a></li>");
                //sb.Append("<li><a class=\"J_menuItem\" href=\"mailbox.html\">信箱</a></li>");
                //sb.Append("<li class=\"divider\"></li>");
                //sb.Append("<li><a href=\"login.html\">安全退出</a></li>");
                //sb.Append("</ul></div>");
                //sb.Append("<div class=\"logo-element\">E</div>");
                //sb.Append("</li>");

                var menuQuery = db.Menu.ToList();
                return this.get_navigation_childs(menuQuery, 0);
            }
        }
        private string get_navigation_childs(List<Menu> menus, int parent_id)
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
                        this.get_navigation_childs(menus, subMenus[i].menuId);
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
                    this.get_navigation_childs(menus, subMenus[i].menuId);
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
