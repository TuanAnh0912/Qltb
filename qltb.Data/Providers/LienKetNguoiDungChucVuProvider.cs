using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class LienKetNguoiDungChucVuProvider : ApplicationDbcontext
    {
        public int Insert(string NguoiDungId, int ChucVuId, string PhongBanId)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {

                    if (GetByNguoiDungChucVu(NguoiDungId, ChucVuId) == null)
                    {
                        var model = new LienKetNguoiDungChucVu();
                        model.NguoiDungId = Guid.Parse(NguoiDungId);
                        model.ChucVuId = ChucVuId;
                        DbContext.LienKetNguoiDungChucVus.Add(model);
                        DbContext.SaveChanges();

                        var model2 = new LienKetNguoiNguoiDungPhongBan();
                        model2.NguoiDungId = Guid.Parse(NguoiDungId);
                        model2.PhongBanId = Guid.Parse(PhongBanId);
                        DbContext.LienKetNguoiNguoiDungPhongBans.Add(model2);
                        DbContext.SaveChanges();

                        transaction.Commit();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                    
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }

        public bool Delete(string NguoiDungId, int ChucVuId)
        {
            try
            {
                var model = GetByNguoiDungChucVu(NguoiDungId, ChucVuId);
                DbContext.LienKetNguoiDungChucVus.Remove(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public LienKetNguoiDungChucVu GetByNguoiDungChucVu(string NguoiDungId, int ChucVuId)
        {
            try
            {
                return DbContext.LienKetNguoiDungChucVus.Where(l => l.NguoiDungId.ToString() == NguoiDungId && l.ChucVuId == ChucVuId).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
