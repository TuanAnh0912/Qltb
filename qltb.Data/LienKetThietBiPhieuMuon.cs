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
    
    public partial class LienKetThietBiPhieuMuon
    {
        public int LienKetId { get; set; }
        public string MaPhieuMuon { get; set; }
        public Nullable<int> ThietBiId { get; set; }
        public Nullable<int> KhoPhongId { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> SoLuongHong { get; set; }
        public Nullable<int> SoLuongMat { get; set; }
    
        public virtual PhieuMuon PhieuMuon { get; set; }
        public virtual ThietBi ThietBi { get; set; }
    }
}
