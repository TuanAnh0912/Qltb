using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class TieuChuanCoSoVaChatProvider : ApplicationDbcontext
    {
        public bool Insert(TieuChuanCoSoVatChat model)
        {
            try
            {
                DbContext.TieuChuanCoSoVatChats.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<TieuChuanCoSoVatChat> getAll(int khoiTruongId)
        {
            try
            {
                return DbContext.TieuChuanCoSoVatChats.Where(m=>m.KhoiTruongId == khoiTruongId).ToList();
            }
            catch (Exception e)
            {
                return new List<TieuChuanCoSoVatChat>();
            }
        }
        public List<TieuChuanCoSoVatChat> getAll()
        {
            try
            {
                return DbContext.TieuChuanCoSoVatChats.ToList();
            }
            catch (Exception e)
            {
                return new List<TieuChuanCoSoVatChat>();
            }
        }
        public TieuChuanCoSoVatChat getById(int id)
        {
            try
            {
                return DbContext.TieuChuanCoSoVatChats.First(m => m.TieuChuanCoSoVatChatId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(TieuChuanCoSoVatChat model)
        {
            try
            {
                var old = getById(model.TieuChuanCoSoVatChatId);
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
