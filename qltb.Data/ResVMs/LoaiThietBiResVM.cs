using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class LoaiThietBiResVM
    {
        public long? STT { get; set; }
        public int? LoaiThietBiId { get; set; }

        public string MaLoaiThietBi { get; set; }

        public string TenLoaiThietBi { get; set; }
    }
}
