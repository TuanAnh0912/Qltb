using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class SelectThietBiViewmodel
    {
        public int ThietBiId { get; set; }
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public int LoaiThietBiId { get; set; }
        public string TenLoaiThietBi { get; set; }
        public Nullable<int> MonHocId { get; set; }
        public int SoLuongConLai { get; set; }
        public string TenMonHoc { get; set; }
        public string Selected { get; set; }
        public int SoLuong { get; set; }
        public Nullable<int> SoLuongHong { get; set; }
        public Nullable<int> SoLuongMat { get; set; }
        public Nullable<int> KhoPhongId { get; set; }
        public string TenKhoPhong { get; set; }
        public int TrangThaiPhieuMuonId { get; set; }
    }
}
