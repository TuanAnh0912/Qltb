using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateDonViTinhReqVM
    {
        public int? DonViTinhId { get; set; }

        public string TenDonViTinh { get; set; }

        public string MaDonViTinh { get; set; }
    }
}
