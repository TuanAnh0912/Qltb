using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ReportMuonTraTheoGiaoVienResVM
    {
        public int? NamHoc { get; set; }

        public int? TongSoThietBiDaMuon { get; set; }

        public int? TongSoThietBiDangMuon { get; set; }

        public int? TongSoThietBiDaTra { get; set; }

        public List<PhieuMuonResVM> MuonPhongs { get; set; }

        public List<PhieuMuonResVM> MuonThietBis { get; set; }

    }
}
