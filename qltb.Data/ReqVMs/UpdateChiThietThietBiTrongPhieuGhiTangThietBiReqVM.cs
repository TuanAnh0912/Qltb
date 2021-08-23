using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateChiThietThietBiTrongPhieuGhiTangThietBiReqVM
    {
        public string ChiTietThietBiId { get; set; }
        public int? ThietBiId { get; set; }
        public string SoHieu { get; set; }
        public int? KhoPhongId { get; set; }
        public DateTime? NgaySuDung { get; set; }
        public int? NamTheoDoi { get; set; }
        public int? SoLuong { get; set; }
        public long? DonGia { get; set; }
        public string NuocSanXuat { get; set; }
        public bool? LaThietBiTuLam { get; set; }
        public DateTime? NgaySanXuat { get; set; }
        public DateTime? HanDung { get; set; }
        public int? NguonCapId { get; set; }
        public int? NguonKinhPhiCapTrenId { get; set; }
        public int? MucDichSuDungId { get; set; }
        public string State { get; set; }
    }
}
