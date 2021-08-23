using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class DonViProvider : ApplicationDbcontext
    {
        public bool Insert(DonVi model)
        {
            try
            {
                DbContext.DonVis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<DonVi> getAll()
        {
            try
            {
                return DbContext.DonVis.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public DonVi getById(Guid id)
        {
            try
            {
                return DbContext.DonVis.First(m => m.DonViId.Equals(id));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Update(DonVi model)
        {
            try
            {
                var old = getById(model.DonViId);
                old.TenDonVi = model.TenDonVi;
                old.TieuChuanCoSoVatChatId = model.TieuChuanCoSoVatChatId;
                old.TieuChuanPhongId = model.TieuChuanPhongId;
                old.KhoiTruongId = model.KhoiTruongId;
                old.TinhId = model.TinhId;
                old.HuyenId = model.HuyenId;
                old.XaId = model.XaId;
                old.DiaChi = model.DiaChi;
                old.HieuTruong = model.HieuTruong;
                old.Email = model.Email;
                old.DienThoai = model.DienThoai;
                old.Website = model.Website;
                old.FAX = model.FAX;
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
