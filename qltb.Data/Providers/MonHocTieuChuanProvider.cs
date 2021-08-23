using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class MonHocTieuChuanProvider : ApplicationDbcontext
    {
        public bool Insert(MonHocTieuChuan model)
        {
            try
            {
                DbContext.MonHocTieuChuans.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<MonHocTieuChuan> getAllByKhoiTruongId(int KhoiTruongId)
        {
            try
            {
                return DbContext.MonHocTieuChuans.Where(m => m.IsDelete == false && m.KhoiTruongId == KhoiTruongId).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<MonHocTieuChuan> getAll()
        {
            try
            {
                return DbContext.MonHocTieuChuans.Where(m=>m.IsDelete==null || m.IsDelete == false).ToList() ;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public MonHocTieuChuan getById(int id)
        {
            try
            {
                return DbContext.MonHocTieuChuans.First(m => m.MonHocTieuChuanId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(MonHocTieuChuan model)
        {
            try
            {
                var old = getById(model.MonHocTieuChuanId);
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
