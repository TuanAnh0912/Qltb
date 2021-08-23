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
    public class TeacherController : Controller
    {
        GiaoVienProvider teacher_p = new GiaoVienProvider();
        KhoPhongProvider storage_p = new KhoPhongProvider();
        ToBoMonProvider tobomon_p = new ToBoMonProvider();

        public ActionResult Index(int? tobomon_id)
        {
            ViewBag.ToBoMon = tobomon_p.GetAllToBoMon();
            //ViewBag.CapQuanLy = storage_p.GetCapQuanLy();
            ViewBag.XepHang = storage_p.GetXepHangKhoPhongs();
            var teacher = new List<GiaoVienResVM>();
            
            if (tobomon_id == null)
            {
                teacher = teacher_p.GetAllGiaoVien();
            }
            else
            {
                teacher = teacher_p.GetAllGiaoVienByToBoMonId((int)tobomon_id);
            }
            return View(teacher);
        }
        [HttpPost]
        public JsonResult Insert(GiaoVien model, string NgayNghiViec, string NgayVaoNganh, string NgaySinh,string TenDangNhap,string MatKhau)
        {
            ResponseModel res = new ResponseModel();
            if (NgayVaoNganh != "") model.NgayVaoNganh = DateTime.ParseExact(NgayVaoNganh, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (NgayNghiViec != "") model.NgayNghiViec = DateTime.ParseExact(NgayNghiViec, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (NgaySinh != "") model.NgaySinh = DateTime.ParseExact(NgaySinh, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                res = teacher_p.InsertGiaoVien(model, Guid.Parse(Session["UserId"].ToString()),TenDangNhap, MatKhau);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(UpdateGiaoVienReqVM model, string NgayNghiViec, string NgayVaoNganh,string NgaySinh)
        {
            ResponseModel res = new ResponseModel();
            if (NgayVaoNganh != "") model.NgayVaoNganh = DateTime.ParseExact(NgayVaoNganh, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (NgayNghiViec != "") model.NgayNghiViec = DateTime.ParseExact(NgayNghiViec, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (NgaySinh != "") model.NgaySinh = DateTime.ParseExact(NgaySinh, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                res = teacher_p.UpdateGiaoVien(model);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int teacher_id)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                res = teacher_p.DeleteGiaoVien(teacher_id);
            }
            catch (Exception e)
            {
                res = new ResponseModel(false, null, e.Message);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGiaoVien(int teacher_id)
        {
            var teacher = teacher_p.GetGiaoVienById(teacher_id);
            return Json(teacher, JsonRequestBehavior.AllowGet);
        }
    }
}