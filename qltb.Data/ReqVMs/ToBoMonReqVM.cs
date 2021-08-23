using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class ToBoMonReqVM
    {
        public int ToBoMonId { get; set; }
        public string MaBoMon { get; set; }
        public string TenToBoMon { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.Guid> PhongBanId { get; set; }
    }
}
