using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class GiaoVienResVM 
    {
        public long STT { get; set; }
        public int GiaoVienId { get; set; }
        public string TenGiaoVien { get; set; }
        public string SoCMND { get; set; }
        public string NgayVaoNganh { get; set; }
        public Nullable<bool> DangLamViec { get; set; }
        public Nullable<int> GioiTinhId { get; set; }
        public Nullable<int> ToBoMonId { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string TenToBoMon { get; set; }
        public string TenGioiTinh { get; set; }
        public string NgaySinh { get; set; } 
        public string MaGiaoVien { get; set; }
        public string NgayNghiViec { get; set; }

    }
}
