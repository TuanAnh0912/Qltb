using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class PhieuDeNghiMuaSamResVM
    {
        public long? STT { get; set; }

        public string SoPhieu { get; set; }

        public string PhieuDeNghiMuaSamId { get; set; }

        public DateTime? NgayLap { get; set; }

        public string NoiDung { get; set; }

        public bool? DaXuLy { get; set; }

        public bool? ChotPhieu { get; set; }

        public string HoTen { get; set; }

        public List<ChiTietThietBiResVM> ChiTietThietBis { get; set; }

        public List<FileResVM> FileTrongPhieuDeNghiMuaSams { get; set; }

    }
}
