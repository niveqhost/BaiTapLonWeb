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
    
    public partial class BAIDANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BAIDANG()
        {
            this.LUOTXEMs = new HashSet<LUOTXEM>();
        }
    
        public int IDBaiDang { get; set; }
        public string TenBaiDang { get; set; }
        public string TieuDe { get; set; }
        public string UrlRequire { get; set; }
        public string AnhDaiDien { get; set; }
        public string NoiDung { get; set; }
        public int IDTaiKhoan { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
        public string TrangThaiBaiDang { get; set; }
        public int IDTheLoai { get; set; }
    
        public virtual TAIKHOAN TAIKHOAN { get; set; }
        public virtual THELOAI THELOAI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LUOTXEM> LUOTXEMs { get; set; }
    }
}
