using qltb.Data.Providers;
using qltb.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qltb.WebApp.Controllers
{
    public class AuthController : Controller
    {
        NguoiDungProvider _nguoidungProvider = new NguoiDungProvider();
        ChucNangProvider _chucNang = new ChucNangProvider();
        // GET: Auth
        public ActionResult Login(string backURL)
        {
            ViewBag.backURL = backURL;
            var model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model, string backURL)
        {
            if (ModelState.IsValid)
            {
                var user = _nguoidungProvider.GetNguoiDungByTenDangNhap(model.TenDangNhap);
                if (user != null && user.IsActive == true)
                {
                    string pass = Data.Helpers.SecurityHelper.Hash(user.Salt + model.MatKhau).ToLower();
                    if (user.MatKhau.Equals(pass))
                    {
                        Session["UserName"] = user.TenDangNhap;
                        Session["FullName"] = user.HoTen;
                        Session["UserId"] = user.NguoiDungId;
                        Session["User"] = user;
                        Session["Menu"] = _chucNang.GetMunuByUser(user.NguoiDungId.ToString());
                        Response.Cookies["UserId"].Value = user.NguoiDungId + "";

                        var donVis = new DonViProvider().getAll();
                        if (donVis != null && donVis.Count>0)
                        {
                            Session["DonVi"] = donVis[0];
                        }
                        if (string.IsNullOrEmpty(backURL))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Redirect(backURL);
                        }
                    }
                    else
                    {
                        ViewBag.TenDangNhap = model.TenDangNhap;
                        TempData["message"] = "Mật khẩu không chính xác vui lòng thử lại";
                        return View(model);
                    }
                }
                else
                {
                    TempData["message"] = "Tên tài khoản không tồn tại vui lòng thử lại.";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}