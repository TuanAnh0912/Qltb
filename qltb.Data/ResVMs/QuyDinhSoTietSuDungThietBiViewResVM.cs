using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class QuyDinhSoTietSuDungThietBiViewResVM
    {
        public long STT { get; set; }
        public string Lops { get; set; }
        public List<int> LstLopIds { get; set; }
        public int? QuyDinhId { get; set; }
        public QuyDinhSoTietSuDungThietBiResVM ThongTin { get; set; } 
    }
}
