using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ListWithPaginationResVM
    {
        public object Objects { get; set; }

        public Dictionary<string, string> CurrentQueryParamsDict { get; set; }

        public List<PaginationResVM> Paginations { get; set; }
    }
}
