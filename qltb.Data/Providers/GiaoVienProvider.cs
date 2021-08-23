using qltb.Data.Helpers;
using qltb.Data.Models;
using qltb.Data.ReqVMs;
using qltb.Data.ResVMs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class GiaoVienProvider : ApplicationDbcontext
    {
        public List<GiaoVienResVM> GetAllGiaoVien()
        {
            try
            {
                string command = @"select ROW_NUMBER() OVER(ORDER BY a.GiaoVienId ASC) as'STT',a.GiaoVienId,a.TenGiaoVien,a.SoCMND,c.TenGioiTinh,a.DangLamViec,a.GioiTinhId,a.DangLamViec,a.ToBoMonId,b.TenToBoMon,a.IsDelete,a.MaGiaoVien,convert(varchar, a.NgayVaoNganh, 103) as 'NgayVaoNganh' ,convert(varchar, a.NgayNghiViec, 103) as 'NgayNghiViec',convert(varchar, a.NgaySinh, 103) as 'NgaySinh'  from GiaoVien as a
                                   left join ToBoMon as b on a.ToBoMonId=b.ToBoMonId
                                   left join GioiTinh as c on a.GioiTinhId=c.GioiTinhId
                                   where a.IsDelete=0";
                var lstDsGiaoVien = DbContext.Database.SqlQuery<GiaoVienResVM>(command).ToList();
                return lstDsGiaoVien;

            }
            catch (Exception e)
            {

                return new List<GiaoVienResVM>();
            }
        }
        public GiaoVienResVM GetGiaoVienById(int id)
        {
            try
            {
                string command = @"select ROW_NUMBER() OVER(ORDER BY a.GiaoVienId ASC) as'STT',a.GiaoVienId,a.TenGiaoVien,a.SoCMND,c.TenGioiTinh,a.DangLamViec,a.GioiTinhId,a.DangLamViec,a.ToBoMonId,b.TenToBoMon,a.IsDelete,a.MaGiaoVien,convert(varchar, a.NgayVaoNganh, 103) as 'NgayVaoNganh' ,convert(varchar, a.NgayNghiViec, 103) as 'NgayNghiViec',convert(varchar, a.NgaySinh, 103) as 'NgaySinh'  from GiaoVien as a
                                   left join ToBoMon as b on a.ToBoMonId=b.ToBoMonId
                                   left join GioiTinh as c on a.GioiTinhId=c.GioiTinhId
                                   where a.IsDelete=0 and a.GiaoVienId=@id";
                var lstDsGiaoVien = DbContext.Database.SqlQuery<GiaoVienResVM>(command, new SqlParameter("@id", id)).FirstOrDefault();
                return lstDsGiaoVien;

            }
            catch (Exception e)
            {

                return new GiaoVienResVM();
            }
        }
        public List<GiaoVienResVM> GetAllGiaoVienByToBoMonId(int toBoMonId)
        {
            try
            {
                string command = @"select ROW_NUMBER() OVER(ORDER BY a.GiaoVienId ASC) as'STT',a.GiaoVienId,a.TenGiaoVien,a.SoCMND,c.TenGioiTinh,a.DangLamViec,a.GioiTinhId,a.DangLamViec,a.ToBoMonId,b.TenToBoMon,a.IsDelete,a.MaGiaoVien,convert(varchar, a.NgayVaoNganh, 103) as 'NgayVaoNganh' ,convert(varchar, a.NgayNghiViec, 103) as 'NgayNghiViec',convert(varchar, a.NgaySinh, 103) as 'NgaySinh'  from GiaoVien as a
                                   left join ToBoMon as b on a.ToBoMonId=b.ToBoMonId
                                   left join GioiTinh as c on a.GioiTinhId=c.GioiTinhId
                                   where a.IsDelete=0 and b.ToBoMonId=@toBoMonId";
                var lstDsGiaoVien = DbContext.Database.SqlQuery<GiaoVienResVM>(command, new SqlParameter("@toBoMonId", toBoMonId)).ToList();
                return lstDsGiaoVien;

            }
            catch (Exception e)
            {

                return new List<GiaoVienResVM>();
            }
        }
        public ResponseModel DeleteGiaoVien(int giaoVienId)
        {
            try
            {
                var oldModel = DbContext.GiaoViens.FirstOrDefault(t => t.GiaoVienId == giaoVienId);
                oldModel.IsDelete = true;
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Xoá thành công");
            }
            catch (Exception e)
            {

                return new ResponseModel(false, "fail", "Xoá nhật thất bại");

            }
        }
        public ResponseModel UpdateGiaoVien(UpdateGiaoVienReqVM model)
        {
            try
            {
                var oldModel = DbContext.GiaoViens.FirstOrDefault(t => t.GiaoVienId == model.GiaoVienId);
                MapData<GiaoVien>.CopyDataObject(model, ref oldModel);
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Cập nhật thành công");
            }
            catch (Exception e)
            {

                return new ResponseModel(false, "fail", "Cập nhật thất bại");

            }
        }
        public ResponseModel InsertGiaoVien(GiaoVien model, Guid nguoiDungId, string tenDangNhap, string matKhau)
        {
            using (var Dbtrans = DbContext.Database.BeginTransaction())
            {
                try
                {
                    model.IsDelete = false;
                    string command = @"select b.* from NguoiDung as
                                    a inner join DonVi as b on a.DonViId=b.DonViId
                                    where a.NguoiDungId=@nguoiDungId";
                    var thongTinDonVi = DbContext.Database.SqlQuery<DonVi>(command, new SqlParameter("@nguoiDungId", nguoiDungId)).FirstOrDefault();
                    if (thongTinDonVi != null)
                    {
                        model.MaGiaoVien = thongTinDonVi.MaDonVi + RamdomHelper.RandomString(4);
                    }
                    else
                    {
                        model.MaGiaoVien = RamdomHelper.RandomString(4);
                    }
                    model.IsDelete = false;
                    DbContext.GiaoViens.Add(model);
                    DbContext.SaveChanges();
                    var nguoiDungModel = new NguoiDung();
                    nguoiDungModel.NguoiDungId = Guid.NewGuid();
                    string salt = Guid.NewGuid().ToString().ToLower();
                    nguoiDungModel.Salt = salt;
                    string matkhau = Data.Helpers.SecurityHelper.Hash(nguoiDungModel.Salt + matKhau).ToLower();
                    nguoiDungModel.MatKhau = matkhau;
                    nguoiDungModel.TenDangNhap = tenDangNhap;
                    nguoiDungModel.GiaoVienId = model.GiaoVienId;
                    nguoiDungModel.HoTen = model.TenGiaoVien;
                    nguoiDungModel.NhomNguoiDungId = 2;
                    nguoiDungModel.NgayTao = DateTime.Now;
                    nguoiDungModel.GioiTinhId = model.GioiTinhId;
                    nguoiDungModel.IsActive = true;
                    DbContext.NguoiDungs.Add(nguoiDungModel);
                    DbContext.SaveChanges();
                    Dbtrans.Commit();
                    return new ResponseModel(true, "success", "Thêm thành công");
                }
                catch (Exception e)
                {
                    Dbtrans.Rollback();
                    return new ResponseModel(false, "fail", "Thêm thất bại");
                }
            }

        }
    }
}
