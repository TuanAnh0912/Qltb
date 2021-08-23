using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateBaiHocSuDungThietBiReqVM
    {
        public int BaiHocSuDungThietBiId { get; set; }
        public string TenBaiHoc { get; set; }
        public Nullable<int> MonHocId { get; set; }
        public Nullable<int> ChuongTrinhHocId { get; set; }
        public Nullable<int> LopId { get; set; }
        public Nullable<int> HocKyId { get; set; }
        public Nullable<int> SoTiet { get; set; }
        public Nullable<int> TietTheoPPTC { get; set; }
        public Nullable<int> LoaiBaiHocId { get; set; }
    }
}
