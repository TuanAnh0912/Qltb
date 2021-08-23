using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class AddPhieuGhiMatThietBiReqVM
    {
        public string SoPhieu { get; set; }

        public string NoiDung { get; set; }

        public DateTime? NgayLap { get; set; }

        public string NguoiCapNhat { get; set; }

        public List<AddChiTietThietBiMatReqVM> ChiTietThietBiMats { get; set; }

        public List<AddTaiLieuDinhKeqReqVM> TaiLieuDinhKems { get; set; }

    }
}
