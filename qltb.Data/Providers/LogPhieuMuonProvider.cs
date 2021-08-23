using qltb.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class LogPhieuMuonProvider : ApplicationDbcontext
    {
        public bool Insert(LogPhieuMuon model)
        {
            try
            {
                DbContext.LogPhieuMuons.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<LogPhieuMuonViewModel> GetByMaPhieuMuon(string maPhieu)
        {
            try
            {
                var commnad = @"SELECT l.*, n.HoTen as TenNguoiThucHien FROM LogPhieuMuon as l INNER JOIN NguoiDung as n on l.NguoiDung = n.NguoiDungId WHERE l.MaPhieuMuon = '" + maPhieu + "' ORDER BY l.ThoiGian DESC";
                return DbContext.Database.SqlQuery<LogPhieuMuonViewModel>(commnad).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
