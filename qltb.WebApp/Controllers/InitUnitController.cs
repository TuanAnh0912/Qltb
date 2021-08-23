using qltb.Data;
using qltb.Data.Providers;
using qltb.WebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static qltb.Data.Helpers.HtmlHelper;

namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    public class InitUnitController : Controller
    {
        // GET: InitUnit
        public ActionResult InitUnit()
        {
            Data.DonVi model = null;
            if (Session["DonVi"] != null)
            {
                model = Session["DonVi"] as Data.DonVi;
                Redirect("/Home");
            }
            else
            {
                model = new Data.DonVi();
            }
            #region VIEW DATA
            Dictionary<int, string> KhoiTruongDictionary = new Dictionary<int, string>();

            KhoiTruongDictionary.Add(0, "--chọn--");
            var khoiTruongs = new Data.Providers.KhoiTruongProvider().getAll();
            foreach (var item in khoiTruongs)
            {
                KhoiTruongDictionary.Add(item.KhoiTruongId, item.TenKhoiTruong);
            }
            ViewData["KhoiTruongs"] = KhoiTruongDictionary;
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult InitUnit(Data.DonVi model)
        {
            #region VIEW DATA
            Dictionary<int, string> KhoiTruongDictionary = new Dictionary<int, string>();
            KhoiTruongDictionary.Add(0, "--chọn--");
            var khoiTruongs = new Data.Providers.KhoiTruongProvider().getAll();
            foreach (var item in khoiTruongs)
            {
                KhoiTruongDictionary.Add(item.KhoiTruongId, item.TenKhoiTruong);
            }
            ViewData["KhoiTruongs"] = KhoiTruongDictionary;
            #endregion
            Data.Providers.DonViProvider donViProvider = new Data.Providers.DonViProvider();
            if (string.IsNullOrEmpty(model.TenDonVi))
            {
                ModelState.AddModelError("TenDonVi", "Vui lòng nhập tên");
            }
            if (model.KhoiTruongId == null || model.KhoiTruongId == 0)
            {
                ModelState.AddModelError("KhoiTruongId", "Vui lòng chọn tiêu chuẩn phòng");
            }
            if (model.TieuChuanPhongId == null || model.TieuChuanPhongId == 0)
            {
                ModelState.AddModelError("TieuChuanPhongId", "Vui lòng chọn tiêu chuẩn phòng");
            }
            if (model.TieuChuanCoSoVatChatId == null || model.TieuChuanCoSoVatChatId == 0)
            {
                ModelState.AddModelError("TieuChuanCoSoVatChatId", "Vui lòng chọn tiêu chuẩn cơ sở vật chất");
            }
            if (ModelState.IsValid)
            {
                if (Session["DonVi"] != null)
                {
                    if (donViProvider.Update(model))
                    {
                        #region MON HOC
                        var monHocTieuChuans = new Data.Providers.MonHocTieuChuanProvider().getAllByKhoiTruongId(model.KhoiTruongId.Value);
                        if (monHocTieuChuans != null)
                        {
                            Data.Providers.MonHocProvider monHocProvider = new Data.Providers.MonHocProvider();
                            foreach (var item in monHocTieuChuans)
                            {
                                var monHoc = new Data.Providers.MonHocProvider().getByMaMonHoc(item.MaMonHoc);
                                if (monHoc == null)
                                {
                                    monHoc = new Data.MonHoc()
                                    {
                                        MaMonHoc = item.MaMonHoc,
                                        TenMonHoc = item.TenMonHoc,
                                        IsDelete = false
                                    };
                                    new Data.Providers.MonHocProvider().Insert(monHoc);
                                }
                            }
                        }
                        #endregion
                        #region KHOI LOP
                        var khoiLopTieuChuans = new Data.Providers.KhoiLopTieuChuanProvider().getAllByKhoiTruongId(model.KhoiTruongId.Value);
                        if (khoiLopTieuChuans != null)
                        {
                            Data.Providers.KhoiLopProvider khoiLopProvider = new Data.Providers.KhoiLopProvider();
                            foreach (var item in khoiLopTieuChuans)
                            {
                                var khoiLop = new Data.Providers.KhoiLopProvider().getByMaKhoiLop(item.MaKhoiLop);
                                if (khoiLop == null)
                                {
                                    khoiLop = new Data.KhoiLop()
                                    {
                                        TenKhoiLop = item.TenKhoiLop,
                                        MaKhoiLop = item.MaKhoiLop,
                                        KhoiLopId =model.KhoiTruongId.Value
                                    };
                                    khoiLopProvider.Insert(khoiLop);
                                }
                            }
                        }
                        #endregion
                        #region CREATE THIET BI

                        var loaiThietBiTieuChuans = new Data.Providers.LoaiThietBiTieuChuanProvider().getAll();
                        if (loaiThietBiTieuChuans != null)
                        {
                            Data.Providers.ThietBiTieuChuanProvider thietBiTieuChuanProvider = new Data.Providers.ThietBiTieuChuanProvider();
                            foreach (var item in loaiThietBiTieuChuans)
                            {
                                var loaiThietBi = new Data.Providers.LoaiThietBiProvider().getByMaLoaiThietBi(item.MaLoaiThietBiTieuChuan);
                                if (loaiThietBi == null)
                                {
                                    loaiThietBi = new Data.LoaiThietBi()
                                    {
                                        MaLoaiThietBi = item.MaLoaiThietBiTieuChuan,
                                        TenLoaiThietBi = item.TenLoaiThietBi,
                                    };
                                    new Data.Providers.LoaiThietBiProvider().Insert(loaiThietBi);
                                }
                                loaiThietBi = new Data.Providers.LoaiThietBiProvider().getByMaLoaiThietBi(loaiThietBi.MaLoaiThietBi);

                                var thietBiTieuChuans = thietBiTieuChuanProvider.getAllByLoaiThietBiTieuChuanIdAndKhoiTruongId(item.LoaiThietBiTieuChuanId, model.KhoiTruongId.Value);

                                if (thietBiTieuChuans != null)
                                {
                                    
                                    foreach (var thietBiTieuChuan in thietBiTieuChuans)
                                    {
                                        var thietBi = new Data.Providers.ThietBiProvider().getByMaThietBi(thietBiTieuChuan.MaThietBi);
                                        if (thietBi == null)
                                        {
                                            thietBi = new Data.ThietBi()
                                            {
                                                MaThietBi = thietBiTieuChuan.MaThietBi,
                                                TenThietBi = thietBiTieuChuan.TenThietBi,
                                                LoaiThietBiId = loaiThietBi.LoaiThietBiId,
                                                DonViId = thietBiTieuChuan.DonViId,
                                                DonViTinhId = thietBiTieuChuan.DonViTinhId,
                                                SoLuongToiThieu = thietBiTieuChuan.SoLuongToiThieu,
                                                IsDelete = false
                                            };
                                            if (thietBiTieuChuan.MonHocTieuChuanId.HasValue)
                                            {
                                                var monHoc = new Data.Providers.MonHocProvider().getByMaMonHoc(thietBiTieuChuan.MonHocTieuChuan.MaMonHoc);
                                                thietBi.MonHocId = monHoc.MonHocId;
                                            }
                                            if(new Data.Providers.ThietBiProvider().Insert(thietBi))
                                            {
                                                thietBi = new Data.Providers.ThietBiProvider().getByMaThietBi(thietBi.MaThietBi);
                                                if (!string.IsNullOrEmpty(thietBiTieuChuan.KhoiLopTieuChuanId))
                                                {
                                                    var khoiLopTieuChuanIds = thietBiTieuChuan.KhoiLopTieuChuanId.Split(',').Select(Int32.Parse).ToList();
                                                    Data.Providers.KhoiLopTieuChuanProvider khoiLopTieuChuanProvider = new Data.Providers.KhoiLopTieuChuanProvider();
                                                    Data.Providers.LienKetKhoiLopThietBiProvider lienKetKhoiLopThietBiProvider = new Data.Providers.LienKetKhoiLopThietBiProvider();
                                                    foreach (var khoiLopTieuChuanId in khoiLopTieuChuanIds)
                                                    {
                                                        var khoiLopTieuChuan = khoiLopTieuChuanProvider.getById(khoiLopTieuChuanId);
                                                        var khoiLop = new Data.Providers.KhoiLopProvider().getByMaKhoiLop(khoiLopTieuChuan.MaKhoiLop);
                                                        Data.LienKetKhoiLopThietBi lienKetKhoiLopThietBi = new LienKetKhoiLopThietBi()
                                                        {
                                                            KhoiLopId = khoiLop.KhoiLopId,
                                                            ThietBiId = thietBi.ThietBiId
                                                        };
                                                        lienKetKhoiLopThietBiProvider.Insert(lienKetKhoiLopThietBi);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #region CREATE PHONG
                        var khoiPhongTieuChuans = new Data.Providers.KhoiPhongTieuChuanProvider().getByTieuChuanId(model.TieuChuanCoSoVatChatId.Value);
                        if (khoiPhongTieuChuans != null)
                        {
                            Data.Providers.PhongProvider phongProvider = new Data.Providers.PhongProvider();
                            foreach (var item in khoiPhongTieuChuans)
                            {
                                var loaiKhoPhong = new Data.Providers.LoaiKhoPhongProvider().getByMaLoaiKhoPhong(item.MaKhoiPhongTieuChuan);
                                if (loaiKhoPhong == null)
                                {
                                    loaiKhoPhong = new Data.LoaiKhoPhong()
                                    {
                                        MaLoaiKhoPhong = item.MaKhoiPhongTieuChuan,
                                        TenLoaiKhoPhong = item.TenKhoiPhong,
                                    };
                                    new Data.Providers.LoaiKhoPhongProvider().Insert(loaiKhoPhong);
                                }
                                loaiKhoPhong = new Data.Providers.LoaiKhoPhongProvider().getByMaLoaiKhoPhong(loaiKhoPhong.MaLoaiKhoPhong);
                                var phongs = phongProvider.getAllByKhoiPhongTieuChuanId(item.KhoiPhongTieuChuanId);
                                if (phongs != null)
                                {
                                    foreach (var phong in phongs)
                                    {
                                        var khoPhong = new Data.Providers.KhoPhongProvider().getByMaKhoPhong(phong.MaPhong);
                                        if (khoPhong == null)
                                        {
                                            khoPhong = new Data.KhoPhong()
                                            {
                                                MaKhoPhong = phong.MaPhong,
                                                TenKhoPhong = phong.TenPhong,
                                                DienTich = phong.DienTich,
                                                LoaiKhoPhongId = loaiKhoPhong.LoaiKhoPhongId,
                                                IsDelete = false
                                            };
                                            new Data.Providers.KhoPhongProvider().Insert(khoPhong);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        TempData["Alert"] = new Alert("Cập nhật thành công", Alert.AlertType.Success);
                    }
                    else
                    {
                        TempData["Alert"] = new Alert("Đã có lỗi xảy ra vui lòng liên hệ với quản trị viên.", Alert.AlertType.Fail);
                    }
                }
                else
                {
                    model.DonViId = Guid.NewGuid();
                    if (donViProvider.Insert(model))
                    {
                        donViProvider = new DonViProvider();
                        Session["DonVi"] = donViProvider.getById(model.DonViId);
                        #region MON HOC
                        var monHocTieuChuans = new Data.Providers.MonHocTieuChuanProvider().getAllByKhoiTruongId( model.KhoiTruongId.Value);
                        if (monHocTieuChuans != null)
                        {
                            Data.Providers.MonHocProvider monHocProvider = new Data.Providers.MonHocProvider();
                            foreach (var item in monHocTieuChuans)
                            {
                                Data.MonHoc monHoc = new Data.MonHoc()
                                {
                                    MaMonHoc = item.MaMonHoc,
                                    TenMonHoc = item.TenMonHoc,
                                    IsDelete = false
                                };
                                monHocProvider.Insert(monHoc);
                            }
                        }
                        #endregion
                        #region KHOI LOP
                        var khoiLopTieuChuans = new Data.Providers.KhoiLopTieuChuanProvider().getAllByKhoiTruongId(model.KhoiTruongId.Value);
                        if (khoiLopTieuChuans != null)
                        {
                            Data.Providers.KhoiLopProvider khoiLopProvider = new Data.Providers.KhoiLopProvider();
                            foreach (var item in khoiLopTieuChuans)
                            {
                                Data.KhoiLop khoiLop = new Data.KhoiLop()
                                {
                                    TenKhoiLop = item.TenKhoiLop,
                                    MaKhoiLop = item.MaKhoiLop,
                                    KhoiLopId = model.KhoiTruongId.Value
                                };
                                khoiLopProvider.Insert(khoiLop);
                            }
                        }
                        #endregion
                        #region CREATE THIET BI
                        var loaiThietBiTieuChuans = new Data.Providers.LoaiThietBiTieuChuanProvider().getAll();
                        if (loaiThietBiTieuChuans != null)
                        {
                            Data.Providers.ThietBiTieuChuanProvider thietBiTieuChuanProvider = new Data.Providers.ThietBiTieuChuanProvider();
                            foreach (var item in loaiThietBiTieuChuans)
                            {
                                Data.LoaiThietBi loaiThietBi = new Data.LoaiThietBi()
                                {
                                    MaLoaiThietBi = item.MaLoaiThietBiTieuChuan,
                                    TenLoaiThietBi = item.TenLoaiThietBi,
                                };
                                new Data.Providers.LoaiThietBiProvider().Insert(loaiThietBi);
                                loaiThietBi = new Data.Providers.LoaiThietBiProvider().getByMaLoaiThietBi(loaiThietBi.MaLoaiThietBi);

                                var thietBiTieuChuans = thietBiTieuChuanProvider.getAllByLoaiThietBiTieuChuanIdAndKhoiTruongId(item.LoaiThietBiTieuChuanId,model.KhoiTruongId.Value);
                                if (thietBiTieuChuans != null)
                                {
                                    foreach (var thietBiTieuChuan in thietBiTieuChuans)
                                    {
                                        Data.ThietBi thietBi = new Data.ThietBi()
                                        {
                                            MaThietBi = thietBiTieuChuan.MaThietBi,
                                            TenThietBi = thietBiTieuChuan.TenThietBi,
                                            LoaiThietBiId = loaiThietBi.LoaiThietBiId,
                                            DonViId = thietBiTieuChuan.DonViId,
                                            DonViTinhId = thietBiTieuChuan.DonViTinhId,
                                            SoLuongToiThieu = thietBiTieuChuan.SoLuongToiThieu,
                                            IsDelete = false
                                        };
                                        if (thietBiTieuChuan.MonHocTieuChuanId.HasValue)
                                        {
                                            var monHoc = new Data.Providers.MonHocProvider().getByMaMonHoc(thietBiTieuChuan.MonHocTieuChuan.MaMonHoc);
                                            thietBi.MonHocId = monHoc.MonHocId;
                                        }
                                        if(new Data.Providers.ThietBiProvider().Insert(thietBi))
                                            {
                                            thietBi = new Data.Providers.ThietBiProvider().getByMaThietBi(thietBi.MaThietBi);
                                            if (!string.IsNullOrEmpty(thietBiTieuChuan.KhoiLopTieuChuanId))
                                            {
                                                var khoiLopTieuChuanIds = thietBiTieuChuan.KhoiLopTieuChuanId.Split(',').Select(Int32.Parse).ToList();
                                                Data.Providers.KhoiLopTieuChuanProvider khoiLopTieuChuanProvider = new Data.Providers.KhoiLopTieuChuanProvider();
                                                Data.Providers.LienKetKhoiLopThietBiProvider lienKetKhoiLopThietBiProvider = new Data.Providers.LienKetKhoiLopThietBiProvider();
                                                foreach (var khoiLopTieuChuanId in khoiLopTieuChuanIds)
                                                {
                                                    var khoiLopTieuChuan = khoiLopTieuChuanProvider.getById(khoiLopTieuChuanId);
                                                    var khoiLop = new Data.Providers.KhoiLopProvider().getByMaKhoiLop(khoiLopTieuChuan.MaKhoiLop);
                                                    Data.LienKetKhoiLopThietBi lienKetKhoiLopThietBi = new LienKetKhoiLopThietBi()
                                                    {
                                                        KhoiLopId = khoiLop.KhoiLopId,
                                                        ThietBiId = thietBi.ThietBiId
                                                    };
                                                    lienKetKhoiLopThietBiProvider.Insert(lienKetKhoiLopThietBi);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #region CREATE PHONG
                        var khoiPhongTieuChuans = new Data.Providers.KhoiPhongTieuChuanProvider().getByTieuChuanId(model.TieuChuanCoSoVatChatId.Value);
                        if (khoiPhongTieuChuans != null)
                        {
                            Data.Providers.PhongProvider phongProvider = new Data.Providers.PhongProvider();
                            foreach (var item in khoiPhongTieuChuans)
                            {
                                Data.LoaiKhoPhong loaiKhoPhong = new Data.LoaiKhoPhong()
                                {
                                    MaLoaiKhoPhong = item.MaKhoiPhongTieuChuan,
                                    TenLoaiKhoPhong = item.TenKhoiPhong
                                };
                                new Data.Providers.LoaiKhoPhongProvider().Insert(loaiKhoPhong);

                                loaiKhoPhong = new Data.Providers.LoaiKhoPhongProvider().getByMaLoaiKhoPhong(loaiKhoPhong.MaLoaiKhoPhong);

                                var phongs = phongProvider.getAllByKhoiPhongTieuChuanId(item.KhoiPhongTieuChuanId);
                                if (phongs != null)
                                {
                                    foreach (var phong in phongs)
                                    {
                                        Data.KhoPhong khoPhong = new Data.KhoPhong()
                                        {
                                            MaKhoPhong = phong.MaPhong,
                                            TenKhoPhong = phong.TenPhong,
                                            DienTich = phong.DienTich,
                                            LoaiKhoPhongId = loaiKhoPhong.LoaiKhoPhongId,
                                            IsDelete = false
                                        };
                                        new Data.Providers.KhoPhongProvider().Insert(khoPhong);
                                    }
                                }
                            }
                        }
                        #endregion
                        return Redirect("/trang-chu/tong-quan-he-thong");
                    }
                    else
                    {
                        TempData["Alert"] = new Alert("Đã có lỗi xảy ra vui lòng liên hệ với quản trị viên.", Alert.AlertType.Fail);
                    }
                }
            }
            return View(model);
        }

        public ActionResult SelectTieuChuanPhong(int KhoiTruongId = 0, int value = 0)
        {
            ViewBag.selected = value;
            return View(new Data.Providers.TieuChuanPhongProvider().getAll(KhoiTruongId));
        }
        public ActionResult SelectTieuChuanCoSoVatChat(int KhoiTruongId = 0, int value = 0)
        {
            ViewBag.selected = value;
            return View(new Data.Providers.TieuChuanCoSoVaChatProvider().getAll(KhoiTruongId));
        }
        #region TIEU CHUAN PHONG
        public ActionResult Standard(int KhoiTruongId = 0)
        {
            var model = new Data.Providers.TieuChuanPhongProvider().getAll(KhoiTruongId);
            return View(model);
        }
        #endregion
        #region THIET BỊ
        public ActionResult Device(int KhoiTruongId = 0)
        {
            var model = new Data.Providers.TieuChuanCoSoVaChatProvider().getAll(KhoiTruongId);
            return View(model);
        }
        #endregion 

        public ActionResult AddThietBiTieuChuan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddThietBiTieuChuan(ThietBiTieuChuan model)
        {
            var res = new ThietBiTieuChuanProvider().Insert(model);
            if (res)
            {
                TempData["message"] = "Thêm thành công";
            }
            else
            {
                TempData["message"] = "Thêm không thành công";
            }
            return View();
        }
    }
}