using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class PhieuGhiHongThietBiProvider : ApplicationDbcontext
    {
        public bool Insert(PhieuGhiHongThietBi model)
        {
            try
            {
                DbContext.PhieuGhiHongThietBis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<PhieuGhiHongThietBi> getAll(int khoPhongId)
        {
            try
            {
                return DbContext.PhieuGhiHongThietBis.Where(m=>m.KhoPhongId== khoPhongId).ToList();
            }
            catch (Exception e)
            {
                return new List<PhieuGhiHongThietBi>();
            }
        }
        public PhieuGhiHongThietBi getById(string id)
        {
            try
            {
                return DbContext.PhieuGhiHongThietBis.First(m => m.PhieuGhiHongThietBiId.Equals(id));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(PhieuGhiHongThietBi model)
        {
            try
            {
                var old = getById(model.PhieuGhiHongThietBiId);
                old.NgayLap = model.NgayLap;
                old.NoiDung = model.NoiDung;
                old.SoPhieu = model.SoPhieu;
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
