using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class LoaiKhoPhongProvider : ApplicationDbcontext
    {
        public bool Insert(LoaiKhoPhong model)
        {
            try
            {
                DbContext.LoaiKhoPhongs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public LoaiKhoPhong getByMaLoaiKhoPhong(string ma)
        {
            try
            {
                return DbContext.LoaiKhoPhongs.First(m => m.MaLoaiKhoPhong.Equals(ma));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public LoaiKhoPhong getById(int id)
        {
            try
            {
                return DbContext.LoaiKhoPhongs.First(m => m.LoaiKhoPhongId==id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(LoaiKhoPhong model)
        {
            try
            {
                var old = getById(model.LoaiKhoPhongId);
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
