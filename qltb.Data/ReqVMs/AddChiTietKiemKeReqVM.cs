using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class AddChiTietKiemKeReqVM
    {
        public int? ThietBiId { get; set; }

        public int? SoLuongMat { get; set; }

        public int? SoLuongHong { get; set; }

        public int? SoLuongConDungDuoc { get; set; }

        public string GhiChu { get; set; }
    }
}
