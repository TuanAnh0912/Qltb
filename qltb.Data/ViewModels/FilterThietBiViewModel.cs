using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class FilterThietBiViewModel
    {
        public List<SelectedThietBiViewModel> SelectedTB { get; set; }
        public int? KhoPhongId { get; set; }
        public int? KhoiLopId { get; set; }
        public int? MonHocId { get; set; }
        public int? LoaiThietBiId { get; set; }
        public string searchString { get; set; }
    }
}
