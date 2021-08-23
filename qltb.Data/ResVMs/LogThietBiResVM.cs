using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class LogThietBiResVM
    {
        public int LogId { get; set; }
        public string ThoiGian { get; set; }
        public Nullable<int> ThietBiId { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string MaLoaiThayDoi { get; set; }
        public string TenLoaiThayDoi { get; set; }
        public string HoTen { get; set; }
        public Nullable<System.Guid> NguoiDungId { get; set; }
        public string TenThietBi { get; set; }
        public string SoPhieuId { get; set; }
        //public string NoiDung { get; set; }

    }
}
