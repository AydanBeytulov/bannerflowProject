using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using BannerflowAPI.Models;
using HtmlAgilityPack;

namespace BannerflowAPI.Controllers
{
    public class BannersController : ApiController
    {
        private BannerflowAPIContext db = new BannerflowAPIContext();

        /// <summary>
        /// Get all banners
        /// </summary>
        /// <returns></returns>
        // GET: api/Banners
        public IQueryable<Banner> GetBanners()
        {
            return db.Banners;
        }


        /// <summary>
        /// Get banner by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Banners/5
        [ResponseType(typeof(Banner))]
        public IHttpActionResult GetBanner(int id)
        {
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            return Ok(banner);
        }

        /// <summary>
        /// Update banner by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="banner"></param>
        /// <returns></returns>
        // PUT: api/Banners/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBanner(int id, Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != banner.Id)
            {
                return BadRequest();
            }

            banner.Modified = DateTime.Now;

            if (Is_correctHtml(banner.Html))
            {

                db.Entry(banner).State = EntityState.Modified;
                db.Entry(banner).Property("Created").IsModified = false;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Add banner
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        // POST: api/Banners
        [ResponseType(typeof(Banner))]
        public IHttpActionResult PostBanner(Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            banner.Created = DateTime.Now;

            if (Is_correctHtml(banner.Html)){
                db.Banners.Add(banner);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = banner.Id }, banner);
            }
            else
            {
                return BadRequest("Not Valid HTML");
            }
        }

        /// <summary>
        /// Delete banner by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Banners/5
        [ResponseType(typeof(Banner))]
        public IHttpActionResult DeleteBanner(int id)
        {
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            db.Banners.Remove(banner);
            db.SaveChanges();

            return Ok(banner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BannerExists(int id)
        {
            return db.Banners.Count(e => e.Id == id) > 0;
        }

        private bool Is_correctHtml(string HTML_code)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTML_code);
            if (doc.ParseErrors.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}