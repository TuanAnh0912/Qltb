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
    
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            this.LienKetNguoiDungChucVus = new HashSet<LienKetNguoiDungChucVu>();
            this.LienKetNguoiNguoiDungPhongBans = new HashSet<LienKetNguoiNguoiDungPhongBan>();
            this.PhieuMuons = new HashSet<PhieuMuon>();
            this.QuyenTruyCaps = new HashSet<QuyenTruyCap>();
        }
    
        public System.Guid NguoiDungId { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Salt { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public Nullable<System.Guid> DonViId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<int> GioiTinhId { get; set; }
        public Nullable<int> NhomNguoiDungId { get; set; }
        public Nullable<int> GiaoVienId { get; set; }
    
        public virtual GioiTinh GioiTinh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LienKetNguoiDungChucVu> LienKetNguoiDungChucVus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LienKetNguoiNguoiDungPhongBan> LienKetNguoiNguoiDungPhongBans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuyenTruyCap> QuyenTruyCaps { get; set; }
    }
}
