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
    
    public partial class Friend
    {
        public string SourceUser { get; set; }
        public string TargetUser { get; set; }
        public string RequestStatus { get; set; }
        public System.DateTime DateRequested { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual UserInfo UserInfo1 { get; set; }
    }
}
