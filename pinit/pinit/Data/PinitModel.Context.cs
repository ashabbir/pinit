﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pinit.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PinitEntities : DbContext
    {
        public PinitEntities()
            : base("name=PinitEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<BoardFollow> BoardFollows { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FollowStream> FollowStreams { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<PinTag> PinTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<Pin> Pins { get; set; }
        public virtual DbSet<Repin> Repins { get; set; }
        public virtual DbSet<UserLike> UserLikes { get; set; }
    
        public virtual ObjectResult<UP_AcceptOrRejectFriendShipRequest_Result> UP_AcceptOrRejectFriendShipRequest(string fromUser, string toUser, Nullable<bool> accepted)
        {
            var fromUserParameter = fromUser != null ?
                new ObjectParameter("FromUser", fromUser) :
                new ObjectParameter("FromUser", typeof(string));
    
            var toUserParameter = toUser != null ?
                new ObjectParameter("ToUser", toUser) :
                new ObjectParameter("ToUser", typeof(string));
    
            var acceptedParameter = accepted.HasValue ?
                new ObjectParameter("accepted", accepted) :
                new ObjectParameter("accepted", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_AcceptOrRejectFriendShipRequest_Result>("UP_AcceptOrRejectFriendShipRequest", fromUserParameter, toUserParameter, acceptedParameter);
        }
    
        public virtual ObjectResult<UP_Comment_Result> UP_Comment(Nullable<int> pinid, string username, string commenttext)
        {
            var pinidParameter = pinid.HasValue ?
                new ObjectParameter("pinid", pinid) :
                new ObjectParameter("pinid", typeof(int));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var commenttextParameter = commenttext != null ?
                new ObjectParameter("commenttext", commenttext) :
                new ObjectParameter("commenttext", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_Comment_Result>("UP_Comment", pinidParameter, usernameParameter, commenttextParameter);
        }
    
        public virtual ObjectResult<UP_Delete_Pin_Result> UP_Delete_Pin(Nullable<int> pinid)
        {
            var pinidParameter = pinid.HasValue ?
                new ObjectParameter("pinid", pinid) :
                new ObjectParameter("pinid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_Delete_Pin_Result>("UP_Delete_Pin", pinidParameter);
        }
    
        public virtual ObjectResult<UP_Display_Stream_Result> UP_Display_Stream(Nullable<int> streamid)
        {
            var streamidParameter = streamid.HasValue ?
                new ObjectParameter("streamid", streamid) :
                new ObjectParameter("streamid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_Display_Stream_Result>("UP_Display_Stream", streamidParameter);
        }
    
        public virtual ObjectResult<UP_Follow_Result> UP_Follow(Nullable<int> boardid, Nullable<int> streamid)
        {
            var boardidParameter = boardid.HasValue ?
                new ObjectParameter("boardid", boardid) :
                new ObjectParameter("boardid", typeof(int));
    
            var streamidParameter = streamid.HasValue ?
                new ObjectParameter("streamid", streamid) :
                new ObjectParameter("streamid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_Follow_Result>("UP_Follow", boardidParameter, streamidParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> UP_KeyWordSearchOnTags(string keyWork)
        {
            var keyWorkParameter = keyWork != null ?
                new ObjectParameter("KeyWork", keyWork) :
                new ObjectParameter("KeyWork", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("UP_KeyWordSearchOnTags", keyWorkParameter);
        }
    
        public virtual ObjectResult<UP_Like_Result> UP_Like(Nullable<int> pinid, string username)
        {
            var pinidParameter = pinid.HasValue ?
                new ObjectParameter("pinid", pinid) :
                new ObjectParameter("pinid", typeof(int));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_Like_Result>("UP_Like", pinidParameter, usernameParameter);
        }
    
        public virtual ObjectResult<UP_Login_Result> UP_Login(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_Login_Result>("UP_Login", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<UP_New_Board_Result> UP_New_Board(string boardname, string owner, Nullable<bool> privatecomments)
        {
            var boardnameParameter = boardname != null ?
                new ObjectParameter("boardname", boardname) :
                new ObjectParameter("boardname", typeof(string));
    
            var ownerParameter = owner != null ?
                new ObjectParameter("owner", owner) :
                new ObjectParameter("owner", typeof(string));
    
            var privatecommentsParameter = privatecomments.HasValue ?
                new ObjectParameter("privatecomments", privatecomments) :
                new ObjectParameter("privatecomments", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_New_Board_Result>("UP_New_Board", boardnameParameter, ownerParameter, privatecommentsParameter);
        }
    
        public virtual ObjectResult<UP_New_Stream_Result> UP_New_Stream(string streamname, string username)
        {
            var streamnameParameter = streamname != null ?
                new ObjectParameter("streamname", streamname) :
                new ObjectParameter("streamname", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_New_Stream_Result>("UP_New_Stream", streamnameParameter, usernameParameter);
        }
    
        public virtual ObjectResult<UP_Pin_Result> UP_Pin(string imgurl, Nullable<int> boardid)
        {
            var imgurlParameter = imgurl != null ?
                new ObjectParameter("imgurl", imgurl) :
                new ObjectParameter("imgurl", typeof(string));
    
            var boardidParameter = boardid.HasValue ?
                new ObjectParameter("boardid", boardid) :
                new ObjectParameter("boardid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_Pin_Result>("UP_Pin", imgurlParameter, boardidParameter);
        }
    
        public virtual ObjectResult<UP_Repin_Result> UP_Repin(Nullable<int> pinid, Nullable<int> boardid)
        {
            var pinidParameter = pinid.HasValue ?
                new ObjectParameter("pinid", pinid) :
                new ObjectParameter("pinid", typeof(int));
    
            var boardidParameter = boardid.HasValue ?
                new ObjectParameter("boardid", boardid) :
                new ObjectParameter("boardid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_Repin_Result>("UP_Repin", pinidParameter, boardidParameter);
        }
    
        public virtual ObjectResult<UP_SendFriendShipRequest_Result> UP_SendFriendShipRequest(string fromUser, string toUser)
        {
            var fromUserParameter = fromUser != null ?
                new ObjectParameter("FromUser", fromUser) :
                new ObjectParameter("FromUser", typeof(string));
    
            var toUserParameter = toUser != null ?
                new ObjectParameter("ToUser", toUser) :
                new ObjectParameter("ToUser", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_SendFriendShipRequest_Result>("UP_SendFriendShipRequest", fromUserParameter, toUserParameter);
        }
    
        public virtual ObjectResult<UP_SignUp_Result> UP_SignUp(string username, string password, string firstName, string lastName, string email)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_SignUp_Result>("UP_SignUp", usernameParameter, passwordParameter, firstNameParameter, lastNameParameter, emailParameter);
        }
    
        public virtual ObjectResult<UP_UpdateProfile_Result> UP_UpdateProfile(string username, string newFirstName, string newLastName, string newEmail)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var newFirstNameParameter = newFirstName != null ?
                new ObjectParameter("newFirstName", newFirstName) :
                new ObjectParameter("newFirstName", typeof(string));
    
            var newLastNameParameter = newLastName != null ?
                new ObjectParameter("newLastName", newLastName) :
                new ObjectParameter("newLastName", typeof(string));
    
            var newEmailParameter = newEmail != null ?
                new ObjectParameter("newEmail", newEmail) :
                new ObjectParameter("newEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_UpdateProfile_Result>("UP_UpdateProfile", usernameParameter, newFirstNameParameter, newLastNameParameter, newEmailParameter);
        }
    
        public virtual ObjectResult<UP_SignUp_Result> FI_SignUp(string username, string password, string firstName, string lastName, string email)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_SignUp_Result>("FI_SignUp", usernameParameter, passwordParameter, firstNameParameter, lastNameParameter, emailParameter);
        }
    
        public virtual ObjectResult<FI_UpdateProfile_Result> FI_UpdateProfile(string username, string newFirstName, string newLastName, string newEmail)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var newFirstNameParameter = newFirstName != null ?
                new ObjectParameter("newFirstName", newFirstName) :
                new ObjectParameter("newFirstName", typeof(string));
    
            var newLastNameParameter = newLastName != null ?
                new ObjectParameter("newLastName", newLastName) :
                new ObjectParameter("newLastName", typeof(string));
    
            var newEmailParameter = newEmail != null ?
                new ObjectParameter("newEmail", newEmail) :
                new ObjectParameter("newEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_UpdateProfile_Result>("FI_UpdateProfile", usernameParameter, newFirstNameParameter, newLastNameParameter, newEmailParameter);
        }
    
        public virtual ObjectResult<FI_Pin_Result> FI_Pin(string imgurl, Nullable<int> boardid)
        {
            var imgurlParameter = imgurl != null ?
                new ObjectParameter("imgurl", imgurl) :
                new ObjectParameter("imgurl", typeof(string));
    
            var boardidParameter = boardid.HasValue ?
                new ObjectParameter("boardid", boardid) :
                new ObjectParameter("boardid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_Pin_Result>("FI_Pin", imgurlParameter, boardidParameter);
        }
    
        public virtual ObjectResult<FI_AcceptRejectRequest_Result> FI_AcceptRejectRequest(string fromUser, string toUser, Nullable<bool> accepted)
        {
            var fromUserParameter = fromUser != null ?
                new ObjectParameter("FromUser", fromUser) :
                new ObjectParameter("FromUser", typeof(string));
    
            var toUserParameter = toUser != null ?
                new ObjectParameter("ToUser", toUser) :
                new ObjectParameter("ToUser", typeof(string));
    
            var acceptedParameter = accepted.HasValue ?
                new ObjectParameter("accepted", accepted) :
                new ObjectParameter("accepted", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_AcceptRejectRequest_Result>("FI_AcceptRejectRequest", fromUserParameter, toUserParameter, acceptedParameter);
        }
    
        public virtual ObjectResult<UP_PossibleFriends_Result> UP_PossibleFriends(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_PossibleFriends_Result>("UP_PossibleFriends", usernameParameter);
        }
    
        public virtual ObjectResult<FP_PossibleFriends_Result> FP_PossibleFriends(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FP_PossibleFriends_Result>("FP_PossibleFriends", usernameParameter);
        }
    
        public virtual ObjectResult<FI_PossibleFriend_Result> FI_PossibleFriend(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_PossibleFriend_Result>("FI_PossibleFriend", usernameParameter);
        }
    
        public virtual ObjectResult<FI_SendFriendShipRequest_Result> FI_SendFriendShipRequest(string fromUser, string toUser)
        {
            var fromUserParameter = fromUser != null ?
                new ObjectParameter("FromUser", fromUser) :
                new ObjectParameter("FromUser", typeof(string));
    
            var toUserParameter = toUser != null ?
                new ObjectParameter("ToUser", toUser) :
                new ObjectParameter("ToUser", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_SendFriendShipRequest_Result>("FI_SendFriendShipRequest", fromUserParameter, toUserParameter);
        }
    
        public virtual ObjectResult<FI_Pin_Result> FI_PinWithId(string imgurl, Nullable<int> boardid)
        {
            var imgurlParameter = imgurl != null ?
                new ObjectParameter("imgurl", imgurl) :
                new ObjectParameter("imgurl", typeof(string));
    
            var boardidParameter = boardid.HasValue ?
                new ObjectParameter("boardid", boardid) :
                new ObjectParameter("boardid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_Pin_Result>("FI_PinWithId", imgurlParameter, boardidParameter);
        }
    
        public virtual ObjectResult<FI_DisplayStream_Result> FI_DisplayStream(Nullable<int> streamid)
        {
            var streamidParameter = streamid.HasValue ?
                new ObjectParameter("streamid", streamid) :
                new ObjectParameter("streamid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_DisplayStream_Result>("FI_DisplayStream", streamidParameter);
        }
    
        public virtual ObjectResult<FI_New_Stream_Result> FI_New_Stream(string streamname, string username)
        {
            var streamnameParameter = streamname != null ?
                new ObjectParameter("streamname", streamname) :
                new ObjectParameter("streamname", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_New_Stream_Result>("FI_New_Stream", streamnameParameter, usernameParameter);
        }
    
        public virtual ObjectResult<FI_Follow_Result> FI_Follow(Nullable<int> boardid, Nullable<int> streamid)
        {
            var boardidParameter = boardid.HasValue ?
                new ObjectParameter("boardid", boardid) :
                new ObjectParameter("boardid", typeof(int));
    
            var streamidParameter = streamid.HasValue ?
                new ObjectParameter("streamid", streamid) :
                new ObjectParameter("streamid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_Follow_Result>("FI_Follow", boardidParameter, streamidParameter);
        }
    
        public virtual ObjectResult<UP_FollowTop5_Result> UP_FollowTop5(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UP_FollowTop5_Result>("UP_FollowTop5", usernameParameter);
        }
    
        public virtual ObjectResult<FI_Follow5_Result> FI_Follow5(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FI_Follow5_Result>("FI_Follow5", usernameParameter);
        }
    }
}
