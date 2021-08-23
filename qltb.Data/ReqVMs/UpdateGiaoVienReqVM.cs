using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateGiaoVienReqVM 
    {
        public int GiaoVienId { get; set; }
        public string SoCMND { get; set; }
        public string TenGiaoVien { get; set; }
        public Nullable<DateTime> NgaySinh { get; set; }
        public Nullable<DateTime> NgayVaoNganh { get; set; }
        public Nullable<bool> DangLamViec { get; set; }
        public Nullable<int> GioiTinhId { get; set; }
        public Nullable<int> ToBoMonId { get; set; }
        public string MaGiaoVien { get; set; }
        public Nullable<DateTime> NgayNghiViec { get; set; }

    }
}
