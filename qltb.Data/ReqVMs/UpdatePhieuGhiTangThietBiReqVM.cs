using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdatePhieuGhiTangThietBiReqVM
    {
        public string PhieuGhiTangThietBiId { get; set; }
        public string SoPhieu { get; set; }

        public DateTime? NgayNhap { get; set; }

        public string ChungTuLienQuan { get; set; }

        public string NoiDung { get; set; }

        public List<UpdateChiThietThietBiTrongPhieuGhiTangThietBiReqVM> ChiTietThietBis { get; set; }

        public List<AddTaiLieuDinhKeqReqVM> TaiLieuDinhKems { get; set; }

    }
}
