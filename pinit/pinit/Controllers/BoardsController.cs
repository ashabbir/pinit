using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pinit.Data;
using pinit.Models;
using pinit.Helpers;
using Microsoft.AspNet.Identity;
using System.IO;

namespace pinit.Controllers
{
    [Authorize]
    public class BoardsController : Controller
    {
        private PinitEntities db = new PinitEntities();



        // GET: /Boards/
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            var boards = db.Boards.Include(b => b.UserInfo).Where(b => b.BoardOwner == user);
            return View(boards.ToList());
        }




        // GET: /Boards/Details/5
        public ActionResult Details(int? id, PinStatusId? pinstatusid)
        {

            ViewBag.StatusMessage =
                pinstatusid == PinStatusId.PinAdded ? "Pin was added."
                : pinstatusid == PinStatusId.PinDeleted ? "Pin was deleted."
                 : pinstatusid == PinStatusId.PinUrlIssue ? "Pin Url Issue."
                 : pinstatusid == PinStatusId.RePinError ? "Pin Url Issue."
                   : pinstatusid == PinStatusId.Extention ? "Invalid Extention."
                : "";

            if (pinstatusid == PinStatusId.Error)
            {
                string error = "pin process just encountered an error";
                ModelState.AddModelError("", error);
            }
            else if (pinstatusid == PinStatusId.PinUrlIssue)
            {
                ModelState.AddModelError("", "pin Url Issue");
            }
            else if (pinstatusid == PinStatusId.RePinError || pinstatusid == PinStatusId.Extention)
            {
                ModelState.AddModelError("", ViewBag.StatusMessage);
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bid = (int)id;

            Board board = db.Boards.Include("Pins").FirstOrDefault(b => b.BoardId == id);
            if (board == null)
            {
                return HttpNotFound();
            }


            return View(board);
        }






        // GET: /Boards/Create
        public ActionResult Create()
        {
            ViewBag.BoardOwner = User.Identity.GetUserName();
            return View();
        }

        // POST: /Boards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BoardId,BoardName,BoardOwner,DateCreated,PrivateComments")] Board board)
        {
            if (string.IsNullOrWhiteSpace(board.BoardName))
            {
                ModelState.AddModelError("", "Board name is required");
            }
            board.BoardOwner = User.Identity.GetUserName();
            board.DateCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Boards.Add(board);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = board.BoardId });
            }

            ViewBag.BoardOwner = new SelectList(db.UserInfoes, "UserName", "FirstName", board.BoardOwner);
            return View(board);
        }




        // GET: /Boards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardOwner = new SelectList(db.UserInfoes, "UserName", "FirstName", board.BoardOwner);
            return View(board);
        }

        // POST: /Boards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoardId,BoardName,BoardOwner,DateCreated,PrivateComments")] Board board)
        {


            if (string.IsNullOrWhiteSpace(board.BoardName))
            {
                ModelState.AddModelError("", "Board Name Required");
            }

            if (ModelState.IsValid)
            {
                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = board.BoardId });
            }
            ViewBag.BoardOwner = new SelectList(db.UserInfoes, "UserName", "FirstName", board.BoardOwner);
            return View(board);
        }




        // GET: /Boards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // POST: /Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Board board = db.Boards.Include("Pins").FirstOrDefault(b => b.BoardId == id);
            var pins = board.Pins.ToList();


            var boardfollow = db.BoardFollows.Where(f => f.BoardId == id).ToList();
            if (boardfollow.Count() > 0)
            {
                foreach (var bf in boardfollow)
                {
                    var followstreams = db.FollowStreams.Where(f => f.StreamId == bf.StreamId).ToList();
                    foreach (var fs in followstreams)
                    {
                        db.FollowStreams.Remove(fs);
                    }
                    db.BoardFollows.Remove(bf);
                }
            }

            db.SaveChanges();

            if (pins.Count() > 0)
            {
                foreach (var pin in pins)
                {
                    StaticHelper.DeletePin(pin.PinId);
                }
            }


            StaticHelper.DeleteBoard(id);
            return RedirectToAction("Index");
        }





        // POST: /Boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePin(string txtPinUrl, HttpPostedFileBase file, int BoardId, string textarea)
        {
            string error = "Pin was not created";
            var success = false;
            var isFile = false;
            List<string> tags = new List<string>();

            if (string.IsNullOrWhiteSpace(txtPinUrl) && (file == null && file.ContentLength <= 0))
            {
                return RedirectToAction("Details", new { id = BoardId, pinstatusid = PinStatusId.PinUrlIssue });
            }

            if ((file != null && file.ContentLength > 0))
            {
                isFile = true;
            }
            if (!string.IsNullOrWhiteSpace(textarea))
            {
                tags = textarea.Replace("\"", "").Replace("[", "").Replace("]", "").Split(',').ToList();
                tags.Remove("");
            }

            var downloadStatus = false;


            //step 1 get the image save it in directory
            string imgLocation = "";

            if (isFile)
            {
                
                downloadStatus = file.DownloadImage(out imgLocation);
                if (!imgLocation.ValidExtention()) 
                {
                    ViewBag.Error = error;
                    return RedirectToAction("Details", new { id = BoardId, pinstatusid = PinStatusId.Extention });
                }
            }
            else
            {
                downloadStatus = txtPinUrl.DownloadImage(out imgLocation);
            }

            if (downloadStatus)
            {
                //step 2 create a pin throu exec UP_Pin '' , 1
                using (var db = new PinitEntities())
                {
                    var result = db.FI_PinWithId(imgLocation, BoardId).ToList();


                    if (result.Count() > 0)
                    {
                        success = result.FirstOrDefault().Success ?? false;
                        if (!success)
                        {
                            error = result.FirstOrDefault().msg;
                        }
                        else
                        {
                            try
                            {
                                foreach (var tag in tags)
                                {
                                    var dbtag = db.Tags.FirstOrDefault(t => t.TagName.ToUpper().Trim() == tag.ToUpper().Trim());
                                    if (dbtag == null)
                                    {
                                        dbtag = new Tag() { TagName = tag };
                                        db.Tags.Add(dbtag);
                                        db.SaveChanges();
                                    }
                                    var pid = (int)result.FirstOrDefault().PinId.Value;
                                    db.PinTags.Add(new PinTag() { TagId = dbtag.TagId, PinId = pid, DateTaged = DateTime.Now });
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                var msg = ex.Message;

                            }

                        }
                    }
                }
            }


            if (success)
            {
                return RedirectToAction("Details", new { id = BoardId });
            }
            else
            {
                ViewBag.Error = error;
                return RedirectToAction("Details", new { id = BoardId, pinstatusid = PinStatusId.Error });
            }
        }





        //
        // GET: /Baord/Repin/1 where 1 is the srouce pinid
        [HttpGet]
        public ActionResult Repin(int? id)
        {
            BoardDropDown model = new BoardDropDown();
            model.PinId = (int)id;
            model.FillMe(User.Identity.GetUserName());
            return View(model);
        }



        // POST: /Boards/Repin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Repin(BoardDropDown model)
        {
            string error = "Pin was not created";
            var success = false;
            int SourcePinId = model.PinId;
            var BoardId = model.BoardId;
            //step 1 get the image save it in directory

            if (true)
            {
                //step 2 create a pin throu exec UP_Pin '' , 1
                using (var db = new PinitEntities())
                {
                    var result = db.FI_Repin(SourcePinId, BoardId).ToList();


                    if (result.Count() > 0)
                    {
                        success = result.FirstOrDefault().Success ?? false;
                        if (!success)
                        {
                            error = result.FirstOrDefault().msg;
                        }

                    }
                }
            }


            if (success)
            {
                return RedirectToAction("Details", new { id = BoardId });
            }
            else
            {
                ViewBag.Error = error;
                return RedirectToAction("Details", new { id = BoardId, pinstatusid = PinStatusId.RePinError });
            }
        }


        [HttpPost]
        public ActionResult RepinWithNewBoard(BoardDropDown model)
        {
            string error = "Pin was not created";
            model.FillMe(User.Identity.GetUserName());
            var success = false;
            int SourcePinId = model.PinId;
            var BoardId = 0;

            if (model.BoardId == 0)
            {
                if (string.IsNullOrWhiteSpace(model.BoardName))
                {
                    ModelState.AddModelError("", "Board name is required");
                    return View("Repin", model);
                }
            }

            using (var db = new PinitEntities())
            {

                var board = new Board();
                if (model.BoardId > 0)
                {
                    board = db.Boards.Find(model.BoardId);
                }
                else
                {
                    board.BoardOwner = User.Identity.GetUserName();
                    board.DateCreated = DateTime.Now;
                    board.BoardName = model.BoardName;
                }
                if (ModelState.IsValid)
                {
                    if (model.BoardId == 0)
                    {
                        db.Boards.Add(board);
                        db.SaveChanges();
                    }
                    BoardId = board.BoardId;
                    var result = db.FI_Repin(SourcePinId, BoardId).ToList();
                    if (result.Count() > 0)
                    {
                        success = result.FirstOrDefault().Success ?? false;
                        if (!success)
                        {
                            error = result.FirstOrDefault().msg;
                        }

                    }
                    if (success)
                    {
                        return RedirectToAction("Details", new { id = BoardId });
                    }
                    else
                    {
                        ViewBag.Error = error;
                        return RedirectToAction("Details", new { id = BoardId, pinstatusid = PinStatusId.RePinError });
                    }

                }

            }
            ModelState.AddModelError("", "Board was not created");

            return View("Repin", model);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region helper
        public enum PinStatusId
        {
            PinAdded,
            PinDeleted,
            PinPresent,
            PinUrlIssue,
            Extention,
            RePinError,
            Error
        }
        #endregion
    }
}
