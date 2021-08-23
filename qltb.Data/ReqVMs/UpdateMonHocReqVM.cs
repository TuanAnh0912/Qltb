using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateMonHocReqVM
    {
        public int? MonHocId { get; set; }

        public string TenMonHoc { get; set; }

        public string MaMonHoc { get; set; }
    }
}
