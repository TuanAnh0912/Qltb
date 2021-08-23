using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class SuDungPhongProvider : ApplicationDbcontext
    {
        public List<SuDungPhong> getAll(string phong,DateTime start, DateTime end)
        {
            try
            {
                return DbContext.SuDungPhongs.Where(m => m.KhoPhong.TenKhoPhong.Contains(phong) && m.NgayTao>=start && m.NgayTao <= end).OrderByDescending(m=>m.NgayTao).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public SuDungPhong getByDateAndThoiKhoaBieuPhongId(int ThoiKhoaBieuPhongId,DateTime date)
        {
            try
            {
                return DbContext.SuDungPhongs.First(m => m.ThoiKhoaBieuPhongId == ThoiKhoaBieuPhongId && m.NgayTao == date);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public SuDungPhong getLastItem(int id)
        {
            try
            {
                return DbContext.SuDungPhongs.Where(m=>m.ThoiKhoaBieuPhongId == id).OrderByDescending(m => m.NgayTao).First();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public SuDungPhong getLastItem()
        {
            try
            {
                return DbContext.SuDungPhongs.OrderByDescending(m=>m.NgayTao).First();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Insert(SuDungPhong model)
        {
            try
            {
                DbContext.SuDungPhongs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
       
        public SuDungPhong getById(int id)
        {
            try
            {
                return DbContext.SuDungPhongs.First(m => m.SuDungPhongId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(SuDungPhong model)
        {
            try
            {
                var old = getById(model.SuDungPhongId);
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
