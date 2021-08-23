using qltb.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class ChucVuProvider : ApplicationDbcontext
    {
        ChucNangProvider _chucnang = new ChucNangProvider();
        public List<ChucVu> GetAll()
        {
            try
            {
                return DbContext.ChucVus.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ChucVu> GetByNguoiDung(string NguoiDungId)
        {
            try
            {
                string command = @"select * from ChucVu as c inner join LienKetNguoiDungChucVu as lk on c.ChucVuId = lk.ChucVuId where lk.NguoiDungId = '" + NguoiDungId + "'";
                return DbContext.Database.SqlQuery<ChucVu>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ChucVuViewModel> GetAllChucVuByPhongBan(string phongBanId)
        {
            try
            {
                var command = "select * from ChucVu as p WHERE p.PhongBanId = '" + phongBanId + "' ORDER BY p.TenChucVu ASC";
                var chucVus = DbContext.Database.SqlQuery<ChucVuViewModel>(command).ToList();
                foreach (var item in chucVus)
                {
                    item.ChucNangs = _chucnang.GetAllByChuCVu(item.ChucVuId);
                }
                return chucVus;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ChucVuViewModel> GetChucVuByNguoiDung(string NguoiDungId)
        {
            try
            {
                var command = "select c.*, p.TenPhongBan from LienKetNguoiDungChucVu as lk inner join ChucVu as c on lk.ChucVuId = c.ChucVuId inner join PhongBan as p on c.PhongBanId = p.PhongBanId where lk.NguoiDungId = '" + NguoiDungId + "'";
                return DbContext.Database.SqlQuery<ChucVuViewModel>(command).ToList();
            }
            catch (Exception e)
            {
                return new List<ChucVuViewModel>();
            }
        }
        public List<ChucVuViewModel> GetByNguoiDungPhongBan(string nguoidungid, string phongbanid)
        {
            try
            {
                var command = @"select * from ChucVu as c inner join LienKetNguoiDungChucVu as lk on c.ChucVuId = lk.ChucVuId where lk.NguoiDungId = '" + nguoidungid + "' and c.PhongBanId = '" + phongbanid + "'";
                return DbContext.Database.SqlQuery<ChucVuViewModel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ChucVu GetById(int id)
        {
            try
            {
                return DbContext.ChucVus.FirstOrDefault(c => c.ChucVuId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ChucVuViewModel GetThongTinChucVu(int id)
        {
            try
            {
                var command = "SELECT * FROM ChucVu as c WHERE c.ChucVuId=" + id;
                return DbContext.Database.SqlQuery<ChucVuViewModel>(command).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ResultViewModel Insert(ChucVu model)
        {
            try
            {
                model.MaChucVu = Helpers.RamdomHelper.RandomString(10);
                DbContext.ChucVus.Add(model);
                DbContext.SaveChanges();
                return new ResultViewModel(true, "Thêm mới chức vụ thành công");
            }
            catch (Exception e)
            {
                return new ResultViewModel(false, e.Message);
            }
        }
        public ResultViewModel DeleteChucVu(int id)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var model = DbContext.ChucVus.FirstOrDefault(c => c.ChucVuId == id);
                    int noOfLkDeleted = DbContext.Database.ExecuteSqlCommand("DELETE from LienKetNguoiDungChucVu WHERE ChucVuId = " + id);
                    int noOfQuyenDeleted = DbContext.Database.ExecuteSqlCommand("DELETE from QuyenTruyCap WHERE ChucVuId = " + id);
                    int noOfRowDeleted = DbContext.Database.ExecuteSqlCommand("DELETE from ChucVu WHERE ChucVuId = " + id);

                    transaction.Commit();
                    return new ResultViewModel(true, "Xóa chức vụ thành công");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new ResultViewModel(false, e.Message);
                }
            }
        }
        public ResultViewModel UpdateChucVu(ChucVu model)
        {
            try
            {
                var old = GetById(model.ChucVuId);
                old.TenChucVu = model.TenChucVu;
                old.Mota = model.Mota;
                DbContext.SaveChanges();
                return new ResultViewModel(true, "Cập nhật thông tin chức vụ thành công");
            }
            catch (Exception e)
            {
                return new ResultViewModel(false, e.Message);
            }
        }
    }
}
