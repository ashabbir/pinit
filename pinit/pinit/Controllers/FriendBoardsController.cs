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
    public class FriendBoardsController : Controller
    {
        private PinitEntities db = new PinitEntities();

        // GET: /FriendBoards/jason
        public ActionResult Index(string id)
        {
            var boards = db.Boards.Include(b => b.UserInfo).Where(b => b.UserInfo.UserName == id);
            return View(boards.ToList());
        }

        // GET: /FriendBoards/Details/5
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
