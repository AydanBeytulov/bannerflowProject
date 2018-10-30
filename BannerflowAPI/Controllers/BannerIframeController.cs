using BannerflowAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BannerflowAPI.Controllers
{
    public class BannerIframeController : Controller
    {
        private BannerflowAPIContext db = new BannerflowAPIContext();

        public ActionResult Index()
        {
            return Content("Not Exist");
        }

        // GET: BannerIframe
        public ActionResult Get(int Id)
        {
            Banner banner = db.Banners.Find(Id);
            if (banner == null)
            {
                return Content("Not Exist");
            }
            return Content(banner.Html, "text/html");
        }
    }
}