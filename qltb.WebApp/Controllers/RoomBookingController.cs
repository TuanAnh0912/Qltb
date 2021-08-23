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
    public class RoomBookingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Booking()
        {
            return View();
        }
    }
}