using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreditManageSystemPro.Admin.Models;
using System.Text;
using CreditManageSystemPro.Admin.Utility;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        StringBuilder sb = new StringBuilder();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            using (CreditManageContext db = new CreditManageContext())
            {
                List<SelectListItem> select = new List<SelectListItem>();
                List<Menu> menues = db.Menu.ToList();
                foreach (var menu in menues)
                {
                    new SelectListItem { Text=menu.menuName,Value=menu.menuId.ToString()};
                }
                ViewData["select"] = new SelectList(select, "Value", "Text", "主菜单");
                var model = new AddMenuModel();
                return View(model);
            }
        }
        [HttpPost]
        public JsonResult Add(AddMenuModel model)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.menuName))
            {
                return Json(new { success = false, msg = "菜单名称不为空" },"text/json");
            }
            if (string.IsNullOrEmpty(model.menuUrl))
            {
                return Json(new { success = false, msg = "菜单Url不为空" }, "text/json");
            }
            using (CreditManageContext db = new CreditManageContext())
            {
                Menu menu = new Menu
                {
                    menuName = model.menuName,
                    menuUrl = model.menuUrl,
                    menuNote = model.menuNote,
                    parentMenuId = 0
                };
                db.Menu.Add(menu);
                db.SaveChanges();
                json.Data=new { success = true, msg = "添加成功！" };
                return json;
            }
        }

        public ActionResult Jstree()
        {
            sb.Append("{\"core\": {\"data\": [");
            get_navigation_list();
            sb.Append("]}}");
            return Content(sb.ToString());
        }

        private string get_navigation_list()
        {
            using (CreditManageContext db = new CreditManageContext())
            {
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
                    sb.Append("{\"text\":\"" + subMenus[i].menuName + "\"");
                    //调用自身迭代
                    this.get_navigation_childs(menus, subMenus[i].menuId);
                    sb.Append("},");
                }
                else //下级导航
                {
                    if (!isWrite)
                    {
                        isWrite = true;
                        sb.Append(",\"state\": { \"opened\": true },\"children\": [");
                    }
                    sb.Append("{ \"text\": \""+subMenus[i].menuName+"\"");
                    //调用自身迭代
                    this.get_navigation_childs(menus, subMenus[i].menuId);
                    sb.Append("},");
                    if (i == (subMenus.Count - 1))
                    {
                        sb.Append("]");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
