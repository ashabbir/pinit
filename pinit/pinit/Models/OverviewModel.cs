using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pinit.Models
{
    public class OverviewModel
    {
        public List<pinit.Data.Board> boards { get; set; }
        public List<pinit.Models.UserFriend> PendingFriendRequests { get; set; }
        public List<pinit.Models.UserFriend> ActiveFriends { get; set; }
        public string username { get; set; }


        public OverviewModel(string _username)
        {
            SetUserName(_username);
            LoadBoards();
            LoadPendingFriendRequest();
            LoadActiveFriends();
        }


        public void SetUserName(string _username) 
        {
            username = _username;
        }


        public void LoadBoards() 
        {
            boards = new List<Data.Board>();
            using (var db = new pinit.Data.PinitEntities())
            {
                boards = db.Boards.Include("Pins").Where(b => b.BoardOwner == username).ToList();
                foreach (var item in boards)
                {
                    if (item.Pins.Count() > 0) 
                    {
                        var p = item.Pins.FirstOrDefault().Picture;
                    }
                }
            }
        }


        public void LoadPendingFriendRequest()
        {
            PendingFriendRequests = new List<UserFriend>();
            using (var db = new pinit.Data.PinitEntities()) {
                var _pendingfriends = db.Friends.Where(x => x.RequestStatus == "requested" && x.TargetUser == username).ToList();
                foreach (var item in _pendingfriends)
                {
                    var user = new UserFriend(item.SourceUser);
                    user.FriendShipStatus = item;
                    PendingFriendRequests.Add(user);
                }
            
            }
            
        }


        public void LoadActiveFriends()
        {
            ActiveFriends = new List<UserFriend>();
            var allactive = new List<UserFriend>();
            using (var db = new pinit.Data.PinitEntities())
            {
                var _activeFriendWhoAskedMe = db.Friends.Where(x => x.RequestStatus.ToUpper() =="ACCEPTED" && x.TargetUser == username).OrderByDescending(x => x.DateModified).ToList();
                var _activeFriendWhoIAsked = db.Friends.Where(x => x.RequestStatus.ToUpper() == "ACCEPTED" && x.SourceUser == username).OrderByDescending(x => x.DateModified).ToList();
                foreach (var item in _activeFriendWhoAskedMe)
                {
                    var user = new UserFriend(item.SourceUser);
                    user.FriendShipStatus = item;
                    allactive.Add(user);
                }

                foreach (var item in _activeFriendWhoIAsked)
                {
                    var user = new UserFriend(item.TargetUser);
                    user.FriendShipStatus = item;
                    allactive.Add(user);
                }

                ActiveFriends = allactive.Take(5).ToList();

            }

        }

    }
}