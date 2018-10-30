using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BannerflowClient.Models
{
    public class Banner
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required] 
        public string Html { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Height { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}