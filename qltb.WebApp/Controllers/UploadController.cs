using qltb.Data.ResVMs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qltb.WebApp.Controllers
{
    [RoutePrefix("upload")]
    public class UploadController : Controller
    {

        [HttpPost]
        [Route("upload-file-phieu-ghi-tang-thiet-bi")]
        public JsonResult UploadFilePhieuGhiTangThietBi(FormCollection formCollection, HttpPostedFileBase file)
        {
            try
            {
                FileResVM fileResVM = new FileResVM();
                fileResVM.FileId = Guid.NewGuid().ToString();
                fileResVM.TenFile = file.FileName;
                fileResVM.Ext = Path.GetExtension(file.FileName);
                string filePath = Path.Combine("Upload", "PhieuGhiTangThietBi", fileResVM.FileId + fileResVM.Ext);
                Directory.CreateDirectory(Path.Combine(Server.MapPath(Path.Combine("~", "Upload", "PhieuGhiTangThietBi"))));
                fileResVM.Url = Flurl.Url.Combine(filePath);
                fileResVM.Icon = GetIcon(fileResVM.Ext);
                if (file != null)
                {
                    file.SaveAs(Server.MapPath(Path.Combine("~", filePath)));
                }
                return Json(fileResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("upload-file-phieu-ghi-hong-thiet-bi")]
        public JsonResult UploadFilePhieuGhiHongThietBi(FormCollection formCollection, HttpPostedFileBase file)
        {
            try
            {
                FileResVM fileResVM = new FileResVM();
                fileResVM.FileId = Guid.NewGuid().ToString();
                fileResVM.TenFile = file.FileName;
                fileResVM.Ext = Path.GetExtension(file.FileName);
                string filePath = Path.Combine("Upload", "PhieuGhiHongThietBi", fileResVM.FileId + fileResVM.Ext);
                Directory.CreateDirectory(Path.Combine(Server.MapPath(Path.Combine("~", "Upload", "PhieuGhiHongThietBi"))));
                fileResVM.Url = Flurl.Url.Combine(filePath);
                fileResVM.Icon = GetIcon(fileResVM.Ext);
                if (file != null)
                {
                    file.SaveAs(Server.MapPath(Path.Combine("~", filePath)));
                }
                return Json(fileResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("upload-file-phieu-ghi-mat-thiet-bi")]
        public JsonResult UploadFilePhieuGhiMatThietBi(FormCollection formCollection, HttpPostedFileBase file)
        {
            try
            {
                FileResVM fileResVM = new FileResVM();
                fileResVM.FileId = Guid.NewGuid().ToString();
                fileResVM.TenFile = file.FileName;
                fileResVM.Ext = Path.GetExtension(file.FileName);
                string filePath = Path.Combine("Upload", "PhieuGhiMatThietBi", fileResVM.FileId + fileResVM.Ext);
                Directory.CreateDirectory(Path.Combine(Server.MapPath(Path.Combine("~", "Upload", "PhieuGhiMatThietBi"))));
                fileResVM.Url = Flurl.Url.Combine(filePath);
                fileResVM.Icon = GetIcon(fileResVM.Ext);
                if (file != null)
                {
                    file.SaveAs(Server.MapPath(Path.Combine("~", filePath)));
                }
                return Json(fileResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("upload-file-phieu-ghi-giam-thiet-bi")]
        public JsonResult UploadFilePhieuGhiGiamThietBi(FormCollection formCollection, HttpPostedFileBase file)
        {
            try
            {
                FileResVM fileResVM = new FileResVM();
                fileResVM.FileId = Guid.NewGuid().ToString();
                fileResVM.TenFile = file.FileName;
                fileResVM.Ext = Path.GetExtension(file.FileName);
                string filePath = Path.Combine("Upload", "PhieuGhiGiamThietBi", fileResVM.FileId + fileResVM.Ext);
                Directory.CreateDirectory(Path.Combine(Server.MapPath(Path.Combine("~", "Upload", "PhieuGhiGiamThietBi"))));
                fileResVM.Url = Flurl.Url.Combine(filePath);
                fileResVM.Icon = GetIcon(fileResVM.Ext);
                if (file != null)
                {
                    file.SaveAs(Server.MapPath(Path.Combine("~", filePath)));
                }
                return Json(fileResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("upload-file-phieu-kiem-ke")]
        public JsonResult UploadFilePhieuKiemKe(FormCollection formCollection, HttpPostedFileBase file)
        {
            try
            {
                FileResVM fileResVM = new FileResVM();
                fileResVM.FileId = Guid.NewGuid().ToString();
                fileResVM.TenFile = file.FileName;
                fileResVM.Ext = Path.GetExtension(file.FileName);
                string filePath = Path.Combine("Upload", "PhieuKiemKe", fileResVM.FileId + fileResVM.Ext);
                Directory.CreateDirectory(Path.Combine(Server.MapPath(Path.Combine("~", "Upload", "PhieuKiemKe"))));
                fileResVM.Url = Flurl.Url.Combine(filePath);
                fileResVM.Icon = GetIcon(fileResVM.Ext);
                if (file != null)
                {
                    file.SaveAs(Server.MapPath(Path.Combine("~", filePath)));
                }
                return Json(fileResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("upload-file-phieu-sua-chua")]
        public JsonResult UploadFilePhieuSuaChua(FormCollection formCollection, HttpPostedFileBase file)
        {
            try
            {
                FileResVM fileResVM = new FileResVM();
                fileResVM.FileId = Guid.NewGuid().ToString();
                fileResVM.TenFile = file.FileName;
                fileResVM.Ext = Path.GetExtension(file.FileName);
                string filePath = Path.Combine("Upload", "PhieuSuaChua", fileResVM.FileId + fileResVM.Ext);
                Directory.CreateDirectory(Path.Combine(Server.MapPath(Path.Combine("~", "Upload", "PhieuSuaChua"))));
                fileResVM.Url = Flurl.Url.Combine(filePath);
                fileResVM.Icon = GetIcon(fileResVM.Ext);
                if (file != null)
                {
                    file.SaveAs(Server.MapPath(Path.Combine("~", filePath)));
                }
                return Json(fileResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("upload-file-phieu-de-nghi-mua-sam")]
        public JsonResult UploadFilePhieuMuaSam(FormCollection formCollection, HttpPostedFileBase file)
        {
            try
            {
                FileResVM fileResVM = new FileResVM();
                fileResVM.FileId = Guid.NewGuid().ToString();
                fileResVM.TenFile = file.FileName;
                fileResVM.Ext = Path.GetExtension(file.FileName);
                string filePath = Path.Combine("Upload", "PhieuDeNghiMuaSam", fileResVM.FileId + fileResVM.Ext);
                Directory.CreateDirectory(Path.Combine(Server.MapPath(Path.Combine("~", "Upload", "PhieuDeNghiMuaSam"))));
                fileResVM.Url = Flurl.Url.Combine(filePath);
                fileResVM.Icon = GetIcon(fileResVM.Ext);
                if (file != null)
                {
                    file.SaveAs(Server.MapPath(Path.Combine("~", filePath)));
                }
                return Json(fileResVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public string GetIcon(string ext)
        {
            string url = "";
            switch (ext.ToLower())
            {
                case ".jpg":
                    url = "/Upload/FileTypes/jpg.png";
                    break;

                case ".jpeg":
                    url = "/Upload/FileTypes/jpg.png";
                    break;

                case ".png":
                    url = "/Upload/FileTypes/png.png";
                    break;

                case ".pdf":
                    url = "/Upload/FileTypes/pdf.png";
                    break;

                case ".xlsx":
                    url = "/Upload/FileTypes/excel.png";
                    break;

                case ".xls":
                    url = "/Upload/FileTypes/excel.png";
                    break;

                case ".doc":
                    url = "/Upload/FileTypes/doc.png";
                    break;

                case ".docx":
                    url = "/Upload/FileTypes/doc.png";
                    break;

                case ".ppt":
                    url = "/Upload/FileTypes/ppt.png";
                    break;

                case ".txt":
                    url = "/Upload/FileTypes/txt.png";
                    break;

                case ".zip":
                    url = "/Upload/FileTypes/zip.png";
                    break;

                default:
                    url = "/Upload/FileTypes/unknow.png";
                    break;
            }

            return url;
        }

    }
}