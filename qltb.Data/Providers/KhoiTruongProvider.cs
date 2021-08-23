using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class KhoiTruongProvider : ApplicationDbcontext
    {
        public List<KhoiTruong> getAll()
        {
            try
            {
                return DbContext.KhoiTruongs.ToList();
            }
            catch (Exception e)
            {
                return new List<KhoiTruong>();
            }
        }
        public bool Insert(KhoiTruong model)
        {
            try
            {
                DbContext.KhoiTruongs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
      
        public KhoiTruong getById(int id)
        {
            try
            {
                return DbContext.KhoiTruongs.First(m => m.KhoiTruongId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
       
    }
}
