using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class NguoiDungViewModel
    {
        public System.Guid NguoiDungId { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Salt { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string TenChucVu { get; set; }
        public Nullable<System.Guid> DonViId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<int> GioiTinhId { get; set; }
        public List<ChucVuViewModel> ChucVus { get; set; }
        public List<ChucNangViewModel> ChucNang { get; set; }
    }
}
