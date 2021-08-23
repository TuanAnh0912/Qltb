using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qltb.Data;
using qltb.Data.Providers;
using qltb.Data.Helpers;
using qltb.Data.Models;
using qltb.WebApp.Infrastructure;
using qltb.Data.ReqVMs;
using qltb.Data.ResVMs;
using qltb.Data.ViewModels;
using qltb.WebApp.Models.StorageRoomViewModel;
using System.IO;

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    public class StorageRoomController : Controller
    {
        KhoPhongProvider storage_p = new KhoPhongProvider();
        MonHocProvider subject_p = new MonHocProvider();
        GetForSelectProvider _get = new GetForSelectProvider();
        // GET: StorageRoom
        public ActionResult Index()
        {
            ViewBag.KieuSuDung = storage_p.GetKieuSuDungKhoPhongs();
            ViewBag.LoaiKhoPhong = storage_p.GetLoaiKhoPhongs();
            ViewBag.XepHang = storage_p.GetXepHangKhoPhongs();
            var storage = storage_p.GetAllKhoPhong();
            return View(storage);
        }
       
        [HttpPost]
        public JsonResult Insert(KhoPhong model)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = storage_p.InsertKhoPhong(model, Guid.Parse(Session["UserId"].ToString()));
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetKhoPhong(int storage_id)
        {
            var res = new KhoPhongResVM();
            try
            {
                 res = storage_p.GetKhoPhongById(storage_id);
            }
            catch (Exception e)
            {
                res = new KhoPhongResVM();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(UpdateKhoPhongReqVM model)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = storage_p.UpdateKhoPhong(model);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int storage_id)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = storage_p.DeleteKhoPhong(storage_id);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetThietBiKhoPhong(int storage_id)
        {
            var equipment = storage_p.GetThietBiByKhoPhong(storage_id);
            return Json(equipment, JsonRequestBehavior.AllowGet);
        }

        // NEW
        public ActionResult Equipment(int storage_id)
        {
            ViewData["MonHocs"] = new qltb.Data.Providers.MonHocProvider().GetAllMonHoc();
            Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();

            KhoPhongDetail khoPhongDetail = new KhoPhongDetail();
            khoPhongDetail.KhoPhongId = storage_id;
            List<ThietBiTrongPhongViewModel> thietBiTrongPhongViewModels = new List<ThietBiTrongPhongViewModel>();
            var khoThietBis = khoThietBiProvider.getAllByKhoPhongId(storage_id);
            foreach(var item in khoThietBis)
            {
                ThietBiTrongPhongViewModel thietBiTrongPhongViewModel = new ThietBiTrongPhongViewModel()
                {
                    KhoThietBiId = item.KhoThietBiId,
                    ThietBiId = item.ThietBiId.Value,
                    MaThietBi =item.ThietBi.MaThietBi,
                    SoLuong = item.SoLuong??0,
                    SoLuongHong =item.SoLuongHong??0,
                    SoLuongMat =item.SoLuongMat??0,
                    TenThietBi = item.ThietBi.TenThietBi,
                };
                thietBiTrongPhongViewModels.Add(thietBiTrongPhongViewModel);
            }
            khoPhongDetail.ThietBis = thietBiTrongPhongViewModels;
            return View(khoPhongDetail);
        }
        [HttpPost]
        public JsonResult ChangeSoluongThietBi(string khoThietBiId,int soLuong)
        {
            Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
            var khoThietBi = khoThietBiProvider.getById(khoThietBiId);
            khoThietBi.SoLuong = soLuong;
            if (khoThietBiProvider.Update(khoThietBi))
            {
                // Add log
                return Json(new { error = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = 500 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult RemoveThietBi(string khoThietBiId)
        {
            Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
            var khoThietBi = khoThietBiProvider.getById(khoThietBiId);
            if (khoThietBiProvider.Delete(khoThietBi))
            {
                // Add log
                return Json(new { error = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = 500 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SelectThietBiView(List<int> thietBiIdSelected,int? monHocId,string str)
        {
            Data.Providers.ThietBiProvider thietBiProvider = new ThietBiProvider();
            ViewData["ThietBiIdSelected"] = thietBiIdSelected??new List<int>();
            if (monHocId == -1)
            {
                monHocId = null;
            }
            return View(thietBiProvider.getAllBy(monHocId, str??""));
        }
        [HttpPost]
        public ActionResult SelectThietBiInPhongView(List<int> thietBiIdSelected,int  khoPhongId)
        {
            Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
            ViewData["ThietBiIdSelected"] = thietBiIdSelected ?? new List<int>();
            return View(khoThietBiProvider.getAllByKhoPhongId(khoPhongId));
        }
        #region BAO HONG

        [HttpPost]
        public ActionResult AddThietBiToPhong(int khoPhongId, List<int> thietBiIdSelected)
        {
            Data.Providers.ThietBiProvider thietBiProvider = new ThietBiProvider();
            Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
            List<ThietBiTrongPhongViewModel> thietBiTrongPhongViewModels = new List<ThietBiTrongPhongViewModel>();
            var khoThietBis = khoThietBiProvider.getAllByKhoPhongId(khoPhongId);

            if (thietBiIdSelected.Count > 0)
            {
                foreach (var item in thietBiIdSelected)
                {
                    if (khoThietBis.Count(m => m.ThietBiId == item) == 0)
                    {
                        var thietBi = thietBiProvider.getById(item);
                        Data.KhoThietBi khoThietBi = new Data.KhoThietBi()
                        {
                            ThietBiId = item,
                            KhoPhongId = khoPhongId,
                            SoLuong = 1,
                            KhoThietBiId = Guid.NewGuid().ToString(),
                        };
                        if (khoThietBiProvider.Insert(khoThietBi))
                        {

                            ThietBiTrongPhongViewModel thietBiTrongPhongViewModel = new ThietBiTrongPhongViewModel()
                            {
                                KhoThietBiId = khoThietBi.KhoThietBiId,
                                ThietBiId = item,
                                MaThietBi = thietBi.MaThietBi,
                                SoLuong = 1,
                                SoLuongHong = 0,
                                SoLuongMat = 0,
                                TenThietBi = thietBi.TenThietBi,
                            };
                            thietBiTrongPhongViewModels.Add(thietBiTrongPhongViewModel);

                        }
                    }
                }
            }
            return View(thietBiTrongPhongViewModels);
        }

        [HttpPost]
        public ActionResult AddThietBiToBaoHongView(List<int> thietBiSelectId, List<string> khoThietBiIdSelected)
        {
            List<BaoHongThietBiChiTietModel> baoHongThietBiChiTietModels = new List<BaoHongThietBiChiTietModel>();
            qltb.Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
            thietBiSelectId = thietBiSelectId ?? new List<int>();
            foreach (var item in khoThietBiIdSelected)
            {
                var khoThietBi = khoThietBiProvider.getById(item);
                if (thietBiSelectId.Count(m => m == khoThietBi.ThietBiId.Value) == 0)
                {
                    BaoHongThietBiChiTietModel baoHongThietBiChiTietModel = new BaoHongThietBiChiTietModel()
                    {
                        MaThietBi = khoThietBi.ThietBi.MaThietBi,
                        SoLuong = khoThietBi.SoLuong ?? 0,
                        SoLuongHong = 1,
                        ThietBiId = khoThietBi.ThietBiId.Value,
                        TenThietBi = khoThietBi.ThietBi.TenThietBi,
                    };
                    baoHongThietBiChiTietModels.Add(baoHongThietBiChiTietModel);
                }

            }
            return View(baoHongThietBiChiTietModels);
        }

        [HttpGet]
        public ActionResult BaoHongThietBiView(int KhoPhongId, string id = "")
        {
            qltb.Data.Providers.PhieuGhiHongThietBiProvider phieuGhiHongThietBiProvider = new PhieuGhiHongThietBiProvider();
            BaoHongThietBiModel baoHongThietBiModel = new BaoHongThietBiModel();
            baoHongThietBiModel.SoPhieu = Data.Helpers.RamdomHelper.RandomString(8);
            baoHongThietBiModel.KhoPhongId = KhoPhongId;
            baoHongThietBiModel.NgayLap = DateTime.Now.ToString("yyyy-MM-dd");
            baoHongThietBiModel.BaoHongThietBiChiTietModels = new List<BaoHongThietBiChiTietModel>();
            var khoThietBis = new KhoThietBiProvider().getAllByKhoPhongId(KhoPhongId);

            if (!string.IsNullOrEmpty(id))
            {
                var phieuGhiHongThietBi = phieuGhiHongThietBiProvider.getById(id);
                baoHongThietBiModel.PhieuGhiHongThietBiId = phieuGhiHongThietBi.PhieuGhiHongThietBiId;
                baoHongThietBiModel.SoPhieu = phieuGhiHongThietBi.SoPhieu;
                baoHongThietBiModel.GhiChu = phieuGhiHongThietBi.NoiDung;
                baoHongThietBiModel.KhoPhongId = phieuGhiHongThietBi.KhoPhongId.Value;
                baoHongThietBiModel.NgayLap = phieuGhiHongThietBi.NgayLap.Value.ToString("yyyy-MM-dd");

                var phieuGhiHongThietBis = new ChiTietPhieuGhiHongThietBiProvider().getAllByPhieuGhiHongThietBiId(phieuGhiHongThietBi.PhieuGhiHongThietBiId);
                foreach (var item in phieuGhiHongThietBis)
                {
                    if (khoThietBis.Count(m => m.ThietBiId == item.ThietBiId) > 0)
                    {
                        var khoThietBi = khoThietBis.First(m => m.ThietBiId == item.ThietBiId);
                        BaoHongThietBiChiTietModel baoHongThietBiChiTietModel = new BaoHongThietBiChiTietModel()
                        {
                            MaThietBi = item.ThietBi.MaThietBi,
                            SoLuong = khoThietBi.SoLuong ?? 0,
                            SoLuongHong = 1,
                            ThietBiId = item.ThietBiId.Value,
                            TenThietBi = item.ThietBi.TenThietBi,
                        };
                        baoHongThietBiModel.BaoHongThietBiChiTietModels.Add(baoHongThietBiChiTietModel);
                    }
                }
            }
            return View(baoHongThietBiModel);
        }

        [HttpPost]
        public JsonResult BaoHongThietBiView(BaoHongThietBiModel model, List<int> ThietBiId, List<int> SoLuongHong, HttpPostedFileBase FileDinhKem)
        {
            qltb.Data.Providers.PhieuGhiHongThietBiProvider phieuGhiHongThietBiProvider = new PhieuGhiHongThietBiProvider();
            PhieuGhiHongThietBi phieuGhiHongThietBi = null;

            if (!string.IsNullOrEmpty(model.PhieuGhiHongThietBiId))
            {
                phieuGhiHongThietBi = phieuGhiHongThietBiProvider.getById(model.PhieuGhiHongThietBiId);
            }
            try
            {
                if (phieuGhiHongThietBi == null)
                {
                    ThietBiId = ThietBiId ?? new List<int>();
                    if (ThietBiId.Count == 0)
                    {

                        return Json(new { error = 500, msg = "Vui lòng chọn thiết bị hỏng" }, JsonRequestBehavior.AllowGet);
                    }
                    var user = Session["User"] as Data.NguoiDung;
                    //Create
                    phieuGhiHongThietBi = new PhieuGhiHongThietBi()
                    {
                        PhieuGhiHongThietBiId = Guid.NewGuid().ToString(),
                        NgayLap = DateTime.ParseExact(model.NgayLap, "yyyy-MM-dd", null),
                        NoiDung = model.GhiChu,
                        SoPhieu = model.SoPhieu,
                        KhoPhongId = model.KhoPhongId,
                        TrangThaiHongMatId =1,
                        NguoiCapNhat = user.NguoiDungId.ToString()
                    };
                    phieuGhiHongThietBiProvider.Insert(phieuGhiHongThietBi);
                    qltb.Data.Providers.ChiTietPhieuGhiHongThietBiProvider chiTietPhieuGhiHongThietBiProvider = new ChiTietPhieuGhiHongThietBiProvider();
                    qltb.Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
                    foreach (var item in ThietBiId)
                    {
                        
                        ChiTietPhieuGhiHongThietBi chiTietPhieuGhiHongThietBi = new ChiTietPhieuGhiHongThietBi()
                        {
                            ChiTietPhieuGhiHongThietBiId = Guid.NewGuid().ToString(),
                            KhoPhongId = model.KhoPhongId,
                            SoLuongHong = SoLuongHong[ThietBiId.IndexOf(item)],
                            ThietBiId = item,
                            PhieuGhiHongThietBiId = phieuGhiHongThietBi.PhieuGhiHongThietBiId
                        };
                        chiTietPhieuGhiHongThietBiProvider.Insert(chiTietPhieuGhiHongThietBi);

                        var khoThietBi = khoThietBiProvider.getById(model.KhoPhongId, item);
                        khoThietBi.SoLuongHong = (khoThietBi.SoLuongHong ?? 0) + chiTietPhieuGhiHongThietBi.SoLuongHong;
                        khoThietBiProvider.Update(khoThietBi);
                    }
                }
                else
                {
                    phieuGhiHongThietBi.NgayLap = DateTime.ParseExact(model.NgayLap, "MM/dd/yyyy", null);
                    phieuGhiHongThietBi.NoiDung = model.GhiChu;
                    phieuGhiHongThietBi.SoPhieu = model.SoPhieu;
                    phieuGhiHongThietBiProvider.Update(phieuGhiHongThietBi);
                    qltb.Data.Providers.ChiTietPhieuGhiHongThietBiProvider chiTietPhieuGhiHongThietBiProvider = new ChiTietPhieuGhiHongThietBiProvider();
                    var phieuGhiHongThietBis = chiTietPhieuGhiHongThietBiProvider.getAllByPhieuGhiHongThietBiId(phieuGhiHongThietBi.PhieuGhiHongThietBiId);

                    qltb.Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();

                    foreach (var item in ThietBiId)
                    {
                        if (phieuGhiHongThietBis.Count(m => m.ThietBiId == item) > 0)
                        {
                            var chiTietPhieuGhiHongThietBi = phieuGhiHongThietBis.First(m => m.ThietBiId == item);

                            var khoThietBi = khoThietBiProvider.getById(model.KhoPhongId, item);
                            khoThietBi.SoLuongHong = (khoThietBi.SoLuongHong ?? 0) -  chiTietPhieuGhiHongThietBi.SoLuongHong;

                            //Update
                            khoThietBi.SoLuongHong = (khoThietBi.SoLuongHong ?? 0) + SoLuongHong[ThietBiId.IndexOf(item)];
                            khoThietBiProvider.Update(khoThietBi);

                            chiTietPhieuGhiHongThietBi.SoLuongHong = SoLuongHong[ThietBiId.IndexOf(item)];
                            chiTietPhieuGhiHongThietBiProvider.Update(chiTietPhieuGhiHongThietBi);
                            phieuGhiHongThietBis.Remove(chiTietPhieuGhiHongThietBi);
                        }
                        else
                        {
                            //Create
                            ChiTietPhieuGhiHongThietBi chiTietPhieuGhiHongThietBi = new ChiTietPhieuGhiHongThietBi()
                            {
                                ChiTietPhieuGhiHongThietBiId = Guid.NewGuid().ToString(),
                                KhoPhongId = model.KhoPhongId,
                                SoLuongHong = SoLuongHong[ThietBiId.IndexOf(item)],
                                ThietBiId = item,
                                PhieuGhiHongThietBiId = phieuGhiHongThietBi.PhieuGhiHongThietBiId
                            };
                            chiTietPhieuGhiHongThietBiProvider.Insert(chiTietPhieuGhiHongThietBi);
                            var khoThietBi = khoThietBiProvider.getById(model.KhoPhongId, item);
                            khoThietBi.SoLuongHong = (khoThietBi.SoLuongHong??0)+ chiTietPhieuGhiHongThietBi.SoLuongHong;
                            khoThietBiProvider.Update(khoThietBi);
                        }
                    }
                    if (phieuGhiHongThietBis.Count > 0)
                    {
                        chiTietPhieuGhiHongThietBiProvider.Delete(phieuGhiHongThietBis);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { error = 0, msg = "Báo hỏng thành công" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region BAO MAT

        [HttpPost]
        public ActionResult AddThietBiToBaoMatView(List<int> thietBiSelectId, List<string> khoThietBiIdSelected)
        {
            List<BaoMatThietBiChiTietModel> baoHongThietBiChiTietModels = new List<BaoMatThietBiChiTietModel>();
            qltb.Data.Providers.KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
            thietBiSelectId = thietBiSelectId ?? new List<int>();
            foreach (var item in khoThietBiIdSelected)
            {
                var khoThietBi = khoThietBiProvider.getById(item);
                if (thietBiSelectId.Count(m => m == khoThietBi.ThietBiId.Value) == 0)
                {
                    BaoMatThietBiChiTietModel baoHongThietBiChiTietModel = new BaoMatThietBiChiTietModel()
                    {
                        MaThietBi = khoThietBi.ThietBi.MaThietBi,
                        SoLuong = khoThietBi.SoLuong ?? 0,
                        SoLuongMat = 1,
                        ThietBiId = khoThietBi.ThietBiId.Value,
                        TenThietBi = khoThietBi.ThietBi.TenThietBi,
                    };
                    baoHongThietBiChiTietModels.Add(baoHongThietBiChiTietModel);
                }

            }
            return View(baoHongThietBiChiTietModels);
        }

        [HttpGet]
        public ActionResult BaoMatThietBiView(int KhoPhongId, string id = "")
        {
            qltb.Data.Providers.PhieuGhiMatThietBiProvider phieuGhiMatThietBiProvider = new PhieuGhiMatThietBiProvider();

            BaoMatThietBiModel baoHongThietBiModel = new BaoMatThietBiModel();
            baoHongThietBiModel.SoPhieu = Data.Helpers.RamdomHelper.RandomString(8);
            baoHongThietBiModel.KhoPhongId = KhoPhongId;
            baoHongThietBiModel.NgayLap = DateTime.Now.ToString("yyyy-MM-dd");
            baoHongThietBiModel.BaoMatThietBiChiTietModels = new List<BaoMatThietBiChiTietModel>();
            var khoThietBis = new KhoThietBiProvider().getAllByKhoPhongId(KhoPhongId);

            if (!string.IsNullOrEmpty(id))
            {
                var phieuGhiMatThietBi = phieuGhiMatThietBiProvider.getById(id);
                baoHongThietBiModel.PhieuGhiMatThietBiId = phieuGhiMatThietBi.PhieuGhiMatThietBiId;
                baoHongThietBiModel.SoPhieu = phieuGhiMatThietBi.SoPhieu;
                baoHongThietBiModel.KhoPhongId = phieuGhiMatThietBi.KhoPhongId.Value;
                baoHongThietBiModel.GhiChu = phieuGhiMatThietBi.NoiDung??"";
                baoHongThietBiModel.NgayLap = phieuGhiMatThietBi.NgayLap.Value.ToString("yyyy-MM-dd");

                var phieuGhiHongThietBis = new ChiTietPhieuGhiMatThietBiProvider().getAllByPhieuGhiMatThietBiId(phieuGhiMatThietBi.PhieuGhiMatThietBiId);
                foreach (var item in phieuGhiHongThietBis)
                {
                    if (khoThietBis.Count(m => m.ThietBiId == item.ThietBiId) > 0)
                    {
                        var khoThietBi = khoThietBis.First(m => m.ThietBiId == item.ThietBiId);
                        BaoMatThietBiChiTietModel baoHongThietBiChiTietModel = new BaoMatThietBiChiTietModel()
                        {
                            MaThietBi = item.ThietBi.MaThietBi,
                            SoLuong = khoThietBi.SoLuong ?? 0,
                            SoLuongMat = 1,
                            ThietBiId = item.ThietBiId.Value,
                            TenThietBi = item.ThietBi.TenThietBi,
                        };
                        baoHongThietBiModel.BaoMatThietBiChiTietModels.Add(baoHongThietBiChiTietModel);
                    }
                }
            }
            return View(baoHongThietBiModel);
        }

        [HttpPost]
        public JsonResult BaoMatThietBiView(BaoMatThietBiModel model, List<int> ThietBiId, List<int> SoLuongMat, HttpPostedFileBase FileDinhKem)
        {
            qltb.Data.Providers.PhieuGhiMatThietBiProvider phieuGhiMatThietBiProvider = new PhieuGhiMatThietBiProvider();
            PhieuGhiMatThietBi phieuGhiMatThietBi = null;

            if (!string.IsNullOrEmpty(model.PhieuGhiMatThietBiId))
            {
                phieuGhiMatThietBi = phieuGhiMatThietBiProvider.getById(model.PhieuGhiMatThietBiId);
            }
            try
            {
                if (phieuGhiMatThietBi == null)
                {
                    ThietBiId = ThietBiId ?? new List<int>();
                    if (ThietBiId.Count == 0)
                    {

                        return Json(new { error = 500, msg = "Vui lòng chọn thiết bị hỏng" }, JsonRequestBehavior.AllowGet);
                    }
                    var user = Session["User"] as Data.NguoiDung;
                    //Create
                    phieuGhiMatThietBi = new PhieuGhiMatThietBi()
                    {
                        PhieuGhiMatThietBiId = Guid.NewGuid().ToString(),
                        NgayLap = DateTime.ParseExact(model.NgayLap, "yyyy-MM-dd", null),
                        NoiDung = model.GhiChu,
                        SoPhieu = model.SoPhieu,
                        TrangThaiHongMatId =1,
                        KhoPhongId = model.KhoPhongId,
                        NguoiCapNhat = user.NguoiDungId.ToString()
                    };
                    phieuGhiMatThietBiProvider.Insert(phieuGhiMatThietBi);
                    qltb.Data.Providers.ChiTietPhieuGhiMatThietBiProvider chiTietPhieuGhiMatThietBiProvider = new ChiTietPhieuGhiMatThietBiProvider();
                    KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
                    foreach (var item in ThietBiId)
                    {
                        ChiTietPhieuGhiMatThietBi chiTietPhieuGhiMatThietBi = new ChiTietPhieuGhiMatThietBi()
                        {
                            ChiTietPhieuGhiMatThietBiId = Guid.NewGuid().ToString(),
                            KhoPhongId = model.KhoPhongId,
                            SoLuongMat = SoLuongMat[ThietBiId.IndexOf(item)],
                            ThietBiId = item,
                            PhieuGhiMatThietBiId = phieuGhiMatThietBi.PhieuGhiMatThietBiId
                        };
                        chiTietPhieuGhiMatThietBiProvider.Insert(chiTietPhieuGhiMatThietBi);
                        var khoThietBi = khoThietBiProvider.getById(model.KhoPhongId, item);
                        khoThietBi.SoLuongMat = (khoThietBi.SoLuongMat ?? 0) + chiTietPhieuGhiMatThietBi.SoLuongMat;
                        khoThietBiProvider.Update(khoThietBi);
                    }
                }
                else
                {
                    phieuGhiMatThietBi.NgayLap = DateTime.ParseExact(model.NgayLap, "MM/dd/yyyy", null);
                    phieuGhiMatThietBi.NoiDung = model.GhiChu;
                    phieuGhiMatThietBi.SoPhieu = model.SoPhieu;
                    phieuGhiMatThietBiProvider.Update(phieuGhiMatThietBi);
                    qltb.Data.Providers.ChiTietPhieuGhiMatThietBiProvider chiTietPhieuGhiMatThietBiProvider = new ChiTietPhieuGhiMatThietBiProvider();
                    var phieuGhiHongThietBis = chiTietPhieuGhiMatThietBiProvider.getAllByPhieuGhiMatThietBiId(phieuGhiMatThietBi.PhieuGhiMatThietBiId);
                    KhoThietBiProvider khoThietBiProvider = new KhoThietBiProvider();
                    foreach (var item in ThietBiId)
                    {
                        if (phieuGhiHongThietBis.Count(m => m.ThietBiId == item) > 0)
                        {
                            var chiTietPhieuGhiHongThietBi = phieuGhiHongThietBis.First(m => m.ThietBiId == item);

                            var khoThietBi = khoThietBiProvider.getById(model.KhoPhongId, item);
                            khoThietBi.SoLuongMat = (khoThietBi.SoLuongMat??0)- chiTietPhieuGhiHongThietBi.SoLuongMat;
                            khoThietBi.SoLuongMat = (khoThietBi.SoLuongMat ?? 0) + SoLuongMat[ThietBiId.IndexOf(item)];
                            khoThietBiProvider.Update(khoThietBi);

                            //Update
                            chiTietPhieuGhiHongThietBi.SoLuongMat = SoLuongMat[ThietBiId.IndexOf(item)];
                            chiTietPhieuGhiMatThietBiProvider.Update(chiTietPhieuGhiHongThietBi);
                            phieuGhiHongThietBis.Remove(chiTietPhieuGhiHongThietBi);
                        }
                        else
                        {
                            //Create
                            ChiTietPhieuGhiMatThietBi chiTietPhieuGhiMatThietBi = new ChiTietPhieuGhiMatThietBi()
                            {
                                ChiTietPhieuGhiMatThietBiId = Guid.NewGuid().ToString(),
                                KhoPhongId = model.KhoPhongId,
                                SoLuongMat = SoLuongMat[ThietBiId.IndexOf(item)],
                                ThietBiId = item,
                                PhieuGhiMatThietBiId = phieuGhiMatThietBi.PhieuGhiMatThietBiId
                            };
                            chiTietPhieuGhiMatThietBiProvider.Insert(chiTietPhieuGhiMatThietBi);
                            var khoThietBi = khoThietBiProvider.getById(model.KhoPhongId, item);
                            khoThietBi.SoLuongMat = (khoThietBi.SoLuongMat ?? 0) + chiTietPhieuGhiMatThietBi.SoLuongMat;
                            khoThietBiProvider.Update(khoThietBi);
                        }
                    }
                    if (phieuGhiHongThietBis.Count > 0)
                    {
                        chiTietPhieuGhiMatThietBiProvider.Delete(phieuGhiHongThietBis);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { error = 0, msg = "Báo mất thành công" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult ListBaoHongBaoMatView(int id)
        {
            List<BaoHongBaoMatItemModel> baoHongBaoMatItemModels = new List<BaoHongBaoMatItemModel>();

            var phieuGhiHongThietBis = new PhieuGhiHongThietBiProvider().getAll(id);

            var phieuGhiMatThietBis = new PhieuGhiMatThietBiProvider().getAll(id);

            Data.Providers.NguoiDungProvider nguoiDungProvider = new NguoiDungProvider();

            foreach(var item in phieuGhiHongThietBis)
            {
                BaoHongBaoMatItemModel baoHongBaoMatItemModel = new BaoHongBaoMatItemModel()
                {
                    Id= item.PhieuGhiHongThietBiId,
                    NgayBao =item.NgayLap.Value,
                    NguoiTao ="",
                    SoHieu = item.SoPhieu,
                    TrangThai =item.TrangThaiHongMat.TenTrangThaiHongMat,
                    TrangThaiColor = item.TrangThaiHongMat.MaMau,
                    Type = "HONG"
                };
                var  nguoiDung =  nguoiDungProvider.GetById(Guid.Parse(item.NguoiCapNhat));
                baoHongBaoMatItemModel.NguoiTao = nguoiDung.HoTen; 
                baoHongBaoMatItemModels.Add(baoHongBaoMatItemModel);
            }

            foreach(var item in phieuGhiMatThietBis)
            {
                BaoHongBaoMatItemModel baoHongBaoMatItemModel = new BaoHongBaoMatItemModel()
                {
                    Id = item.PhieuGhiMatThietBiId,
                    NgayBao = item.NgayLap.Value,
                    NguoiTao = "",
                    SoHieu = item.SoPhieu,
                    TrangThai = item.TrangThaiHongMat.TenTrangThaiHongMat,
                    TrangThaiColor = item.TrangThaiHongMat.MaMau,
                    Type="MAT"
                };
                var nguoiDung = nguoiDungProvider.GetById(Guid.Parse(item.NguoiCapNhat));
                baoHongBaoMatItemModel.NguoiTao = nguoiDung.HoTen;
                baoHongBaoMatItemModels.Add(baoHongBaoMatItemModel);
            }

            return View(baoHongBaoMatItemModels.OrderByDescending(m=>m.NgayBao));
        }
    }
}