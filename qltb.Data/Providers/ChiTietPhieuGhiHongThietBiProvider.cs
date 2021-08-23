using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class ChiTietPhieuGhiHongThietBiProvider : ApplicationDbcontext
    {
        public bool Insert(ChiTietPhieuGhiHongThietBi model)
        {
            try
            {
                DbContext.ChiTietPhieuGhiHongThietBis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<ChiTietPhieuGhiHongThietBi> getAllByPhieuGhiHongThietBiId(string id)
        {
            try
            {
                return DbContext.ChiTietPhieuGhiHongThietBis.Where(m => m.PhieuGhiHongThietBiId.Equals(id)).ToList();
            }
            catch (Exception e)
            {
                return new List<ChiTietPhieuGhiHongThietBi>();
            }
        }
        public ChiTietPhieuGhiHongThietBi getById(string id)
        {
            try
            {
                return DbContext.ChiTietPhieuGhiHongThietBis.First(m => m.ChiTietPhieuGhiHongThietBiId.Equals(id));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(ChiTietPhieuGhiHongThietBi model)
        {
            try
            {
                var old = getById(model.ChiTietPhieuGhiHongThietBiId);
                old.SoLuongHong = model.SoLuongHong;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete(List<ChiTietPhieuGhiHongThietBi> models)
        {
            try
            {
                DbContext.ChiTietPhieuGhiHongThietBis.RemoveRange(models);
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
