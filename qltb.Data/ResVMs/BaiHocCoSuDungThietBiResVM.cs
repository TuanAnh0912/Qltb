using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class BaiHocCoSuDungThietBiResVM
    {
        public long STT { get; set; }
        public int BaiHocSuDungThietBiId { get; set; }
        public string TenBaiHoc { get; set; }
        public string TenLop  { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> MonHocId { get; set; }
        public string TenMonHoc { get; set; }
        public Nullable<int> ChuongTrinhHocId { get; set; }
        public Nullable<int> LopId { get; set; }
        public string TenChuongTrinhHoc { get; set; }
        public string ThoiGian { get; set; }
        public Nullable<int> HocKyId { get; set; }
        public string TenHocKy { get; set; }
        public Nullable<int> SoTiet { get; set; }
        public Nullable<int> TietTheoPPTC { get; set; }
        public Nullable<int> LoaiBaiHocId { get; set; }
        public string TenLoaiBaiHoc { get; set; }

    }
}
