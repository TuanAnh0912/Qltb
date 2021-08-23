using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateNguonNganSachReqVM
    {
        public int? NguonNganSachId { get; set; }

        public string TenNguonNganSach { get; set; }

        public string MaNguonNganSach { get; set; }
    }
}
