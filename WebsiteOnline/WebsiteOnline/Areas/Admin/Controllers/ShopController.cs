using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteOnline.Models.Data;
using WebsiteOnline.Models.ViewModel;

namespace WebsiteOnline.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        ProjectReviewEntities db = new ProjectReviewEntities();
        // GET: Admin/Shop/Categories
        public ActionResult Categories()
        {
            List<DanhMuc> list = db.DanhMucs.ToArray().OrderBy(x => x.Sort).ToList();
            return View(list);
        }

        [HttpPost]
        public string AddNewCategory(string catname)
        {
            string id;

            if (db.DanhMucs.Any(x => x.Ten == catname))
                return "titletaken";
            DanhMuc danhMuc = new DanhMuc();
            danhMuc.Ten = catname;
            danhMuc.Slug = catname.Replace(" ", "-").ToLower();
            danhMuc.Sort = 100;

            db.DanhMucs.Add(danhMuc);
            db.SaveChanges();

            id = danhMuc.MaDanhMuc.ToString();

            return id;
        }

        [HttpPost]
        public string renameCategory(string newCatName, int id)
        {
            if (db.DanhMucs.Any(x => x.Ten == newCatName))
            {
                return "titletaken";
            }

            DanhMuc danhMuc = db.DanhMucs.Find(id);
            danhMuc.Ten = newCatName;
            danhMuc.Slug = newCatName.Replace(" ", "-").ToLower();
            danhMuc.Sort = 100;
            db.SaveChanges();
            return "Ok";
        }

        [HttpPost]
        public void ReorderPages(int[] id)
        {

            // Set initial count
            int count = 1;

            // Declare PageDTO
            DanhMuc dto = new DanhMuc();

            // Set sorting for each page
            foreach (var cateId in id)
            {
                dto = db.DanhMucs.Find(cateId);
                dto.Sort = count;

                db.SaveChanges();

                count++;
            }
        }

        public ActionResult DeleteCategory(int id)
        {
            DanhMuc danhMuc = db.DanhMucs.Find(id);

            if( danhMuc != null)
            {
                db.DanhMucs.Remove(danhMuc);
                db.SaveChanges();
            }
            return RedirectToAction("Categories");
        }

        public ActionResult AddProduct()
        {
            List<DanhMuc> list = db.DanhMucs.ToList();
            ProductViewModel product = new ProductViewModel();

            ViewBag.listDanhMuc = new SelectList(list, "MaDanhMuc", "Ten");
            return View();
        }
    }
}