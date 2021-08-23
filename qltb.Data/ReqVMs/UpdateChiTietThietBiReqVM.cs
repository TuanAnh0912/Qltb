using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateChiTietThietBiReqVM
    {
        public string KhoThietBiId { get; set; }
        public int? ThietBiId { get; set; }
        public int? KhoPhongId { get; set; }
        public int? SoLuong { get; set; }
        public string SoHieu { get; set; }
    }
}
