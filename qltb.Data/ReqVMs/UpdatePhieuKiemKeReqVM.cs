using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdatePhieuKiemKeReqVM 
    { 
        public string PhieuKiemKeId { get; set; }

        public int? KhoPhongId { get; set; }

        public DateTime? NgayLap { get; set; }

        public DateTime? NgayKiemKe { get; set; }

        public string GhiChu { get; set; }

        public string SoPhieu { get; set; }

        public List<AddChiTietKiemKeReqVM> ChiTietKiemKes { get; set; }
        public List<AddTaiLieuDinhKeqReqVM> TaiLieuDinhKems { get; set; }

    }
}
