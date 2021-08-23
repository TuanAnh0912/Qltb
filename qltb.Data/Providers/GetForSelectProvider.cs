using qltb.Data.ResVMs;
using qltb.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class GetForSelectProvider : ApplicationDbcontext
    {
        public List<ChuongTrinhHocResVM> GetChuongTrinhHocs()
        {
            try
            {
                var command = "SELECT * FROM ChuongTrinhHoc as c ORDER BY c.TenChuongTrinhHoc";
                return DbContext.Database.SqlQuery<ChuongTrinhHocResVM>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<MonHocResVM> GetMonHocs()
        {
            try
            {
                var command = "SELECT * FROM MonHoc as c ORDER BY c.TenMonHoc";
                return DbContext.Database.SqlQuery<MonHocResVM>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<KhoiLopResVM> GetKhoiLops()
        {
            try
            {
                var command = "SELECT * FROM KhoiLop as c ORDER BY c.TenKhoiLop";
                return DbContext.Database.SqlQuery<KhoiLopResVM>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<LopResVM> GetLops()
        {
            try
            {
                var command = "SELECT * FROM Lop as c ORDER BY c.TenLop";
                return DbContext.Database.SqlQuery<LopResVM>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<MucDichSuDungResVM> GetMucDichSuDungs()
        {
            try
            {
                var command = "SELECT * FROM MucDichSuDung as c ORDER BY c.TenMucDichSuDung";
                return DbContext.Database.SqlQuery<MucDichSuDungResVM>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<TietHocResVM> GetTietHocs()
        {
            try
            {
                var command = "SELECT * FROM TietHoc as c ORDER BY c.TenTietHoc";
                return DbContext.Database.SqlQuery<TietHocResVM>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<KhoPhongResVM> GetKhoPhongs()
        {
            try
            {
                var command = "SELECT * FROM KhoPhong as c ORDER BY c.TenKhoPhong";
                return DbContext.Database.SqlQuery<KhoPhongResVM>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<LoaiKhoPhongResVM> GetLoaiKhoPhong()
        {
            var command = "SELECT * FROM LoaiKhoPhong as c ORDER BY c.TenLoaiKhoPhong";
            return DbContext.Database.SqlQuery<LoaiKhoPhongResVM>(command).ToList();
        }
        public List<LoaiThietBiResVM> GetLoaiThietBis()
        {
            try
            {
                var command = "SELECT * FROM LoaiThietBi as c ORDER BY c.TenLoaiThietBi";
                return DbContext.Database.SqlQuery<LoaiThietBiResVM>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<SelectThietBiViewmodel> GetThietBiByBaiHoc(int id)
        {
            try
            {
                var command = @"SELECT t.ThietBiId, t.MaThietBi, t.TenThietBi, l.TenLoaiThietBi, ct.SoLuong, b.TenBaiHoc, k.SoLuongConLai, kp.TenKhoPhong, b.MonHocId, 
                            m.TenMonHoc, kp.KhoPhongId, l.LoaiThietBiId FROM ChiTietBaiHocSuDungThietBi as ct inner join ThietBi as t on ct.ThietBiId = t.ThietBiId 
                            inner join LoaiThietBi as l on t.LoaiThietBiId = l.LoaiThietBiId inner join BaiHocSuDungThietBi as b on ct.BaiHocSuDungThietBiId = b.BaiHocSuDungThietBiId 
                            inner join KhoThietBi as k on t.ThietBiId = k.ThietBiId inner join KhoPhong as kp on k.KhoPhongId = kp.KhoPhongId inner join MonHoc as m on b.MonHocId = m.MonHocId
                            WHERE t.IsDelete = 0 and ct.BaiHocSuDungThietBiId =" + id;
                return DbContext.Database.SqlQuery<SelectThietBiViewmodel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
       

        public List<SelectThietBiViewmodel> GetThietBiBy(int? KhoPhongId, int? MonHocId, int? LoaiThietBiId)
        {
            try
            {
                var command = @"SELECT t.ThietBiId, t.MaThietBi, t.TenThietBi, l.TenLoaiThietBi, k.SoLuongConLai, kp.TenKhoPhong, m.MonHocId, 
                                m.TenMonHoc, kp.KhoPhongId, l.LoaiThietBiId FROM KhoThietBi as k inner join ThietBi as t on k.ThietBiId = t.ThietBiId inner join MonHoc as m 
                                on t.MonHocId = m.MonHocId inner join LoaiThietBi as l on t.LoaiThietBiId = l.LoaiThietBiId inner join KhoPhong as kp on k.KhoPhongId = kp.KhoPhongId
                                WHERE t.IsDelete = 0";
                if (KhoPhongId.HasValue)
                {
                    command += " and k.KhoPhongId = " + KhoPhongId.Value;
                }
                if (MonHocId.HasValue)
                {
                    command += " and t.MonHocId  = " + MonHocId.Value;
                }
                if (LoaiThietBiId.HasValue)
                {
                    command += " and t.LoaiThietBiId = " + LoaiThietBiId.Value;
                }
                return DbContext.Database.SqlQuery<SelectThietBiViewmodel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<SelectThietBiViewmodel> GetThietBiBy2(int? KhoPhongId, int? MonHocId, int? LoaiThietBiId)
        {
            try
            {
                var command = @"SELECT t.ThietBiId, t.MaThietBi, t.TenThietBi, l.TenLoaiThietBi, k.SoLuongConLai, kp.TenKhoPhong, m.MonHocId, k.SoLuong,
                                m.TenMonHoc, kp.KhoPhongId, l.LoaiThietBiId FROM KhoThietBi as k inner join ThietBi as t on k.ThietBiId = t.ThietBiId left join MonHoc as m 
                                on t.MonHocId = m.MonHocId inner join LoaiThietBi as l on t.LoaiThietBiId = l.LoaiThietBiId inner join KhoPhong as kp on k.KhoPhongId = kp.KhoPhongId
                                WHERE t.IsDelete = 0";
                if (KhoPhongId.HasValue)
                {
                    command += " and k.KhoPhongId = " + KhoPhongId.Value;
                }
                if (MonHocId.HasValue)
                {
                    command += " and t.MonHocId  = " + MonHocId.Value;
                }
                if (LoaiThietBiId.HasValue)
                {
                    command += " and t.LoaiThietBiId = " + LoaiThietBiId.Value;
                }
                return DbContext.Database.SqlQuery<SelectThietBiViewmodel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<SelectThietBiViewmodel> GetThietBiMuonTrongThoiGian(string start, string end)
        {
            try
            {
                var command = @"SELECT t.ThietBiId, t.MaThietBi, t.TenThietBi, l.TenLoaiThietBi, lk.SoLuong, kp.TenKhoPhong, m.MonHocId, p.NgayMuon, p.NgayTra, p.TrangThaiPhieuMuonId,
                                m.TenMonHoc, kp.KhoPhongId, l.LoaiThietBiId FROM LienKetThietBiPhieuMuon as lk inner join ThietBi as t on lk.ThietBiId = t.ThietBiId inner join MonHoc as m 
                                on t.MonHocId = m.MonHocId inner join LoaiThietBi as l on t.LoaiThietBiId = l.LoaiThietBiId inner join KhoPhong as kp on lk.KhoPhongId = kp.KhoPhongId
								INNER JOIN PhieuMuon as p on p.MaPhieuMuon = lk.MaPhieuMuon
                                WHERE t.IsDelete = 0 and (p.NgayMuon <= '" + end + "' and p.NgayTra >='" + start + "' and p.TrangThaiPhieuMuonId = 3) or (p.NgayTra <= '" + start + "' and p.TrangThaiPhieuMuonId = 1)";
                var thietbis = DbContext.Database.SqlQuery<SelectThietBiViewmodel>(command).ToList();
                return thietbis;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<PhongViewModel> GetPhongThayDoiTrongThoiGian(string start, string end)
        {
            try
            {
                var command = @"SELECT p.KhoPhongId, p.NgayMuon, p.NgayTra, p.TrangThaiPhieuMuonId, k.MaKhoPhong, k.TenKhoPhong, k.DienTich, k.NamSuDung, k.IsDelete,
                                k.TrangThaiPhongId, g.TenGiaoVien as TenNguoiDangKy, t.TenTrangThai, t.MaMau
                                FROM PhieuMuon as p INNER JOIN KhoPhong as k on p.KhoPhongId = k.KhoPhongId INNER JOIN GiaoVien as g on p.GiaoVienId = g.GiaoVienId
                                INNER JOIN TrangTraiPhong as t on k.TrangThaiPhongId = t.TrangThaiPhongId  
                                WHERE p.IsDelete = 0 and (p.NgayMuon <= '" + end + "' and p.NgayTra >='" + start + "' and p.TrangThaiPhieuMuonId = 3) or (p.NgayTra <= '" + start + "' and p.TrangThaiPhieuMuonId = 1);";
                var phongs = DbContext.Database.SqlQuery<PhongViewModel>(command).ToList();
                return phongs;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool CheckPhongDangKyTrongThoiGian(string start, string end, int KhoPhongId, string MaPhieu)
        {
            try
            {
                var command = @"SELECT p.KhoPhongId, p.NgayMuon, p.NgayTra, p.TrangThaiPhieuMuonId, k.MaKhoPhong, k.TenKhoPhong, k.DienTich, k.NamSuDung, k.IsDelete,
                                k.TrangThaiPhongId, g.TenGiaoVien as TenNguoiDangKy, t.TenTrangThai, t.MaMau
                                FROM PhieuMuon as p INNER JOIN KhoPhong as k on p.KhoPhongId = k.KhoPhongId INNER JOIN GiaoVien as g on p.GiaoVienId = g.GiaoVienId
                                INNER JOIN TrangTraiPhong as t on k.TrangThaiPhongId = t.TrangThaiPhongId  
                                WHERE p.IsDelete = 0 and p.KhoPhongId = " + KhoPhongId + " and p.MaPhieuMuon !='" + MaPhieu + "' and (p.NgayMuon <= '" + end + "' and p.NgayTra >='" + start + "' and p.TrangThaiPhieuMuonId = 3)";
                var phongs = DbContext.Database.SqlQuery<PhongViewModel>(command).ToList();
                if (phongs != null && phongs.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<PhongViewModel> GetPhongCoTheMuon(int? LoaiKhoPhongId, string start, string end, string search)
        {
            try
            {
                var result = new List<PhongViewModel>();
                var command = @"SELECT kp.*, t.TenTrangThai, t.MaMau from KhoPhong as kp INNER JOIN TrangTraiPhong as t on kp.TrangThaiPhongId = t.TrangThaiPhongId WHERE kp.IsDelete = 0";
                if (LoaiKhoPhongId.HasValue)
                {
                    command += " and kp.LoaiKhoPhongId = " + LoaiKhoPhongId.Value;
                }
                var phongs = DbContext.Database.SqlQuery<PhongViewModel>(command).ToList();
                var phongthaydoi = GetPhongThayDoiTrongThoiGian(start, end);
                foreach (var item in phongs)
                {
                    foreach (var p in phongthaydoi)
                    {
                        if (item.KhoPhongId == p.KhoPhongId)
                        {
                            if (p.TrangThaiPhieuMuonId == 3)
                            {
                                item.TrangThaiPhongId = 4;
                                item.TenTrangThai = "Đã được đăng ký";
                                item.MaMau = "dark";
                            }
                            if (p.TrangThaiPhieuMuonId == 1)
                            {
                                item.TrangThaiPhongId = 1;
                                item.TenTrangThai = "Có thể sử dụng";
                                item.MaMau = "success";
                            }

                        }
                    }
                    result.Add(item);
                }
                return result.Where(p => p.TenKhoPhong.Contains(search)).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public PhongViewModel GetByMaPhieuMuon(string MaPhieu)
        {
            try
            {
                var command = @"SELECT kp.*, t.TenTrangThai, t.MaMau from PhieuMuon as p INNER JOIN KhoPhong as kp on p.KhoPhongId = kp.KhoPhongId INNER JOIN TrangTraiPhong as t on kp.TrangThaiPhongId = t.TrangThaiPhongId WHERE kp.IsDelete = 0 and p.MaPhieuMuon = '" + MaPhieu + "'";
                return DbContext.Database.SqlQuery<PhongViewModel>(command).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
