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
    
    public partial class Phong
    {
        public int PhongId { get; set; }
        public Nullable<int> KhoiPhongTieuChuanId { get; set; }
        public string TenPhong { get; set; }
        public string DienTich { get; set; }
        public string MaPhong { get; set; }
    
        public virtual KhoiPhongTieuChuan KhoiPhongTieuChuan { get; set; }
    }
}
