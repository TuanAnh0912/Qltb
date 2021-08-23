using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateNguonKinhPhiCapTrenReqVM
    {
        public int? NguonKinhPhiCapTrenId { get; set; }

        public string TenNguonKinhPhiCapTren { get; set; }

        public string MaNguonKinhPhiCapTren { get; set; }
    }
}
