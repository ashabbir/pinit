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
    
    public partial class Tag
    {
        public Tag()
        {
            this.PinTags = new HashSet<PinTag>();
        }
    
        public int TagId { get; set; }
        public string TagName { get; set; }
    
        public virtual ICollection<PinTag> PinTags { get; set; }
    }
}
