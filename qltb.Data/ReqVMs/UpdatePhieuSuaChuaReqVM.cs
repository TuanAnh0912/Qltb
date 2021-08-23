using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdatePhieuSuaChuaReqVM
    {
        public string PhieuSuaChuaId { get; set; }

        public string SoPhieu { get; set; }

        public DateTime? NgayLap { get; set; }

        public string GhiChu { get; set; }

        public List<AddChiTietPhieuSuaChuaReqVM> ChiTietPhieuSuaChuas { get; set; }

        public List<AddTaiLieuDinhKeqReqVM> TaiLieuDinhKems { get; set; }

    }
}
