using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class PhieuKiemKeResVM
    {
        public long?  STT { get; set; }

        public string PhieuKiemKeId { get; set; }

        public string SoPhieu { get; set; }

        public DateTime? NgayLap { get; set; }

        public DateTime? NgayKiemKe { get; set; }

        public string GhiChu { get; set; }

        public int? KhoPhongId { get; set; }

        public string HoTen { get; set; }

        public List<ChiTietThietBiResVM> ChiTietKiemKes { get; set; }
        public List<FileResVM> FileTrongPhieuKiemKes { get; set; }

    }
}
