using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
   public class UpdateCanBoThietBiReqVm
    {
        public int CanBoThietBiId { get; set; }
        public Nullable<int> VaiTroCanBoId { get; set; }
        public Nullable<int> GiaoVienId { get; set; }
        public string TrinhDoVanHoa { get; set; }
        public Nullable<DateTime> ThoiGianBatDauQuanLy { get; set; }
        public Nullable<DateTime> ThoiGianHetHanQuanLy { get; set; }
        public Nullable<bool> CoTrinhDoNghiepVu { get; set; }
        public string GhiChu { get; set; }
    }
}
