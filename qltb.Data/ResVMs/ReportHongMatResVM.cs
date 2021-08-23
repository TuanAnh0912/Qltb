using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ReportHongMatResVM
    {
        public int? TongSoThietBiHong { get; set; }

        public int? TongSoThietBiMat { get; set; }

        public int? TongSoSuaChua { get; set; }

        public ListWithPaginationResVM ThietBis { get; set; }

        public int? NamHoc { get; set; }
    }
}
