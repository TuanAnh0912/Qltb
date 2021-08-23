using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qltb.Data.Providers;
using qltb.Data.ResVMs;
using qltb.WebApp.Infrastructure;

namespace qltb.WebApp.Controllers
{
    [RoutePrefix("trang-chu")]
    public class HomeController : Controller
    {
        ChucNangProvider _chucnang = new ChucNangProvider();
        ThongKeProvider _thongKeProvider = new ThongKeProvider();
        
        [CustomAuthenticationFilter]
        [CustomAuthorize("ALL")]
        [Route("tong-quan-he-thong")]
        public ActionResult Index()
        {
            ThongKeChungResVM thongKeChungResVM = new ThongKeChungResVM();
            try
            {
                var s = new NguoiDungProvider().CountNguoIDungByPhongBan("c270d415-6a3e-478f-a739-3b44b812e12e");

                thongKeChungResVM = _thongKeProvider.GetThongKeChung();
                return View(thongKeChungResVM);
            }
            catch(Exception ex)
            {
                return View(thongKeChungResVM);
            }
        }
    }
}