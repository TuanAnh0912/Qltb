using qltb.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qltb.Data;
using qltb.Data.Providers;
using qltb.Data.ViewModels;
using PagedList;

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    [RoutePrefix("muon-tra")]
    public class MuonTraController : Controller
    {
        PhieuMuonProvider _phieumuon = new PhieuMuonProvider();
        GetForSelectProvider _get = new GetForSelectProvider();
        // GET: MuonTra
        #region GET
        public JsonResult GetThietBiByBaiHoc(int BaiHocId)
        {
            var result = _get.GetThietBiByBaiHoc(BaiHocId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetThietBi(FilterThietBiViewModel model, string ngayMuon, string ngayTra)
        {
            var thietbis = new List<SelectThietBiViewmodel>();
            var result = _get.GetThietBiBy(model.KhoPhongId, model.MonHocId, model.LoaiThietBiId);
            if (model.searchString != null && model.searchString.Length > 0)
            {
                result = result.Where(r => r.TenThietBi.ToLower().Contains(model.searchString.ToLower()) || r.TenLoaiThietBi.ToLower().Contains(model.searchString.ToLower())).ToList();
            }
            var thietbidamuons = _get.GetThietBiMuonTrongThoiGian(ngayMuon, ngayTra);

            if (model.SelectedTB != null && model.SelectedTB.Count() > 0)
            {
                foreach (var item in result)
                {
                    if (model.SelectedTB.Any(s => s.ThietBiId == item.ThietBiId && s.KhoPhongId == item.KhoPhongId))
                    {
                        item.Selected = "selected";
                    }
                    foreach(var t in thietbidamuons)
                    {
                        if(t.MaThietBi == item.MaThietBi && t.KhoPhongId == item.KhoPhongId)
                        {
                            if(t.TrangThaiPhieuMuonId == 1)
                            {
                                item.SoLuongConLai += t.SoLuong;
                            }
                            if(t.TrangThaiPhieuMuonId == 3)
                            {
                                item.SoLuongConLai -= t.SoLuong;
                            }
                        }
                    }
                    thietbis.Add(item);
                }
            }
            else
            {
                foreach (var item in result)
                {
                    foreach (var t in thietbidamuons)
                    {
                        if (t.MaThietBi == item.MaThietBi && t.KhoPhongId == item.KhoPhongId)
                        {
                            if (t.TrangThaiPhieuMuonId == 1)
                            {
                                item.SoLuongConLai += t.SoLuong;
                            }
                            if (t.TrangThaiPhieuMuonId == 3)
                            {
                                item.SoLuongConLai -= t.SoLuong;
                            }
                        }
                    }
                    thietbis.Add(item);
                }
            }
            return Json(thietbis, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetThietBiByKhoPhong(int KhoPhongId)
        {
            var thietbis = _get.GetThietBiBy2(KhoPhongId, null, null);
            return Json(thietbis, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPhongMuon(int? LoaiKhoPhongId, string ngayMuon, string ngayTra, string searchString)
        {
            var res = _get.GetPhongCoTheMuon(LoaiKhoPhongId, ngayMuon, ngayTra, searchString);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [Route("thiet-bi")]
        public ActionResult Index(int? pageNum)
        {
            int pageSize = 9;
            int pageNumber = (pageNum ?? 1);
            var model = _phieumuon.GetAll(1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        [Route("phong")]
        public ActionResult DanhSachPhieuMuon(int ? pageNum)
        {
            int pageSize = 9;
            int pageNumber = (pageNum ?? 1);
            var model = _phieumuon.GetAll(2);
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        [Route("them-phieu-muon-thiet-bi")]
        public ActionResult AddNew()
        {
            var model = _phieumuon.GetToAdd();
            return View(model);
        }
        [HttpPost]
        public JsonResult AddNewPhieuMuon(PhieuMuonViewModel model)
        {
            model.NguoiTao = Guid.Parse(Session["UserId"].ToString());
            var result = _phieumuon.Insert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Route("chi-tiet-phieu-muon")]
        public ActionResult ChiTietPhieuMuonThietBi(string maPhieu)
        {
            var model = _phieumuon.GetByMaPhieu(maPhieu);
            return View(model);
        }
        [Route("cap-nhat-phieu-muon")]
        public ActionResult EditPhieuMuonThietBi(string maPhieu)
        {
            var model = _phieumuon.GetByMaPhieu(maPhieu);
            return View(model);
        }
        [Route("cap-nhat-phieu-muon-phong")]
        public ActionResult EditPhieuMuonPhong(string maPhieu)
        {
            var model = _phieumuon.GetByMaPhieu(maPhieu);
            return View(model);
        }
        [HttpPost]
        public JsonResult UpdateSoLuongThietBi(LienKetThietBiPhieuMuon model)
        {
            string user = Session["UserId"].ToString();
            var result = _phieumuon.UpdateSoLuongMuonThietBi(model, user);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Route("tra-thiet-bi")]
        public ActionResult TraThietBi(string maPhieu)
        {
            var model = _phieumuon.GetByMaPhieu(maPhieu);
            if(model.IsDelete.Value || model.TrangThaiPhieuMuonId == 2)
            {
                return RedirectToAction("khong-tim-thay", "muon-tra");
            }
            else
            {
                return View(model);
            }
        }
        [Route("khong-tim-thay")]
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult GiaoThietBi(PhieuMuonViewModel model)
        {
            return RedirectToAction("Index", "MuonTra");
        }
        [Route("them-phieu-muon-phong")]
        public ActionResult ThemPhieuMuonPhong()
        {
            var model = _phieumuon.GetToAdd();
            return View(model);
        }
        [HttpPost]
        public JsonResult CheckPhongMuonTruongThoiGian(string start, string end, int KhoPhongId, string MaPhieu)
        {
            var status = _get.CheckPhongDangKyTrongThoiGian(start, end, KhoPhongId, MaPhieu);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdatePhieuMuonPhong(PhieuMuonViewModel model)
        {
            string user = Session["UserId"].ToString();
            var res = _phieumuon.UpdatePhieuMuon(model, user);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [Route("chi-tiet-phieu-muon-phong")]
        public ActionResult ChiTietPhieuMuonPhong(string MaPhieu)
        {
            var model = _phieumuon.GetByMaPhieu(MaPhieu);
            return View(model);
        }
        [HttpPost]
        public JsonResult GiaoNhanThietBi(bool? NguoiMuonNhan, bool? NguoiMuonTra, bool? NguoiChoMuonGiao, bool? NguoiChoMuonNhan, string MaPhieu)
        {
            string user = Session["UserId"].ToString();
            var res = _phieumuon.XacNhanMuonTra(NguoiMuonNhan, NguoiMuonTra, NguoiChoMuonGiao, NguoiChoMuonNhan, MaPhieu, user);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddThietBiVaoPhieu(List<SelectThietBiViewmodel> ThietBis, string MaPhieuMuon)
        {
            string user = Session["UserId"].ToString();
            var res = _phieumuon.AddThietBiToPhieu(ThietBis, MaPhieuMuon, user);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TraThietBi(List<LienKetThietBiPhieuMuon> ThietBis)
        {
            string user = Session["UserId"].ToString();
            var res = _phieumuon.TraThietBi(ThietBis, user);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }

}