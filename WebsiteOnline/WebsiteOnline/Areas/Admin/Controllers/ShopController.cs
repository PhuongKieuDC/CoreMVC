using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteOnline.Models.Data;

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
    }
}