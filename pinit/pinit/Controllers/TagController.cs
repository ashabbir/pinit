﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pinit.Data;

namespace pinit.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private PinitEntities db = new PinitEntities();
        List<string> tags;

        /// <summary>
        /// for ajax calls
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTags()
        {
            if (tags == null) 
            {
                tags = new List<string>();
            }

            if (tags.Count() > 0) 
            {
                return Json(tags, JsonRequestBehavior.AllowGet);
            } 
            var dbtags = db.Tags.Distinct().ToList();

            foreach (var item in dbtags    )
            {
                if (!tags.Any(tag => tag.ToUpper().Trim() == item.TagName.ToUpper().Trim())) 
                {
                    tags.Add(item.TagName);
                }
            }

            return Json(tags, JsonRequestBehavior.AllowGet);
        }



        // GET: /Tag/
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }



        // GET: /Tag/Search/aaa
        public ActionResult Search(string SearchTag)
        {
            ViewBag.message = "";
            var pins = new List<Pin>();

            if (string.IsNullOrWhiteSpace(SearchTag))
            {
                ViewBag.message = "Enter Search Criteria";
                return View(pins);
            }
            var relatedPins = db.FI_TagSearch(SearchTag).ToList();
            if (relatedPins.Count() == 0)
            {
                ViewBag.message = "no results to display";
            }
            foreach (var item in relatedPins)
            {
                var pic = new Picture() 
                {
                    PictureId = item.PictureId,
                    ImageUrl = item.ImageUrl
                };
                pins.Add(new Pin()
                {
                    PinId = item.pinid,
                    Picture = pic
                });
            }
            ViewBag.message = pins.Count() + " pins found";
            return View(pins);
        }


        // GET: /Tag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: /Tag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Tag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TagId,TagName")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Tags.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
