using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ChiTietThietBiResVM
    {
        public string KhoThietBiId { get; set; }
        public long? STT { get; set; }
        public string ChiTietThietBiId { get; set; }
        public int? ThietBiId { get; set; }
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public int? LoaiThietBiId { get; set; }
        public string TenLoaiThietBi { get; set; }
        public string SoHieu { get; set; }
        public int? KhoPhongId { get; set; }
        public string TenKhoPhong { get; set; }
        public DateTime? NgaySuDung { get; set; }
        public string NgaySuDungString { get; set; }
        public int? NamTheoDoi { get; set; }
        public int? SoLuong { get; set; }
        public int? SoLuongConLai { get; set; }
        public long? DonGia { get; set; }
        public long? ThanhTien { get; set; }
        public int? DonViTinhId { get; set; }
        public string TenDonViTinh { get; set; }
        public string NuocSanXuat { get; set; }
        public bool? LaThietBiTuLam { get; set; }
        public DateTime? NgaySanXuat { get; set; }
        public string NgaySanXuatString { get; set; }
        public DateTime? HanDung { get; set; }
        public string HanDungString { get; set; }
        public int? NguonCapId { get; set; }
        public string TenNguonCap { get; set; }
        public int? NguonKinhPhiCapTrenId { get; set; }
        public string TenNguonKinhPhiCapTren { get; set; }
        public int? MucDichSuDungId { get; set; }
        public string TenMucDichSuDung { get; set; }
        public int? MonHocId { get; set; }
        public string TenMonHoc { get; set; }
        public string LyDo { get; set; }
        public int? SoLuongGiam { get; set; }
        public int? SoLuongHongMat { get; set; }
        public int? SoLuongHong { get; set; }
        public int? SoLuongHongConLai { get; set; }
        public int? SoLuongMat { get; set; }
        public int? SoLuongConDungDuoc { get; set; }
        public int? SoLuongSuaChua { get; set; }
        public int? SoLuongDeNghi { get; set; }
        public int? KinhPhiId { get; set; }
        public string GhiChu { get; set; }
        public string State { get; set; }
        public string SoPhieuGhiMatThietBi { get; set; }
        public string PhieuGhiMatThietBiId { get; set; }
        public string SoPhieuGhiHongThietBi { get; set; }
        public string PhieuGhiHongThietBiId { get; set; }
        public string SoPhieuKiemKe { get; set; }
        public string NgayKiemKe { get; set; }
    }
}
