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
    
    public partial class ChuongTrinhHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongTrinhHoc()
        {
            this.PhieuMuons = new HashSet<PhieuMuon>();
        }
    
        public int ChuongTrinhHocId { get; set; }
        public string TenChuongTrinhHoc { get; set; }
        public string MaChuongTrinhHoc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
    }
}