using qltb.Data.Helpers.ExceptionHelpers;
using qltb.Data.Providers;
using qltb.Data.ReqVMs;
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
    [RoutePrefix("quan-ly-thiet-bi")]
    public class QuanLyThietBiController : Controller
    {

        private QuanLyThietBiProvider _quanLyThietBiProvider;
        public QuanLyThietBiController()
        {
            _quanLyThietBiProvider = new QuanLyThietBiProvider();
        }

        #region kho thiet bi
        [HttpGet]
        [Route("danh-sach-thiet-bi-trong-kho")]
        public JsonResult GetThietBiTrongKhos(string tenThietBi, int? monHocId)
        {
            List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
            try
            {
                chiTietThietBiResVMs = _quanLyThietBiProvider.GetThietBiTrongKhos(tenThietBi, monHocId);
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-thiet-bi-trong-kho")]
        public JsonResult GetThietBiTrongKho(int? thietBiId, int? khoPhongId)
        {
            ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
            try
            {
                chiTietThietBiResVM = _quanLyThietBiProvider.GetThietBiTrongKho(thietBiId, khoPhongId);
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-thiet-bi-hong-mat-trong-kho")]
        public JsonResult GetThietBiTrongKho(int? thietBiId, int? khoPhongId, string phieuGhiMatThietBiId, string phieuGhiHongThietBiId)
        {
            ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
            try
            {
                chiTietThietBiResVM = _quanLyThietBiProvider.GetThietBiTrongKho(thietBiId, khoPhongId, phieuGhiMatThietBiId, phieuGhiHongThietBiId);
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("danh-sach-thiet-bi-trong-kho-va-thiet-bi-hong-mat")]
        public JsonResult GetThietBiTrongKho(string tenThietBi, int? monHocId, int? loaiId)
        {
            List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
            try
            {
                chiTietThietBiResVMs = _quanLyThietBiProvider.GetThietBiTrongKhos(tenThietBi, monHocId, loaiId);
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region chi tiet thiet bi
        [HttpGet]
        [Route("danh-sach-thiet-bi")]
        public ActionResult GetChiTietThietBis(string tenThietbi, int? khoPhongId, int? monHocId, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _quanLyThietBiProvider.GetChiTietThietBis(tenThietbi, khoPhongId, monHocId, currentPage, pageSize);
                return View(result);
            }
            catch(Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<ChiTietThietBiResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-chon-thiet-bi")]
        public JsonResult GetChiTietThietBis(string tenThietBi, int? monHocId)
        {
            List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
            try
            {
                chiTietThietBiResVMs = _quanLyThietBiProvider.GetChiTietThietBis(tenThietBi, monHocId);
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("danh-sach-chon-thiet-bi-co-loai")]
        public JsonResult GetChiTietThietBis(string tenThietBi, int? monHocId, int? loaiId)
        {
            List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
            try
            {
                chiTietThietBiResVMs = _quanLyThietBiProvider.GetChiTietThietBis(tenThietBi, monHocId, loaiId);
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-chi-tiet-thiet-bi")]
        public JsonResult GetChiTietThietBi(string khoThietBiId)
        {
            ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
            try
            {
                chiTietThietBiResVM = _quanLyThietBiProvider.GetChiTietThietBi(khoThietBiId);
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-chi-tiet-thiet-bi-voi-phieu-ghi-hong-mat-thiet-bi")]
        public JsonResult GetChiTietThietBi(string chiTietThietBiId, string phieuGhiHongMatThietBiId, int? loaiId)
        {
            ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
            try
            {
                chiTietThietBiResVM = _quanLyThietBiProvider.GetChiTietThietBi(chiTietThietBiId, phieuGhiHongMatThietBiId, loaiId);
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [Route("them-thong-tin-chi-tiet-thiet-bi")]
        public JsonResult AddChiTietThietBi(AddChiTietThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.AddChiTietThietBi(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-chi-tiet-thiet-bi")]
        public JsonResult UpdateChiTietThietBi(UpdateChiTietThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.UpdateChiTietThietBi(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-chi-tiet-thiet-bi")]
        public JsonResult DeleteChiTietThietBi(DeleteChiTietThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.DeleteChiTietThietBi(reqVM);
                TempData["SUCCESS"] = "Xóa dữ liệu thành công";
                return Json("Xóa dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
        }
        #endregion

        #region phieu ghi tang thiet bi
        [HttpGet]
        [Route("danh-sach-phieu-ghi-tang-thiet-bi")]
        public ActionResult GetPhieuGhiTangThietBis(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _quanLyThietBiProvider.GetPhieuGhiTangThietBis(soPhieu, tuNgay, denNgay, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<PhieuGhiTangThietBiResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-phieu-ghi-tang-thiet-bi")]
        public ActionResult GetPhieuGhiTangThietBi(string phieuGhiTangThietBiId)
        {
            PhieuGhiTangThietBiResVM phieuGhiTangThietBiResVM = new PhieuGhiTangThietBiResVM();
            try
            {
                phieuGhiTangThietBiResVM = _quanLyThietBiProvider.GetPhieuGhiTangThietBi(phieuGhiTangThietBiId);
                return View(phieuGhiTangThietBiResVM);
            }
            catch (Exception ex)
            {
                return View(phieuGhiTangThietBiResVM);
            }
        }

        [HttpGet]
        [Route("chi-tiet-phieu-ghi-tang-thiet-bi")]
        public JsonResult DetailPhieuGhiTangThietBi(string phieuGhiTangThietBiId)
        {
            PhieuGhiTangThietBiResVM phieuGhiTangThietBiResVM = new PhieuGhiTangThietBiResVM();
            try
            {
                phieuGhiTangThietBiResVM = _quanLyThietBiProvider.GetPhieuGhiTangThietBi(phieuGhiTangThietBiId);
                return Json(phieuGhiTangThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(phieuGhiTangThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("them-thong-tin-phieu-ghi-tang-thiet-bi")]
        public ActionResult AddPhieuGhiTangThietBi()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [Route("them-thong-tin-phieu-ghi-tang-thiet-bi")]
        public JsonResult AddPhieuGhiTangThietBi(AddPhieuGhiTangThietBiReqVM reqVM)
        {
            try
            {
                reqVM.NguoiCapNhat = Session["UserId"].ToString();
                _quanLyThietBiProvider.AddPhieuGhiTangThietBi(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-phieu-ghi-tang-thiet-bi")]
        public JsonResult UpdatePhieuGhiTangThietBi(UpdatePhieuGhiTangThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.UpdatePhieuGhiTangThietBi(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-phieu-ghi-tang-thiet-bi")]
        public JsonResult DeletePhieuGhiTangThietBi(DeletePhieuGhiTangThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.DeletePhieuGhiTangThietBi(reqVM);
                TempData["SUCCESS"] = "Xóa dữ liệu thành công";
                return Json("Xóa dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
        }


        #endregion

        #region phieu ghi hong thiet bi
        [HttpGet]
        [Route("danh-sach-phieu-ghi-hong-thiet-bi")]
        public ActionResult GetPhieuGhiHongThietBis(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _quanLyThietBiProvider.GetPhieuGhiHongThietBis(soPhieu, tuNgay, denNgay, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<PhieuGhiHongThietBiResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("them-thong-tin-phieu-ghi-hong-thiet-bi")]
        public ActionResult AddPhieuGhiHongThietBi()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        [Route("thong-tin-phieu-ghi-hong-thiet-bi")]
        public ActionResult GetPhieuGhiHongThietBi(string phieuGhiHongThietBiId)
        {
            PhieuGhiHongThietBiResVM phieuGhiHongThietBiResVM = new PhieuGhiHongThietBiResVM();
            try
            {
                phieuGhiHongThietBiResVM = _quanLyThietBiProvider.GetPhieuGhiHongThietBi(phieuGhiHongThietBiId);
                return View(phieuGhiHongThietBiResVM);
            }
            catch (Exception ex)
            {
                return View(phieuGhiHongThietBiResVM);
            }
        }

        [HttpGet]
        [Route("chi-tiet-phieu-ghi-hong-thiet-bi")]
        public JsonResult DetailPhieuGhiHongThietBi(string phieuGhiHongThietBiId)
        {
            PhieuGhiHongThietBiResVM DetailPhieuGhiHongThietBi = new PhieuGhiHongThietBiResVM();
            try
            {
                DetailPhieuGhiHongThietBi = _quanLyThietBiProvider.GetPhieuGhiHongThietBi(phieuGhiHongThietBiId);
                return Json(DetailPhieuGhiHongThietBi, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DetailPhieuGhiHongThietBi, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-phieu-ghi-hong-thiet-bi")]
        public JsonResult AddPhieuGhiHongThietBi(AddPhieuGhiHongThietBiReqVM reqVM)
        {
            try
            {
                reqVM.NguoiCapNhat = Session["UserId"].ToString();
                _quanLyThietBiProvider.AddPhieuGhiHongThietBi(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-phieu-ghi-hong-thiet-bi")]
        public JsonResult UpdatePhieuGhiHongThietBi(UpdatePhieuGhiHongThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.UpdatePhieuGhiHongThietBi(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-phieu-ghi-hong-thiet-bi")]
        public JsonResult DeletePhieuGhiHongThietBi(DeletePhieuGhiHongThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.DeletePhieuGhiHongThietBi(reqVM);
                TempData["SUCCESS"] = "Xóa dữ liệu thành công";
                return Json("Xóa dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
        }

        #endregion

        #region phieu ghi mat thiet bi
        [HttpGet]
        [Route("danh-sach-phieu-ghi-mat-thiet-bi")]
        public ActionResult GetPhieuGhiMatThietBis(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _quanLyThietBiProvider.GetPhieuGhiMatThietBis(soPhieu, tuNgay, denNgay, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<PhieuGhiMatThietBiResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("them-thong-tin-phieu-ghi-mat-thiet-bi")]
        public ActionResult AddPhieuGhiMatThietBi()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        [Route("thong-tin-phieu-ghi-mat-thiet-bi")]
        public ActionResult GetPhieuGhiMatThietBi(string phieuGhiMatThietBiId)
        {
            PhieuGhiMatThietBiResVM phieuGhiMatThietBiResVM = new PhieuGhiMatThietBiResVM();
            try
            {
                phieuGhiMatThietBiResVM = _quanLyThietBiProvider.GetPhieuGhiMatThietBi(phieuGhiMatThietBiId);
                return View(phieuGhiMatThietBiResVM);
            }
            catch (Exception ex)
            {
                return View(phieuGhiMatThietBiResVM);
            }
        }

        [HttpGet]
        [Route("chi-tiet-phieu-ghi-mat-thiet-bi")]
        public JsonResult DetailPhieuGhiMatThietBi(string phieuGhiMatThietBiId)
        {
            PhieuGhiMatThietBiResVM phieuGhiMatThietBiResVM = new PhieuGhiMatThietBiResVM();
            try
            {
                phieuGhiMatThietBiResVM = _quanLyThietBiProvider.GetPhieuGhiMatThietBi(phieuGhiMatThietBiId);
                return Json(phieuGhiMatThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(phieuGhiMatThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-phieu-ghi-mat-thiet-bi")]
        public JsonResult AddPhieuGhiMatThietBi(AddPhieuGhiMatThietBiReqVM reqVM)
        {
            try
            {
                reqVM.NguoiCapNhat = Session["UserId"].ToString();
                _quanLyThietBiProvider.AddPhieuGhiMatThietBi(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-phieu-ghi-mat-thiet-bi")]
        public JsonResult UpdatePhieuGhiMatThietBi(UpdatePhieuGhiMatThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.UpdatePhieuGhiMatThietBi(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật dữ liệu thành công");
            }
            catch(WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-phieu-ghi-mat-thiet-bi")]
        public JsonResult DeletePhieuGhiMatThietBi(DeletePhieuGhiMatThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.DeletePhieuGhiMatThietBi(reqVM);
                TempData["SUCCESS"] = "Xóa dữ liệu thành công";
                return Json("Xóa dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
        }

        #endregion

        #region phieu ghi giam thiet bi
        [HttpGet]
        [Route("danh-sach-phieu-ghi-giam-thiet-bi")]
        public ActionResult GetPhieuGhiGiamThietBis(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _quanLyThietBiProvider.GetPhieuGhiGiamThietBis(soPhieu, tuNgay, denNgay, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<PhieuGhiGiamThietBiResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("them-thong-tin-phieu-ghi-giam-thiet-bi")]
        public ActionResult AddPhieuGhiGiamThietBi()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [Route("them-thong-tin-phieu-ghi-giam-thiet-bi")]
        public JsonResult AddPhieuGhiGiamThietBi(AddPhieuGhiGiamThietBiReqVM reqVM)
        {
            try
            {
                reqVM.NguoiCapNhat = Session["UserId"].ToString();
                _quanLyThietBiProvider.AddPhieuGhiGiamThietBi(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpGet]
        [Route("thong-tin-phieu-ghi-giam-thiet-bi")]
        public ActionResult GetPhieuGhiGiamThietBi(string phieuGhiGiamThietBiId)
        {
            PhieuGhiGiamThietBiResVM phieuGhiGiamThietBiResVM = new PhieuGhiGiamThietBiResVM();
            try
            {
                phieuGhiGiamThietBiResVM = _quanLyThietBiProvider.GetPhieuGhiGiamThietBi(phieuGhiGiamThietBiId);
                return View(phieuGhiGiamThietBiResVM);
            }
            catch (Exception ex)
            {
                return View(phieuGhiGiamThietBiResVM);
            }
        }

        [HttpGet]
        [Route("chi-tiet-phieu-ghi-giam-thiet-bi")]
        public JsonResult DetailPhieuGhiGiamThietBi(string phieuGhiGiamThietBiId)
        {
            PhieuGhiGiamThietBiResVM phieuGhiGiamThietBiResVM = new PhieuGhiGiamThietBiResVM();
            try
            {
                phieuGhiGiamThietBiResVM = _quanLyThietBiProvider.GetPhieuGhiGiamThietBi(phieuGhiGiamThietBiId);
                return Json(phieuGhiGiamThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(phieuGhiGiamThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-phieu-ghi-giam-thiet-bi")]
        public JsonResult UpdatePhieuGhiGiamThietBi(UpdatePhieuGhiGiamThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.UpdatePhieuGhiGiamThietBi(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật dữ liệu thành công");
            }
            catch(WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-phieu-ghi-giam-thiet-bi")]
        public JsonResult DeletePhieuGhiGiamThietBi(DeletePhieuGhiGiamThietBiReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.DeletePhieuGhiGiamThietBi(reqVM);
                TempData["SUCCESS"] = "Xóa dữ liệu thành công";
                return Json("Xóa dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
        }

        #endregion

        #region kiem ke
        [HttpGet]
        [Route("danh-sach-phieu-kiem-ke")]
        public ActionResult GetPhieuKiemKes(string soPhieu, int? khoPhongId, DateTime? ngayLapTuNgay, DateTime? ngayLapDenNgay, DateTime? ngayKiemKeTuNgay, DateTime? ngayKiemKeDenNgay, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _quanLyThietBiProvider.GetPhieuKiemKes(soPhieu, khoPhongId, ngayLapTuNgay, ngayLapDenNgay, ngayKiemKeTuNgay, ngayKiemKeDenNgay, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<PhieuKiemKeResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-thiet-bi-de-kiem-ke")]
        public JsonResult GetThietBiDeKiemKes(string tenThietBi, int? khoPhongId, int? monHocId, string phieuKiemKeId)
        {
            List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
            try
            {
                chiTietThietBiResVMs = _quanLyThietBiProvider.GetThietBiDeKiemKes(tenThietBi, khoPhongId, monHocId, phieuKiemKeId);
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-thiet-bi-de-kiem-ke")]
        public JsonResult GetThietBiDeKiemKe(int? thietBiId, int? khoPhongId, string phieuKiemKeId)
        {
            ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
            try
            {
                chiTietThietBiResVM = _quanLyThietBiProvider.GetThietBiDeKiemKe(thietBiId, khoPhongId, phieuKiemKeId);
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("them-thong-tin-phieu-kiem-ke")]
        public ActionResult AddPhieuKiemKe()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [Route("them-thong-tin-phieu-kiem-ke")]
        public JsonResult AddPhieuKiemKe(AddPhieuKiemKeReqVM reqVM)
        {
            try
            {
                reqVM.NguoiCapNhat = Session["UserId"].ToString();
                _quanLyThietBiProvider.AddPhieuKiemKe(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpGet]
        [Route("thong-tin-phieu-kiem-ke")]
        public ActionResult GetPhieuKiemKe(string phieuKiemKeId)
        {
            PhieuKiemKeResVM phieuKiemKeResVM = new PhieuKiemKeResVM();
            try
            {
                phieuKiemKeResVM = _quanLyThietBiProvider.GetPhieuKiemKe(phieuKiemKeId);
                return View(phieuKiemKeResVM);
            }
            catch (Exception ex)
            {
                return View(phieuKiemKeResVM);
            }
        }

        [HttpGet]
        [Route("chi-tiet-phieu-kiem-ke")]
        public JsonResult DetailPhieuKiemKe(string phieuKiemKeId)
        {
            PhieuKiemKeResVM phieuKiemKeResVM = new PhieuKiemKeResVM();
            try
            {
                phieuKiemKeResVM = _quanLyThietBiProvider.GetPhieuKiemKe(phieuKiemKeId);
                return Json(phieuKiemKeResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(phieuKiemKeResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-phieu-kiem-ke")]
        public JsonResult UpdatePhieuKiemKe(UpdatePhieuKiemKeReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.UpdatePhieuKiemKe(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-phieu-kiem-ke")]
        public JsonResult DeletePhieuKiemKe(DeletePhieuKiemKeReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.DeletePhieuKiemKe(reqVM);
                TempData["SUCCESS"] = "Xóa dữ liệu thành công";
                return Json("Xóa dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
        }

        #endregion

        #region phieu sua chua
        [HttpGet]
        [Route("danh-sach-phieu-sua-chua")]
        public ActionResult GetPhieuSuaChuas(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _quanLyThietBiProvider.GetPhieuSuaChuas(soPhieu, tuNgay, denNgay, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<PhieuSuaChuaResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-thiet-bi-de-sua-chua")]
        public JsonResult GetThietBiDeSuaChuas(string tenThietBi, int? monHocId)
        {
            List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
            try
            {
                chiTietThietBiResVMs = _quanLyThietBiProvider.GetThietBiDeSuaChuas(tenThietBi, monHocId);
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet) ;
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(chiTietThietBiResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-thiet-bi-de-sua-chua")]
        public JsonResult GetThietBiDeSuaChua(int? thietBiId, int? khoPhongId, string phieuGhiHongThietBiId)
        {
            ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
            try
            {
                chiTietThietBiResVM = _quanLyThietBiProvider.GetThietBiDeSuaChua(thietBiId, khoPhongId, phieuGhiHongThietBiId);
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("them-thong-tin-phieu-sua-chua")]
        public ActionResult AddPhieuSuaChua()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [Route("them-thong-tin-phieu-sua-chua")]
        public JsonResult AddPhieuSuaChua(AddPhieuSuaChuaReqVM reqVM)
        {
            try
            {
                reqVM.NguoiCapNhat = Session["UserId"].ToString();
                _quanLyThietBiProvider.AddPhieuSuaChua(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpGet]
        [Route("thong-tin-phieu-sua-chua")]
        public ActionResult GetPhieuSuaChua(string phieuSuaChuaId)
        {
            PhieuSuaChuaResVM phieuSuaChuaResVM = new PhieuSuaChuaResVM();
            try
            {
                phieuSuaChuaResVM = _quanLyThietBiProvider.GetPhieuSuaChua(phieuSuaChuaId);
                return View(phieuSuaChuaResVM);
            }
            catch (Exception ex)
            {
                return View(phieuSuaChuaResVM);
            }
        }

        [HttpGet]
        [Route("chi-tiet-phieu-sua-chua")]
        public JsonResult DetailPhieuSuaChua(string phieuSuaChuaId)
        {
            PhieuSuaChuaResVM phieuSuaChuaResVM = new PhieuSuaChuaResVM();
            try
            {
                phieuSuaChuaResVM = _quanLyThietBiProvider.GetPhieuSuaChua(phieuSuaChuaId);
                return Json(phieuSuaChuaResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(phieuSuaChuaResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-phieu-sua-chua")]
        public JsonResult UpdatePhieuSuaChua(UpdatePhieuSuaChuaReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.UpdatePhieuSuaChua(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-phieu-sua-chua")]
        public JsonResult DeletePhieuSuaChua(DeletePhieuSuaChuaReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.DeletePhieuSuaChua(reqVM);
                TempData["SUCCESS"] = "Xóa dữ liệu thành công";
                return Json("Xóa dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }
        #endregion

        #region phieu de nghi mua sam
        [HttpGet]
        [Route("danh-sach-phieu-de-nghi-mua-sam")]
        public ActionResult GetPhieuDeNghiMuaSams(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? daXuLy, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _quanLyThietBiProvider.GetPhieuDeNghiMuaSams(soPhieu, tuNgay, denNgay, daXuLy, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<PhieuDeNghiMuaSamResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-thiet-bi-ton-trong-kho")]
        public JsonResult GetThietBiTonTrongKhos(string tenThietBi, int? monHocId)
        {
            List<ChiTietThietBiResVM> chiTietThietBiRes = new List<ChiTietThietBiResVM>();
            try
            {
                chiTietThietBiRes = _quanLyThietBiProvider.GetThietBiTonTrongKhos(tenThietBi, monHocId);
                return Json(chiTietThietBiRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiRes, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-thiet-bi-ton-trong-kho")]
        public JsonResult GetThietBiTonTrongKho(int? thietBiId)
        {
            ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
            try
            {
                chiTietThietBiResVM = _quanLyThietBiProvider.GetThietBiTonTrongKho(thietBiId);
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chiTietThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("them-thong-tin-phieu-de-nghi-mua-sam")]
        public ActionResult AddPhieuDeNghiMuaSam()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [Route("them-thong-tin-phieu-de-nghi-mua-sam")]
        public JsonResult AddPhieuDeNghiMuaSam(AddPhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                reqVM.NguoiCapNhat = Session["UserId"].ToString();
                _quanLyThietBiProvider.AddPhieuDeNghiMuaSam(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpGet]
        [Route("thong-tin-phieu-de-nghi-mua-sam")]
        public ActionResult GetPhieuDeNghiMuaSam(string phieuDeNghiMuaSamId)
        {
            PhieuDeNghiMuaSamResVM phieuDeNghiMuaSamResVM = new PhieuDeNghiMuaSamResVM();
            try
            {
                phieuDeNghiMuaSamResVM = _quanLyThietBiProvider.GetPhieuDeNghiMuaSam(phieuDeNghiMuaSamId);
                return View(phieuDeNghiMuaSamResVM);
            }
            catch (Exception ex)
            {
                return View(phieuDeNghiMuaSamResVM);
            }
        }

        [HttpGet]
        [Route("chi-tiet-phieu-de-nghi-mua-sam")]
        public JsonResult DetailPhieuDeNghiMuaSam(string phieuDeNghiMuaSamId)
        {
            PhieuDeNghiMuaSamResVM phieuDeNghiMuaSamResVM = new PhieuDeNghiMuaSamResVM();
            try
            {
                phieuDeNghiMuaSamResVM = _quanLyThietBiProvider.GetPhieuDeNghiMuaSam(phieuDeNghiMuaSamId);
                return Json(phieuDeNghiMuaSamResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(phieuDeNghiMuaSamResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-phieu-de-nghi-mua-sam")]
        public JsonResult UpdatePhieuDeNghiMuaSam(UpdatePhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.UpdatePhieuDeNghiMuaSam(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật liệu thành công");
            }
            catch(WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("chot-phieu-de-nghi-mua-sam")]
        public JsonResult ChotPhieuDeNghiMuaSam(ChotPhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.ChotPhieuDeNghiMuaSam(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xu-ly-phieu-de-nghi-mua-sam")]
        public JsonResult XuLyDeNghiMuaSam(XuLyPhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.XuLyPhieuDeNghiMuaSam(reqVM);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-phieu-de-nghi-mua-sam")]
        public JsonResult DeleteDeNghiMuaSam(DeletePhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                _quanLyThietBiProvider.DeletePhieuDeNghiMuaSam(reqVM);
                TempData["SUCCESS"] = "Xóa dữ liệu thành công";
                return Json("Xóa liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Có lỗi xảy ra, vui lòng liên hệ nhà phát triển";
                return Json(ex.Message);
            }
        }

        #endregion

        #region thong ke bao cao
        public ActionResult ChiTietThietBi()
        {
            return View();
        }
        #endregion
    }
}