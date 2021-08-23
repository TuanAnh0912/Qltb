using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdatePhieuDeNghiMuaSamReqVM
    {
        public string PhieuDeNghiMuaSamId { get; set; } 
        public string SoPhieu { get; set; }

        public string NoiDung { get; set; }

        public DateTime? NgayLap { get; set; }

        public List<AddChiTietPhieuDeNghiMuaSamReqVM> ChiTietPhieuDeNghiMuaSams { get; set; }
        public List<AddTaiLieuDinhKeqReqVM> TaiLieuDinhKems { get; set; }

    }
}
