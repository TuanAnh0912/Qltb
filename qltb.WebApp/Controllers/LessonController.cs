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
    public class LessonController : Controller
    {
        BaiHocSuDungThietBiProvider lesson_p = new BaiHocSuDungThietBiProvider();
        MonHocProvider _monHocProvider = new MonHocProvider();
        ThietBiProvider equipment_p = new ThietBiProvider();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LessonEquipment( int? page, int? lesson_id,string kyTu, int? loaiThietBiId)
        {
            TempData["LoaiThietBi"] = equipment_p.GetAllLoaiThietBi();
            ViewBag.ChuongTrinhHoc = lesson_p.GetAllCTH();
            ViewBag.LessonId = lesson_id;
            ViewBag.LoaiThietBi = loaiThietBiId;
            ViewBag.HocKy = lesson_p.GetAllHocKy();
            ViewBag.Lop = _monHocProvider.GetAllLopHoc();
            ViewBag.LoaiBaiHoc = lesson_p.GetAllLoaiBaiHoc();
            ViewBag.MonHoc = _monHocProvider.GetAllMonHoc();
            ViewBag.Kytu = kyTu;
            ViewBag.BaiHocID = (lesson_id != null) ? lesson_id : null;
            IPagedList<ThietBiCuaBaiGiangResVM> lesson_equipment=new List<ThietBiCuaBaiGiangResVM>().ToPagedList(1,1);
            if (lesson_id != null) {
                 lesson_equipment = lesson_p.GetThietBiCuaBaiHoc((int)lesson_id,kyTu, loaiThietBiId).lstThietBi.ToPagedList( page ?? 1,10);
            }
            return View(lesson_equipment);
        }
        [HttpPost]
        public JsonResult Update(UpdateBaiHocSuDungThietBiReqVM model,string ThoiGian)
        {
            //var thoiGian = new ParseStringToDateTime().ParseStringToTime(ThoiGian);
           //model.ThoiGian = thoiGian;
            var res = lesson_p.Update(model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {

            var res = lesson_p.DeleteBaiHocSuDungThietBi(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EquipmentNeeded()
        {
            ViewBag.ChuongTrinhHoc = lesson_p.GetAllCTH();
            ViewBag.HocKy = lesson_p.GetAllHocKy();
            ViewBag.Lop = _monHocProvider.GetAllLopHoc();
            ViewBag.LoaiBaiHoc = lesson_p.GetAllLoaiBaiHoc();
            ViewBag.MonHoc = _monHocProvider.GetAllMonHoc();
            var lessons = lesson_p.GettAllBaiHocSuDungTB();
            return View(lessons);
        }
        [HttpPost]
        public JsonResult InsertLessonEquipment(int lesson_id, List<AddThietBiBaiHocReqVM> model)
        {
            var res = lesson_p.ChangeThietBiBaiHoc(lesson_id, model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Insert(BaiHocSuDungThietBi model)
        {
            var res = lesson_p.Insert(model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetBaiHocSuDungByID( int id)
        {
            var res = lesson_p.GettBaiHocSuDungTBById(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetThietBiBaiHoc(int lesson_id)
        {
            var equipments = lesson_p.GetThietBiCuaBaiHoc(lesson_id,null,null);
            equipments.lstThietBi = equipments.lstThietBi.Where(e => e.CheckThietBi == 1).ToList();
            return Json(equipments, JsonRequestBehavior.AllowGet);
        }
    }
}