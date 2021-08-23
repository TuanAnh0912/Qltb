using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class AddThietBiBaiHocReqVM
    {
        public int ThietBiId { get; set; }
        public string TenThietBi { get; set; }
        public int SoLuong { get; set; }
    }
}
