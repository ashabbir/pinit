using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pinit.Data;
using pinit.Helpers;
using Microsoft.AspNet.Identity;
using System.Net;
using pinit.Models;

namespace pinit.Controllers
{
    [Authorize]
    public class StreamController : Controller
    {

        /*
        steps are create follow stream [UP_New_Stream]
        then add boards to it [UP_Follow]
        see whats in a stram by [UP_Display_Stream]
        */



        //
        // GET: /Stram/
        public ActionResult Index(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                ModelState.AddModelError("", error);
            }

            List<FollowStream> streams = new List<FollowStream>();
            using (var db = new PinitEntities())
            {
                var username = User.Identity.GetUserName();
                streams = db.FollowStreams.Where(s => s.UserName == username).ToList();
            }
            return View(streams);
        }


        //
        // GET: /Stram/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var streamId = (int)id;
            List<FI_DisplayStream_Result> stream = new List<FI_DisplayStream_Result>();
            using (var db = new PinitEntities())
            {
                stream = db.FI_DisplayStream(id).ToList();
            }
            return View(stream);
        }

        [HttpGet]
        public ActionResult CreateStream()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateStream(FollowStream model)
        {
            model.DateCreated = DateTime.Now;
            model.UserName = User.Identity.GetUserName();
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (var db = new PinitEntities())
            {

                var res = db.FI_New_Stream(model.StreamName, model.UserName).FirstOrDefault();
                if (res != null)
                {
                    if (res.Success.Value == true)
                    {
                        return RedirectToAction("Details", new { id = res.StreamId });
                    }
                }
            }
            ModelState.AddModelError("", "Stream was not created");
            return View(model);
        }



        [HttpPost]
        public ActionResult CreateStreamForBoard(StreamDropDownModel model)
        {

            if (string.IsNullOrWhiteSpace(model.StreamName))
            {
                ModelState.AddModelError("", "Stream name is required");
                return View("FollowBoard", model);
            }

            using (var db = new PinitEntities())
            {

                var res = db.FI_New_Stream(model.StreamName, User.Identity.GetUserName()).FirstOrDefault();
                if (res != null)
                {
                    if (res.Success.Value == true)
                    {
                        model.StreamsId = (int)res.StreamId.Value;
                        return FollowBoard(model);
                    }
                    else 
                    {
                        ModelState.AddModelError("", res.msg);
                        model.FillMe(User.Identity.GetUserName());
                        return View("FollowBoard", model);
                    }
                }
            }
            ModelState.AddModelError("", "Stream was not created");
            model.FillMe(User.Identity.GetUserName());
            return View("FollowBoard" , model);
        }





        //
        // GET: /Stream/FollowBoard/1
        [HttpGet]
        public ActionResult FollowBoard(int? id)
        {
            StreamDropDownModel model = new StreamDropDownModel();
            model.BoardId = (int)id;

          
          model.FillMe( User.Identity.GetUserName());
                
          
            return View(model);
        }

        [HttpPost]
        public ActionResult FollowBoard(StreamDropDownModel model)
        {
            using (var db = new PinitEntities())
            {
                var res = db.FI_Follow(model.BoardId, model.StreamsId).FirstOrDefault();
                if (res != null)
                {
                    if (res.Success.Value == true)
                    {
                        return RedirectToAction("Index", new { error = "" });
                    }
                    return RedirectToAction("Index", new { error = res.msg });
                }
            }
            return RedirectToAction("Index", new { error = "Could not follow" });
        }


        // GET: /Stream/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FollowStream stream;
            using (var db = new PinitEntities())
            {
                stream = db.FollowStreams.Find(id);
            }
            if (stream == null) 
            {
                return HttpNotFound();
            }
            return View(stream);
        }

        // POST: /Stream/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (var db = new PinitEntities()) 
            {
                var stream = db.FollowStreams.FirstOrDefault(s => s.StreamId == id);
                var boardFollows = db.BoardFollows.Where(f => f.StreamId == id);
                foreach (var item in boardFollows)
                {
                    db.BoardFollows.Remove(item);
                }
                db.FollowStreams.Remove(stream);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}