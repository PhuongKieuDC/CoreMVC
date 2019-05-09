using CRUDJqueryAjax.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDJqueryAjax.Controllers
{
    public class HomeController : Controller
    {
        TodoApiEntities db = new TodoApiEntities();

        public ActionResult Index()
        {
            List<Department> list = db.Departments.ToList();
            ViewBag.ListofDepartment = new SelectList(list, "DepartmentId", "DepartmentName");
            return View();
        }

        public JsonResult GetStudentList()
        {
            List<StudentViewModel> studentList = db.Students.Where(x => x.IsDeleted == false).Select(x => new StudentViewModel
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Email = x.Email,
                IsDeleted = x.IsDeleted,
                DepartmentName = x.Department.DepartmentName
            }).ToList();

            return Json(studentList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentById(int StudentId)
        {
            Student st = db.Students.Where(x => x.StudentId == StudentId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(st, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDatabase(StudentViewModel model)
        {
            var result = false;
            try
            {
                if (model.StudentId > 0)
                {
                    Student stu = db.Students.SingleOrDefault(x => x.IsDeleted == false && x.StudentId == model.StudentId);
                    stu.StudentName = model.StudentName;
                    stu.Email = model.Email;
                    stu.DepartmentId = model.DepartmentId;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Student st = new Student();
                    st.StudentName = model.StudentName;
                    st.Email = model.Email;
                    st.DepartmentId = model.DepartmentId;
                    st.IsDeleted = false;
                    db.Students.Add(st);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStudent(int StudentId)
        {
            var result = false;
            Student st = db.Students.SingleOrDefault(x => x.IsDeleted == false && x.StudentId == StudentId);
            if (st != null)
            {
                st.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}