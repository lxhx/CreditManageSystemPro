using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreditManageSystemPro.Admin.Models;
using System.Text;
using System.Transactions;
using CreditManageSystemPro.Admin.Utility;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        StringBuilder sb = new StringBuilder();
        private CreditManageContext db = new CreditManageContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(int id = 0)
        {
            Menu model = db.Menu.Find(id);
            if (model==null)
            {
                return HttpNotFound();
            }
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            List<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem { Text = "一级主菜单", Value = "0" });
            List<Menu> menues = db.Menu.Where(m => m.parentMenuId == 0).ToList();
            foreach (var menu in menues)
            {
                select.Add(new SelectListItem { Text = menu.menuName, Value = menu.menuId.ToString() });
            }
            ViewData["select"] = new SelectList(select, "Value", "Text", "一级主菜单");

            List<SelectListItem> powerRight = new List<SelectListItem>();
            powerRight.Add(new SelectListItem { Text = "请选择权限", Value = "0" });
            List<Privilege> privileges = db.Privilege.ToList();
            foreach (var p in privileges)
            {
                powerRight.Add(new SelectListItem { Text = p.privilegeName, Value = p.privilegeId.ToString() });
            }
            ViewData["powerRight"] = new SelectList(powerRight, "Value", "Text", "请选择权限");
            return View();
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
            Menu menu = new Menu
            {
                menuName = model.menuName,
                menuUrl = model.menuUrl,
                menuNote = model.menuNote,
                parentMenuId = model.menuId
            };
            db.Menu.Add(menu);
            if (db.SaveChanges() > 0)
            {
                json.Data = new { success = true, msg = "添加成功！" };
            }
            else
            {
                json.Data = new { success = false, msg = "添加失败！" };
            }
            return json;
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Update(UpdateMenuModel model)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.menuName))
            {
                return Json(new { success = false, msg = "菜单名称不为空" }, "text/json");
            }
            if (string.IsNullOrEmpty(model.menuUrl))
            {
                return Json(new { success = false, msg = "菜单Url不为空" }, "text/json");
            }
            Menu menu = db.Menu.FirstOrDefault(m => m.menuId == model.menuId);
            if (menu!=null)
            {
                menu.menuName = model.menuName;
                menu.menuUrl = model.menuUrl;
                menu.menuNote = model.menuNote;
                menu.parentMenuId = model.parentMenuId;
                db.Entry(menu).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    json.Data = new { success = true, msg = "修改成功！" };
                }
                else
                {
                    json.Data = new { success = false, msg = "修改失败！" };
                }
            }
            else
            {
                json.Data = new { success = false, msg = "菜单不存在！" };
            }
            return json;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            JsonResult json = new JsonResult();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Menu menu = db.Menu.Find(id);
                    if (menu != null)
                    {
                        if (menu.parentMenuId == 0)//说明次菜单为父菜单
                        {
                            var childMenu = db.Menu.Where(m => m.parentMenuId == id).ToList();
                            if (childMenu!=null)
                            {
                                foreach (var child in childMenu)
                                {
                                    //删除所有的子菜单
                                    db.Menu.Remove(child);         
                                }
                            }
                        }
                        db.Menu.Remove(menu);
                    }
                    if (db.SaveChanges() > 0)
                    {
                        json.Data = new { success = true, msg = "删除成功！" };
                    }
                    else
                    {
                        json.Data = new { success = false, msg = "删除失败！" };
                    }
                }
                catch (DbEntityValidationException ex)
                {

                    throw;
                }
                scope.Complete();
            }
            return json;
        }

        public ActionResult Jstree()
        {
            sb.Append("[");
            get_navigation_list();
            sb.Append("]");
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
                    sb.Append("{\"text\":\"" + subMenus[i].menuName + "\",\"id\":\"" + subMenus[i].menuId + "\"");
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
                    sb.Append("{ \"text\": \"" + subMenus[i].menuName + "\",\"id\":\"" + subMenus[i].menuId + "\"");
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
