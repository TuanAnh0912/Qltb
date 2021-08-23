using qltb.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class ChucNangProvider : ApplicationDbcontext
    {
        public List<ChucNangViewModel> GetAll()
        {
            var command = "select c.*, nc.TenNhomChucNang from ChucNang as c left join NhomChucNang as nc on nc.NhomChucNangId = c.NhomChucNangId";
            try
            {
                return DbContext.Database.SqlQuery<ChucNangViewModel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ChucNangViewModel> GetByUser(string NguoiDungId)
        {
            try
            {
                var chucvus = new ChucVuProvider().GetByNguoiDung(NguoiDungId);
                if (chucvus.Count() > 0)
                {
                    var command = @" select c.*, nc.TenNhomChucNang from ChucNang as c inner join QuyenTruyCap as q on c.ChucNangId = q.ChucNangId left join NhomChucNang as nc on nc.NhomChucNangId = c.NhomChucNangId where c.HienTrenMenu = 1 and ";
                    foreach (var item in chucvus)
                    {
                        command += "q.ChucVuId = " + item.ChucVuId + " or ";
                    }
                    command = command.Substring(1, command.Length - 4);

                    var chucnags =  DbContext.Database.SqlQuery<ChucNangViewModel>(command).ToList();
                    var result = chucnags.GroupBy(x => x.ChucNangId).Select(x => x.First()).ToList();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ChucNangViewModel> GetAllByUser(string NguoiDungId)
        {
            try
            {
                var chucvus = new ChucVuProvider().GetByNguoiDung(NguoiDungId);
                if (chucvus.Count() > 0)
                {
                    var command = @" select c.*, nc.TenNhomChucNang from ChucNang as c inner join QuyenTruyCap as q on c.ChucNangId = q.ChucNangId left join NhomChucNang as nc on nc.NhomChucNangId = c.NhomChucNangId where ";
                    foreach (var item in chucvus)
                    {
                        command += "q.ChucVuId = " + item.ChucVuId + " or ";
                    }
                    command = command.Substring(1, command.Length - 4);

                    var chucnags = DbContext.Database.SqlQuery<ChucNangViewModel>(command).ToList();
                    var result = chucnags.GroupBy(x => x.ChucNangId).Select(x => x.First()).ToList();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ChucNangViewModel> GetAllByChuCVu(int ChucVuId)
        {
            try
            {
                var command = "select c.*, nc.TenNhomChucNang from ChucNang as c inner join QuyenTruyCap as q on c.ChucNangId = q.ChucNangId left join NhomChucNang as nc on nc.NhomChucNangId = c.NhomChucNangId where q.ChucVuId = " + ChucVuId;
                var chucnags = DbContext.Database.SqlQuery<ChucNangViewModel>(command).ToList();
                return chucnags;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string RenderMenu(List<ChucNangViewModel> ChucNangs)
        {
            try
            {
                string str = @"<nav id='stacked-menu' class='stacked-menu'></br><ul class='menu'>";
                str += @"<li class='menu-item'>
                                <a href='/trang-chu/tong-quan-he-thong' class='menu-link'><span class='menu-icon far fa-tachometer-alt'></span> <span class='menu-text'>Dashboard</span></a>
                            </li>";
                var nhomchucnangs = DbContext.NhomChucNangs.ToList();
                foreach (var cat in nhomchucnangs)
                {
                    str += "<li class='menu-header'>" + cat.TenNhomChucNang + "</li>";
                    foreach (var parent in ChucNangs.Where(c =>c.NhomChucNangId == cat.NhomChucNangId))
                    {
                        if (GetByParentId(parent.ChucNangId).Count() > 0)
                        {
                            str += @"<li class='menu-item has-child'><a href='#' class='menu-link'><span class='menu-icon " + parent.Icon + "'></span> <span class='menu-text'>" + parent.TenChucNang + "</span></a><ul class='menu'>";
                            foreach (var child in ChucNangs)
                            {
                                if (child.KhoaChaId == parent.ChucNangId)
                                {
                                    str += @"<li class='menu-item'><a href='" + child.DuongDan + "' class='menu-link'>" + child.TenChucNang + "</a></li>";
                                }
                            }
                            str += @"</ul></li>";
                        }
                        else
                        {
                            str += @"<li class='menu-item'><a href='" + parent.DuongDan + "' class='menu-link'><span class='menu-icon " + parent.Icon + "'></span> <span class='menu-text'>"+parent.TenChucNang+"</span></a></li>";
                        }
                    }
                }
                str += "</ul></nav>";
                return str;
            }
            catch (Exception e)
            {
                return "<div class='p-3'><h4 class='text-muted'>Bạn chưa được phân chức vụ nào, liên hệ admin để được hỗ trợ</h4></div>";
            }
        }
        public List<ChucNangViewModel> GetByParentId(int id)
        {
            try
            {
                var command = "select c.*, n.TenNhomChucNang from ChucNang as c left join NhomChucNang as n on c.NhomChucNangId = n.NhomChucNangId where c.KhoaChaId = " + id;
                return DbContext.Database.SqlQuery<ChucNangViewModel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string GetMunuByUser(string NguoiDungId)
        {
            try
            {
                var chucnangs = GetByUser(NguoiDungId);
                return RenderMenu(chucnangs);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
