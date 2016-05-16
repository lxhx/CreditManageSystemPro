using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreditManageSystemPro.Admin.Models;
using CreditManageSystemPro.Admin.Utility;

namespace CreditManageSystemPro.Admin.tools
{
    /// <summary>
    /// admin_ajax 的摘要说明
    /// </summary>
    public class admin_ajax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            switch (action)
            {
                case "get_navigation_list": //获取后台导航字符串
                    get_navigation_list(context);
                    break;
            }
        }

        private void get_navigation_list(HttpContext context)
        {
            using(CreditManageContext db = new CreditManageContext())
            {
                var menuQuery = from m in db.Menu where m.parentMenuId == 0 select m;
                this.get_navigation_childs(context,menuQuery.ToList() , 0);
            }
        }
        private void get_navigation_childs(HttpContext context, List<Menu> menus, int parent_id)
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
                        context.Response.Write("<li>\n");
                        context.Response.Write("<a class=\"J_menuItem\" data-index=\"0\" href=\""+subMenus[i].menuUrl+"\"><i class=\"fa fa-home\"></i>\n");
                        context.Response.Write("<span class=\"nav-label\">"+subMenus[i].menuName+"</span>\n");
                        context.Response.Write("</a>\n");
                        context.Response.Write("</li>\n");
                    }
                    else if (subMenus[i].menuName.Equals("个人资料"))
                    {
                        context.Response.Write("<li>\n");
                        context.Response.Write("<a class=\"J_menuItem\"  data-index=\"1\" href=\"" + subMenus[i].menuUrl + "\"><i class=\"fa fa-street-view\"></i> <span class=\"nav-label\">" + subMenus[i].menuName + "</span></a>\n");
                        context.Response.Write("</li>\n");
                        
                    }
                    else if (subMenus[i].menuName.Equals("贷后管理"))
                    {
                        context.Response.Write("<li>\n");
                        context.Response.Write("<a class=\"J_menuItem\" href=\"" + subMenus[i].menuUrl +
                                               "\"><i class=\"fa fa-building-o\"></i> <span class=\"nav-label\">" +
                                               subMenus[i].menuName + "</span></a>\n");
                        context.Response.Write("</li>\n");
                    }
                    else
                    {
                        context.Response.Write("<li>\n");
                        context.Response.Write("<a href=\"contacts.html\"><i class=\"fa fa-user\"></i> <span class=\"nav-label\">" +
                                               subMenus[i].menuName + "</span><span class=\"fa arrow\"></span></a>\n");
                        //调用自身迭代
                        this.get_navigation_childs(context, menus, subMenus[i].menuId);
                        context.Response.Write("</li>\n");
                    }
                }
                else //下级导航
                {
                    if (!isWrite)
                    {
                        isWrite = true;
                        context.Response.Write("<ul class=\"nav nav-second-level\">\n");
                    }
                    context.Response.Write("<li><a class=\"J_menuItem\" href=\"" + subMenus[i].menuUrl +
                                               "\">" +subMenus[i].menuName + "</a>\n");
                    //调用自身迭代
                    this.get_navigation_childs(context, menus, subMenus[i].menuId);
                    context.Response.Write("</li>\n");

                    if (i == (subMenus.Count - 1))
                    {
                        context.Response.Write("</ul>\n");
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}