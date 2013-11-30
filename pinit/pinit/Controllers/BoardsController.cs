using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pinit.Data;
using Microsoft.AspNet.Identity;

namespace pinit.Controllers
{
    public class BoardsController : Controller
    {
        private PinitEntities db = new PinitEntities();

        // GET: /Boards/
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            var boards = db.Boards.Include(b => b.UserInfo).Where(b => b.BoardOwner == user );
            return View(boards.ToList());
        }

        // GET: /Boards/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create([Bind(Include="BoardId,BoardName,BoardOwner,DateCreated,PrivateComments")] Board board)
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
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include="BoardId,BoardName,BoardOwner,DateCreated,PrivateComments")] Board board)
        {
            if (ModelState.IsValid)
            {
                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
