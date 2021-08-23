using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qltb.Data.ResVMs;

namespace qltb.Data.ViewModels
{
    public class PhieuMuonViewModel
    {
        public int PhieuMuonId { get; set; }
        public string MaPhieuMuon { get; set; }
        public Nullable<int> ChuongTrinhHocId { get; set; }
        public Nullable<int> KhoiLopId { get; set; }
        public Nullable<int> GiaoVienId { get; set; }
        public Nullable<int> MucDichSuDungId { get; set; }
        public Nullable<int> MonHocId { get; set; }
        public Nullable<int> LopHocId { get; set; }
        public Nullable<int> SoTietHoc { get; set; }
        public Nullable<System.DateTime> NgayMuon { get; set; }
        public Nullable<System.DateTime> NgayTra { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.Guid> NguoiTao { get; set; }
        public Nullable<int> LoaiPhieuMuonId { get; set; }
        public Nullable<int> KhoPhongId { get; set; }
        public string GhiChu { get; set; }
        public Nullable<int> BaiHocId { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> TrangThaiPhieuMuonId { get; set; }

        public List<LogPhieuMuonViewModel> Logs { get; set; }
        public List<GiaoVienResVM> GiaoViens { get; set; }
        public List<ChuongTrinhHocResVM> ChuongTrinhHocs { get; set; }
        public List<MonHocResVM> MonHocs { get; set; }
        public List<KhoiLopResVM> KhoiLops { get; set; }
        public List<LopResVM> Lops { get; set; }
        public List<BaiHocCoSuDungThietBiResVM> BaiHocCoSuDungThietBis { get; set; }
        public List<MucDichSuDungResVM> MucDichSuDungs { get; set; }
        public List<int> BuoiTrongNgay { get; set; }
        public List<TietHocResVM> TietHocs { get; set; }
        public List<int> TietHocTrongPhieu { get; set; }
        public List<TietHocTrongPhieuMuonViewModel> TietHocDaChon { get; set; }
        public List<KhoPhongResVM> KhoPhongs { get; set; }
        public List<LoaiThietBiResVM> LoaiThietBis { get; set; }
        public List<SelectThietBiViewmodel> ThietBis { get; set; }
        public List<LoaiKhoPhongResVM> LoaiPhongs { get; set; }
        public PhongViewModel PhongMuon { get; set; }

    }
}
