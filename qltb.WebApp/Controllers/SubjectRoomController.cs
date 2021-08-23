using qltb.Data;
using qltb.Data.Providers;
using qltb.Data.ReqVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qltb.WebApp.Controllers
{
    public class SubjectRoomController: Controller
    {
        PhongBanProvider _phongBanProvider = new PhongBanProvider();
        ToBoMonProvider _toBoMonProvider = new ToBoMonProvider();
        public ActionResult Index()
        {
            ViewBag.PhongBan = _phongBanProvider.GetAll();
            var lstAllSubjectRoom = _toBoMonProvider.GetAllToBoMon();
            return View(lstAllSubjectRoom);
        }
        [HttpPost]
        public JsonResult Insert(ToBoMon model)
        {
            var res = _toBoMonProvider.Insert(model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int  id)
        {
            var res = _toBoMonProvider.Delete(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(ToBoMonReqVM model)
        {
            var res = _toBoMonProvider.UpdateToBoMon(model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetToBoMon(int id )
        {
            var res = _toBoMonProvider.GetAllToBoMonById(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}