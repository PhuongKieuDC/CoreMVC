using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteOnline.Models.ViewModel
{
    public class ProductViewModel
    {
        [Display(Name = "Mã Sản Phẩm")]
        public int MaSanPham { get; set; }

        [Required]
        [Display(Name = "Tên sản phẩm")]
        public string Ten { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Đơn giá")]
        public Nullable<decimal> Gia { get; set; }
        [Display(Name = "Số lượng tồn")]
        public Nullable<int> SoLuongTon { get; set; }
        public string Hinh { get; set; }
        public Nullable<bool> Available { get; set; }
        [Display(Name = "Tên danh mục")]
        public Nullable<int> MaDanhMuc { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string TenDanhMuc { get; set; }
    }
}