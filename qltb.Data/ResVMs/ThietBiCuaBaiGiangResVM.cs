using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class ThietBiCuaBaiGiangResVM
    {
        public long? STT { get; set; }
        public int? ThietBiID { get; set; }
        public int? MonHocId { get; set; }
        public int? BaiHocSuDungThietBiId { get; set; }
        public string TenThietBi { get; set; }
        public string MaThietBi { get; set; }
        public string TenMonHoc { get; set; }
        public string TenLoaiThietBi { get; set; }
        public string TenDonViTinh { get; set; }
        public int? SoLuong { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public int? CheckThietBi { get; set; }

    }
}
