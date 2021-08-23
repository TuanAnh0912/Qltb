using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateKhoiLopReqVM
    {
        public int? KhoiLopId { get; set; }

        public string TenKhoiLop { get; set; }

        public string MaKhoiLop { get; set; }
    }
}
