using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdatePhieuGhiHongThietBiReqVM
    {
        public string PhieuGhiHongThietBiId { get; set; }

        public string SoPhieu { get; set; }

        public string NoiDung { get; set; }

        public DateTime? NgayLap { get; set; }

        public List<AddChiTietThietBiHongReqVM> ChiTietThietBiHongs { get; set; }

        public List<AddTaiLieuDinhKeqReqVM> TaiLieuDinhKems { get; set; }
    }
}
