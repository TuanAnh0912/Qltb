using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class ChucVuViewModel
    {
        public int ChucVuId { get; set; }
        public string MaChucVu { get; set; }
        public string TenChucVu { get; set; }
        public Nullable<System.Guid> PhongBanId { get; set; }
        public string TenPhongBan { get; set; }
        public string Mota { get; set; }
        public List<ChucNangViewModel> ChucNangs { get; set; }
        public List<NguoiDungViewModel> NguoiDungs { get; set; }
    }
}
