using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateLoaiKhoPhongReqVM
    {
        public int? LoaiKhoPhongId { get; set; }

        public string TenLoaiKhoPhong { get; set; }

        public string MaLoaiKhoPhong { get; set; }
    }
}
