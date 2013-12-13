using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pinit.Data;

namespace pinit.Helpers
{
    public class StaticHelper
    {

        public static void DeletePin(int id)
        {
            using (var db = new PinitEntities())
            {
                Pin pin = db.Pins.Include(p => p.UserLikes).FirstOrDefault(p => p.PinId == id);
                if (pin == null) { return; }
                DeleteRepin(id);
                DeleteRepinTargets(id);
                pin = db.Pins.Include(p => p.UserLikes).FirstOrDefault(p => p.PinId == id);
                var comments = db.Comments.Where(c => c.PinId == id).ToList();
                var userlikes = db.UserLikes.Where(l => l.PinId == id).ToList();
                var pintags = db.PinTags.Where(t => t.PinId == id).ToList();

                foreach (var item in userlikes)
                {
                    db.UserLikes.Remove(item);
                }
                foreach (var item in comments)
                {
                    db.Comments.Remove(item);
                }
                

                foreach (var item in pintags)
                {
                    db.PinTags.Remove(item);
                }
                db.Pins.Remove(pin);
                db.SaveChanges();
            }
        }



        public static void DeleteRepin(int id)
        {
            using (var db = new PinitEntities())
            {
                Repin pin = db.Repins.FirstOrDefault(p => p.PinId == id);

                if (pin == null) { return; }
                var comments = db.Comments.Where(c => c.PinId == id).ToList();

                foreach (var item in comments)
                {
                    db.Comments.Remove(item);
                }
                
                db.Repins.Remove(pin);

                db.SaveChanges();
            }

        }


        public static void DeleteRepinTargets(int id)
        {
            using (var db = new PinitEntities())
            {
                var tagetRepins = db.Repins.Where(p => p.TargetPinId == id).ToList();
                foreach (var item in tagetRepins)
                {
                    DeletePin(item.PinId);
                    var repin = db.Repins.FirstOrDefault(p => p.RepinId == item.RepinId);
                    if (repin != null) {
                        db.Repins.Remove(repin);
                        db.SaveChanges();
                    
                    }
                   
                }
                
            }

        }

        public static void DeleteBoard(int id)
        {
            using (var db = new PinitEntities()) 
            {
               var board = db.Boards.FirstOrDefault(b => b.BoardId == id);
               if (board == null) { return; }
                db.Boards.Remove(board);
                db.SaveChanges();
            }
            
        }
    }
}