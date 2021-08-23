using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateLoaiThietBiReqVM
    {
        public int? LoaiThietBiId { get; set; }

        public string MaLoaiThietBi { get; set; }

        public string TenLoaiThietBi { get; set; }
    }
}
