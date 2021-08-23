using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qltb.WebApp.Models
{
    public class LoginModel
    {
        public int NguoiDungId { set; get; }
        public string TenDangNhap { set; get; }
        public string EmailNguoiDung { set; get; }
        public string MatKhau { set; get; }
    }
}