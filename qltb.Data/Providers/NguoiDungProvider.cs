using qltb.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class NguoiDungProvider : ApplicationDbcontext
    {
        #region GET METHOD
        public NguoiDung GetById(Guid NguoiDungId)
        {
            try
            {
                return DbContext.NguoiDungs.FirstOrDefault(n => n.NguoiDungId == NguoiDungId);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int CountNguoIDungByPhongBan(string PhongBanId)
        {
            try
            {
                var command = @"select COUNT(*) as SoLuongNguoiDung from (select n.NguoiDungId from LienKetNguoiDungChucVu as lk inner join NguoiDung as n on lk.NguoiDungId = n.NguoiDungId inner join ChucVu as c on lk.ChucVuId = c.ChucVuId inner join PhongBan as p on c.PhongBanId = p.PhongBanId where p.PhongBanId = '" + PhongBanId + "' Group by n.NguoiDungId) as NguoiDungInPhongBan";
                var soluong = DbContext.Database.SqlQuery<int>(command).FirstOrDefault();
                return soluong;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public List<NguoiDungViewModel> GetByChucVu(int chucVuId)
        {
            try
            {
                var command = "select n.* from LienKetNguoiDungChucVu as lk inner join NguoiDung as n on lk.NguoiDungId = n.NguoiDungId inner join ChucVu as c on lk.ChucVuId = c.ChucVuId where lk.ChucVuId = " + chucVuId;
                return DbContext.Database.SqlQuery<NguoiDungViewModel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<NguoiDungViewModel> GetByPhongBan(string PhongBanId)
        {
            try
            {
                var command = @"select n.NguoiDungId, n.HoTen, n.TenDangNhap from LienKetNguoiDungChucVu as lk inner join NguoiDung as n on lk.NguoiDungId = n.NguoiDungId inner join ChucVu as c on lk.ChucVuId = c.ChucVuId inner join PhongBan as p on c.PhongBanId = p.PhongBanId where p.PhongBanId = '" + PhongBanId + "' Group by n.NguoiDungId, n.TenDangNhap, n.HoTen";
                var nguoidungs = DbContext.Database.SqlQuery<NguoiDungViewModel>(command).ToList();
                return nguoidungs;
            }
            catch (Exception e)
            {
                return null; ;
            }
        }
        public List<NguoiDung> GetAll()
        {
            try
            {
                return DbContext.NguoiDungs.OrderByDescending(n => n.NgayTao).ToList();
            }
            catch (Exception e)
            {
                return new List<NguoiDung>();
            }
        }
        #endregion
        public NguoiDung GetNguoiDungByTenDangNhap(string username)
        {
            try
            {
                return DbContext.NguoiDungs.FirstOrDefault(n => n.TenDangNhap.Equals(username));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool CheckTenDangNhap(string username)
        {
            try
            {
                var nguoidung = DbContext.NguoiDungs.FirstOrDefault(n => n.TenDangNhap.Equals(username));
                if (nguoidung == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Insert(NguoiDung model)
        {
            try
            {
                DbContext.NguoiDungs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdateInfo(NguoiDung model)
        {
            try
            {
                var old = GetById(model.NguoiDungId);
                if(model.MatKhau!=null && model.MatKhau != "")
                {
                    string matkhau = Helpers.SecurityHelper.Hash(old.Salt + model.MatKhau).ToLower();
                    old.MatKhau = matkhau;
                }
                old.HoTen = model.HoTen;
                old.DienThoai = model.DienThoai;
                old.Email = model.Email;
                old.GioiTinhId = model.GioiTinhId;
                old.IsActive = model.IsActive;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdateStatus(Guid NguoiDungId)
        {
            try
            {
                var model = GetById(NguoiDungId);
                model.IsActive = !model.IsActive;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool ChangePassword(Guid NguoiDungId, string newPassword)
        {
            try
            {
                var model = GetById(NguoiDungId);
                model.MatKhau = newPassword;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public NguoiDungViewModel GetThongTinNguoiDung(string NguoiDungId)
        {
            try
            {
                string command = "select * from NguoiDung as n where n.NguoiDungId='" + NguoiDungId + "'";
                var user = DbContext.Database.SqlQuery<NguoiDungViewModel>(command).FirstOrDefault();
                user.ChucVus = new ChucVuProvider().GetChucVuByNguoiDung(NguoiDungId);
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
    }
}
