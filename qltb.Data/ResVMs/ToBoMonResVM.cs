using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ToBoMonResVM
    {
        public long STT { get; set; }
        public int ToBoMonId { get; set; }
        public string MaBoMon { get; set; }
        public string TenToBoMon { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.Guid> PhongBanId { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string TenPhongBan { get; set; }
    }
}
