using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qltb.Data.ViewModels;

namespace qltb.Data.Providers
{
    public class PhieuMuonProvider : ApplicationDbcontext
    {
        GetForSelectProvider _get = new GetForSelectProvider();
        KhoThietBiProvider _khothietbi = new KhoThietBiProvider();
        LogPhieuMuonProvider _log = new LogPhieuMuonProvider();

        public KhoThietBi GetKhoTBByThietBiAndKhoPhong(int ThietBiId, int KhoPhongId)
        {
            try
            {
                return DbContext.KhoThietBis.FirstOrDefault(k => k.KhoPhongId == KhoPhongId & k.ThietBiId == ThietBiId);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public PhieuMuonViewModel GetToAdd()
        {
            try
            {
                var model = new PhieuMuonViewModel();
                model.GiaoViens = new GiaoVienProvider().GetAllGiaoVien();
                model.BaiHocCoSuDungThietBis = new BaiHocSuDungThietBiProvider().GettAllBaiHocSuDungTB();
                model.ChuongTrinhHocs = _get.GetChuongTrinhHocs();
                model.MonHocs = _get.GetMonHocs();
                model.KhoiLops = _get.GetKhoiLops();
                model.Lops = _get.GetLops();
                model.MucDichSuDungs = _get.GetMucDichSuDungs();
                model.TietHocs = _get.GetTietHocs();
                model.KhoPhongs = _get.GetKhoPhongs();
                model.LoaiThietBis = _get.GetLoaiThietBis();
                model.LoaiPhongs = _get.GetLoaiKhoPhong();
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ResultViewModel Insert(PhieuMuonViewModel model)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var phieuMuon = new PhieuMuon();
                    var code = Helpers.RamdomHelper.RandomString(10);
                    phieuMuon.MaPhieuMuon = code;
                    phieuMuon.ChuongTrinhHocId = model.ChuongTrinhHocId;
                    phieuMuon.KhoiLopId = model.KhoiLopId;
                    phieuMuon.GiaoVienId = model.GiaoVienId;
                    phieuMuon.MucDichSuDungId = model.MucDichSuDungId;
                    phieuMuon.MonHocId = model.MonHocId;
                    phieuMuon.LopHocId = model.LopHocId;
                    phieuMuon.NgayTao = DateTime.Now;
                    phieuMuon.NgayMuon = model.NgayMuon;
                    phieuMuon.KhoPhongId = model.KhoPhongId;
                    phieuMuon.NguoiTao = model.NguoiTao;
                    phieuMuon.NgayTra = model.NgayTra;
                    phieuMuon.GhiChu = model.GhiChu;
                    phieuMuon.BaiHocId = model.BaiHocId;
                    phieuMuon.LoaiPhieuMuonId = model.LoaiPhieuMuonId;
                    phieuMuon.IsDelete = false;
                    phieuMuon.TrangThaiPhieuMuonId = 3;
                    phieuMuon.NguoiMuonNhan = false;
                    phieuMuon.NguoiMuonTra = false;
                    phieuMuon.NguoiChoMuonGiao = false;
                    phieuMuon.NguoiChoMuonNhan = false;
                    DbContext.PhieuMuons.Add(phieuMuon);

                    var lienketTietHocPhieuMuon = new List<LienKetTietHocPhieuMuon>();
                    foreach (var item in model.TietHocs)
                    {
                        var obj = new LienKetTietHocPhieuMuon();
                        obj.MaPhieuMuon = code;
                        obj.TietHocId = item.TietHocId;
                        lienketTietHocPhieuMuon.Add(obj);
                    }
                    DbContext.LienKetTietHocPhieuMuons.AddRange(lienketTietHocPhieuMuon);

                    if (model.ThietBis != null && model.ThietBis.Count() > 0)
                    {
                        var lienKetThietbiPhieuMuon = new List<LienKetThietBiPhieuMuon>();
                        foreach (var item in model.ThietBis)
                        {
                            var obj = new LienKetThietBiPhieuMuon();
                            obj.MaPhieuMuon = code;
                            obj.ThietBiId = item.ThietBiId;
                            obj.KhoPhongId = item.KhoPhongId;
                            obj.SoLuong = item.SoLuong;
                            lienKetThietbiPhieuMuon.Add(obj);
                        }
                        DbContext.LienKetThietBiPhieuMuons.AddRange(lienKetThietbiPhieuMuon);
                    }
                    var log = new LogPhieuMuon();
                    log.NguoiDung = model.NguoiTao.ToString();
                    log.ThoiGian = DateTime.Now;
                    log.HanhDong = "Đã tạo phiếu " + code;
                    log.MaPhieuMuon = code;
                    log.TieuDe = "Tạo phiếu mượn thiết bị";
                    DbContext.LogPhieuMuons.Add(log);

                    DbContext.SaveChanges();

                    transaction.Commit();
                    return new ResultViewModel(true, "Thêm mới phiếu mượn thiết bị thành công!", "success");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new ResultViewModel(false, e.Message, "error");
                }
            }
        }
        public List<PhieuMuon> GetAll(int LoaiPhieu)
        {
            try
            {
                return DbContext.PhieuMuons.Where(p => p.IsDelete == false && p.LoaiPhieuMuonId == LoaiPhieu).OrderByDescending(p => p.NgayTao).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public PhieuMuonViewModel GetByMaPhieu(string MaPhieu)
        {
            try
            {
                var command = "SELECT * FROM PhieuMuon as p WHERE p.IsDelete = 0 and p.MaPhieuMuon = '" + MaPhieu + "'";
                var model = DbContext.Database.SqlQuery<PhieuMuonViewModel>(command).FirstOrDefault();
                model.GiaoViens = new GiaoVienProvider().GetAllGiaoVien();
                model.BaiHocCoSuDungThietBis = new BaiHocSuDungThietBiProvider().GettAllBaiHocSuDungTB();
                model.ChuongTrinhHocs = _get.GetChuongTrinhHocs();
                model.MonHocs = _get.GetMonHocs();
                model.KhoiLops = _get.GetKhoiLops();
                model.Lops = _get.GetLops();
                model.BuoiTrongNgay = GetBuoiTrongNgayByPhieuMuon(MaPhieu);
                model.MucDichSuDungs = _get.GetMucDichSuDungs();
                model.TietHocs = _get.GetTietHocs();
                model.TietHocTrongPhieu = GetTietHocByPhieuMuon(MaPhieu);
                model.TietHocDaChon = GetTietHocByPhieuMuon2(MaPhieu);
                model.KhoPhongs = _get.GetKhoPhongs();
                model.LoaiThietBis = _get.GetLoaiThietBis();
                model.ThietBis = GetByPhieuMuon(MaPhieu);
                model.LoaiPhongs = _get.GetLoaiKhoPhong();
                model.PhongMuon = _get.GetByMaPhieuMuon(MaPhieu);
                model.Logs = _log.GetByMaPhieuMuon(MaPhieu);
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public PhieuMuon GetPhieuMuonByMaPhieu(string MaPhieu)
        {
            try
            {
                return DbContext.PhieuMuons.FirstOrDefault(p => p.MaPhieuMuon == MaPhieu);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<int> GetTietHocByPhieuMuon(string MaPhieu)
        {
            try
            {
                return DbContext.LienKetTietHocPhieuMuons.Where(l => l.MaPhieuMuon == MaPhieu).Select(l => l.TietHocId.Value).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<TietHocTrongPhieuMuonViewModel> GetTietHocByPhieuMuon2(string MaPhieu)
        {
            try
            {
                var command = "select * from LienKetTietHocPhieuMuon as lk inner join TietHoc as t on lk.TietHocId = t.TietHocId where lk.MaPhieuMuon = '" + MaPhieu + "'";
                return DbContext.Database.SqlQuery<TietHocTrongPhieuMuonViewModel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<int> GetBuoiTrongNgayByPhieuMuon(string MaPhieu)
        {
            try
            {
                var command = "select t.BuoiTrongNgay from LienKetTietHocPhieuMuon as lk inner join TietHoc as t on lk.TietHocId = t.TietHocId where lk.MaPhieuMuon = '" + MaPhieu + "' GROUP BY t.BuoiTrongNgay";
                return DbContext.Database.SqlQuery<int>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<SelectThietBiViewmodel> GetByPhieuMuon(string MaPhieuMuon)
        {
            try
            {
                var command = @"SELECT lk.SoLuong, t.ThietBiId, t.MaThietBi, t.TenThietBi, l.TenLoaiThietBi, kp.TenKhoPhong, m.MonHocId, 
                                m.TenMonHoc, kp.KhoPhongId, l.LoaiThietBiId FROM LienKetThietBiPhieuMuon as lk inner join ThietBi as t on lk.ThietBiId = t.ThietBiId inner join MonHoc as m
                                on t.MonHocId = m.MonHocId inner join LoaiThietBi as l on t.LoaiThietBiId = l.LoaiThietBiId inner join KhoPhong as kp on lk.KhoPhongId = kp.KhoPhongId
                                WHERE lk.MaPhieuMuon = '" + MaPhieuMuon + "'";
                return DbContext.Database.SqlQuery<SelectThietBiViewmodel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ResultViewModel UpdateSoLuongMuonThietBi(LienKetThietBiPhieuMuon model, string user)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var old = DbContext.LienKetThietBiPhieuMuons.FirstOrDefault(l => l.MaPhieuMuon == model.MaPhieuMuon && l.ThietBiId == model.ThietBiId && l.KhoPhongId == model.KhoPhongId);
                    var slChange = old.SoLuong - model.SoLuong;
                    var tb = DbContext.ThietBis.FirstOrDefault(t => t.ThietBiId == model.ThietBiId);
                    old.SoLuong = model.SoLuong;
                    DbContext.SaveChanges();
                    var res = _khothietbi.TangSoLuongKhoThietBi(model.ThietBiId.Value, model.KhoPhongId.Value, slChange.Value);
                    if (res.Status)
                    {
                        var log = new LogPhieuMuon();
                        log.NguoiDung = user;
                        log.ThoiGian = DateTime.Now;
                        log.HanhDong = "Đã cập nhật số lượng thiết bị "+tb.TenThietBi+" từ " + old.SoLuong + " thành " + model.SoLuong;
                        log.MaPhieuMuon = model.MaPhieuMuon;
                        log.TieuDe = "Cập nhật phiếu";
                        DbContext.LogPhieuMuons.Add(log);

                        transaction.Commit();
                        return new ResultViewModel(true, "Cập nhật số lượng thành công", "success");
                    }
                    else
                    {
                        transaction.Rollback();
                        return new ResultViewModel(false, res.Message, "error");
                    }

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new ResultViewModel(false, e.Message, "error");
                }
            }
        }
        public ResultViewModel UpdatePhieuMuon(PhieuMuonViewModel model, string user)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var phieuMuon = GetPhieuMuonByMaPhieu(model.MaPhieuMuon);
                    phieuMuon.ChuongTrinhHocId = model.ChuongTrinhHocId;
                    phieuMuon.KhoiLopId = model.KhoiLopId;
                    phieuMuon.MucDichSuDungId = model.MucDichSuDungId;
                    phieuMuon.MonHocId = model.MonHocId;
                    phieuMuon.LopHocId = model.LopHocId;
                    phieuMuon.NgayMuon = model.NgayMuon;
                    phieuMuon.KhoPhongId = model.KhoPhongId;
                    phieuMuon.NgayTra = model.NgayTra;
                    phieuMuon.GhiChu = model.GhiChu;
                    phieuMuon.BaiHocId = model.BaiHocId;
                    phieuMuon.LoaiPhieuMuonId = model.LoaiPhieuMuonId;
                    if (model.IsDelete.HasValue)
                    {
                        phieuMuon.IsDelete = model.IsDelete;
                    }
                    phieuMuon.TrangThaiPhieuMuonId = model.TrangThaiPhieuMuonId;

                    var lk = DbContext.LienKetTietHocPhieuMuons.Where(l => l.MaPhieuMuon == model.MaPhieuMuon);
                    DbContext.LienKetTietHocPhieuMuons.RemoveRange(lk);

                    var lienketTietHocPhieuMuon = new List<LienKetTietHocPhieuMuon>();
                    foreach (var item in model.TietHocs)
                    {
                        var obj = new LienKetTietHocPhieuMuon();
                        obj.MaPhieuMuon = model.MaPhieuMuon;
                        obj.TietHocId = item.TietHocId;
                        lienketTietHocPhieuMuon.Add(obj);
                    }
                    DbContext.LienKetTietHocPhieuMuons.AddRange(lienketTietHocPhieuMuon);

                    var log = new LogPhieuMuon();
                    log.NguoiDung = user;
                    log.ThoiGian = DateTime.Now;
                    log.HanhDong = "Đã cập nhật thông tin phiếu " + model.MaPhieuMuon;
                    log.MaPhieuMuon = model.MaPhieuMuon;
                    log.TieuDe = "Cập nhật phiếu";
                    DbContext.LogPhieuMuons.Add(log);

                    DbContext.SaveChanges();

                    transaction.Commit();
                    return new ResultViewModel(true, "Cập nhật phiếu mượn thành công", "success");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new ResultViewModel(false, e.Message, "error");
                }
            }
        }
        public ResultViewModel XacNhanMuonTra(bool ? NguoiMuonNhan, bool ? NguoiMuonTra, bool ? NguoiChoMuonGiao, bool ? NguoiChoMuonNhan, string MaPhieu, string user)
        {
            try
            {
                var log = new LogPhieuMuon();
                var model = GetPhieuMuonByMaPhieu(MaPhieu);
                if (NguoiMuonNhan.HasValue)
                {
                    model.NguoiMuonNhan = NguoiMuonNhan.Value;
                    if (model.NguoiChoMuonGiao.Value)
                    {
                        model.TrangThaiPhieuMuonId = 1;
                    }
                    
                    log.NguoiDung = user;
                    log.ThoiGian = DateTime.Now;
                    if(model.LoaiPhieuMuonId == 1)
                    {
                        log.HanhDong = "Đã nhận thiết bị bàn giao";
                    }
                    else
                    {
                        log.HanhDong = "Đã nhận phòng bàn giao";
                    }
                    
                    log.MaPhieuMuon = model.MaPhieuMuon;
                    log.TieuDe = "Xác nhận";
                    DbContext.LogPhieuMuons.Add(log);
                }
                if (NguoiMuonTra.HasValue)
                {
                    model.NguoiMuonTra = NguoiMuonTra.Value;
                    if (model.NguoiChoMuonNhan.Value)
                    {
                        model.TrangThaiPhieuMuonId = 2;
                    }
                    log.NguoiDung = user;
                    log.ThoiGian = DateTime.Now;
                    if (model.LoaiPhieuMuonId == 1)
                    {
                        log.HanhDong = "Đã bàn giao trả thiết bị";
                    }
                    else
                    {
                        log.HanhDong = "Đã bàn giao trả phòng";
                    }
                    log.MaPhieuMuon = model.MaPhieuMuon;
                    log.TieuDe = "Xác nhận";
                    DbContext.LogPhieuMuons.Add(log);
                }
                if (NguoiChoMuonGiao.HasValue)
                {
                    model.NguoiChoMuonGiao = NguoiChoMuonGiao.Value;
                    if (model.NguoiMuonNhan.Value)
                    {
                        model.TrangThaiPhieuMuonId = 1;
                    }
                    log.NguoiDung = user;
                    log.ThoiGian = DateTime.Now;
                    if (model.LoaiPhieuMuonId == 1)
                    {
                        log.HanhDong = "Đã bàn giao thiết bị cho mượn";
                    }
                    else
                    {
                        log.HanhDong = "Đã bàn giao phòng cho mượn";
                    }
                    
                    log.MaPhieuMuon = model.MaPhieuMuon;
                    log.TieuDe = "Xác nhận";
                    DbContext.LogPhieuMuons.Add(log);
                }
                if (NguoiChoMuonNhan.HasValue)
                {
                    model.NguoiChoMuonNhan = NguoiChoMuonNhan.Value;
                    if (model.NguoiChoMuonNhan.Value)
                    {
                        model.TrangThaiPhieuMuonId = 2;
                    }
                    log.NguoiDung = user;
                    log.ThoiGian = DateTime.Now;
                    if (model.LoaiPhieuMuonId == 1)
                    {
                        log.HanhDong = "Đã nhận bàn giao trả thiết bị từ người mượn";
                    }
                    else
                    {
                        log.HanhDong = "Đã nhận bàn giao trả phòng từ người mượn";
                    }
                    log.MaPhieuMuon = model.MaPhieuMuon;
                    log.TieuDe = "Xác nhận";
                    DbContext.LogPhieuMuons.Add(log);
                }
                DbContext.SaveChanges();
                return new ResultViewModel(false, "Xác nhận thành công!", "success");
            }
            catch (Exception e)
            {
                return new ResultViewModel(false, e.Message, "error");
            }
        }
        public ResultViewModel AddThietBiToPhieu(List<SelectThietBiViewmodel> ThietBis, string MaPhieuMuon, string user)
        {
            try
            {
                var lst = new List<LienKetThietBiPhieuMuon>();
                var log = new LogPhieuMuon();
                log.MaPhieuMuon = MaPhieuMuon;
                log.TieuDe = "Thay đổi thiết bị";
                log.NguoiDung = user;
                log.ThoiGian = DateTime.Now;
                string noidungthaydoi = "Thêm thiết bị: ";
                foreach(var item in ThietBis)
                {
                    var obj = new LienKetThietBiPhieuMuon();
                    obj.MaPhieuMuon = MaPhieuMuon;
                    obj.KhoPhongId = item.KhoPhongId;
                    obj.SoLuong = item.SoLuong;
                    obj.ThietBiId = item.ThietBiId;
                    lst.Add(obj);
                    noidungthaydoi += item.TenThietBi + " số lượng: " + item.SoLuong +", ";
                }
                log.HanhDong = noidungthaydoi;
                DbContext.LienKetThietBiPhieuMuons.AddRange(lst);
                DbContext.LogPhieuMuons.Add(log);
                DbContext.SaveChanges();
                return new ResultViewModel(true, "Cập nhật thiết bị thành công", "success");
            }
            catch (Exception e)
            {
                return new ResultViewModel(false, e.Message, "error");
            }
        }
        public ResultViewModel TraThietBi(List<LienKetThietBiPhieuMuon> ThietBis, string user)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var MaPhieuMuon = ThietBis.First().MaPhieuMuon;
                    var phieuMuon = GetPhieuMuonByMaPhieu(MaPhieuMuon);
                    phieuMuon.TrangThaiPhieuMuonId = 2;
                    phieuMuon.NguoiChoMuonNhan = true;
                    var log = new LogPhieuMuon();
                    log.MaPhieuMuon = MaPhieuMuon;
                    log.TieuDe = "Xác nhận";
                    log.NguoiDung = user;
                    log.ThoiGian = DateTime.Now;
                    log.HanhDong = "Xác nhận đã nhận thiết bị từ " + phieuMuon.GiaoVien.TenGiaoVien;
                    if (phieuMuon.NguoiMuonTra.Value)
                    {
                        var lst = DbContext.LienKetThietBiPhieuMuons.Where(l => l.MaPhieuMuon == MaPhieuMuon);
                        DbContext.LienKetThietBiPhieuMuons.RemoveRange(lst);
                        DbContext.LienKetThietBiPhieuMuons.AddRange(ThietBis);
                        DbContext.LogPhieuMuons.Add(log);

                        foreach(var item in ThietBis)
                        {
                            var tb = DbContext.KhoThietBis.FirstOrDefault(k => k.KhoPhongId == item.KhoPhongId && k.ThietBiId == item.ThietBiId);
                            tb.SoLuongConLai += item.SoLuong;
                        }

                        DbContext.SaveChanges();
                        transaction.Commit();
                        return new ResultViewModel(true, "Xác nhận trả thành công", "success");
                    }
                    else
                    {
                        return new ResultViewModel(false, "Yêu cầu người mượn xác nhận trả thiết bị trước!", "error");
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new ResultViewModel(false, e.Message, "error");
                }
            }
        }
    }
}
