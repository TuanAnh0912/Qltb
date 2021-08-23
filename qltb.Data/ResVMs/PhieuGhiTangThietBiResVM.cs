using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class PhieuGhiTangThietBiResVM
    {
        public long? STT { get; set; }
        public string PhieuGhiTangThietBiId { get; set; }
        public string SoPhieu { get; set; }

        public DateTime? NgayNhap { get; set; }

        public string ChungTuLienQuan { get; set; }

        public string NoiDung { get; set; }

        public string HoTen { get; set; }

        public List<ChiTietThietBiResVM> ChiTietThietBis { get; set; }

        public List<FileResVM> FileTrongPhieuGhiTangs { get; set; }
    }
}
