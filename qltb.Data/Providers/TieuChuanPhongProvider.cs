using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class TieuChuanPhongProvider : ApplicationDbcontext
    {
        public bool Insert(TieuChuanPhong model)
        {
            try
            {
                DbContext.TieuChuanPhongs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<TieuChuanPhong> getAll(int KhoiTruongId)
        {
            try
            {
                return DbContext.TieuChuanPhongs.Where(m=>m.KhoiTruongId == KhoiTruongId).ToList();
            }
            catch (Exception e)
            {
                return new List<TieuChuanPhong>() ;
            }
        }
        public IEnumerable<TieuChuanPhong> getAll()
        {
            try
            {
                return DbContext.TieuChuanPhongs;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public TieuChuanPhong getById(int id)
        {
            try
            {
                return DbContext.TieuChuanPhongs.First(m => m.TieuChuanId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(TieuChuanPhong model)
        {
            try
            {
                var old = getById(model.TieuChuanId);
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
