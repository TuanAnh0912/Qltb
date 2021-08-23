using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class LienKetKhoiLopThietBiProvider : ApplicationDbcontext
    {
        public bool Insert(LienKetKhoiLopThietBi model)
        {
            try
            {
                DbContext.LienKetKhoiLopThietBis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
       
       
        public LienKetKhoiLopThietBi getById(int id)
        {
            try
            {
                return DbContext.LienKetKhoiLopThietBis.First(m => m.LienKetId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(LienKetKhoiLopThietBi model)
        {
            try
            {
                var old = getById(model.LienKetId);
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
