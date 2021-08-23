using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qltb.Data.ResVMs;

namespace qltb.Data.ViewModels
{
    public class ChiTietPhongViewModel
    {

        public List<ThietBiResVM> ThietBis { get; set; }

        

        public List<KhoPhongResVM> KhoPhongs { get; set; }
        public List<LoaiThietBiResVM> LoaiThietBis { get; set; }
        public List<MonHocResVM> MonHocs { get; set; }


        /*        public List<LogPhieuMuonViewModel> Logs { get; set; }
                public List<GiaoVienResVM> GiaoViens { get; set; }
                public List<ChuongTrinhHocResVM> ChuongTrinhHocs { get; set; }
                public List<MonHocResVM> MonHocs { get; set; }
                public List<KhoiLopResVM> KhoiLops { get; set; }
                public List<LopResVM> Lops { get; set; }
                public List<BaiHocCoSuDungThietBiResVM> BaiHocCoSuDungThietBis { get; set; }
                public List<MucDichSuDungResVM> MucDichSuDungs { get; set; }
                public List<int> BuoiTrongNgay { get; set; }
                public List<TietHocResVM> TietHocs { get; set; }
                public List<int> TietHocTrongPhieu { get; set; }
                public List<TietHocTrongPhieuMuonViewModel> TietHocDaChon { get; set; }

                public List<LoaiThietBiResVM> LoaiThietBis { get; set; }
                public List<SelectThietBiViewmodel> ThietBis { get; set; }
                public List<LoaiKhoPhongResVM> LoaiPhongs { get; set; }
                public PhongViewModel PhongMuon { get; set; }*/

    }
}
