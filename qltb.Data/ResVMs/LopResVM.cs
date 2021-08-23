using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class LopResVM
    {

        public long? STT { get; set; }
        public int? LopId { get; set; }

        public string TenLop { get; set; }

        public string MaLop { get; set; }

        public int? KhoiLopId { get; set; }

        public string TenKhoiLop { get; set; }
    }
}
