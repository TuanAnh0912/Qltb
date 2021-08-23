using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qltb.WebApp.Models.StorageRoomViewModel
{
    public class KhoPhongDetail
    {
        public int KhoPhongId { get; set; }
        public  List<ThietBiTrongPhongViewModel> ThietBis { set; get; }

    }
    public class ThietBiTrongPhongViewModel
    {
        public string KhoThietBiId { get; set; }
        public int ThietBiId { get; set; }
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public int  SoLuong { get; set; }
        public int SoLuongHong { get; set; }
        public int SoLuongMat { get; set; }

    }
    public class BaoHongThietBiModel
    {
        public string PhieuGhiHongThietBiId { get; set; }
        public int KhoPhongId { get; set; }
        public string SoPhieu { get; set; }
        public string NgayLap { get; set; }
        public string GhiChu { get; set; }
        public List<BaoHongThietBiChiTietModel> BaoHongThietBiChiTietModels   { get; set; }

}
    public class BaoHongThietBiChiTietModel
    {
        public int ThietBiId { get; set; }
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongHong { get; set; }
    }
    public class BaoMatThietBiModel
    {
        public string PhieuGhiMatThietBiId { get; set; }
        public int KhoPhongId { get; set; }
        public string SoPhieu { get; set; }
        public string NgayLap { get; set; }
        public string GhiChu { get; set; }
        public List<BaoMatThietBiChiTietModel> BaoMatThietBiChiTietModels { get; set; }

    }
    public class BaoMatThietBiChiTietModel
    {
        public int ThietBiId { get; set; }
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongMat { get; set; }
    }

    public class BaoHongBaoMatItemModel
    {
        public string Id { get; set; }
        public string SoHieu { get; set; }
        public DateTime NgayBao { get; set; }
        public string NguoiTao { get; set; }
        public string TrangThai { get; set; }
        public string TrangThaiColor { get; set; }
        public string Type { get; set; }
    }
}