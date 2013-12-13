using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pinit.Data;

namespace pinit.Models
{
    /// <summary>
    /// mother of all models .. this here wil have every thing trying to break it apart to make it easy 
    /// </summary>
    public class OverviewModel
    {
        public List<Board> boards { get; set; }
        public List<UserFriend> PendingFriendRequests { get; set; }
        public List<UserFriend> ActiveFriends { get; set; }
        public string username { get; set; }
        public List<Pin> FollowPin { get; set; }


        public OverviewModel(string _username)
        {
            SetUserName(_username);
            LoadBoards();
            LoadPendingFriendRequest();
            LoadActiveFriends();
            LoadFollowStream();
        }


        public void SetUserName(string _username)
        {
            username = _username;
        }


        /// <summary>
        /// this will load my boards
        /// </summary>
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


        /// <summary>
        /// this will load pending request for the overview model
        /// </summary>
        public void LoadPendingFriendRequest()
        {
            PendingFriendRequests = new List<UserFriend>();
            using (var db = new pinit.Data.PinitEntities())
            {
                var _pendingfriends = db.Friends.Where(x => x.RequestStatus == "requested" && x.TargetUser == username).ToList();
                foreach (var item in _pendingfriends)
                {
                    var user = new UserFriend(item.SourceUser);
                    user.FriendShipStatus = item;
                    PendingFriendRequests.Add(user);
                }

            }

        }


        /// <summary>
        /// this will load active friends for the overview model
        /// </summary>
        public void LoadActiveFriends()
        {
            ActiveFriends = new List<UserFriend>();
            var allactive = new List<UserFriend>();
            using (var db = new pinit.Data.PinitEntities())
            {
                var _activeFriendWhoAskedMe = db.Friends.Where(x => x.RequestStatus.ToUpper() == "ACCEPTED" && x.TargetUser == username).OrderByDescending(x => x.DateModified).ToList();
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

        /// <summary>
        /// this will load follow streams for the overview model
        /// </summary>
        public void LoadFollowStream()
        {
            FollowPin = new List<Pin>();

            using (var db = new pinit.Data.PinitEntities())
            {
                var pins = db.FI_Follow5(username).ToList();
                foreach (var item in pins)
                {
                    var pin = new Pin()
                    {
                        PinId = item.PinId
                    };
                    var pic = new Picture()
                    {
                        DateUploaded = item.DateUploaded,
                        ImageUrl = item.ImageUrl
                    };
                    pin.Picture = pic;
                    FollowPin.Add(pin);
                }
            }

        }

    }
}