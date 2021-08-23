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
using PagedList;

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    public class EquipmentController : Controller
    {
        ThietBiProvider equipment_p = new ThietBiProvider();
        BaiHocSuDungThietBiProvider bhsdtb_p = new BaiHocSuDungThietBiProvider();
        MonHocProvider subject_p = new MonHocProvider();
        KhoiLopProvider _khoiLopProvider = new KhoiLopProvider();
        // GET: Equipment
        public ActionResult Index(int? page, int? monhocId, int? loaiThietBiId, int? khoiLopId, string kyTu)
        {
            ViewBag.MonHoc = monhocId;
            ViewBag.LoaiThietBi = loaiThietBiId;
            ViewBag.KhoiLop = khoiLopId;
            ViewBag.kytu = kyTu;
            TempData["DonVi"] = equipment_p.GetAllDonVi();
            TempData["KhoiLop"] = subject_p.GetAllKhoiLop();
            TempData["DonViTinh"] = equipment_p.GetAllDonViTinh();
            TempData["LoaiThietBi"] = equipment_p.GetAllLoaiThietBi();
            TempData["MonHoc"] = subject_p.GetAllMonHoc();
            var equipment = equipment_p.GetAllThietBi(kyTu, monhocId, khoiLopId, loaiThietBiId).ToPagedList((page ?? 1), 10);
            return View(equipment);
        }
        [HttpPost]
        public JsonResult DeleteQuyDinh(int regulation_id)
        {
            var res = bhsdtb_p.DeleteQuyDinhSuDungThietBi(regulation_id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LessonRegulation(int? grade_id, int? subject_id)
        {
            ViewBag.DonVi = equipment_p.GetAllDonVi();
            ViewBag.KhoiLop = subject_p.GetAllKhoiLop();
            ViewBag.MonHoc = subject_p.GetAllMonHoc();
            ViewBag.LopHoc = subject_p.GetAllLopHoc();
            ViewBag.ChuongTrinh = bhsdtb_p.GetAllCTH();
            var class_room = equipment_p.GetAllLopHocTietQuyDinh();
            if (grade_id != null || subject_id != null)
            {
                class_room = equipment_p.FilterLopHocTietQuyDinh(subject_id, grade_id);
                ViewBag.Khoi = grade_id;
                ViewBag.Mon = subject_id;
            }
            return View(class_room);
        }
        //public PartialViewResult Equipment(int? page,int? monhocId,int? loaiThietBiId,int? khoiLopId)
        //{
        //    TempData["KhoiLop"] = subject_p.GetAllKhoiLop();
        //    TempData["DonViTinh"] = equipment_p.GetAllDonViTinh();
        //    TempData["LoaiThietBi"] = equipment_p.GetAllLoaiThietBi();
        //    TempData["MonHoc"] = subject_p.GetAllMonHoc();
        //    if (monhocId == null && loaiThietBiId==null && khoiLopId == null)
        //    {
        //       var equipment = equipment_p.GetAllThietBi().ToPagedList((page ??1),5);
        //        return PartialView("Equipment", equipment);
        //    }
        //    else
        //    {
        //        var equipment = equipment_p.GetThietBiByMonHoc(monhocId,loaiThietBiId,khoiLopId);
        //        return PartialView("Equipment", equipment);
        //    }
        //}
        //[HttpPost]
        //public JsonResult Test()
        //{
        //    var equipment = new List<ThietBiResVM>();
        //    equipment = equipment_p.GetAllThietBi();
        //    return Json(equipment, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult InsertQuyDinh(QuyDinhSoTietSuDungThietBi model, List<int> LopHocID)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = equipment_p.InsertQuyDinhSuDung(LopHocID, model);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateQuyDinh(UpdateQuyDinhSoTietSuDungThietBiReqVM model)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = equipment_p.UpdateQuyDinhSoTiet(model);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetQuyDinh(int quydinh_id)
        {
            var res = equipment_p.GetQuyDinhTietHocById(quydinh_id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Insert(ThietBi model, List<int> KhoiHocID)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = equipment_p.InsertThietBi(model, KhoiHocID, Guid.Parse(Session["UserId"].ToString()));
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(UpdateThietBiReqVM model, List<int> KhoiHocID)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = equipment_p.UpdateThietBi(model, KhoiHocID);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int equipment_id)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = equipment_p.DeleteThietBi(equipment_id);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetThietBi(int equipment_id)
        {
            var equipment = equipment_p.GetThietBiById(equipment_id);
            return Json(equipment, JsonRequestBehavior.AllowGet);
        }
    }
}