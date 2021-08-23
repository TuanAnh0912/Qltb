using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class ThietBiTieuChuanProvider : ApplicationDbcontext
    {
        public bool Insert(ThietBiTieuChuan model)
        {
            try
            {
                DbContext.ThietBiTieuChuans.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<ThietBiTieuChuan> getAllByLoaiThietBiTieuChuanIdAndKhoiTruongId(int id,int khoiTruongId)
        {
            try
            {
                return DbContext.ThietBiTieuChuans.Where(m => m.LoaiThietBiTieuChuanId == id && m.KhoiTruongId == khoiTruongId).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ThietBiTieuChuan> getAllByLoaiThietBiTieuChuanId(int id)
        {
            try
            {
                return DbContext.ThietBiTieuChuans.Where(m => m.LoaiThietBiTieuChuanId == id).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ThietBiTieuChuan getById(int id)
        {
            try
            {
                return DbContext.ThietBiTieuChuans.First(m => m.ThietBiId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(ThietBiTieuChuan model)
        {
            try
            {
                var old = getById(model.ThietBiId);
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
