using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class MonHocProvider : ApplicationDbcontext
    {
        public List<MonHoc> GetAllMonHoc()
        {
            try
            {
                return DbContext.MonHocs.ToList();
            }
            catch (Exception e)
            {

                return new List<MonHoc>();
            }
        }
        public bool Insert(MonHoc model)
        {
            try
            {
                DbContext.MonHocs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Lop> GetAllLopHoc()
        {
            try
            {
                return DbContext.Lops.ToList();
            }
            catch (Exception e)
            {

                return new List<Lop>();
            }
        }
        public List<KhoiLop> GetAllKhoiLop()
        {
            try
            {
                return DbContext.KhoiLops.ToList();
            }
            catch (Exception e)
            {

                return new List<KhoiLop>();
            }
        }
        public MonHoc getByMaMonHoc(string ma)
        {
            try
            {
                return DbContext.MonHocs.First(m => m.MaMonHoc.Equals(ma));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public MonHoc getById(int id)
        {
            try
            {
                return DbContext.MonHocs.First(m => m.MonHocId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
