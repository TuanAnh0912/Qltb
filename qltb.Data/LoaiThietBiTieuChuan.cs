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
    
    public partial class LoaiThietBiTieuChuan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiThietBiTieuChuan()
        {
            this.ThietBiTieuChuans = new HashSet<ThietBiTieuChuan>();
        }
    
        public int LoaiThietBiTieuChuanId { get; set; }
        public string TenLoaiThietBi { get; set; }
        public string MaLoaiThietBiTieuChuan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThietBiTieuChuan> ThietBiTieuChuans { get; set; }
    }
}
