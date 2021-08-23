using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ThietBiResVM
    {
        public long STT { get; set; }
        public int? ThietBiId { get; set; }
        public int? SoLuongToiThieu { get; set; }
        public string MaThietBi { get; set; }
        public Nullable<int> LoaiThietBiId { get; set; }
        public Nullable<int> DonViTinhId { get; set; }
        public Nullable<int> MonHocId { get; set; }
        public Nullable<System.Guid> DonViId { get; set; }
        public string TenThietBi { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string TenLoaiThietBi { get; set; }
        public string TenDonViTinh { get; set; }
        public string TenDonVi { get; set; }
        public Nullable<bool> QuanLyTieuHao { get; set; }
        public string MoTa { get; set; }
        public String TenMonHoc { get; set; }
        public List<KhoiLop> DanhSachKhoiLop { get; set; }
        public int? KhoPhongId { get; set; }
        public int? SoLuong { get; set; }
        public int? SoLuongConLai { get; set; }
        public string TenKhoPhong { get; set; }
        public bool CheckTonTaiTrongKho { get; set; }
        public int SoLuongHong { get; set; }
        public int SoLuongMat { get; set; }
    }
}
