using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ThongKeChungResVM
    {
        public int? TongSoThietBiTrongKho { get; set; }

        public int? TongSoThietBiHongMat { get; set; }

        public int? TongSoThietBiDangMuon { get; set; }

        public int? TongSoThietBiCoTheSuDung { get; set; }

        public int? TongSoPhieuDangMuon { get; set; }

        public int? TongSoPhieuDaTra { get; set; }
        
        public int? TongSoPhieuDangKy { get; set; }

        public int? TongSoThietBiHong { get; set; }

        public int? TongSoThietBiMat { get; set; }
        
        public int? TongSoThietBiDaSua { get; set; }

        public List<PhieuMuonResVM> PhieuMuonTBs { get; set; }
        public List<PhieuMuonResVM> PhieuMuonPhongs { get; set; }
    }
}
