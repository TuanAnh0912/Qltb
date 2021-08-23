using qltb.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class KhoThietBiProvider : ApplicationDbcontext
    {
        public ResultViewModel TangSoLuongKhoThietBi(int ThietBiId, int KhoPhongId, int SoLuong)
        {
            try
            {
                var model = DbContext.KhoThietBis.FirstOrDefault(k => k.ThietBiId == ThietBiId & k.KhoPhongId == KhoPhongId);
                model.SoLuongConLai += SoLuong;
                if(model.SoLuongConLai < 0)
                {
                    return new ResultViewModel(false, "Số lượng mượt vượt quá lượng trong kho");
                }
                else
                {
                    DbContext.SaveChanges();
                    return new ResultViewModel(true, "Cập nhật số lượng thành công");
                }
                
            }
            catch (Exception e)
            {
                return new ResultViewModel(false, e.Message);
            }
        }
        public KhoThietBi getById(int khoPhongId,int thietBiId)
        {
            try
            {
                return DbContext.KhoThietBis.First(m => m.ThietBiId == thietBiId && m.KhoPhongId == khoPhongId);
            }
            catch (Exception)
            {

                return null;
            }
        }
        public KhoThietBi getById(string id)
        {
            try
            {
                return DbContext.KhoThietBis.First(m => m.KhoThietBiId.Equals(id));
            }
            catch (Exception)
            {

                return null;
            }
        }
        public List<KhoThietBi> getAllByKhoPhongId(int khoPhongId)
        {
            try
            {
                return DbContext.KhoThietBis.Where(m => m.KhoPhongId == khoPhongId).ToList();
            }
            catch (Exception)
            {

                return new List<KhoThietBi>();
            }
        }
        public bool Update(KhoThietBi model)
        {
            try
            {
                var old = getById(model.KhoThietBiId);
                old.SoLuong = old.SoLuong;
                old.SoLuongHong = old.SoLuongHong;
                old.SoLuongMat = old.SoLuongMat;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete(KhoThietBi model)
        {
            try
            {
                DbContext.KhoThietBis.Remove(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete(List<KhoThietBi> model)
        {
            try
            {
                DbContext.KhoThietBis.RemoveRange(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Insert(KhoThietBi model)
        {
            try
            {
                DbContext.KhoThietBis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
