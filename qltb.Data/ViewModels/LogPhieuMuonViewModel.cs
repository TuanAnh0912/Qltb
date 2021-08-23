using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class LogPhieuMuonViewModel
    {
        public int LogId { get; set; }
        public string MaPhieuMuon { get; set; }
        public string NguoiDung { get; set; }
        public string HanhDong { get; set; }
        public Nullable<System.DateTime> ThoiGian { get; set; }
        public string TieuDe { get; set; }
        public string TenNguoiThucHien { get; set; }
    }
}
