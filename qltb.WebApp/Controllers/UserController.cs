using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qltb.Data;
using qltb.Data.Providers;
using qltb.Data.ViewModels;
using qltb.WebApp.Infrastructure;
using static qltb.Data.Helpers.HtmlHelper;

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    [RoutePrefix("nguoi-dung")]
    public class UserController : Controller
    {
        NguoiDungProvider _nguoidung = new NguoiDungProvider();
        LienKetNguoiDungChucVuProvider _lienketChucVuNguoiDung = new LienKetNguoiDungChucVuProvider();
        // GET: User
        [Route("tat-ca-nguoi-dung")]
        public ActionResult AllUser()
        {
            var model = _nguoidung.GetAll();
            return View(model);
        }
        [Route("them-moi")]
        public ActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNew(NguoiDung model)
        {
            if (string.IsNullOrEmpty(model.TenDangNhap))
            {
                ModelState.AddModelError("TenDangNhap", "Không được để trống");
            }
            else if (model.TenDangNhap != null && !_nguoidung.CheckTenDangNhap(model.TenDangNhap))
            {
                ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại");
            }
            else if (string.IsNullOrEmpty(model.MatKhau))
            {
                ModelState.AddModelError("MatKhau", "Không được để trống");
            }
            else if (string.IsNullOrEmpty(model.HoTen))
            {
                ModelState.AddModelError("HoTen", "Không được để trống");
            }
            else
            {
                model.NguoiDungId = Guid.NewGuid();
                string salt = Guid.NewGuid().ToString().ToLower();
                model.Salt = salt;
                string matkhau = Data.Helpers.SecurityHelper.Hash(model.Salt + model.MatKhau).ToLower();
                model.MatKhau = matkhau;
                if (_nguoidung.Insert(model))
                {
                    
                    TempData["Alert"] = new Alert("Thêm mới người dùng <b>" + model.HoTen + "</b> thành công", Alert.AlertType.Success);
                    return RedirectToAction("AllUser", "Account", new {id = model.NguoiDungId.ToString()});
                }
                else
                {
                    ModelState.AddModelError("error", "Tạo mới không thành công vui lòng thử lại hoặc liên hệ với kĩ thuật.");
                }
            }
            return View(model);
        }
        public ActionResult GrantPermission(string id)
        {
            var model = _nguoidung.GetThongTinNguoiDung(id);
            return View(model);
        }
        public ActionResult DeleteNguoiDungChucVu(string NguoiDungId, int ChucVuId, string ReturnAction)
        {
            if (_lienketChucVuNguoiDung.Delete(NguoiDungId, ChucVuId))
            {
                TempData["Alert"] = new Alert("Gỡ bỏ chức vụ thành công", Alert.AlertType.Success);
            }
            else
            {
                TempData["Alert"] = new Alert("Gỡ bỏ chức vụ không thành công", Alert.AlertType.Fail);
            }
            return RedirectToAction(ReturnAction, "User", new { id = NguoiDungId });
        }
        public ActionResult InsertNguoiDungChucVu(string NguoiDungId, int ChucVuId, string PhongBanId, string Action)
        {

            if (_lienketChucVuNguoiDung.Insert(NguoiDungId, ChucVuId, PhongBanId) == 1)
            {
                TempData["Alert"] = new Alert("Thêm chức vụ thành công", Alert.AlertType.Success);
            }
            else if(_lienketChucVuNguoiDung.Insert(NguoiDungId, ChucVuId, PhongBanId) == 0)
            {
                TempData["Alert"] = new Alert("Người dùng đang giữ chức vụ này rồi", Alert.AlertType.Warning);
            }
            else
            {
                TempData["Alert"] = new Alert("Thêm chức vụ không thành công, vui lòng thử lại!", Alert.AlertType.Fail);
            }
            return RedirectToAction(Action, "User", new { id = NguoiDungId });
        }
        [Route("thong-tin-nguoi-dung")]
        public ActionResult UserProfile(string id)
        {
            var model = _nguoidung.GetThongTinNguoiDung(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateNguoiDung(NguoiDung model)
        {
            if (_nguoidung.UpdateInfo(model))
            {
                TempData["Alert"] = new Alert("Cập nhật thông tin người dùng <b>" + model.HoTen + "</b> thành công", Alert.AlertType.Success);
            }
            else
            {
                TempData["Alert"] = new Alert("Cập nhật thông tin người dùng <b>"+model.HoTen+"</b> không thành công", Alert.AlertType.Fail);
            }
            return RedirectToAction("UserProfile", "User", new { id = model.NguoiDungId.ToString() });
        }
    }
}