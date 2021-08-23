using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class PhieuGhiMatThietBiResVM
    {
        public long? STT { get; set; }

        public string SoPhieu { get; set; }

        public string PhieuGhiMatThietBiId { get; set; }

        public DateTime? NgayLap { get; set; }

        public string NoiDung { get; set; }

        public int? TrangThaiSuaChua { get; set; }

        public string HoTen { get; set; }

        public List<ChiTietThietBiResVM> ChiTietThietBis { get; set; }

        public List<FileResVM> FileTrongPhieuGhiMats { get; set; }


    }
}
