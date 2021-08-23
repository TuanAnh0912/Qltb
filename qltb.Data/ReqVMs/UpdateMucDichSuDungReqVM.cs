using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateMucDichSuDungReqVM
    {
        public int? MucDichSuDungId { get; set; }

        public string TenMucDichSuDung { get; set; }

        public string MaMucDichSuDung { get; set; }
    }
}
