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

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    public class ExpenseController : Controller
    {
        KhoPhongProvider storage_p = new KhoPhongProvider();
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
            //try
            //{
            //    res = storage_p.InsertKhoPhong(model, Guid.Parse(Session["UserId"].ToString()));
            //}
            //catch (Exception e)
            //{
            //    res = new ResponseModel(false, null, e.Message);
            //}
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(UpdateKhoPhongReqVM model)
        {
            ResponseModel res = new ResponseModel();
            //try
            //{
            //    res = storage_p.UpdateKhoPhong(model);
            //}
            //catch (Exception e)
            //{
            //    res = new ResponseModel(false, null, e.Message);
            //}
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int storage_id)
        {
            ResponseModel res = new ResponseModel();
            //try
            //{
            //    res = storage_p.DeleteKhoPhong(storage_id);
            //}
            //catch (Exception e)
            //{
            //    res = new ResponseModel(false, null, e.Message);
            //}
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult GetKhoPhong(int storage_id)
        //{
        //    var storage = storage_p.GetKhoPhongById(storage_id);
        //    return Json(storage, JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public JsonResult GetThietBiKhoPhong(int storage_id)
        //{
        //    var equipment = storage_p.GetThietBiByKhoPhong(storage_id);
        //    return Json(equipment, JsonRequestBehavior.AllowGet);
        //}
    }
}