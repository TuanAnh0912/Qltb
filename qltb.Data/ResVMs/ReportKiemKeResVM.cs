using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ReportKiemKeResVM
    {
        public int? NamHoc { get; set; }

        public ListWithPaginationResVM ThietBis { get; set; }
    }
}
