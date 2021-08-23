using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class LoaiThietBiProvider : ApplicationDbcontext
    {
        public bool Insert(LoaiThietBi model)
        {
            try
            {
                DbContext.LoaiThietBis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public LoaiThietBi getByMaLoaiThietBi(string ma)
        {
            try
            {
                return DbContext.LoaiThietBis.First(m => m.MaLoaiThietBi.Equals(ma));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public LoaiThietBi getById(int id)
        {
            try
            {
                return DbContext.LoaiThietBis.First(m => m.LoaiThietBiId ==id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(LoaiThietBi model)
        {
            try
            {
                var old = getById(model.LoaiThietBiId);
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
