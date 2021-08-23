using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class PhieuGhiMatThietBiProvider : ApplicationDbcontext
    {
        public bool Insert(PhieuGhiMatThietBi model)
        {
            try
            {
                DbContext.PhieuGhiMatThietBis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<PhieuGhiMatThietBi> getAll(int khoPhongId)
        {
            try
            {
                return DbContext.PhieuGhiMatThietBis.Where(m => m.KhoPhongId == khoPhongId).ToList();
            }
            catch (Exception e)
            {
                return new List<PhieuGhiMatThietBi>();
            }
        }

        public PhieuGhiMatThietBi getById(string id)
        {
            try
            {
                return DbContext.PhieuGhiMatThietBis.First(m => m.PhieuGhiMatThietBiId.Equals(id));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(PhieuGhiMatThietBi model)
        {
            try
            {
                var old = getById(model.PhieuGhiMatThietBiId);
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
