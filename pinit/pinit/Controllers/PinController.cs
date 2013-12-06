using System;
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
    public class PinController : Controller
    {
        private PinitEntities db = new PinitEntities();

      

        // GET: /Pin/Details/5
        public ActionResult Details(int? id)
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

        // GET: /Pin/Create
        public ActionResult Create()
        {
            ViewBag.BoardId = new SelectList(db.Boards, "BoardId", "BoardName");
            ViewBag.PictureId = new SelectList(db.Pictures, "PictureId", "ImageUrl");
            return View();
        }

        // POST: /Pin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PinId,PictureId,BoardId,DateCreated")] Pin pin)
        {
            if (ModelState.IsValid)
            {
                db.Pins.Add(pin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoardId = new SelectList(db.Boards, "BoardId", "BoardName", pin.BoardId);
            ViewBag.PictureId = new SelectList(db.Pictures, "PictureId", "ImageUrl", pin.PictureId);
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

        // POST: /Pin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pin pin = db.Pins.Find(id);
            var boardId = pin.BoardId;
           
            db.Pins.Remove(pin);
            db.SaveChanges();
            return RedirectToAction("Details", "Boards", new { id = boardId  });
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
