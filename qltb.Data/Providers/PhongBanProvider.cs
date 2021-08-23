using qltb.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class PhongBanProvider : ApplicationDbcontext
    {
        ChucVuProvider _chucvu = new ChucVuProvider();
        NguoiDungProvider _nguoidung = new NguoiDungProvider();
        public List<PhongBanViewModel> GetAll()
        {
            try
            {
                var result = new List<PhongBanViewModel>();
                var command = "select * from PhongBan as p ORDER BY p.TenPhongBan ASC";
                var phongBan = DbContext.Database.SqlQuery<PhongBanViewModel>(command).ToList();
                foreach(var item in phongBan)
                {
                    item.ChucVus = _chucvu.GetAllChucVuByPhongBan(item.PhongBanId.ToString());
                    item.SoLuongNguoiDung = _nguoidung.CountNguoIDungByPhongBan(item.PhongBanId.ToString());
                    result.Add(item);
                }
                
                return result;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public PhongBan GetById(string id)
        {
            try
            {
                return DbContext.PhongBans.FirstOrDefault(p =>p.PhongBanId.ToString().ToLower() == id.ToLower());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ResultViewModel Insert(PhongBan model)
        {
            try
            {
                model.PhongBanId = Guid.NewGuid();
                model.MaPhongBan = Helpers.RamdomHelper.RandomString(10);
                DbContext.PhongBans.Add(model);
                DbContext.SaveChanges();
                return new ResultViewModel(true, "Thêm mới phòng ban thành công");
            }
            catch (Exception e)
            {
                return new ResultViewModel(false, e.Message);
            }
        }

        public ResultViewModel UpdatePhongBan(PhongBan model)
        {
            try
            {
                var old = GetById(model.PhongBanId.ToString());
                old.TenPhongBan = model.TenPhongBan;
                old.DienThoai = model.DienThoai;
                old.MoTa = model.MoTa;
                DbContext.SaveChanges();

                return new ResultViewModel(true, "Cập nhật thông tin phòng ban thành công!");
            }
            catch(Exception e)
            {
                return new ResultViewModel(false, e.Message);
            }
        }

        public PhongBanViewModel GetChiTietPhongBan(string id)
        {
            try
            {
                var command = "select * from PhongBan as p where p.PhongBanId = '" + id + "'";
                var phongban = DbContext.Database.SqlQuery<PhongBanViewModel>(command).FirstOrDefault();
                phongban.ChucVus = _chucvu.GetAllChucVuByPhongBan(id);
                phongban.NguoiDungs = _nguoidung.GetByPhongBan(id);
                if (phongban.NguoiDungs!=null && phongban.NguoiDungs.Count() > 0)
                {
                    foreach (var item in phongban.NguoiDungs)
                    {
                        item.ChucVus = _chucvu.GetByNguoiDungPhongBan(item.NguoiDungId.ToString(), id);
                    }
                }
                phongban.SoLuongNguoiDung = _nguoidung.CountNguoIDungByPhongBan(id);
                return phongban;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
