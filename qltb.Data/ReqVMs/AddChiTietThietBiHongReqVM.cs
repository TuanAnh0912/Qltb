using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class AddChiTietThietBiHongReqVM
    {
        public int? ThietBiId { get; set; }

        public int? KhoPhongId { get; set; }

        public int? SoLuongHong { get; set; }

        public string LyDo { get; set; }
    }
}
