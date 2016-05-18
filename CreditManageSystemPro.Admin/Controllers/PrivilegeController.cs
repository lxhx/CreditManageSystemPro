using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using CreditManageSystemPro.Admin.Models;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class PrivilegeController : Controller
    {
        //
        // GET: /Privilege/
        StringBuilder sb = new StringBuilder();
        private CreditManageContext db = new CreditManageContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(int id = 0)
        {
            Privilege model = db.Privilege.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            List<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem { Text = "请选择菜单", Value = "0" });
            List<Menu> menues = db.Menu.Where(m => m.parentMenuId == 0).ToList();
            foreach (var menu in menues)
            {
                select.Add(new SelectListItem { Text = menu.menuName, Value = menu.menuId.ToString() });
            }
            ViewData["select"] = new SelectList(select, "Value", "Text", "0");
            return View();
        }

        [HttpPost]
        public JsonResult Add(AddPrivilegeModel model)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.privilegeName))
            {
                return Json(new { success = false, msg = "权限名称不为空" }, "text/json");
            }
            if (model.menuId == 0)
            {
                return Json(new { success = false, msg = "请选择相应的菜单" }, "text/json");
            }
            Privilege mPrivilege = db.Privilege.Where(m => m.privilegeName == model.privilegeName).FirstOrDefault();
            if (mPrivilege != null)
            {
                return Json(new { success = false, msg = "该权限已经存在，请更换权限名称" }, "text/json");
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Privilege privilege = new Privilege
                    {
                        privilegeName = model.privilegeName,
                        privilegeNote = model.privilegeNote
                    };
                    db.Privilege.Add(privilege);
                    MenuPrivilege menuPrivilege = new MenuPrivilege
                    {
                        menuId = model.menuId,
                        privilegeId = model.privilegeId
                    };
                    db.MenuPrivilege.Add(menuPrivilege);
                    if (db.SaveChanges() > 0)
                    {
                        json.Data = new { success = true, msg = "添加成功！" };
                    }
                    else
                    {
                        json.Data = new { success = false, msg = "添加失败！" };
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

        public ActionResult Update(int privilegeId = 0)
        {
            int menuId = 0;
            MenuPrivilege menuPrivilege = db.MenuPrivilege.Where(m => m.privilegeId == privilegeId).FirstOrDefault();
            if (menuPrivilege != null)
            {
                menuId = menuPrivilege.menuId;
            }
            List<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem { Text = "请选择菜单", Value = "0" });
            List<Menu> menues = db.Menu.Where(m => m.parentMenuId == 0).ToList();
            foreach (var menu in menues)
            {
                select.Add(new SelectListItem { Text = menu.menuName, Value = menu.menuId.ToString() });
            }
            ViewData["select"] = new SelectList(select, "Value", "Text", menuId);
            return View();
        }

        [HttpPost]
        public JsonResult Update(UpdatePrivilegeModel model)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.privilegeName))
            {
                return Json(new { success = false, msg = "权限名称不为空" }, "text/json");
            }
            if (model.menuId == 0)
            {
                return Json(new { success = false, msg = "请选择相应的菜单" }, "text/json");
            }
            List<Privilege> mPrivilege = db.Privilege.Where(m => m.privilegeName == model.privilegeName).ToList();
            if (mPrivilege!=null&mPrivilege.Count > 1)
            {
                return Json(new { success = false, msg = "该权限已经存在，请更换权限名称" }, "text/json");
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Privilege privilege = db.Privilege.FirstOrDefault(m => m.privilegeId == model.privilegeId);
                    if (privilege != null)
                    {
                        privilege.privilegeName = model.privilegeName;
                        privilege.privilegeNote = model.privilegeNote;
                    }
                    db.Entry(privilege).State = EntityState.Modified;

                    MenuPrivilege menuPrivilege =
                        db.MenuPrivilege.Where(m => m.privilegeId == model.privilegeId).FirstOrDefault();
                    if (menuPrivilege!=null)
                    {
                        menuPrivilege.menuId = model.menuId;
                        menuPrivilege.privilegeId = model.privilegeId;
                    }
                    db.Entry(menuPrivilege).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        json.Data = new { success = true, msg = "修改成功！" };
                    }
                    else
                    {
                        json.Data = new { success = false, msg = "修改失败！" };
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            JsonResult json = new JsonResult();
            Privilege privilege = db.Privilege.Find(id);
            if (privilege != null)
            {
                db.Privilege.Remove(privilege);
            }
            if (db.SaveChanges() > 0)
            {
                json.Data = new { success = true, msg = "删除成功！" };
            }
            else
            {
                json.Data = new { success = false, msg = "删除失败！" };
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
            var privilegeQuery = db.Privilege.ToList();
            for (int i = 0; i < privilegeQuery.Count; i++)
            {
                sb.Append("{\"text\":\"" + privilegeQuery[i].privilegeName + "\",\"id\":\"" + privilegeQuery[i].privilegeId + "\"");
                sb.Append("},");
            }
            return sb.ToString();

        }
    }
}
