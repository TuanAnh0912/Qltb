using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class AddChiTietPhieuDeNghiMuaSamReqVM
    {
        public int? ThietBiId { get; set; }

        public int? SoLuongDeNghi { get; set; }

        public long DonGia { get; set; }

        public int? KinhPhiId { get; set; }
    }
}
