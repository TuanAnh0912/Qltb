using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateLoaiPhuTrachReqVM
    {
        public int? LoaiPhuTrachId { get; set; }

        public string TenLoaiPhuTrach { get; set; }

        public string MaLoaiPhuTrach { get; set; }
    }
}
