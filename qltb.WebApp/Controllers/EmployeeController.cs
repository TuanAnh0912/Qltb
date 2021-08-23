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

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    public class EmployeeController : Controller
    {
        GiaoVienProvider teacher_p = new GiaoVienProvider();
        KhoPhongProvider storage_p = new KhoPhongProvider();
        ToBoMonProvider tobomon_p = new ToBoMonProvider();
        CanBoThietBiProvider incharge_p = new CanBoThietBiProvider();
        // GET: StorageRoom
        public ActionResult InCharge()
        {
            ViewBag.GiangVien = teacher_p.GetAllGiaoVien();
            ViewBag.VaiTro = incharge_p.GetAllVaiTro();
            var incharger = incharge_p.GetAllCanBoThietBi();
            return View(incharger);
        }
        [HttpPost]
        public JsonResult Insert(CanBoThietBi model, string ThoiGianQuanLy)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                var date = ThoiGianQuanLy.Split(new string[] { " to " }, StringSplitOptions.None).ToList();
                model.ThoiGianBatDauQuanLy = DateTime.ParseExact(date[0], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                model.ThoiGianHetHanQuanLy = DateTime.ParseExact(date[1], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                res = incharge_p.InsertCanBoThietBi(model);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null,"Thêm mới không thành công");
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(UpdateCanBoThietBiReqVm model, string ThoiGianQuanLy)
        {
            ResponseModel res = new ResponseModel();
            var date = ThoiGianQuanLy.Split(new string[] { " to " }, StringSplitOptions.None).ToList();
            model.ThoiGianBatDauQuanLy = DateTime.ParseExact(date[0], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            model.ThoiGianHetHanQuanLy = DateTime.ParseExact(date[1], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                res = incharge_p.UpdateCanBoThietBi(model);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int incharger_id)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = incharge_p.DeleteCanBoThietBi(incharger_id);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCanBoThietBi(int incharger_id)
        {
            var teacher = incharge_p.GetCanBoThietBiById(incharger_id);
            return Json(teacher, JsonRequestBehavior.AllowGet);
        }
    }
}