using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class ChiTietPhieuGhiMatThietBiProvider : ApplicationDbcontext
    {
        public bool Insert(ChiTietPhieuGhiMatThietBi model)
        {
            try
            {
                DbContext.ChiTietPhieuGhiMatThietBis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<ChiTietPhieuGhiMatThietBi> getAllByPhieuGhiMatThietBiId(string id)
        {
            try
            {
                return DbContext.ChiTietPhieuGhiMatThietBis.Where(m => m.PhieuGhiMatThietBiId.Equals(id)).ToList();
            }
            catch (Exception e)
            {
                return new List<ChiTietPhieuGhiMatThietBi>();
            }
        }
        public ChiTietPhieuGhiMatThietBi getById(string id)
        {
            try
            {
                return DbContext.ChiTietPhieuGhiMatThietBis.First(m => m.ChiTietPhieuGhiMatThietBiId.Equals(id));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(ChiTietPhieuGhiMatThietBi model)
        {
            try
            {
                var old = getById(model.ChiTietPhieuGhiMatThietBiId);
                old.SoLuongMat = model.SoLuongMat;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete(List<ChiTietPhieuGhiMatThietBi> models)
        {
            try
            {
                DbContext.ChiTietPhieuGhiMatThietBis.RemoveRange(models);
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
