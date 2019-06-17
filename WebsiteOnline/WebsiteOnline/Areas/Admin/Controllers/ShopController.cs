using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        //get method addproduct
        public ActionResult AddProduct()
        {
            List<DanhMuc> list = db.DanhMucs.ToList();
            ProductViewModel product = new ProductViewModel();

            ViewBag.listDanhMuc = new SelectList(list, "MaDanhMuc", "Ten");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel productViewModel, HttpPostedFileBase file)
        {
            if(!ModelState.IsValid)
            {
                List<DanhMuc> list = db.DanhMucs.ToList();
                ViewBag.listDanhMuc = new SelectList(list, "MaDanhMuc", "Ten");
                return View(productViewModel);
            }

            if (db.SanPhams.Any(x => x.Ten == productViewModel.Ten))
            {
                //List<DanhMuc> list = db.DanhMucs.ToList();
                //ViewBag.listDanhMuc = new SelectList(list, "MaDanhMuc", "Ten");
                ModelState.AddModelError("", "Tên sản phẩm đã tồn tại");
                return View(productViewModel);
            }

            int id;

            SanPham sanPham = new SanPham();
            sanPham.Ten = productViewModel.Ten;
            sanPham.Slug = productViewModel.Slug;
            sanPham.MoTa = productViewModel.MoTa;
            sanPham.Gia = productViewModel.Gia;
            sanPham.SoLuongTon = productViewModel.SoLuongTon;
            sanPham.Available = productViewModel.Available;
            sanPham.MaDanhMuc = productViewModel.MaDanhMuc;

            db.SanPhams.Add(sanPham);
            db.SaveChanges();

            id = sanPham.MaSanPham;

            TempData["Message"] = "Sản phẩm thêm thành công";

            #region upload image

            //create directories
            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

            var pathstring1 = Path.Combine(originalDirectory.ToString(), "SanPhams");
            var pathstring2 = Path.Combine(originalDirectory.ToString(), "SanPhams" + id.ToString());
            var pathstring3 = Path.Combine(originalDirectory.ToString(), "SanPhams" + id.ToString() + "\\Thumbs");
            var pathstring4 = Path.Combine(originalDirectory.ToString(), "SanPhams" + id.ToString() + "\\Gallery");
            var pathstring5 = Path.Combine(originalDirectory.ToString(), "SanPhams" + id.ToString() + "\\Gallery\\Thumbs");

            if (!Directory.Exists(pathstring1))
                Directory.CreateDirectory(pathstring1);
            if (!Directory.Exists(pathstring2))
                Directory.CreateDirectory(pathstring2);
            if (!Directory.Exists(pathstring3))
                Directory.CreateDirectory(pathstring3);
            if (!Directory.Exists(pathstring4))
                Directory.CreateDirectory(pathstring4);
            if (!Directory.Exists(pathstring5))
                Directory.CreateDirectory(pathstring5);

            if (file != null && file.ContentLength > 0)
            {
                string ext = file.ContentType.ToLower();

                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/x-png" &&
                    ext != "image/png")
                {
                    ModelState.AddModelError("", "Hình ảnh upload không thành công");
                    return View(productViewModel);
                }
                string imagename = file.FileName;

                sanPham = db.SanPhams.Find(id);
                sanPham.Hinh = imagename;
                db.SaveChanges();

                var path = string.Format("{0}\\{1}", pathstring2, imagename);
                var path2 = string.Format("{0}\\{1}", pathstring3, imagename);

                file.SaveAs(path);

                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(path2);
            }
            db.SaveChanges();
            #endregion
            return RedirectToAction("AddProduct");
        }
    }
}