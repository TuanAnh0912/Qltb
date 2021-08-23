using qltb.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qltb.Data.Providers;
using qltb.Data.ViewModels;
using qltb.Data;
using static qltb.Data.Helpers.HtmlHelper;

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    [RoutePrefix("cai-dat")]
    public class SettingController : Controller
    {
        PhongBanProvider _phongban = new PhongBanProvider();
        ChucVuProvider _chucvu = new ChucVuProvider();
        ChucNangProvider _chucnang = new ChucNangProvider();
        QuyenTruyCapProvider _quyentruycap = new QuyenTruyCapProvider();
        // GET: Setting
        #region GET DATA
        public JsonResult GetPhongBan()
        {
            var data = _phongban.GetAll().Select(a => new
            {
                id = a.PhongBanId,
                text = a.TenPhongBan
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChucVuByPhongBan(string id)
        {
            var data = _chucvu.GetAllChucVuByPhongBan(id).Select(a => new
            {
                id = a.ChucVuId,
                text = a.TenChucVu
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetQuyenByUser(string userId)
        {
            var chucnangs = _chucnang.GetAllByUser(userId);
            var data = chucnangs.Select(a => new
            {
                id = a.ChucNangId,
                text = a.TenChucNang,
                parent = a.KhoaChaId.HasValue ? a.KhoaChaId.ToString() : "#",
                icon = false
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChucNangByChucVu(int ChucVuId)
        {
            var all = _chucnang.GetAll();
            var chucnangs = _chucnang.GetAllByChuCVu(ChucVuId).Select(c => c.ChucNangId);
            var chucnangs2 = _chucnang.GetAllByChuCVu(ChucVuId);
            var data = new List<TreeViewModel>();
            foreach (var item in all)
            {
                var i = new TreeViewModel();
                if (chucnangs.Contains(item.ChucNangId))
                {
                    i.id = item.ChucNangId;
                    i.text = item.TenChucNang;
                    i.parent = item.KhoaChaId.HasValue ? item.KhoaChaId.ToString() : "#";
                    i.icon = false;
                    if (chucnangs2.Where(c => c.KhoaChaId == item.ChucNangId).Count() > 0)
                    {
                        i.state = new TreeNoteStateViewModel(false);
                    }
                    else
                    {
                        i.state = new TreeNoteStateViewModel(true);
                    }
                    //if (!item.KhoaChaId.HasValue)
                    //{

                    //}
                    //else
                    //{
                    //    if (chucnangs2.Where(c => c.KhoaChaId == item.ChucNangId).Count() > 0)
                    //    {
                    //        i.state = new TreeNoteStateViewModel(false);
                    //    }
                    //    else
                    //    {
                    //        i.state = new TreeNoteStateViewModel(true);
                    //    }
                    //}
                }
                else
                {
                    i.id = item.ChucNangId;
                    i.text = item.TenChucNang;
                    i.parent = item.KhoaChaId.HasValue ? item.KhoaChaId.ToString() : "#";
                    i.icon = false;
                    i.state = new TreeNoteStateViewModel(false);
                }
                data.Add(i);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChucVu(int id)
        {
            var result = _chucvu.GetThongTinChucVu(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ACTION
        [Route("quyen-truy-cap")]
        public ActionResult Permission()
        {
            var phongBans = _phongban.GetAll();
            return View(phongBans);
        }
        [HttpPost]
        public JsonResult UpdatePermission(int ChucVuId, List<int> quyentruycaps)
        {
            var check = _quyentruycap.Insert(ChucVuId, quyentruycaps);
            return Json(new { status = check }, JsonRequestBehavior.AllowGet);
        }
        [Route("phong-ban")]
        public ActionResult AllDepartment()
        {
            var model = _phongban.GetAll();
            return View(model);
        }
        [HttpPost]
        public JsonResult AddNewPhongBan(PhongBan model)
        {
            var result = _phongban.Insert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Route("chi-tiet-phong-ban")]
        public ActionResult ChiTietPhongBan(string phongbanId)
        {
            var model = _phongban.GetChiTietPhongBan(phongbanId);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdatePhongBan(PhongBan model)
        {
            var result = _phongban.UpdatePhongBan(model);
            if (result.Status)
            {
                TempData["Alert"] = new Alert("Cập nhật thông tin <b>" + model.TenPhongBan + "</b> thành công", Alert.AlertType.Success);
                return RedirectToAction("ChiTietPhongBan", "Setting", new { phongbanId = model.PhongBanId.ToString() });
            }
            else
            {

                TempData["Alert"] = new Alert("Cập nhật thông tin <b>" + model.TenPhongBan + "</b> không thành công!< br> Lỗi: " + result.Message, Alert.AlertType.Fail);
                return RedirectToAction("ChiTietPhongBan", "Setting", new { phongbanId = model.PhongBanId.ToString() });
            }
        }
        [HttpPost]
        public ActionResult AddNewChucVu(ChucVu model)
        {
            var result = _chucvu.Insert(model);
            if (result.Status)
            {
                TempData["Alert"] = new Alert("Thêm mới chức vụ <b>" + model.TenChucVu + "</b> thành công", Alert.AlertType.Success);
                return RedirectToAction("ChiTietPhongBan", "Setting", new { phongbanId = model.PhongBanId.ToString() });
            }
            else
            {

                TempData["Alert"] = new Alert("Thêm mới chức vụ <b>" + model.TenChucVu + "</b> không thành công!< br> Lỗi: " + result.Message, Alert.AlertType.Fail);
                return RedirectToAction("ChiTietPhongBan", "Setting", new { phongbanId = model.PhongBanId.ToString() });
            }
        }
        public ActionResult UpdateChucVu(ChucVu model)
        {
            var result = _chucvu.UpdateChucVu(model);
            if (result.Status)
            {
                TempData["Alert"] = new Alert(result.Message, Alert.AlertType.Success);
            }
            else
            {
                TempData["Alert"] = new Alert("Cập nhật chức vụ không thành công!<br> Lỗi: "+result.Message, Alert.AlertType.Fail);
            }
            return RedirectToAction("ChiTietPhongBan", "Setting", new { phongbanId = model.PhongBanId.ToString() });
        }
        [HttpPost]
        public ActionResult XoaChucVu(ChucVu model)
        {
            var result = _chucvu.DeleteChucVu(model.ChucVuId);
            if (result.Status)
            {
                TempData["Alert"] = new Alert(result.Message, Alert.AlertType.Success);
            }
            else
            {
                TempData["Alert"] = new Alert("Xóa chức vụ không thành công!<br> Lỗi: " + result.Message, Alert.AlertType.Fail);
            }
            return RedirectToAction("ChiTietPhongBan", "Setting", new { phongbanId = model.PhongBanId.ToString() });
        }
        #endregion
    }
}