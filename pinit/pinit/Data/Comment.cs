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
    
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public int PinId { get; set; }
        public string CommentText { get; set; }
        public System.DateTime DateCommented { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual Pin Pin { get; set; }
    }
}
