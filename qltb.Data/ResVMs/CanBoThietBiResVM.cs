using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
   public class CanBoThietBiResVM
    {
        public long STT { get; set; }
        public int CanBoThietBiId { get; set; }
        public Nullable<int> VaiTroCanBoId { get; set; }
        public string TenVaiTro { get; set; }
        public Nullable<int> GiaoVienId { get; set; }
        public string TenGiaoVien { get; set; }
        public string MaGiaoVien { get; set; }

        public string TrinhDoVanHoa { get; set; }
        public string ThoiGianBatDauQuanLy { get; set; }
        public string ThoiGianHetHanQuanLy { get; set; }
        public Nullable<bool> CoTrinhDoNghiepVu { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }

        public Nullable<bool> IsDelete { get; set; }
    }
}
