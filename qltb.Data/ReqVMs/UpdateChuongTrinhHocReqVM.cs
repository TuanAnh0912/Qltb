using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateChuongTrinhHocReqVM
    {
        public int? ChuongTrinhHocId { get; set; }

        public string TenChuongTrinhHoc { get; set; }

        public string MaChuongTrinhHoc { get; set; }
    }
}
