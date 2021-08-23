using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class ThoiKhoaBieuPhongProvider : ApplicationDbcontext
    {
        public bool Insert(ThoiKhoaBieuPhong model)
        {
            try
            {
                DbContext.ThoiKhoaBieuPhongs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<ThoiKhoaBieuPhong> getAll()
        {
            try
            {
                return DbContext.ThoiKhoaBieuPhongs.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ThoiKhoaBieuPhong getById(int id)
        {
            try
            {
                return DbContext.ThoiKhoaBieuPhongs.First(m => m.Id == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(ThoiKhoaBieuPhong model)
        {
            try
            {
                var old = getById(model.Id);
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
