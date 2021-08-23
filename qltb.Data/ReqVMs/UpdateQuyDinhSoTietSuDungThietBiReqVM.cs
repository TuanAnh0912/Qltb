using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateQuyDinhSoTietSuDungThietBiReqVM
    {
        public Nullable<int> ChuongTrinhHocId { get; set; }
        public Nullable<int> MonHocId { get; set; }
        public Nullable<int> TietTNHKI { get; set; }
        public Nullable<int> TietTNHKII { get; set; }
        public Nullable<int> TietTHHKI { get; set; }
        public Nullable<int> TietTHHKII { get; set; }
        public Nullable<int> NamHocBatDau { get; set; }
        public Nullable<int> NamHocKetThuc { get; set; }
        public Nullable<int> QuyDinhSuDungSoTietId { get; set; }
        public List<int> LstLopId { get; set; }

    }
}
