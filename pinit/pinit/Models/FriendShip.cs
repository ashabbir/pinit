using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pinit.Data;

namespace pinit.Models
{
    /// <summary>
    /// friendship logic is removed from the data base and brought in to models 
    /// all friends are made into different groups 
    /// </summary>
    public class FriendShip
    {
        public string UserName { get; set; }
        /// <summary>
        /// list of all my active firend --(list userinfo)
        /// </summary>
        public List<UserFriend> ActiveFriends { get; set; }

        /// <summary>
        /// list of all reuqiest that were sent to me and no one has acted on them yyyy-- (targetuserinfo , daterequested) 
        /// </summary>
        public List<UserFriend> PendingFriends { get; set; }

        /// <summary>
        /// list of all his denied request that i denied -- (targetuserinfo , datemodified )
        /// </summary>
        public List<UserFriend> DeniedFriends { get; set; }

        /// <summary>
        /// list of all pending request that i sent out and no one acted on em -- (sourceUserinfo , daterequested)
        /// </summary>
        public List<UserFriend> PendingRequests { get; set; }

        /// <summary>
        /// list of all denied request that i sent out-- -- (sourceUserinfo , daterequested)
        /// </summary>
        public List<UserFriend> DeniedRequest { get; set; }


        /// <summary>
        /// ctor
        /// </summary>
        public FriendShip()
        {
            ActiveFriends = new List<UserFriend>();
            PendingFriends = new List<UserFriend>();
            ActiveFriends = new List<UserFriend>();
            PendingRequests = new List<UserFriend>();
            DeniedRequest = new List<UserFriend>();
        }


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="username"></param>
        public FriendShip(string username)
        {
            UserName = username;
        }

        /// <summary>
        /// gets all the friends to be friends rejects etc.
        /// </summary>
        /// <param name="myUserName"></param>
        public void FillMe(string myUserName)
        {
            UserName = myUserName;
            using (var db = new PinitEntities())
            {

                //go thorugh friend table and get all request that are active and i m either target or srouce
                //That will be all my active friends
                var _acceptedfriend = db.Friends.Where(x => x.RequestStatus == "accepted" && (x.TargetUser == myUserName || x.SourceUser == myUserName)).ToList();
                foreach (var item in _acceptedfriend)
                {
                    if (item.TargetUser != myUserName) 
                    {
                        var user = new UserFriend(item.TargetUser);
                        user.FriendShipStatus = item;
                        ActiveFriends.Add(user);
                    }
                    else if (item.SourceUser != myUserName) 
                    {
                        var user = new UserFriend(item.SourceUser);
                        user.FriendShipStatus = item;
                        ActiveFriends.Add(user);
                    }

                }


                //go thorugh friend table and get all request that are pending and i m the target
                //these request are pending i have to either approve them ok deny them
                var _pendingfriends = db.Friends.Where(x => x.RequestStatus == "requested" && x.TargetUser == myUserName ).ToList();
                foreach (var item in _pendingfriends)
                {
                    var user = new UserFriend(item.SourceUser);
                    user.FriendShipStatus = item;
                    PendingFriends.Add(user);
                }

                //go thorugh friend table and get all request that are denied and i m the target
                //these request are denied i dont need to do any thing with them 
                var _deniedfriends = db.Friends.Where(x => x.RequestStatus == "denied" && x.TargetUser == myUserName).ToList();
                foreach (var item in _deniedfriends)
                {
                    var user = new UserFriend(item.SourceUser);
                    user.FriendShipStatus = item;
                    DeniedFriends.Add(user);
                }

                //go thorugh friend table and get all request that are pending and i m the srouce
                //these are the request that i sent out and are not yet approved
                var _pendingrequest = db.Friends.Where(x => x.RequestStatus == "requested" && x.SourceUser == myUserName).ToList();
                foreach (var item in _pendingrequest)
                {
                    var user = new UserFriend(item.TargetUser);
                    user.FriendShipStatus = item;
                    PendingRequests.Add(user);
                }

                //go thorugh friend table and get all request that are denied and i m the source
                //these are the request that i sent out are are denied
                var _deniedrequest = db.Friends.Where(x => x.RequestStatus == "denied" && x.SourceUser == myUserName).ToList();
                foreach (var item in _deniedrequest)
                {
                    var user = new UserFriend(item.SourceUser);
                    user.FriendShipStatus = item;
                    DeniedRequest.Add(user);
                }
            }
        }

        /// <summary>
        /// this will call the SP to accept or reject a request
        /// </summary>
        /// <param name="sourceUserName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string AcceptReject(string sourceUserName , bool status)
        {
            var toret = "";
            using (var db = new PinitEntities())
            {
                var res = db.FI_AcceptRejectRequest(sourceUserName, UserName, status).FirstOrDefault();
                if (res!= null)
                {
                    if (res.Success == true)
                    {
                        toret = "";
                    }
                    else 
                    {
                        toret = res.msg;
                    }
                }
                else 
                {
                    toret = "Action was not performed";
                }
            }
            return toret;

        }
        
    }


    /// <summary>
    /// just a place holder (think of it as project over a join) dont wana bring in every thing
    /// </summary>
    public class UserFriend
    {
        public UserInfo PersonalInfo { get; set; }
        public Friend FriendShipStatus { get; set; }


        public UserFriend()
        {
            PersonalInfo = new UserInfo();
            FriendShipStatus = new Friend();
        }


        public UserFriend(string username)
        {
            PersonalInfo = new UserInfo();
            FriendShipStatus = new Friend();
            using (var db = new PinitEntities())
            {
                PersonalInfo = db.UserInfoes.FirstOrDefault(u => u.UserName == username);
            }
        }
    }


}




/*
 * exec dbo.UP_SendFriendShipRequest
	@FromUser = '',
	@ToUser = ''



--List of all friends 
--send request 
--deny or accept request


--friends mode
--for a user
list of all his active firend --(list userinfo)
list of all his pending request (sent out by him)  -- (targetuserinfo , daterequested) 
list of all his denied request (sent out by him) -- (targetuserinfo , datemodified )
list of all pending request Sent to him -- (sourceUserinfo , daterequested)
list of all denied request Sent to him -- -- (sourceUserinfo , daterequested)

 */