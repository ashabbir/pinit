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
    
    public partial class PinTag
    {
        public int PinId { get; set; }
        public int TagId { get; set; }
        public System.DateTime DateTaged { get; set; }
    
        public virtual Pin Pin { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
