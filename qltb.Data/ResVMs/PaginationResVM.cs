using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class PaginationResVM
    {
        public string Text { get; set; }

        public int? PageIndex { get; set; }

        public string Url { get; set; }

        public bool? IsActived { get; set; }

        public bool? IsDisabled { get; set; }
    }
}
