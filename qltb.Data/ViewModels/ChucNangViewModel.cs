using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class ChucNangViewModel
    {
        public int ChucNangId { get; set; }
        public string MaChucNang { get; set; }
        public string TenChucNang { get; set; }
        public Nullable<int> KhoaChaId { get; set; }
        public string DuongDan { get; set; }
        public string Icon { get; set; }
        public Nullable<int> NhomChucNangId { get; set; }
        public string TenNhomChucNang { get; set; }
    }
}
