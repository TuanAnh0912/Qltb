using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class AddPhieuGhiTangThietBiReqVM
    {
        public string SoPhieu { get; set; }

        public DateTime? NgayNhap { get; set; }

        public string ChungTuLienQuan { get; set; }

        public string NoiDung { get; set; }

        public string NguoiCapNhat { get; set; }

        public List<AddChiTietThietBiReqVM> ChiTietThietBis { get; set; }

        public List<AddTaiLieuDinhKeqReqVM> TaiLieuDinhKems { get; set; }
    }
}
