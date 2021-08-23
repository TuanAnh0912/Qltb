using qltb.Data.Providers;
using qltb.Data.ResVMs;
using qltb.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    [RoutePrefix("bao-cao")]
    public class ReportController : Controller
    {
        ReportProvider reportProvider = new ReportProvider(); 
        // GET: Report
        [Route("danh-muc-bao-cao")]
        public ActionResult Caterogy()
        {
            return View();
        }
        [Route("chi-tiet-bao-cao")]
        public ActionResult ReportDetail()
        {
            return View();
        }

        [Route("chi-tiet-bao-cao-hong-mat")]
        public ActionResult GetBaoCaoHongMat(int? namHoc,string tenThietBi, int? monHocId, int? khoPhongId, int? currentPage, int? pageSize)
        {
            if (!namHoc.HasValue)
            {
                namHoc = DateTime.Now.Year;
            }
            ReportHongMatResVM reportHongMatResVM = new ReportHongMatResVM();
            try
            {
                reportHongMatResVM = reportProvider.GetReportHongMat(namHoc, tenThietBi, monHocId, khoPhongId, currentPage, pageSize);
                reportHongMatResVM.NamHoc = namHoc.HasValue ? namHoc.Value : DateTime.Now.Year;
                return View(reportHongMatResVM);
            }
            catch(Exception ex)
            {
                return View(reportHongMatResVM);
            }
        }

        [Route("thong-tin-bao-cao-hong-mat")]
        public JsonResult DetailBaoCaoHongMat(int? namHoc, string tenThietBi, int? monHocId, int? khoPhongId, int? currentPage, int? pageSize)
        {
            ReportHongMatResVM reportHongMatResVM = new ReportHongMatResVM();
            try
            {
                reportHongMatResVM = reportProvider.GetReportHongMat(namHoc, tenThietBi, monHocId, khoPhongId, currentPage, pageSize);
                return Json(reportHongMatResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(reportHongMatResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("chi-tiet-bao-cao-kiem-ke")]
        public ActionResult GetBaoCaoKiemKe(int? namHoc, int? khoPhongId, int? thang, int? currentPage, int? pageSize)
        {
            ReportKiemKeResVM reportKiemKeResVM= new ReportKiemKeResVM();
            try
            {
                reportKiemKeResVM = reportProvider.GetReportKiemKe(namHoc, khoPhongId, thang, currentPage, pageSize);
                reportKiemKeResVM.NamHoc = namHoc.HasValue ? namHoc.Value : DateTime.Now.Year;
                return View(reportKiemKeResVM);
            }
            catch (Exception ex)
            {
                return View(reportKiemKeResVM);
            }
        }

        [Route("thong-tin-bao-cao-kiem-ke")]
        public JsonResult DetailBaoCaoKiemKe(int? namHoc, int? khoPhongId, int? thang, int? currentPage, int? pageSize)
        {
            ReportKiemKeResVM reportKiemKeResVM = new ReportKiemKeResVM();
            try
            {
                reportKiemKeResVM = reportProvider.GetReportKiemKe(namHoc, khoPhongId, thang, currentPage, pageSize);
                reportKiemKeResVM.NamHoc = namHoc.HasValue ? namHoc.Value : DateTime.Now.Year;
                return Json(reportKiemKeResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(reportKiemKeResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("chi-tiet-bao-cao-muon-tra-theo-giao-vien")]
        public ActionResult GetBaoCaoMuonTraTheoGiaoVien(int? namHoc, int? currentPage, int? pageSize)
        {
            ReportMuonTraTheoGiaoVienResVM resVM = new ReportMuonTraTheoGiaoVienResVM();
            try
            {
                //reportHongMatResVM = reportProvider.GetBaoCaoMuonTraTheoGiaoVien(namHoc, tenThietBi, monHocId, khoPhongId, currentPage, pageSize);
                resVM.NamHoc = namHoc.HasValue ? namHoc.Value : DateTime.Now.Year;
                return View(resVM);
            }
            catch (Exception ex)
            {
                return View(resVM);
            }
        }


    }
}