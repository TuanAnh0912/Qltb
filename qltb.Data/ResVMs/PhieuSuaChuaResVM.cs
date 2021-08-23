using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class PhieuSuaChuaResVM
    {
        public long? STT { get; set; }

        public string SoPhieu { get; set; }

        public string PhieuSuaChuaId { get; set; }

        public DateTime? NgayLap { get; set; }

        public string GhiChu { get; set; }

        public string HoTen { get; set; }

        public List<ChiTietThietBiResVM> ChiTietThietBis { get; set; }
        public List<FileResVM> FileTrongPhieuSuaChuas { get; set; }

    }
}
