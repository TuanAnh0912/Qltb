using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class PhongBanViewModel
    {
        public System.Guid PhongBanId { get; set; }
        public string MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public string DienThoai { get; set; }
        public string MoTa { get; set; }
        public int SoLuongNguoiDung { get; set; }
        public List<ChucVuViewModel> ChucVus { get; set; }
        public List<NguoiDungViewModel> NguoiDungs { get; set; }
    }
}
