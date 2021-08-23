using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class KhoiPhongTieuChuanProvider : ApplicationDbcontext
    {
        public bool Insert(KhoiPhongTieuChuan model)
        {
            try
            {
                DbContext.KhoiPhongTieuChuans.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<KhoiPhongTieuChuan> getByTieuChuanId(int id)
        {
            try
            {
                return DbContext.KhoiPhongTieuChuans.Where(m => m.TieuChuanId == id).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public KhoiPhongTieuChuan getById(int id)
        {
            try
            {
                return DbContext.KhoiPhongTieuChuans.First(m => m.KhoiPhongTieuChuanId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(KhoiPhongTieuChuan model)
        {
            try
            {
                var old = getById(model.KhoiPhongTieuChuanId);
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
