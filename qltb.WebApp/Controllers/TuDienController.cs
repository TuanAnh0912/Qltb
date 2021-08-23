using qltb.Data.Helpers.ExceptionHelpers;
using qltb.Data.Providers;
using qltb.Data.ReqVMs;
using qltb.Data.ResVMs;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qltb.WebApp.Controllers
{
    [RoutePrefix("tu-dien")]
    public class TuDienController : Controller
    {
        private TuDienProvider _tuDienProvider;
        public TuDienController()
        {
            _tuDienProvider = new TuDienProvider();
        }

        #region dghc
        [HttpPost]
        [Route("cap-nhat-dghc")]
        public JsonResult UpdateDGHC()
        {

            try
            {
                //Load xlsx file
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(Server.MapPath("~/App_Data/DuLieuTuDien/DGHC.xls"));

                // tinh
                Worksheet sheet = workbook.Worksheets[0];
                CellRange range = sheet.Range[1, 1, 64, 9];
                DataTable dt = sheet.ExportDataTable(range, true, true);
                List<AddTinhReqVM> tinhReqVMs = new List<AddTinhReqVM>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    AddTinhReqVM tinhReqVM = new AddTinhReqVM();
                    tinhReqVM.TinhId = Convert.ToString(dataRow[0]);
                    tinhReqVM.TenTinh = Convert.ToString(dataRow[1]);
                    tinhReqVM.MaLienKet = Convert.ToString(dataRow[8]);
                    tinhReqVMs.Add(tinhReqVM);
                }

                // huyen
                Worksheet sheet_1 = workbook.Worksheets[1];
                CellRange range_1 = sheet_1.Range[1, 1, 710, 10];
                DataTable dt_1 = sheet_1.ExportDataTable(range_1, true, true);
                List<AddHuyenReqVM> huyenReqVMs = new List<AddHuyenReqVM>();
                foreach (DataRow dataRow in dt_1.Rows)
                {
                    AddTinhReqVM tinhReqVM = tinhReqVMs.Where(x => x.MaLienKet == Convert.ToString(dataRow[9])).FirstOrDefault();
                    AddHuyenReqVM huyenReqVM = new AddHuyenReqVM();
                    huyenReqVM.HuyenId = tinhReqVM.TinhId + Convert.ToString(dataRow[0]);
                    huyenReqVM.TenHuyen = Convert.ToString(dataRow[1]);
                    huyenReqVM.MaLienKet = Convert.ToString(dataRow[8]);
                    huyenReqVMs.Add(huyenReqVM);
                }

                // Xa
                Worksheet sheet_2 = workbook.Worksheets[2];
                CellRange range_2 = sheet_2.Range[1, 1, 11164, 10];
                DataTable dt_2 = sheet_2.ExportDataTable(range_2, true, true);
                List<AddXaReqVM> xaReqVMs = new List<AddXaReqVM>();
                foreach (DataRow dataRow in dt_2.Rows)
                {
                    AddHuyenReqVM huyenReqVM = huyenReqVMs.Where(x => x.MaLienKet == Convert.ToString(dataRow[9])).FirstOrDefault();
                    AddXaReqVM xaReqVM = new AddXaReqVM();
                    xaReqVM.XaId = huyenReqVM.HuyenId + Convert.ToString(dataRow[0]);
                    xaReqVM.TenXa = Convert.ToString(dataRow[1]);
                    xaReqVMs.Add(xaReqVM);
                }
                _tuDienProvider.NhapDGHC(tinhReqVMs, huyenReqVMs, xaReqVMs);
                TempData["SUCCESS"] = "Cập nhật dữ liệu thành công";
                return Json("Cập nhật dữ liệu thành công");
            }
            catch (Exception ex)
            {
                TempData["WARNING"] = "Cập nhật dữ liệu thành công";
                return Json(ex.Message);

            }
        }

        [HttpGet]
        [Route("danh-sach-tinh")]
        public JsonResult GetTinhs(string tenTinh)
        {
            List<TinhResVM> tinhResVMs = new List<TinhResVM>();
            try
            {
                tinhResVMs = _tuDienProvider.GetTinhs(tenTinh);
                return Json(tinhResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(tinhResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("tim-kiem-tinh")]
        public ActionResult GetTinhs(string tenTinh, int? currentPage, int? pageSize)
        {
            try
            {

                var result = _tuDienProvider.GetTinhs(tenTinh, currentPage, pageSize);

                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Objects = new List<TinhResVM>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-tinh")]
        public JsonResult GetTinh(string tinhId)
        {
            TinhResVM tinhResVM = new TinhResVM();
            try
            {
                tinhResVM = _tuDienProvider.GetTinh(tinhId);
                return Json(tinhResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(tinhResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("tim-kiem-huyen")]
        public ActionResult GetHuyens(string tenHuyen, string tinhId, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetHuyens(tenHuyen, tinhId, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<HuyenResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-huyen")]
        public JsonResult GetHuyens(string tenHuyen, string tinhId)
        {
            List<HuyenResVM> huyenResVMs = new List<HuyenResVM>();
            try
            {
                huyenResVMs = _tuDienProvider.GetHuyens(tenHuyen, tinhId);
                return Json(huyenResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(huyenResVMs, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        [Route("thong-tin-huyen")]
        public JsonResult GetHuyen(string huyenId)
        {
            HuyenResVM huyenResVM = new HuyenResVM();
            try
            {
                huyenResVM = _tuDienProvider.GetHuyen(huyenId);
                return Json(huyenResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(huyenResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("tim-kiem-xa")]
        public ActionResult GetXas(string tenXa, string tinhId, string huyenId, int? currentPage, int? pageSize)
        {
            try
            {

                var result = _tuDienProvider.GetXas(tenXa, tinhId, huyenId, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<XaResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }
        #endregion

        #region loai thiet bi
        [HttpGet]
        [Route("loai-thiet-bi")]
        public ActionResult GetLoaiThietBis(string tenLoaiThietBi, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetLoaiThietBis(tenLoaiThietBi, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<LoaiThietBiResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-loai-thiet-bi")]
        public JsonResult GetLoaiThietBi(int? loaiThietBiId)
        {
            LoaiThietBiResVM loaiThietBiResVM = new LoaiThietBiResVM();
            try
            {
                loaiThietBiResVM = _tuDienProvider.GetLoaiThietBi(loaiThietBiId);
                return Json(loaiThietBiResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(loaiThietBiResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-loai-thiet-bi")]
        public JsonResult UpdateLoaiThietBi(UpdateLoaiThietBiReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateLoaiThietBi(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-loai-thiet-bi")]
        public JsonResult DeleteLoaiThietBi(DeleteLoaiThietBiReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteLoaiThietBi(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-loai-thiet-bi")]
        public JsonResult AddLoaiThietBi(AddLoaiThietBiReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddLoaiThietBi(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region loai bai hoc
        [HttpGet]
        [Route("loai-bai-hoc")]
        public ActionResult GetLoaiBaiHocs(string tenLoaiBaiHoc, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetLoaiBaiHocs(tenLoaiBaiHoc, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<LoaiBaiHocResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-loai-bai-hoc")]
        public JsonResult GetLoaiBaiHoc(int? loaiBaiHocId)
        {
            LoaiBaiHocResVM loaiBaiHocResVM = new LoaiBaiHocResVM();
            try
            {
                loaiBaiHocResVM = _tuDienProvider.GetLoaiBaiHoc(loaiBaiHocId);
                return Json(loaiBaiHocResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(loaiBaiHocResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-loai-bai-hoc")]
        public JsonResult UpdateLoaiBaiHoc(UpdateLoaiBaiHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateLoaiBaiHoc(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-loai-bai-hoc")]
        public JsonResult DeleteLoaiBaiHoc(DeleteLoaiBaiHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteLoaiBaiHoc(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-loai-bai-hoc")]
        public JsonResult AddLoaiBaiHoc(AddLoaiBaiHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddLoaiBaiHoc(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region chuong trinh hoc
        [HttpGet]
        [Route("chuong-trinh-hoc")]
        public ActionResult GetChuongTrinhHocs(string tenChuongTrinhHoc, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetChuongTrinhHocs(tenChuongTrinhHoc, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<ChuongTrinhHocResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-chuong-trinh-hoc")]
        public JsonResult GetChuongTrinhHoc(int? chuongTrinhHocId)
        {
            ChuongTrinhHocResVM chuongTrinhHocResVM = new ChuongTrinhHocResVM();
            try
            {
                chuongTrinhHocResVM = _tuDienProvider.GetChuongTrinhHoc(chuongTrinhHocId);
                return Json(chuongTrinhHocResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(chuongTrinhHocResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-chuong-trinh-hoc")]
        public JsonResult UpdateChuongTrinhHoc(UpdateChuongTrinhHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateChuongTrinhHoc(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-chuong-trinh-hoc")]
        public JsonResult DeleteChuongTrinhHoc(DeleteChuongTrinhHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteChuongTrinhHoc(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-chuong-trinh-hoc")]
        public JsonResult AddChuongTrinhHoc(AddChuongTrinhHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddChuongTrinhHoc(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region loai don vi tinh
        [HttpGet]
        [Route("don-vi-tinh")]
        public ActionResult GetDonViTinhs(string tenDonViTinh, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetDonViTinhs(tenDonViTinh, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<DonViTinhResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-don-vi-tinh")]
        public JsonResult GetDonViTinh(int? donViTinhId)
        {
            DonViTinhResVM donViTinhResVM = new DonViTinhResVM();
            try
            {
                donViTinhResVM = _tuDienProvider.GetDonViTinh(donViTinhId);
                return Json(donViTinhResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(donViTinhResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-don-vi-tinh")]
        public JsonResult UpdateDonViTinh(UpdateDonViTinhReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateDonViTinh(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-don-vi-tinh")]
        public JsonResult DeleteDonViTinh(DeleteDonViTinhReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteDonViTinh(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-don-vi-tinh")]
        public JsonResult AddDonViTinh(AddDonViTinhResVM reqVM)
        {
            try
            {
                _tuDienProvider.AddDonViTinh(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region loai kho phong
        [HttpGet]
        [Route("loai-kho-phong")]
        public ActionResult GetLoaiKhoPhongs(string tenLoaiKhoPhong, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetLoaiKhoPhongs(tenLoaiKhoPhong, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<LoaiKhoPhongResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-loai-kho-phong")]
        public JsonResult GetLoaiKhoPhong(int? loaiKhoPhongId)
        {
            LoaiKhoPhongResVM loaiKhoPhongResVM = new LoaiKhoPhongResVM();
            try
            {
                loaiKhoPhongResVM = _tuDienProvider.GetLoaiKhoPhong(loaiKhoPhongId);
                return Json(loaiKhoPhongResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(loaiKhoPhongResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-loai-kho-phong")]
        public JsonResult UpdateLoaiKhoPhong(UpdateLoaiKhoPhongReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateLoaiKhoPhong(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-loai-kho-phong")]
        public JsonResult DeleteLoaiKhoPhong(DeleteLoaiKhoPhongReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteLoaiKhoPhong(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-loai-kho-phong")]
        public JsonResult AddLoaiKhoPhong(AddLoaiKhoPhongReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddLoaiKhoPhong(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region loai phu trach
        [HttpGet]
        [Route("loai-phu-trach")]
        public ActionResult GetLoaiPhuTrachs(string tenLoaiPhuTrach, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetLoaiPhuTrachs(tenLoaiPhuTrach, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<LoaiPhuTrachResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-loai-phu-trach")]
        public JsonResult GetLoaiPhuTrach(int? loaiPhuTrachId)
        {
            LoaiPhuTrachResVM loaiPhuTrachResVM = new LoaiPhuTrachResVM();
            try
            {
                loaiPhuTrachResVM = _tuDienProvider.GetLoaiPhuTrach(loaiPhuTrachId);
                return Json(loaiPhuTrachResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(loaiPhuTrachResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-loai-phu-trach")]
        public JsonResult UpdateLoaiPhuTrach(UpdateLoaiPhuTrachReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateLoaiPhuTrach(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-loai-phu-trach")]
        public JsonResult DeleteLoaiPhuTrach(DeleteLoaiPhuTrachReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteLoaiPhuTrach(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-loai-phu-trach")]
        public JsonResult AddLoaiPhuTrach(AddLoaiPhuTrachReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddLoaiPhuTrach(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region nguon cap
        [HttpGet]
        [Route("nguon-cap")]
        public ActionResult GetNguonCaps(string tenNguonCap, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetNguonCaps(tenNguonCap, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<NguonCapResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-nguon-cap")]
        public JsonResult GetNguonCaps()
        {
            List<NguonCapResVM> nguonCapResVMs = new List<NguonCapResVM>();
            try
            {
                nguonCapResVMs = _tuDienProvider.GetNguonCaps();
                return Json(nguonCapResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(nguonCapResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-nguon-cap")]
        public JsonResult GetNguonCap(int? nguonCapId)
        {
            NguonCapResVM nguonCapResVM = new NguonCapResVM();
            try
            {
                nguonCapResVM = _tuDienProvider.GetNguonCap(nguonCapId);
                return Json(nguonCapResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(nguonCapResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-nguon-cap")]
        public JsonResult UpdateNguonCap(UpdateNguonCapReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateNguonCap(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-nguon-cap")]
        public JsonResult DeleteNguonCap(DeleteNguonCapReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteNguonCap(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-nguon-cap")]
        public JsonResult AddNguonCap(AddNguonCapReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddNguonCap(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region muc dich su dung
        [HttpGet]
        [Route("muc-dich-su-dung")]
        public ActionResult GetMucDichSuDungs(string tenMucDichSuDung, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetMucDichSuDungs(tenMucDichSuDung, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<MucDichSuDungResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-muc-dich-su-dung")]
        public JsonResult GetMucDichSuDungs()
        {
            List<MucDichSuDungResVM> mucDichSuDungResVMs = new List<MucDichSuDungResVM>();
            try
            {
                mucDichSuDungResVMs = _tuDienProvider.GetMucDichSuDungs();
                return Json(mucDichSuDungResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(mucDichSuDungResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-muc-dich-su-dung")]
        public JsonResult GetMucDichSuDung(int? mucDichSuDungId)
        {
            MucDichSuDungResVM mucDichSuDungResVM = new MucDichSuDungResVM();
            try
            {
                mucDichSuDungResVM = _tuDienProvider.GetMucDichSuDung(mucDichSuDungId);
                return Json(mucDichSuDungResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(mucDichSuDungResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-muc-dich-su-dung")]
        public JsonResult UpdateMucDichSuDung(UpdateMucDichSuDungReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateMucDichSuDung(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-muc-dich-su-dung")]
        public JsonResult DeleteMucDichSuDung(DeleteMucDichSuDungReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteMucDichSuDung(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-muc-dich-su-dung")]
        public JsonResult AddMucDichSuDung(AddMucDichSuDungReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddMucDichSuDung(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region nguon ngan sach
        [HttpGet]
        [Route("nguon-ngan-sach")]
        public ActionResult GetNguonNganSachs(string tenNguonNganSach, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetNguonNganSachs(tenNguonNganSach, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<NguonNganSachResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-nguon-ngan-sach")]
        public JsonResult GetNguonNganSach(int? nguonNganSachId)
        {
            NguonNganSachResVM nguonNganSachResVM = new NguonNganSachResVM();
            try
            {
                nguonNganSachResVM = _tuDienProvider.GetNguonNganSach(nguonNganSachId);
                return Json(nguonNganSachResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(nguonNganSachResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-nguon-ngan-sach")]
        public JsonResult UpdateNguonNganSach(UpdateNguonNganSachReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateNguonNganSach(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-nguon-ngan-sach")]
        public JsonResult DeleteNguonNganSach(DeleteNguonNganSachReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteNguonNganSach(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-nguon-ngan-sach")]
        public JsonResult AddNguonNganSach(AddNguonNganSachReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddNguonNganSach(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region nguon kinh phi cap tren
        [HttpGet]
        [Route("nguon-kinh-phi-cap-tren")]
        public ActionResult GetNguonKinhPhiCapTrens(string tenNguonKinhPhiCapTren, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetNguonKinhPhiCapTrens(tenNguonKinhPhiCapTren, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<NguonKinhPhiCapTrenResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-nguon-kinh-phi-cap-tren")]
        public JsonResult GetNguonKinhPhiCapTrens()
        {
            List<NguonKinhPhiCapTrenResVM> nguonKinhPhiCapTrenResVMs = new List<NguonKinhPhiCapTrenResVM>();
            try
            {
                nguonKinhPhiCapTrenResVMs = _tuDienProvider.GetNguonKinhPhiCapTrens();
                return Json(nguonKinhPhiCapTrenResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(nguonKinhPhiCapTrenResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-nguon-kinh-phi-cap-tren")]
        public JsonResult GetNguonKinhPhiCapTren(int? nguonKinhPhiCapTrenId)
        {
            NguonKinhPhiCapTrenResVM nguonKinhPhiCapTrenResVM = new NguonKinhPhiCapTrenResVM();
            try
            {
                nguonKinhPhiCapTrenResVM = _tuDienProvider.GetNguonKinhPhiCapTren(nguonKinhPhiCapTrenId);
                return Json(nguonKinhPhiCapTrenResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(nguonKinhPhiCapTrenResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-nguon-kinh-phi-cap-tren")]
        public JsonResult UpdateNguonKinhPhiCapTren(UpdateNguonKinhPhiCapTrenReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateNguonKinhPhiCapTren(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-nguon-kinh-phi-cap-tren")]
        public JsonResult DeleteNguonKinhPhiCapTren(DeleteNguonKinhPhiCapTrenReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteNguonKinhPhiCapTren(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-nguon-kinh-phi-cap-tren")]
        public JsonResult AddNguonKinhPhiCapTren(AddNguonKinhPhiCapTrenReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddNguonKinhPhiCapTren(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region khoi-lop
        [HttpGet]
        [Route("khoi-lop")]
        public ActionResult GetKhoiLops(string tenKhoiLop, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetKhoiLops(tenKhoiLop, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<KhoiLopResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [Route("danh-sach-khoi-lop")]
        public JsonResult GetKhoiLops()
        {
            List<KhoiLopResVM> khoiLopResVMs = new List<KhoiLopResVM>();
            try
            {
                khoiLopResVMs = _tuDienProvider.GetKhoiLops();
                return Json(khoiLopResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(khoiLopResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-khoi-lop")]
        public JsonResult GetKhoiLop(int? khoiLopId)
        {
            KhoiLopResVM khoiLopResVM = new KhoiLopResVM();
            try
            {
                khoiLopResVM = _tuDienProvider.GetKhoiLop(khoiLopId);
                return Json(khoiLopResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(khoiLopResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-khoi-lop")]
        public JsonResult UpdateKhoiLop(UpdateKhoiLopReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateKhoiLop(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-khoi-lop")]
        public JsonResult DeleteKhoiLop(DeleteKhoiLopReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteKhoiLop(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-khoi-lop")]
        public JsonResult AddKhoiLop(AddKhoiLopReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddKhoiLop(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region mon hoc
        [HttpGet]
        [Route("mon-hoc")]
        public ActionResult GetMonHocs(string tenMonHoc, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetMonHocs(tenMonHoc, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<MonHocResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("danh-sach-mon-hoc")]
        public JsonResult GetMonHocs()
        {
            List<MonHocResVM> monHocResVMs = new List<MonHocResVM>();
            try
            {
                monHocResVMs = _tuDienProvider.GetMonHocs();
                return Json(monHocResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(monHocResVMs, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("thong-tin-mon-hoc")]
        public JsonResult GetMonHoc(int? monHocId)
        {
            MonHocResVM monHocResVM = new MonHocResVM();
            try
            {
                monHocResVM = _tuDienProvider.GetMonHoc(monHocId);
                return Json(monHocResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(monHocResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-mon-hoc")]
        public JsonResult UpdateMonHoc(UpdateMonHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateMonHoc(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-mon-hoc")]
        public JsonResult DeleteMonHoc(DeleteMonHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteMonHoc(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-mon-hoc")]
        public JsonResult AddMonHoc(AddMonHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddMonHoc(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region lop
        [HttpGet]
        [Route("lop")]
        public ActionResult GetLops(string tenLop, int? khoiLopid, int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetLops(tenLop, khoiLopid, currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<LopResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-lop")]
        public JsonResult GetLop(int? lopId)
        {
            LopResVM lopResVM = new LopResVM();
            try
            {
                lopResVM = _tuDienProvider.GetLop(lopId);
                return Json(lopResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(lopResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-lop")]
        public JsonResult UpdateLop(UpdateLopReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateLop(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-lop")]
        public JsonResult DeleteLop(DeleteLopReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteLop(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-lop")]
        public JsonResult AddLop(AddLopReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddLop(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        #region kho phong
        [HttpGet]
        [Route("danh-sach-kho-phong")]
        public JsonResult GetKhoPhongs()
        {
            List<KhoPhongResVM> khoPhongResVMs = new List<KhoPhongResVM>();
            try
            {
                khoPhongResVMs = _tuDienProvider.GetKhoPhongs();
                return Json(khoPhongResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(khoPhongResVMs, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region thiet bi
        [HttpGet]
        [Route("danh-sach-thiet-bi")]
        public JsonResult GetThietBis(int? monHocId)
        {
            List<ThietBiResVM> thietBiResVMs = new List<ThietBiResVM>();
            try
            {
                thietBiResVMs = _tuDienProvider.GetThietBis(monHocId);
                return Json(thietBiResVMs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(thietBiResVMs, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion 

        #region loai phu trach
        [HttpGet]
        [Route("tiet-hoc")]
        public ActionResult GetTietHocs(int? currentPage, int? pageSize)
        {
            try
            {
                var result = _tuDienProvider.GetTietHocs(currentPage, pageSize);
                return View(result);
            }
            catch (Exception ex)
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                listWithPaginationResVM.Objects = new List<TietHocResVM>();
                listWithPaginationResVM.CurrentQueryParamsDict = new Dictionary<string, string>();
                listWithPaginationResVM.Paginations = new List<PaginationResVM>();
                return View(listWithPaginationResVM);
            }
        }

        [HttpGet]
        [Route("thong-tin-tiet-hoc")]
        public JsonResult GetTietHoc(int? tietHocId)
        {
            TietHocResVM tietHocResVM = new TietHocResVM();
            try
            {
                tietHocResVM = _tuDienProvider.GetTietHoc(tietHocId);
                return Json(tietHocResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(tietHocResVM, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("cap-nhat-thong-tin-tiet-hoc")]
        public JsonResult UpdateTietHoc(UpdateTietHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.UpdateTietHoc(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("xoa-thong-tin-tiet-hoc")]
        public JsonResult DeleteTietHoc(DeleteTietHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.DeleteTietHoc(reqVM);
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
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [Route("them-thong-tin-tiet-hoc")]
        public JsonResult AddTietHoc(AddTietHocReqVM reqVM)
        {
            try
            {
                _tuDienProvider.AddTietHoc(reqVM);
                TempData["SUCCESS"] = "Thêm dữ liệu thành công";
                return Json("Thêm dữ liệu thành công");
            }
            catch (WarningException ex)
            {
                TempData["WARNING"] = ex.Message;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion
    }
}