//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class FollowStream
    {
        public FollowStream()
        {
            this.BoardFollows = new HashSet<BoardFollow>();
        }
    
        public int StreamId { get; set; }
        public string StreamName { get; set; }
        public string UserName { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual ICollection<BoardFollow> BoardFollows { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
