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
