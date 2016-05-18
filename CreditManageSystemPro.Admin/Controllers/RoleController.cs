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
    public class RoleController : Controller
    {
        //
        // GET: /Role/
        StringBuilder sb = new StringBuilder();
        private CreditManageContext db = new CreditManageContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(int id = 0)
        {
            Role model = db.Role.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            List<SelectListItem> select = new List<SelectListItem>();
            List<Privilege> privileges = db.Privilege.ToList();
            foreach (var privilege in privileges)
            {
                select.Add(new SelectListItem { Text = privilege.privilegeName, Value = privilege.privilegeId.ToString() });
            }
            ViewData["select"] = new SelectList(select, "Value", "Text", "0");
            return View();
        }

        [HttpPost]
        public JsonResult Add(AddRoleModel model, string pIds)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.roleName))
            {
                return Json(new { success = false, msg = "角色名称不为空" }, "text/json");
            }
            Role mrRole = db.Role.Where(m => m.roleName == model.roleName).FirstOrDefault();
            if (mrRole!=null)
            {
                return Json(new { success = false, msg = "角色名称已经存在" }, "text/json");
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Role role = new Role
                    {
                        roleName = model.roleName,
                        roleNote = model.roleNote
                    };
                    db.Role.Add(role);
                    RolePrivilege rolePrivilege =new RolePrivilege
                    {
                        roleId = model.roleId,
                        privilegeIds = pIds
                    };
                    db.RolePrivilege.Add(rolePrivilege);
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

        public ActionResult Update(int roleId = 0)
        {
            string privilegeIds =string.Empty;
            RolePrivilege rolePrivilege = db.RolePrivilege.Where(m => m.roleId == roleId).FirstOrDefault();
            if (rolePrivilege != null)
            {
                privilegeIds = rolePrivilege.privilegeIds;
            }
            List<SelectListItem> select = new List<SelectListItem>();
            List<Privilege> privileges = db.Privilege.ToList();
            foreach (var privilege in privileges)
            {
                select.Add(new SelectListItem { Text = privilege.privilegeName, Value = privilege.privilegeId.ToString() });
            }
            ViewData["select"] = new SelectList(select, "Value", "Text");
            ViewBag.privilegeIds = privilegeIds;
            return View();
        }

        [HttpPost]
        public JsonResult Update(UpdateRoleModel model, string pIds)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.roleName))
            {
                return Json(new { success = false, msg = "角色名称不为空" }, "text/json");
            }
            List<Role> mrRole = db.Role.Where(m => m.roleName == model.roleName).ToList();
            if (mrRole!=null&mrRole.Count > 1)
            {
                return Json(new { success = false, msg = "角色名称已经存在" }, "text/json");
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Role role = db.Role.FirstOrDefault(m => m.roleName == model.roleName);
                    if (role != null)
                    {
                        role.roleName = model.roleName;
                        role.roleNote = model.roleNote;
                    }
                    db.Entry(role).State = EntityState.Modified;

                    RolePrivilege rolePrivilege =
                        db.RolePrivilege.Where(m => m.roleId == model.roleId).FirstOrDefault();
                    if (rolePrivilege != null)
                    {
                        rolePrivilege.privilegeIds = pIds;
                        rolePrivilege.roleId = model.roleId;
                    }
                    db.Entry(rolePrivilege).State = EntityState.Modified;
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
            Role role = db.Role.Find(id);
            if (role != null)
            {
                db.Role.Remove(role);
                if (db.SaveChanges() > 0)
                {
                    json.Data = new { success = true, msg = "删除成功！" };
                }
                else
                {
                    json.Data = new { success = false, msg = "删除失败！" };
                }
            }
            else
            {
                json.Data = new { success = false, msg = "删除失败,角色不存在！" };
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
            var roleQuery = db.Role.ToList();
            for (int i = 0; i < roleQuery.Count; i++)
            {
                sb.Append("{\"text\":\"" + roleQuery[i].roleName + "\",\"id\":\"" + roleQuery[i].roleId + "\"");
                sb.Append("},");
            }
            return sb.ToString();
           
        }
    }
}
