using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class LoaiThietBiTieuChuanProvider : ApplicationDbcontext
    {
        public bool Insert(LoaiThietBiTieuChuan model)
        {
            try
            {
                DbContext.LoaiThietBiTieuChuans.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
      
        public List<LoaiThietBiTieuChuan> getAll()
        {
            try
            {
                return DbContext.LoaiThietBiTieuChuans.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public LoaiThietBiTieuChuan getById(int id)
        {
            try
            {
                return DbContext.LoaiThietBiTieuChuans.First(m => m.LoaiThietBiTieuChuanId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(LoaiThietBiTieuChuan model)
        {
            try
            {
                var old = getById(model.LoaiThietBiTieuChuanId);
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
