﻿using System;
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
    [Authorize]
    public class UserInfoController : Controller
    {
        private PinitEntities db = new PinitEntities();
     
        // GET: /UserInfo/
        public ActionResult Index(string error)
        {

            if (!string.IsNullOrWhiteSpace(error))
            {
                ModelState.AddModelError("", error);
            }



            var user = User.Identity.GetUserName();
            var model = new List<UserInfo>();
            var possiblefriends = db.FI_PossibleFriend(user).Take(10);
            foreach (var item in possiblefriends)
            {
                model.Add(new UserInfo() 
                {
                    FirstName = item.firstname,
                    LastName = item.lastname,
                    UserName = item.username,
                    Email = item.email
                });
            }
            return View(model);
        }


        public ActionResult RequestFriendShip(string id)
        {
          
            try
            {
                var source = User.Identity.GetUserName();
                var res = db.FI_SendFriendShipRequest(source, id).FirstOrDefault();
                if (res !=  null) 
                {

                    if (res.Success == true)
                    {
                        return RedirectToAction("Index", new { error = "" });
                    }
                    return RedirectToAction("Index", new { error = res.msg });
                }
                return RedirectToAction("Index", new { error = "request was not sent" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { error = ex.Message });
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
    }
}