//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LUOTXEM
    {
        public int ID { get; set; }
        public System.DateTime NgayThang { get; set; }
        public int IDBaiDang { get; set; }
    
        public virtual BAIDANG BAIDANG { get; set; }
    }
}
