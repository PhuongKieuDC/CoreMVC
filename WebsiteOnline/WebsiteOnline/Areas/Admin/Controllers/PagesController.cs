using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteOnline.Models.Data;
using WebsiteOnline.Models.ViewModel;

namespace WebsiteOnline.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        ProjectReviewEntities db = new ProjectReviewEntities();
        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListPage()
        {
            List<PageViewModel> listPage = db.Pages.OrderBy(x => x.Sorting).Select(x => new PageViewModel
            {
                MaPage = x.MaPage,
                Title = x.Title,
                Slug = x.Slug,
                Body = x.Body,
                Sorting = x.Sorting,
                HasSidebar = x.HasSidebar
            }).ToList();
            return Json(listPage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDatabase(PageViewModel model)
        {
            var result = false;
            try
            {
                if (model.MaPage > 0)
                {
                    string slugedit = "home";
                    Page page = db.Pages.Where(x => x.MaPage == model.MaPage).SingleOrDefault();
                    
                    page.Title = model.Title;
                    if (model.Slug != "home")
                    {

                        if (string.IsNullOrWhiteSpace(model.Slug))
                        {
                            slugedit = model.Title.Replace(" ", "-").ToLower();
                        }
                        else
                        {
                            slugedit = model.Slug.Replace(" ", "-").ToLower();
                        }
                    }
                    page.Slug = slugedit;
                    page.Body = model.Body;
                    page.HasSidebar = model.HasSidebar;

                    TempData["Message"] = "You have edited page";
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    string slug;
                    Page page = new Page();
                    page.Title = model.Title;
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                    if (db.Pages.Any(x => x.Title == model.Title) || db.Pages.Any(x => x.Slug == slug))
                    {
                        result = false;
                    }
                    page.Slug = slug;
                    page.Body = model.Body;
                    page.HasSidebar = model.HasSidebar;
                    page.Sorting = 100;

                    TempData["Message"] = "You have added a new page";

                    db.Pages.Add(page);
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

        public JsonResult GetIdPage(int idPage)
        {
            Page page = db.Pages.Where(x => x.MaPage == idPage).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(page, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePage(int idPage)
        {
            var result = false;
            Page page = db.Pages.Find(idPage);
            if(page != null)
            {
                db.Pages.Remove(page);
                db.SaveChanges();
                TempData["Message"] = "Page was deleted successfully";
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/Pages/ReorderPages
        [HttpPost]
        public void ReorderPages(int[] id)
        {
            
            // Set initial count
            int count = 1;

            // Declare PageDTO
            Page dto;

            // Set sorting for each page
            foreach (var pageId in id)
            {
                dto = db.Pages.Find(pageId);
                dto.Sorting = count;

                db.SaveChanges();

                count++;
            }
        }
    }
}
