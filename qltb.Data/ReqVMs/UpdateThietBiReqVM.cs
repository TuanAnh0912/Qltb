using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
   public class UpdateThietBiReqVM
    {
        public int ThietBiId { get; set; }
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public Nullable<int> LoaiThietBiId { get; set; }
        public Nullable<int> DonViTinhId { get; set; }
        public Nullable<bool> QuanLyTieuHao { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> MonHocId { get; set; }
        public Nullable<int> SoLuongToiThieu { get; set; }
    }
}
