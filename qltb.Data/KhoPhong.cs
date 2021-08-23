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
    
    public partial class KhoPhong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoPhong()
        {
            this.PhieuMuons = new HashSet<PhieuMuon>();
            this.SuDungPhongs = new HashSet<SuDungPhong>();
            this.ThoiKhoaBieuPhongs = new HashSet<ThoiKhoaBieuPhong>();
        }
    
        public int KhoPhongId { get; set; }
        public string MaKhoPhong { get; set; }
        public string TenKhoPhong { get; set; }
        public string DienTich { get; set; }
        public Nullable<int> NamSuDung { get; set; }
        public Nullable<int> LoaiKhoPhongId { get; set; }
        public Nullable<int> KieuSuDungKhoPhongId { get; set; }
        public Nullable<int> XepHangKhoPhongId { get; set; }
        public Nullable<int> SoNguoiQuanLy { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public Nullable<bool> LaPhongHocChucNang { get; set; }
        public Nullable<System.Guid> DonViId { get; set; }
        public Nullable<int> TrangThaiPhongId { get; set; }
    
        public virtual KieuSuDungKhoPhong KieuSuDungKhoPhong { get; set; }
        public virtual LoaiKhoPhong LoaiKhoPhong { get; set; }
        public virtual TrangTraiPhong TrangTraiPhong { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuDungPhong> SuDungPhongs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThoiKhoaBieuPhong> ThoiKhoaBieuPhongs { get; set; }
    }
}
