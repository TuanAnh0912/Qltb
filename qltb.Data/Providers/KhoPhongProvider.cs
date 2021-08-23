using qltb.Data.Helpers;
using qltb.Data.Models;
using qltb.Data.ReqVMs;
using qltb.Data.ResVMs;
using qltb.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class KhoPhongProvider : ApplicationDbcontext
    {
        GetForSelectProvider _get = new GetForSelectProvider();

        public KhoPhong getByMaKhoPhong(string ma)
        {
            try
            {
                return DbContext.KhoPhongs.First(m => m.MaKhoPhong.Equals(ma));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<KhoPhongResVM> GetAllKhoPhong()
        {
            try
            {
                string command = @"select  ROW_NUMBER() OVER(ORDER BY a.KhoPhongId ASC) AS STT,* from KhoPhong as a
                                    left join LoaiKhoPhong as b on a.LoaiKhoPhongId=b.LoaiKhoPhongId
                                    left join KieuSuDungKhoPhong as c on a.KieuSuDungKhoPhongId=c.KieuSuDungKhoPhongId
                                    left join XepHangKhoPhong as d on a.XepHangKhoPhongId=d.XepHangKhoPhongId
                                    where a.IsDelete=0";
                var lstAllPhongBan = DbContext.Database.SqlQuery<KhoPhongResVM>(command).ToList();
                return lstAllPhongBan;

            }
            catch (Exception e)
            {

                return new List<KhoPhongResVM>();
            }
        }
        public List<LogThietBiResVM> GetAllLogByKhoPhong(int khoPhongId)
        {
            try
            {
                var lstLog = new List<LogThietBiResVM>();
                string command = @"select a.LogId,(CONVERT(varchar,a.ThoiGian,108) + ' ' + CONVERT(varchar,a.ThoiGian,103)) as 'ThoiGian',a.SoPhieuId,a.ThietBiId,a.SoLuong,a.MaLoaiThayDoi,b.*,c.*,d.* from LogThietBi as a
                                left join NguoiDung as b on a.NguoiDungId=b.NguoiDungId
                                left join ThietBi as c on c.ThietBiId=a.ThietBiId
                                left join LoaiThayDoi as d on a.MaLoaiThayDoi=d.MaLoaiThayDoi
                                left join KhoPhong as e on a.KhoPhongId=e.KhoPhongId
                                where a.KhoPhongId=@khoPhongId";
                lstLog = DbContext.Database.SqlQuery<LogThietBiResVM>(command,new SqlParameter("@khoPhongId",khoPhongId)).ToList();
                return lstLog;
            }
            catch (Exception)
            {

                return new List<LogThietBiResVM>();
            }
        }
        public KhoPhongResVM GetKhoPhongById(int khoPhongId)
        {
            try
            {
                string command = @"select  ROW_NUMBER() OVER(ORDER BY a.KhoPhongId ASC) AS STT,* from KhoPhong as a
                                    left join LoaiKhoPhong as b on a.LoaiKhoPhongId=b.LoaiKhoPhongId
                                    left join KieuSuDungKhoPhong as c on a.KieuSuDungKhoPhongId=c.KieuSuDungKhoPhongId
                                    left join XepHangKhoPhong as d on a.XepHangKhoPhongId=d.XepHangKhoPhongId
                                    where a.IsDelete=0 and a.KhoPhongId=@khoPhongId";
                var lstAllPhongBan = DbContext.Database.SqlQuery<KhoPhongResVM>(command, new SqlParameter("@khoPhongId", khoPhongId)).FirstOrDefault();
                return lstAllPhongBan;

            }
            catch (Exception e)
            {

                return new KhoPhongResVM();
            }
        }
        public List<XepHangKhoPhong> GetXepHangKhoPhongs()
        {
            try
            {
                return DbContext.XepHangKhoPhongs.ToList();
            }
            catch (Exception e)
            {

                return new List<XepHangKhoPhong>();
            }
        }
        public bool Insert(KhoPhong model)
        {
            try
            {
                DbContext.KhoPhongs.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<LoaiKhoPhong> GetLoaiKhoPhongs()
        {
            try
            {
                return DbContext.LoaiKhoPhongs.ToList();
            }
            catch (Exception e)
            {

                return new List<LoaiKhoPhong>();
            }
        }
        public List<KieuSuDungKhoPhong> GetKieuSuDungKhoPhongs()
        {
            try
            {
                return DbContext.KieuSuDungKhoPhongs.ToList();
            }
            catch (Exception e)
            {

                return new List<KieuSuDungKhoPhong>();
            }
        }
        public ResponseModel DeleteKhoPhong(int khoPhongId)
        {
            try
            {
                var khoPhong = DbContext.KhoPhongs.FirstOrDefault(x => x.KhoPhongId == khoPhongId);
                khoPhong.IsDelete = true;
                DbContext.SaveChanges();
                return new ResponseModel(true, "true", "Xoá thành công");

            }
            catch (Exception e)
            {

                return new ResponseModel(false, "fail", "Xoá thất bại");


            }
        }
        public List<ThietBiResVM> GetThietBiByKhoPhong(int khoPhongId)
        {
            try
            {
                string command = @"select ROW_NUMBER() OVER(order by c.ThietBiId ASC) as 'STT' ,c.ThietBiId,c.MaThietBi,c.TenThietBi,b.SoLuong,b.SoLuongConLai from KhoPhong as a
                                   left join KhoThietBi as b on a.KhoPhongId=b.KhoPhongId
                                   left join ThietBi as c on b.ThietBiId=c.ThietBiId
                                   where a.IsDelete=0 and a.KhoPhongId=@khoPhongId";
                var lstThietBi = DbContext.Database.SqlQuery<ThietBiResVM>(command, new SqlParameter("@khoPhongId", khoPhongId)).ToList();
                foreach (var item in lstThietBi)
                {
                    string command1 = @"select sum(SoLuongHong) as 'SoLuongHong' from ChiTietPhieuGhiHongThietBi
                                      where KhoPhongId=@khoPhongId and ThietBiId=@thietBiId";
                    var hong = DbContext.Database.SqlQuery<HongMatThietViModel>(command1, new SqlParameter("@khoPhongId", khoPhongId), 
                                                                                             new SqlParameter("@thietBiId", item.ThietBiId)).FirstOrDefault();
                    string command2 = @"select SUM(SoLuongMat) as 'SoLuongMat' from ChiTietPhieuGhiMatThietBi
                                      where KhoPhongId=@khoPhongId and ThietBiId=@thietBiId";
                    var mat = DbContext.Database.SqlQuery<HongMatThietViModel>(command2, new SqlParameter("@khoPhongId", khoPhongId),
                                                                                             new SqlParameter("@thietBiId", item.ThietBiId)).FirstOrDefault();
                    item.SoLuongHong = hong.SoLuongHong;
                    item.SoLuongMat = mat.SoLuongMat;
                }
                return lstThietBi;
            }
            catch (Exception e)
            {

                return new List<ThietBiResVM>();
            }
        }
        public ResponseModel UpdateKhoPhong(UpdateKhoPhongReqVM model)
        {
            try
            {
                var khoPhongNew = DbContext.KhoPhongs.FirstOrDefault(x => x.KhoPhongId == model.KhoPhongId);
                MapData<KhoPhong>.CopyDataObject(model, ref khoPhongNew);
                DbContext.SaveChanges();
                return new ResponseModel(true, "true", "cập nhật thành công");

            }
            catch (Exception e)
            {

                return new ResponseModel(false, "fail", "cập nhật thất bại");

            }
        }
        public ResponseModel InsertKhoPhong(KhoPhong model, Guid nguoiDungId)
        {
            try
            {
                string command = @"select b.* from NguoiDung as
                                    a inner join DonVi as b on a.DonViId=b.DonViId
                                    where a.NguoiDungId=@nguoiDungId";
                var thongTinDonVi = DbContext.Database.SqlQuery<DonVi>(command, new SqlParameter("@nguoiDungId", nguoiDungId)).FirstOrDefault();
                if (thongTinDonVi != null)
                {
                    model.MaKhoPhong = thongTinDonVi.MaDonVi + RamdomHelper.RandomString(4);
                    model.DonViId = thongTinDonVi.DonViId;
                }
                else
                {
                    model.MaKhoPhong = RamdomHelper.RandomString(4);
                }
                model.NgayTao = DateTime.Now;
                model.IsDelete = false;
                DbContext.KhoPhongs.Add(model);
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Thêm mới thành công");
            }
            catch (Exception e)
            {

                return new ResponseModel(false, "fail", "Thêm thất bại");
            }
        }
        public ChiTietPhongViewModel GetToDetail(int storage_id)
        {
            try
            {
                var model = new ChiTietPhongViewModel();
                model.ThietBis = GetThietBiByKhoPhong(storage_id);
                model.MonHocs = _get.GetMonHocs();
                model.KhoPhongs = _get.GetKhoPhongs();
                model.LoaiThietBis = _get.GetLoaiThietBis();
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<LoaiKhoPhongViewModel> GetAllLoaiKhoPhong()
        {
            try
            {
                var command = @"SELECT l.TenLoaiKhoPhong, l.MaLoaiKhoPhong, l.LoaiKhoPhongId, COUNT(k.KhoPhongId) as SoLuongPhong
                            FROM LoaiKhoPhong as l left join KhoPhong as k on l.LoaiKhoPhongId = k.LoaiKhoPhongId
                            Group by l.TenLoaiKhoPhong, l.MaLoaiKhoPhong, l.LoaiKhoPhongId";
                return DbContext.Database.SqlQuery<LoaiKhoPhongViewModel>(command).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<KhoPhong> GetByLoaiKhoPhongId(int LoaiKhoPhongId)
        {
            try
            {
                return DbContext.KhoPhongs.Where(k => k.LoaiKhoPhongId == LoaiKhoPhongId).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
