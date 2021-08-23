using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class PhongProvider : ApplicationDbcontext
    {
        public bool Insert(Phong model)
        {
            try
            {
                DbContext.Phongs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Phong> getAllByKhoiPhongTieuChuanId(int id)
        {
            try
            {
                return DbContext.Phongs.Where(m => m.KhoiPhongTieuChuanId == id).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Phong getById(int id)
        {
            try
            {
                return DbContext.Phongs.First(m => m.PhongId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(Phong model)
        {
            try
            {
                var old = getById(model.PhongId);
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
