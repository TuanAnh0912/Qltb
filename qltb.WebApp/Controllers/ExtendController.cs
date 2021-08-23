using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qltb.WebApp.Controllers
{
    public class ExtendController : Controller
    {
        // GET: Extend
        public ActionResult SelectTinh(string value="")
        {
            ViewBag.selected = value;
            return View(new Data.Providers.TinhProvider().getAll());
        }
        public ActionResult SelectHuyen(string id="",string value="")
        {
            ViewBag.selected = value;
            return View(new Data.Providers.HuyenProvider().getAllStartWithId(id));
        }
        public ActionResult SelectXa(string id="", string value="")
        {
            ViewBag.selected = value;
            return View(new Data.Providers.XaPovider().getAllStartWithId(id));
        }
    }
}