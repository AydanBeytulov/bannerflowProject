using BannerflowClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace BannerflowClient.Controllers
{
    public class HomeController : Controller
    {
        public static HttpClient WebAPI;

        public HomeController()
        {
            WebAPI = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:51156/api/")
            };
            WebAPI.DefaultRequestHeaders.Clear();
            WebAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Index()
        {
            IEnumerable<Banner> dataList;
            HttpResponseMessage APIresponse = WebAPI.GetAsync("Banners").Result;
            dataList = APIresponse.Content.ReadAsAsync<IEnumerable<Banner>>().Result;
            return View(dataList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Banner banner)
        {
            HttpResponseMessage APIresponse = WebAPI.PostAsJsonAsync("Banners", banner).Result;
            if (APIresponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
               
        }

        public ActionResult Delete(Banner banner)
        {
            HttpResponseMessage APIresponse = WebAPI.DeleteAsync($"Banners/{banner.Id}").Result;
            if (APIresponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int Id) {
            Banner bannerResult = null;
            HttpResponseMessage APIresponse = WebAPI.GetAsync($"Banners/{Id}").Result;
            if (APIresponse.IsSuccessStatusCode)
            {
                bannerResult = APIresponse.Content.ReadAsAsync<Banner>().Result;
                return View(bannerResult);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Banner banner)
        {
            HttpResponseMessage APIresponse = WebAPI.PutAsJsonAsync($"Banners/{banner.Id}", banner).Result;

            if (APIresponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int Id) {
            Banner bannerResult = null;
            HttpResponseMessage APIresponse = WebAPI.GetAsync($"Banners/{Id}").Result;
            if (APIresponse.IsSuccessStatusCode)
            {
                bannerResult = APIresponse.Content.ReadAsAsync<Banner>().Result;
                return View(bannerResult);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}