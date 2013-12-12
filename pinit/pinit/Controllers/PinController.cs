using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pinit.Data;
using pinit.Helpers;
using Microsoft.AspNet.Identity;

namespace pinit.Controllers
{
    [Authorize]
    public class PinController : Controller
    {
        private PinitEntities db = new PinitEntities();



        // GET: /Pin/Details/5
        public ActionResult Details(int id, string error)
        {
            
            var user = User.Identity.GetUserName();
            var commentable = false;
            var likeable = false;
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (!string.IsNullOrWhiteSpace(error))
            {
                ModelState.AddModelError("", error);
            }

            var pin = db.Pins.Include(p => p.Comments).Include(p => p.Board).Include( p=> p.PinTags).FirstOrDefault(p => p.PinId == id);
            if (pin == null)
            {
                return HttpNotFound();
            }

            var comments = pin.Comments;


            //user is only allowed to comment is its private and user is in friend list
            if (!pin.Board.PrivateComments) //user is only allowed to comment if its public 
            {
                commentable = true;
            }
            else if (pin.Board.BoardOwner == user) //user is only allowed to comment if its his own pin
            {
                commentable = true;
            }
            else 
            {
                if (db.Friends.Any(f => (f.TargetUser == user && f.RequestStatus == "Accepted") || (f.SourceUser == user && f.RequestStatus == "Accepted"))) 
                {
                    commentable = true;
                }
                
            }

            //user can only like if he is not the owner of the pin
            //user can only like is he has not already liked it before
            var alreadylike = db.UserLikes.Any(l => l.UserName == user && l.PinId == id);
            if (!alreadylike && pin.Board.BoardOwner != user)
            {
                likeable =true;
            }

            ViewBag.Likes = db.UserLikes.Count(l => l.PinId == id);
           
            ViewBag.AllowComment = commentable;
            ViewBag.Likeable = likeable;
          
            return View(pin);
        }




        // GET: /Pin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            return View(pin);
        }


        //POST: /pin/addcomment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comment model)
        {

            try
            {
                var msg = model.CommentText;
                if (string.IsNullOrWhiteSpace(msg))
                {
                    throw new Exception("you need to add in comment text");

                }
                model.DateCommented = DateTime.Now;

                if (ModelState.IsValid)
                {
                    using (var db = new PinitEntities())
                    {
                        db.Comments.Add(model);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Details", new { id = model.PinId });
                }
                else
                {
                    return RedirectToAction("Details", new { id = model.PinId, error = "comment was not added" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", new { id = model.PinId, error = ex.Message });
            }


        }

        //POST: /pin/addcomment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLike(UserLike model)
        {

            try
            {
                model.DateLiked = DateTime.Now;

                if (ModelState.IsValid)
                {
                    using (var db = new PinitEntities())
                    {
                        db.UserLikes.Add(model);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Details", new { id = model.PinId });
                }
                else
                {
                    return RedirectToAction("Details", new { id = model.PinId, error = "cant not like this right now please try again latter" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", new { id = model.PinId, error = ex.Message });
            }


        }

        // POST: /Pin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            /*
                when we delete a pin
                
                1- delete that pin 
                2- delete comments on it
                3- delete likes on it
                4- delete all repins where its the target
                5- delete att pins where repin target > pinid is present
                6- delete tags for the pin 
            */
            var boardId = db.Pins.FirstOrDefault(p => p.PinId == id).BoardId;
            StaticHelper.DeletePin(id);    
            return RedirectToAction("Details", "Boards", new { id = boardId });
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
