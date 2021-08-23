using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class AddChiTietThietBiGiamReqVM
    {
        public int? ThietBiId { get; set; }

        public int? KhoPhongId { get; set; }

        public int? SoLuongGiam { get; set; }

        public string LyDo { get; set; }

        public string PhieuGhiHongThietBiId { get; set; }

        public string PhieuGhiMatThietBiId { get; set; }
    }
}
