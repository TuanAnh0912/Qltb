using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using qltb.Data.Providers;

namespace qltb.WebApp.Business
{
    public class StaticData
    {
        

        public static List<string> getRoles(string NguoiDungId)
        {
            ChucNangProvider _chucnang = new ChucNangProvider();
            List<string> roles = new List<string>();
            try
            {
                var chucnangs = _chucnang.GetAllByUser(NguoiDungId);
                roles = chucnangs.Select(c => c.MaChucNang).ToList();
            }
            catch (Exception)
            {
            }
            return roles;

        }
    }
}