using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BannerflowAPI.Models
{
    public class Banner
    {
        public int Id { get; set; }
        [AllowHtml]
        public string Html { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}