using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class PhieuGhiGiamThietBiResVM
    {
        public long? STT { get; set; }

        public string SoPhieu { get; set; }

        public string PhieuGhiGiamThietBiId { get; set; }

        public DateTime? NgayLap { get; set; }

        public string NoiDung { get; set; }

        public string HoTen { get; set; }

        public List<ChiTietThietBiResVM> ChiTietThietBis { get; set; }

        public List<FileResVM> FileTrongPhieuGhiGiams { get; set; }

    }
}
