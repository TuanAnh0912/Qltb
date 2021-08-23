using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class KhoiLopTieuChuanProvider : ApplicationDbcontext
    {
        public bool Insert(KhoiLopTieuChuan model)
        {
            try
            {
                DbContext.KhoiLopTieuChuans.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<KhoiLopTieuChuan> getAllByKhoiTruongId(int khoiTruongId)
        {
            try
            {
                return DbContext.KhoiLopTieuChuans.Where(m=>m.KhoiTruongId== khoiTruongId).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<KhoiLopTieuChuan> getAll()
        {
            try
            {
                return DbContext.KhoiLopTieuChuans.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public KhoiLopTieuChuan getById(int id)
        {
            try
            {
                return DbContext.KhoiLopTieuChuans.First(m => m.KhoiLopTieuChuanId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(KhoiLopTieuChuan model)
        {
            try
            {
                var old = getById(model.KhoiLopTieuChuanId);
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
