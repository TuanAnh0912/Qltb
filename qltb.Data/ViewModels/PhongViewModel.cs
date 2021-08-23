using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class PhongViewModel
    {
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
        public string TenTrangThai { get; set; }
        public int TrangThaiPhieuMuonId { get; set; }
        public string MaMau { get; set; }
        public string TenNguoiDangKy { get; set; }
        public bool Selected { get; set; }
    }
}
