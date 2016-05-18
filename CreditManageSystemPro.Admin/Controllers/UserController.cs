using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using CreditManageSystemPro.Admin.Models;
using CreditManageSystemPro.Admin.Utility;

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
            var userList = db.UserProfile.ToList();
            return View(userList);
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

        public ActionResult AddUser()
        {
            List<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem { Text = "请选择角色", Value = "0" });
            List<Role> roles = db.Role.ToList();
            foreach (var role in roles)
            {
                select.Add(new SelectListItem { Text = role.roleName, Value = role.roleId.ToString() });
            }
            ViewData["select"] = new SelectList(select, "Value", "Text", "0");
            return View();
        }

        [HttpPost]
        public JsonResult AddUser(AddUserModel model)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.userName))
            {
                return Json(new { success = false, msg = "用户名不能为空" }, "text/json");
            }
            if (string.IsNullOrEmpty(model.UserProfile.password))
            {
                return Json(new { success = false, msg = "密码不能为空" }, "text/json");
            }
            User mUser = db.User.Where(m => m.userName == model.userName).FirstOrDefault();
            if (mUser != null)
            {
                return Json(new { success = false, msg = "该用户已经存在，请更换用户名" }, "text/json");
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    User user = new User
                    {
                        userName = model.userName
                    };
                    db.User.Add(user);
                    string msalt = Utils.GetCheckCode(6);
                    UserProfile userProfile = new UserProfile
                    {
                        //获得6位的salt加密字符串
                        salt = msalt,
                        userId = user.userId,
                        password = DESEncrypt.Encrypt(model.UserProfile.password, msalt),
                        createDate = DateTime.Now,
                        userNote = model.UserProfile.userNote,
                        avatar = model.UserProfile.avatar
                    };
                    db.UserProfile.Add(userProfile);

                    UserRole userRole = new UserRole
                    {
                        userId = user.userId,
                        roleId = model.roleId
                    };
                    db.UserRole.Add(userRole);
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

        public ActionResult Update(int id = 0)
        {
            int roleId = 0;
            UserRole userRole = db.UserRole.Where(m => m.userId == id).FirstOrDefault();
            if (userRole != null)
            {
                roleId = userRole.roleId;
            }
            List<SelectListItem> select = new List<SelectListItem>();
            List<Role> roles = db.Role.ToList();
            foreach (var role in roles)
            {
                select.Add(new SelectListItem { Text = role.roleName, Value = role.roleId.ToString() });
            }
            ViewData["select"] = new SelectList(select, "Value", "Text", roleId);

            User user = db.User.Find(id);
            UserProfile userProfile = db.UserProfile.Where(m => m.userId == id).FirstOrDefault();
            userProfile.password = DESEncrypt.Decrypt(userProfile.password, userProfile.salt);
            UpdateUserModel updateUser=new UpdateUserModel();
            updateUser.userId = id;
            updateUser.userName = user.userName;
            updateUser.UserProfile = userProfile;
            updateUser.RePassword = userProfile.password;
            ViewBag.userPASS = userProfile.password;
            return View(updateUser);
        }

        [HttpPost]
        public ActionResult Update(UpdateUserModel model)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.userName))
            {
                return Json(new { success = false, msg = "用户名不能为空" }, "text/json");
            }
            if (string.IsNullOrEmpty(model.UserProfile.password))
            {
                return Json(new { success = false, msg = "密码不能为空" }, "text/json");
            }
            List<User> mUser = db.User.Where(m => m.userName == model.userName).ToList();
            if (mUser != null&&mUser.Count>1)
            {
                return Json(new { success = false, msg = "该用户已经存在，请更换用户名" }, "text/json");
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    User user = db.User.Where(m => m.userId == model.userId).FirstOrDefault();
                    if (user != null)
                    {
                        user.userName = model.userName;
                        db.Entry(user).State = EntityState.Modified;
                    }

                    UserProfile userProfile =
                        db.UserProfile.Where(m => m.userId == model.userId).FirstOrDefault();
                    if (userProfile != null)
                    {
                        userProfile.password = DESEncrypt.Encrypt(model.UserProfile.password,model.UserProfile.salt);
                        userProfile.passwordChangedDate = DateTime.Now;
                        userProfile.userNote = model.UserProfile.userNote;
                        userProfile.avatar = model.UserProfile.avatar;
                        db.Entry(userProfile).State = EntityState.Modified;
                    }

                    UserRole userRole = db.UserRole.Where(m => m.userId == model.userId).FirstOrDefault();
                    if (userRole != null)
                    {
                        userRole.roleId = model.roleId;
                        db.Entry(userRole).State = EntityState.Modified;
                    }
                    
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
        public ActionResult Delete(string[] ids)
        {
            //string[] userids=ids.Split(',');
            string[] userids=ids;
            JsonResult json = new JsonResult();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    int tempUserid;
                    for (int i = 0; i < userids.Length; i++)
                    {
                        tempUserid = Convert.ToInt32(userids[i]);
                        User user = db.User.Find(tempUserid);
                        if (user != null)
                        {
                            db.User.Remove(user);
                        }
                        UserProfile userProfile =
                        db.UserProfile.Where(m => m.userId == tempUserid).FirstOrDefault();
                        if (userProfile != null)
                        {
                            db.UserProfile.Remove(userProfile);
                        }
                        UserRole userRole = db.UserRole.Where(m => m.userId == tempUserid).FirstOrDefault();
                        if (userRole != null)
                        {
                            db.UserRole.Remove(userRole);
                        }
                    }

                    if (db.SaveChanges() > 0)
                    {
                        json.Data = new {success = true, msg = "删除成功！"};
                    }
                    else
                    {
                        json.Data = new {success = false, msg = "删除失败！"};
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
    }
}
