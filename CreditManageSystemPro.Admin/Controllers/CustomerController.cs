using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using CreditManageSystemPro.Admin.Models;

namespace CreditManageSystemPro.Admin.Controllers
{
    public class CustomerController : Controller
    {

        private CreditManageContext db = new CreditManageContext();
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得所有的客户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AllCustomer()
        {
            var customerList = db.Customer.ToList();
            ViewBag.CustomerCount = customerList.Count;
            return View(customerList);
        }

        [HttpPost]
        public ActionResult Details(int id = 0)
        {
            Customer model = db.Customer.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddCustomer()
        {
            List<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem { Text = "男", Value = "0" });
            select.Add(new SelectListItem { Text = "女", Value = "1" });
            ViewData["sex"] = new SelectList(select, "Value", "Text");

            List<SelectListItem> educationLevel = new List<SelectListItem>();
            educationLevel.Add(new SelectListItem { Text = "小学", Value = "0" });
            educationLevel.Add(new SelectListItem { Text = "初中", Value = "1" });
            educationLevel.Add(new SelectListItem { Text = "高中", Value = "2" });
            educationLevel.Add(new SelectListItem { Text = "技校", Value = "3" });
            educationLevel.Add(new SelectListItem { Text = "中专", Value = "4" });
            educationLevel.Add(new SelectListItem { Text = "大专", Value = "5" });
            educationLevel.Add(new SelectListItem { Text = "本科", Value = "6" });
            educationLevel.Add(new SelectListItem { Text = "硕士", Value = "7" });
            educationLevel.Add(new SelectListItem { Text = "博士", Value = "8" });
            ViewData["educationLevel"] = new SelectList(educationLevel, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(AddCustomerModel model)
        {
            JsonResult json = new JsonResult();
            if (string.IsNullOrEmpty(model.customerName))
            {
                return Json(new { success = false, msg = "客户姓名不能为空" }, "text/json");
            }
            if (string.IsNullOrEmpty(model.IdCard))
            {
                return Json(new { success = false, msg = "身份证号码不能为空" }, "text/json");
            } 
            if (string.IsNullOrEmpty(model.telphone))
            {
                return Json(new { success = false, msg = "座机电话不能为空" }, "text/json");
            }
            if (string.IsNullOrEmpty(model.mobile))
            {
                return Json(new { success = false, msg = "移动电话不能为空" }, "text/json");
            }
            Customer mCustomer = db.Customer.Where(m => m.IdCard == model.IdCard).FirstOrDefault();
            if (mCustomer != null)
            {
                return Json(new { success = false, msg = "该身份证号码已经存在，请更换身份证号" }, "text/json");
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Customer customer = new Customer()
                    {
                        customerName = model.customerName,
                        IdCard =model.IdCard,
                        domicilePlace = model.domicilePlace,
                        sex = model.sex,
                        birthday = model.birthday,
                        expireOfId = model.expireOfId,
                        mobile=model.mobile,
                        telphone = model.telphone,
                        educationLevel = model.educationLevel,
                        occupation = model.occupation,
                        workUnit = model.workUnit,
                        incomeOfYear = model.incomeOfYear,
                        healthStatus = model.healthStatus,
                        bankAccount = model.bankAccount,
                        accountName = model.accountName,
                        badRecord = model.badRecord,
                        debtValue = model.debtValue,
                        houseRight = model.houseRight,
                        houseEvaluation = model.houseEvaluation,
                        spouseName = model.spouseName,
                        spouseIdCard = model.spouseIdCard,
                        recentPhoto = model.recentPhoto,
                        remarks = model.remarks
                    };
                    db.Customer.Add(customer);
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

        public ActionResult Upload(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string filePathName = string.Empty;

            string localPath = "F:\\Upload";
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return Json(new
            {
                jsonrpc = "2.0",
                id = id,
                filePath = filePathName
            });
        }

        public ActionResult WhetherFill(int customerId)
        {
            var customer = db.LoanInfo.Where(m => m.customerId == customerId).ToList();
            if (customer.Count>0)
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }
    }
}
