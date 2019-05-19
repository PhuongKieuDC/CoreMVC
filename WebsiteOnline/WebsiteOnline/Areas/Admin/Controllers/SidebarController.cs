using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteOnline.Models.Data;

namespace WebsiteOnline.Areas.Admin.Controllers
{
    public class SidebarController : Controller
    {
        ProjectReviewEntities db = new ProjectReviewEntities();
        // GET: Admin/Sidebar
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getListSidebar()
        {
            List<Sidebar> list = db.Sidebars.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDatabase(Sidebar model)
        {
            var result = false;
            try
            {
                if (model.MaSidebar > 0)
                {
                    Sidebar sidebar = db.Sidebars.Find(model.MaSidebar);
                    sidebar.Body = model.Body;
                    TempData["SMS"] = "Edit successfully";
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Sidebar sidebar = new Sidebar();
                    sidebar.Body = model.Body;
                    TempData["SMS"] = "Add successfully";
                    db.Sidebars.Add(sidebar);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIdSidebar(int id)
        {
            Sidebar sidebar = db.Sidebars.Find(id);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(sidebar, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteSidebar(int id)
        {
            var result = false;
            Sidebar sidebar = db.Sidebars.Find(id);
            if (sidebar != null)
            {
                db.Sidebars.Remove(sidebar);
                db.SaveChanges();
                TempData["SMS"] = "Delete successfully";
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}