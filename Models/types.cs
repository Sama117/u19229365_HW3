//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HW3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class types
    {
        public types()
        {
            this.books = new HashSet<books>();
        }
    
        public int typeId { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<books> books { get; set; }
    }
}