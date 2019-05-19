using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteOnline.Models.ViewModel
{
    public class PageViewModel
    {
        public int MaPage { get; set; }

        [Required]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Body { get; set; }

        public Nullable<int> Sorting { get; set; }

        public Nullable<bool> HasSidebar { get; set; }
    }
}