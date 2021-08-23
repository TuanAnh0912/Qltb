using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateTietHocReqVM
    {
        public int? TietHocId { get; set; }

        public int? TenTietHoc { get; set; }

        public int? BuoiTrongNgay { get; set; }
    }
}
