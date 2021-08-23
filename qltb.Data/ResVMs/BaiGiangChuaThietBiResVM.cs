using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class BaiGiangChuaThietBiResVM
    {
        public int? MonHocId { get; set; }
        public string TenMonHoc { get; set; }
        public List<ThietBiCuaBaiGiangResVM> lstThietBi { get; set; }

    }
}
