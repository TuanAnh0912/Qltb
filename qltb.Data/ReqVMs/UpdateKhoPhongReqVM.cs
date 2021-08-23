using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
   public class UpdateKhoPhongReqVM
    {
        public int KhoPhongId { get; set; }
        public string MaKhoPhong { get; set; }
        public string TenKhoPhong { get; set; }
        public string DienTich { get; set; }
        public Nullable<int> NamSuDung { get; set; }
        public Nullable<int> LoaiKhoPhongId { get; set; }
        public Nullable<int> KieuSuDungKhoPhongId { get; set; }
        public Nullable<int> XepHangKhoPhongId { get; set; }
        public Nullable<int> SoNguoiQuanLy { get; set; }
        public Nullable<bool> LaPhongHocChucNang { get; set; }
    }
}
