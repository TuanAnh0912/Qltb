//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace qltb.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChucNang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChucNang()
        {
            this.QuyenTruyCaps = new HashSet<QuyenTruyCap>();
        }
    
        public int ChucNangId { get; set; }
        public string MaChucNang { get; set; }
        public string TenChucNang { get; set; }
        public Nullable<int> KhoaChaId { get; set; }
        public string DuongDan { get; set; }
        public string Icon { get; set; }
        public Nullable<int> NhomChucNangId { get; set; }
        public Nullable<bool> HienTrenMenu { get; set; }
    
        public virtual NhomChucNang NhomChucNang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuyenTruyCap> QuyenTruyCaps { get; set; }
    }
}
