using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateNguonCapReqVM
    {
        public int? NguonCapId { get; set; }

        public string TenNguonCap { get; set; }

        public string MaNguonCap { get; set; }
    }
}
