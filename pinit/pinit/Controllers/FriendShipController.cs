using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using pinit.Models;

namespace pinit.Controllers
{
    [Authorize]
    public class FriendShipController : Controller
    {
        //
        // GET: /FriendShip/
        public ActionResult Index(string error)
        {

            if (!string.IsNullOrWhiteSpace(error)) 
            {
                ModelState.AddModelError("", error);
            }

            var model  = new FriendShip();
            
            var user = User.Identity.GetUserName();
            model.FillMe(user);
            return View(model);
        }

        //
        // GET: /FriendShip/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FriendShip/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FriendShip/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FriendShip/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FriendShip/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index", new { error = "" });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FriendShip/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FriendShip/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index", new { error = "" });
            }
            catch
            {
                return View();
            }
        }


        // GET: /FriendShip/acceptrequest/testuser2
        [HttpGet]
        public ActionResult AcceptRequest(string id)
        {
            try
            {
                var user = User.Identity.GetUserName();
                var model = new FriendShip(user);
                var res = model.AcceptReject(id, true);
                return RedirectToAction("Index", new { error = res  });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { error = ex.Message });
            }
           
        }

        // GET: /FriendShip/RejectRequest/testuser2 
        [HttpGet]
        public ActionResult RejectRequest(string id)
        {
            try
            {
                var user = User.Identity.GetUserName();
                var model = new FriendShip(user);
                var res = model.AcceptReject(id, false);
                return RedirectToAction("Index", new { error = res });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { error = ex.Message });
            }
        }
    }
}
