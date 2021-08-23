using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class KhoiLopProvider : ApplicationDbcontext
    {
        public bool Insert(KhoiLop model)
        {
            try
            {
                DbContext.KhoiLops.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public KhoiLop getByMaKhoiLop(string ma)
        {
            try
            {
                return DbContext.KhoiLops.First(m => m.MaKhoiLop.Equals(ma));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public KhoiLop getById(int id)
        {
            try
            {
                return DbContext.KhoiLops.First(m => m.KhoiLopId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(KhoiLop model)
        {
            try
            {
                var old = getById(model.KhoiLopId);
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
