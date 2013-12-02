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
                pinstatusid ==  PinStatusId.PinAdded ? "Pin was added."
                : pinstatusid ==  PinStatusId.PinDeleted ? "Pin was deleted."
                 : pinstatusid == PinStatusId.PinUrlIssue ? "Pin Url Issue."
                : "";

            if (pinstatusid == PinStatusId.Error )
            {
                ModelState.AddModelError("" , "pin process just encountered an error");
            } 
            else if (pinstatusid == PinStatusId.PinUrlIssue) 
            {
                ModelState.AddModelError("", "pin Url Issue");
            }
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bid = (int)id;

            Board board = db.Boards.Include("Pins").FirstOrDefault( b => b.BoardId == id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoardId,BoardName,BoardOwner,DateCreated,PrivateComments")] Board board)
        {
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
            Board board = db.Boards.Find(id);
            db.Boards.Remove(board);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: /Boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePin(string txtPinUrl, int BoardId)
        {

            var success = false;
            if (string.IsNullOrWhiteSpace(txtPinUrl)) 
            {
              
                return RedirectToAction("Details", new { id = BoardId, pinstatusid = PinStatusId.PinUrlIssue });
            }
            //step 1 get the image save it in directory
            string imgLocation = "";
            if (txtPinUrl.DownloadImage(out imgLocation))
            {
                //step 2 create a pin throu exec UP_Pin '' , 1
                using (var db = new PinitEntities())
                {
                    var result = db.FI_Pin(imgLocation, BoardId).ToList();
                    if (result.Count() > 0)
                    {
                        success = result.FirstOrDefault().Success ?? false;
                    }
                }
            }
            

            if (success)
            {
                return RedirectToAction("Details", new { id = BoardId });
            }
            else
            {
                ModelState.AddModelError("", "Pin was not added try again");
                return RedirectToAction("Details", new { id = BoardId  , pinstatusid = PinStatusId.Error});
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public enum PinStatusId
        {
            PinAdded,
            PinDeleted,
            PinUrlIssue,
            Error
        }

    }
}
