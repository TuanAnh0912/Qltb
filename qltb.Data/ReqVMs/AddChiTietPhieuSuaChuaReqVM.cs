using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class AddChiTietPhieuSuaChuaReqVM
    {
        public int? ThietBiId { get; set; }

        public int? KhoPhongId { get; set; }

        public string PhieuGhiHongThietBiId { get; set; }
        
        public int? SoLuongSuaChua { get; set; }

        public long? DonGia { get; set; }

    }
}
