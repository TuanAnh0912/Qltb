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
    public class ThietBiProvider : ApplicationDbcontext
    {

        public ThietBi getByMaThietBi(string ma)
        {
            try
            {
                return DbContext.ThietBis.First(m => m.MaThietBi.Equals(ma));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ThietBi getById(int id)
        {
            try
            {
                return DbContext.ThietBis.First(m => m.ThietBiId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ThietBiResVM> GetAllThietBi(string searchString, int? MonHocId, int? KhoiLopId, int? LoaiThietBiId)
        {
            try
            {
                string command = @"select * from ThietBi as a
                                   left join LoaiThietBi as b on a.LoaiThietBiId=b.LoaiThietBiId
                                   left join DonViTinh as c on a.DonViTinhId=c.DonViTinhId
                                   left join MonHoc as d on a.MonHocId= d.MonHocId
								   left join DonVi as e on a.DonViId=e.DonViId
						           where a.IsDelete=0 and a.TenThietBi LIKE N'%" + searchString + "%'";
                if (MonHocId.HasValue)
                {
                    if(MonHocId.Value == 0)
                    {
                        command += " and a.MonHocId IS NULL";
                    }
                    else
                    {
                        command += " and a.MonHocId = " + MonHocId.Value;
                    }
                    
                }
                if (LoaiThietBiId.HasValue)
                {
                    command += " and a.LoaiThietBiId = " + LoaiThietBiId.Value;
                }
                var lstDSThietBi = DbContext.Database.SqlQuery<ThietBiResVM>(command).ToList();
                foreach (var tb in lstDSThietBi)
                {
                    string command2 = @"SELECT k.* from LienKetKhoiLopThietBi as lk inner join ThietBi as t on lk.ThietBiId = t.ThietBiId 
                                        inner join KhoiLop as k on k.KhoiLopId = lk.KhoiLopId 
                                        WHERE t.ThietBiId = @thietBiId";
                    tb.DanhSachKhoiLop = DbContext.Database.SqlQuery<KhoiLop>(command2, new SqlParameter("@thietBiId", tb.ThietBiId)).ToList();

                }
                if (KhoiLopId.HasValue)
                {
                    lstDSThietBi = lstDSThietBi.Where(i => i.DanhSachKhoiLop.Select(a => a.KhoiLopId).Contains(KhoiLopId.Value)).ToList();
                }
                return lstDSThietBi;

            }
            catch (Exception e)
            {

                return new List<ThietBiResVM>();
                throw;
            }
        }
        public LoaiThietBi GetLoaiThietBiByID(int loaiThietbiId)
        {
            try
            {
                return DbContext.LoaiThietBis.FirstOrDefault(t => t.LoaiThietBiId == loaiThietbiId);
            }
            catch (Exception)
            {

                return new LoaiThietBi();
            }
        }
        public ThietBiResVM GetThietBiById(int thietbiId)
        {
            try
            {
                string command = @"select  ROW_NUMBER() OVER(ORDER BY a.ThietBiId ASC) AS STT,* from ThietBi as a
                                   left join LoaiThietBi as b on a.LoaiThietBiId=b.LoaiThietBiId
                                   left join DonViTinh as c on a.DonViTinhId=c.DonViTinhId
                                   left join MonHoc as d on a.MonHocId= d.MonHocId
								   left join DonVi as e on a.DonViId=e.DonViId
						           where a.IsDelete=0 and a.ThietBiId=@thietbiId";
                var ThietBi = DbContext.Database.SqlQuery<ThietBiResVM>(command, new SqlParameter("@thietbiId", thietbiId)).FirstOrDefault();
                var check = DbContext.KhoThietBis.Where(x => x.ThietBiId == thietbiId).ToList();
                if (check.Count > 0) ThietBi.CheckTonTaiTrongKho = true;
                else ThietBi.CheckTonTaiTrongKho = false;
                string command2 = @"select c.* from ThietBi as a 
                                    left join LienKetKhoiLopThietBi as b on a.ThietBiId=b.ThietBiId
                                    left join KhoiLop as c on b.KhoiLopId=c.KhoiLopId
                                    where a.ThietBiId=@thietBiId";
                ThietBi.DanhSachKhoiLop = DbContext.Database.SqlQuery<KhoiLop>(command2, new SqlParameter("@thietBiId", thietbiId)).ToList();


                return ThietBi;

            }
            catch (Exception e)
            {

                return new ThietBiResVM();
                throw;
            }
        }
        public List<ThietBiResVM> Search(string Keyword)
        {
            try
            {
                string sqlSearch = @"select  ROW_NUMBER() OVER(ORDER BY a.ThietBiId ASC) AS STT,* from ThietBi as a
                                   left join LoaiThietBi as b on a.LoaiThietBiId=b.LoaiThietBiId
                                   left join DonViTinh as c on a.DonViTinhId=c.DonViTinhId
                                   left join MonHoc as d on a.MonHocId= d.MonHocId
								   left join DonVi as e on a.DonViId=e.DonViId
								   left join KhoPhong as f on a.KhoPhongId=f.KhoPhongId
						           where a.IsDelete=0 and (a.MaThietBi like N'%'+@Keyword+'%')
                                                       or (a.TenThietBi like N'%'+@Keyword+'%')
                                                       or (b.TenLoaiThietBi like N'%'+@Keyword+'%')
                                                       or (d.TenMonHoc like N'%'+@Keyword+'%') ";
                var lstThietBi = DbContext.Database.SqlQuery<ThietBiResVM>(sqlSearch, new SqlParameter("@keyWord", Keyword)).ToList();
                return lstThietBi;
            }
            catch (Exception e)
            {

                return new List<ThietBiResVM>();
            }
        }
        public List<DonVi> GetAllDonVi()
        {
            try
            {
                return DbContext.DonVis.ToList();
            }
            catch (Exception e)
            {

                return new List<DonVi>();
            }
        }
        public List<DonViTinh> GetAllDonViTinh()
        {
            try
            {
                return DbContext.DonViTinhs.ToList();
            }
            catch (Exception e)
            {

                return new List<DonViTinh>();
            }
        }
        public List<LoaiThietBi> GetAllLoaiThietBi()
        {
            try
            {
                return DbContext.LoaiThietBis.ToList();
            }
            catch (Exception e)
            {

                return new List<LoaiThietBi>();
            }
        }
        public bool InsertLienKetKhoiLopThietBi(int khoiLopId, List<int> lstKhoiHoc)
        {
            try
            {
                var lstKhoiHocThietBi = new List<LienKetKhoiLopThietBi>();
                foreach (var lk in lstKhoiHoc)
                {
                    var lkModel = new LienKetKhoiLopThietBi();
                    lkModel.KhoiLopId = lk;
                    lkModel.ThietBiId = khoiLopId;
                    lstKhoiHocThietBi.Add(lkModel);
                }
                DbContext.LienKetKhoiLopThietBis.AddRange(lstKhoiHocThietBi);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public ResponseModel DeleteThietBi(int thietBiId)
        {
            try
            {
                var model = DbContext.ThietBis.FirstOrDefault(Xa => Xa.ThietBiId == thietBiId);
                model.IsDelete = true;
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Xoá thành công");
            }
            catch (Exception e)
            {

                return new ResponseModel(false, "fail", "Xoá thất bại");
            }
        }
        public ResponseModel UpdateThietBi(UpdateThietBiReqVM model, List<int> lstKhoiHoc)
        {
            using (var dbTransection = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var newModel = DbContext.ThietBis.FirstOrDefault(x => x.ThietBiId == model.ThietBiId);
                    MapData<ThietBi>.CopyDataObject(model, ref newModel);
                    DbContext.SaveChanges();
                    var oldLK = DbContext.LienKetKhoiLopThietBis.Where(x => x.ThietBiId == model.ThietBiId).ToList();
                    DbContext.LienKetKhoiLopThietBis.RemoveRange(oldLK);
                    DbContext.SaveChanges();
                    if (InsertLienKetKhoiLopThietBi(model.ThietBiId, lstKhoiHoc))
                    {
                        dbTransection.Commit();
                        return new ResponseModel(true, "success", "Cập nhật thành công");
                    }
                    else
                    {
                        dbTransection.Rollback();
                        return new ResponseModel(false, "fail", "Cập nhật thất bại");
                    }
                }
                catch (Exception e)
                {
                    dbTransection.Rollback();
                    return new ResponseModel(false, "fail", "Cập nhật thất bại");
                }
            }

        }
        public ResponseModel InsertThietBi(ThietBi model, List<int> lstKhoiHoc, Guid nguoiDungId)
        {
            using (var dbTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    string command = @"select b.* from NguoiDung as
                                    a inner join DonVi as b on a.DonViId=b.DonViId
                                    where a.NguoiDungId=@nguoiDungId";
                    var thongTinDonVi = DbContext.Database.SqlQuery<DonVi>(command, new SqlParameter("@nguoiDungId", nguoiDungId)).FirstOrDefault();

                    model.NgayTao = DateTime.Now;
                    model.IsDelete = false;
                    if (thongTinDonVi != null)
                    {
                        model.MaThietBi = thongTinDonVi.MaDonVi + RamdomHelper.RandomString(4);
                        model.DonViId = thongTinDonVi.DonViId;
                    }
                    else
                    {
                        model.MaThietBi = RamdomHelper.RandomString(4);
                    }
                    DbContext.ThietBis.Add(model);
                    DbContext.SaveChanges();
                    if (InsertLienKetKhoiLopThietBi(model.ThietBiId, lstKhoiHoc))
                    {
                        dbTransaction.Commit();
                        return new ResponseModel(true, "success", "Thêm thiết bị thành công");
                    }
                    else
                    {
                        dbTransaction.Rollback();
                        return new ResponseModel(false, "fail", "Thêm thất bại");
                    }
                }
                catch (Exception e)
                {
                    dbTransaction.Rollback();
                    return new ResponseModel(false, "fail", "Thêm thất bại");

                }
            }

        }
        public List<ThietBiResVM> GetThietBiByMonHoc(int? monHocId, int? loaiThietBiId, int? khoiLopId, string kyTu)
        {
            try
            {
                string command = @"select  ROW_NUMBER() OVER(ORDER BY a.ThietBiId ASC) AS STT,* from ThietBi as a
                                   left join LoaiThietBi as b on a.LoaiThietBiId=b.LoaiThietBiId
                                   left join DonViTinh as c on a.DonViTinhId=c.DonViTinhId
                                   left join MonHoc as d on a.MonHocId= d.MonHocId
								   left join DonVi as e on a.DonViId=e.DonViId
                                   left join LienKetKhoiLopThietBi as f on f.ThietBiId=a.ThietBiId
                                   where ((@monHocId is null) or(a.MonHocId=@monHocId)) and ((@loaiThietBiId is null) or (a.LoaiThietBiId=@loaiThietBiId)) and ((@khoiLopId is null) or (f.KhoiLopId=@khoiLopId)) and ( (@kyTu is null) or (a.MaThietBi like N'%'+@kyTu+'%') or (a.TenThietBi like N'%'+@kyTu+'%') or (b.TenLoaiThietBi like N'%'+@kyTu+'%' ))and a.IsDelete=0 ";
                var lstDSThietBi = DbContext.Database.SqlQuery<ThietBiResVM>(command, new SqlParameter("@monHocId", monHocId != null ? monHocId.Value : (object)DBNull.Value),
                                                                                      new SqlParameter("@loaiThietBiId", loaiThietBiId != null ? loaiThietBiId.Value : (object)DBNull.Value),
                                                                                      new SqlParameter("@khoiLopId", khoiLopId != null ? khoiLopId.Value : (object)DBNull.Value),
                                                                                      new SqlParameter("@kyTu", kyTu != "" ? kyTu : (object)DBNull.Value)).ToList();

                foreach (var tb in lstDSThietBi)
                {
                    string command2 = @"select c.* from ThietBi as a 
                                    left join LienKetKhoiLopThietBi as b on a.ThietBiId=b.ThietBiId
                                    left join KhoiLop as c on b.KhoiLopId=c.KhoiLopId
                                    where a.ThietBiId=@thietBiId and ((@khoiLopId is null) or ( b.KhoiLopId = @khoiLopId))";
                    tb.DanhSachKhoiLop = DbContext.Database.SqlQuery<KhoiLop>(command2, new SqlParameter("@thietBiId", tb.ThietBiId),
                                                                                        new SqlParameter("@khoiLopId", khoiLopId != null ? khoiLopId.Value : (object)DBNull.Value)).ToList();

                }
                return lstDSThietBi;

            }
            catch (Exception e)
            {

                return new List<ThietBiResVM>();
                throw;
            }
        }
        public List<ThietBiResVM> GetThietBiByDonVi(Guid donviId)
        {
            try
            {
                string command = @"select  ROW_NUMBER() OVER(ORDER BY a.ThietBiId ASC) AS STT,* from ThietBi as a
                                   left join LoaiThietBi as b on a.LoaiThietBiId=b.LoaiThietBiId
                                   left join DonViTinh as c on a.DonViTinhId=c.DonViTinhId
                                   left join MonHoc as d on a.MonHocId= d.MonHocId
								   left join DonVi as e on a.DonViId=e.DonViId
								   left join KhoPhong as f on a.KhoPhongId=f.KhoPhongId
                                   where a.DonViId=@donviId and a.IsDelete=0";
                var lstDSThietBi = DbContext.Database.SqlQuery<ThietBiResVM>(command, new SqlParameter("@donviId", donviId)).ToList();

                foreach (var tb in lstDSThietBi)
                {
                    string command2 = @"select c.* from ThietBi as a 
                                    left join LienKetKhoiLopThietBi as b on a.ThietBiId=b.ThietBiId
                                    left join KhoiLop as c on b.KhoiLopId=c.KhoiLopId
                                    where a.ThietBiId=@thietBiId";
                    tb.DanhSachKhoiLop = DbContext.Database.SqlQuery<KhoiLop>(command2, new SqlParameter("@thietBiId", tb.ThietBiId)).ToList();

                }
                return lstDSThietBi;

            }
            catch (Exception e)
            {

                return new List<ThietBiResVM>();
                throw;
            }
        }
        public List<QuyDinhSoTietSuDungThietBiViewResVM> GetAllLopHocTietQuyDinh()
        {
            try
            {
                var rs = new List<QuyDinhSoTietSuDungThietBiViewResVM>();
                string command = @"select  ROW_NUMBER() Over(order by a.QuyDinhSoTietSuDungThietBiId ASC) as 'STT',a.QuyDinhSoTietSuDungThietBiId,a.QuyDinhSuDungSoTietId,b.TenLop,b.LopId,d.ChuongTrinhHocId,d.TenChuongTrinhHoc,c.MonHocId,c.TenMonHoc,
                                   a.TietTHHKI,a.TietTHHKII,a.TietTNHKI,a.TietTNHKII,a.NamHocBatDau,a.NamHocKetThuc from QuyDinhSoTietSuDungThietBi as a
                                   left join Lop as b on a.LopId=b.LopId
                                   left join MonHoc as c on a.MonHocId=c.MonHocId
                                   left join ChuongTrinhHoc as d on a.ChuongTrinhHocId=d.ChuongTrinhHocId
                                   Where a.IsDelete=0";
                var lst = DbContext.Database.SqlQuery<QuyDinhSoTietSuDungThietBiResVM>(command).ToList();
                var lstGroupBy = lst.GroupBy(
                    c => new
                    {
                        c.ChuongTrinhHocId,
                        c.MonHocId,
                        c.TenMonHoc,
                        c.TietTHHKI,
                        c.TietTHHKII,
                        c.TietTNHKI,
                        c.TietTNHKII,
                        c.NamHocBatDau,
                        c.NamHocKetThuc,
                        c.TenChuongTrinhHoc,
                        c.QuyDinhSuDungSoTietId
                    });
                int stt = 1;
                foreach (var item in lstGroupBy.ToList())
                {
                    var model = new QuyDinhSoTietSuDungThietBiViewResVM();
                    model.LstLopIds = new List<int>();
                    model.ThongTin = new QuyDinhSoTietSuDungThietBiResVM();
                    model.QuyDinhId = item.Key.QuyDinhSuDungSoTietId.Value;
                    foreach (var x in item.ToList())
                    {
                        model.LstLopIds.Add(x.LopId.Value);
                        model.Lops += x.TenLop.ToString() + ",";
                    }
                    model.Lops = model.Lops.Substring(0, model.Lops.Length - 1);
                    model.ThongTin.ChuongTrinhHocId = item.Key.ChuongTrinhHocId;
                    model.ThongTin.TenChuongTrinhHoc = item.Key.TenChuongTrinhHoc;
                    model.ThongTin.MonHocId = item.Key.MonHocId;
                    model.ThongTin.TenMonHoc = item.Key.TenMonHoc;
                    model.ThongTin.TietTHHKI = item.Key.TietTHHKI;
                    model.ThongTin.TietTHHKII = item.Key.TietTHHKII;
                    model.ThongTin.TietTNHKI = item.Key.TietTNHKI;
                    model.ThongTin.TietTNHKII = item.Key.TietTNHKII;
                    model.ThongTin.NamHocBatDau = item.Key.NamHocBatDau;
                    model.ThongTin.NamHocKetThuc = item.Key.NamHocKetThuc;
                    model.STT = stt;
                    stt++;
                    rs.Add(model);
                }
                return rs;
            }
            catch (Exception e)
            {

                return new List<QuyDinhSoTietSuDungThietBiViewResVM>();
            }
        }
        public List<QuyDinhSoTietSuDungThietBiViewResVM> FilterLopHocTietQuyDinh(int? monhocId, int? khoiLopId)
        {
            try
            {
                var rs = new List<QuyDinhSoTietSuDungThietBiViewResVM>();
                string command = @"select  ROW_NUMBER() Over(order by a.QuyDinhSoTietSuDungThietBiId ASC) as 'STT',a.QuyDinhSoTietSuDungThietBiId,a.QuyDinhSuDungSoTietId,b.TenLop,b.LopId,d.ChuongTrinhHocId,d.TenChuongTrinhHoc,c.MonHocId,c.TenMonHoc,
                                   a.TietTHHKI,a.TietTHHKII,a.TietTNHKI,a.TietTNHKII,a.NamHocBatDau,a.NamHocKetThuc from QuyDinhSoTietSuDungThietBi as a
                                   left join Lop as b on a.LopId=b.LopId
                                   left join MonHoc as c on a.MonHocId=c.MonHocId
                                   left join ChuongTrinhHoc as d on a.ChuongTrinhHocId=d.ChuongTrinhHocId
								   left join KhoiLop as e on b.KhoiLopId=e.KhoiLopId
                                   Where  ((@monhocId is null) or (a.MonHocId=@monhocId )) and (( @khoiLopId is null) or (b.KhoiLopId=@khoiLopId)) and a.IsDelete=0";
                var lst = DbContext.Database.SqlQuery<QuyDinhSoTietSuDungThietBiResVM>(command,
                                                           new SqlParameter("@monhocId", monhocId == null ? (object)DBNull.Value : monhocId),
                                                           new SqlParameter("@khoiLopId", khoiLopId == null ? (object)DBNull.Value : khoiLopId)).ToList();
                var lstGroupBy = lst.GroupBy(
                    c => new
                    {
                        c.ChuongTrinhHocId,
                        c.MonHocId,
                        c.TenMonHoc,
                        c.TietTHHKI,
                        c.TietTHHKII,
                        c.TietTNHKI,
                        c.TietTNHKII,
                        c.NamHocBatDau,
                        c.NamHocKetThuc,
                        c.TenChuongTrinhHoc,
                        c.QuyDinhSuDungSoTietId
                    });
                int stt = 1;
                foreach (var item in lstGroupBy.ToList())
                {
                    var model = new QuyDinhSoTietSuDungThietBiViewResVM();
                    model.LstLopIds = new List<int>();
                    model.ThongTin = new QuyDinhSoTietSuDungThietBiResVM();
                    model.QuyDinhId = item.Key.QuyDinhSuDungSoTietId.Value;
                    foreach (var x in item.ToList())
                    {
                        model.LstLopIds.Add(x.LopId.Value);
                        model.Lops += x.TenLop.ToString() + ",";
                    }
                    model.Lops = model.Lops.Substring(0, model.Lops.Length - 1);
                    model.ThongTin.ChuongTrinhHocId = item.Key.ChuongTrinhHocId;
                    model.ThongTin.TenChuongTrinhHoc = item.Key.TenChuongTrinhHoc;
                    model.ThongTin.MonHocId = item.Key.MonHocId;
                    model.ThongTin.TenMonHoc = item.Key.TenMonHoc;
                    model.ThongTin.TietTHHKI = item.Key.TietTHHKI;
                    model.ThongTin.TietTHHKII = item.Key.TietTHHKII;
                    model.ThongTin.TietTNHKI = item.Key.TietTNHKI;
                    model.ThongTin.TietTNHKII = item.Key.TietTNHKII;
                    model.ThongTin.NamHocBatDau = item.Key.NamHocBatDau;
                    model.ThongTin.NamHocKetThuc = item.Key.NamHocKetThuc;
                    model.STT = stt;
                    stt++;
                    rs.Add(model);
                }
                return rs;
            }
            catch (Exception e)
            {

                return new List<QuyDinhSoTietSuDungThietBiViewResVM>();
            }
        }
        public ResponseModel InsertQuyDinhSuDung(List<int> lstLopInt, QuyDinhSoTietSuDungThietBi model)
        {
            using (var DbTrans = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var modelQuyDinhTiet = new QuyDinhSuDungSoTiet();
                    modelQuyDinhTiet.NgayTao = DateTime.Now;
                    DbContext.QuyDinhSuDungSoTiets.Add(modelQuyDinhTiet);
                    DbContext.SaveChanges();
                    foreach (var item in lstLopInt)
                    {
                        model.QuyDinhSuDungSoTietId = modelQuyDinhTiet.QuyDinhSuDungSoTietId;
                        model.LopId = item;
                        model.IsDelete = false;
                        DbContext.QuyDinhSoTietSuDungThietBis.Add(model);
                        DbContext.SaveChanges();
                    }
                    DbTrans.Commit();
                    return new ResponseModel(true, "success", "Thêm thành công");
                }
                catch (Exception e)
                {
                    DbTrans.Rollback();
                    return new ResponseModel(false, "fail", "Thêm thất bại");
                }

            }
        }
        public QuyDinhSoTietSuDungThietBiViewResVM GetQuyDinhTietHocById(int quyDinhId)
        {
            try
            {
                var rs = new List<QuyDinhSoTietSuDungThietBiViewResVM>();
                string command = @"select  ROW_NUMBER() Over(order by a.QuyDinhSoTietSuDungThietBiId ASC) as 'STT',a.QuyDinhSoTietSuDungThietBiId,a.QuyDinhSuDungSoTietId,b.TenLop,b.LopId,d.ChuongTrinhHocId,d.TenChuongTrinhHoc,c.MonHocId,c.TenMonHoc,
                                   a.TietTHHKI,a.TietTHHKII,a.TietTNHKI,a.TietTNHKII,a.NamHocBatDau,a.NamHocKetThuc from QuyDinhSoTietSuDungThietBi as a
                                   left join Lop as b on a.LopId=b.LopId
                                   left join MonHoc as c on a.MonHocId=c.MonHocId
                                   left join ChuongTrinhHoc as d on a.ChuongTrinhHocId=d.ChuongTrinhHocId
                                   where a.IsDelete=0 and a.QuyDinhSuDungSoTietId=@quyDinhId";
                var lst = DbContext.Database.SqlQuery<QuyDinhSoTietSuDungThietBiResVM>(command, new SqlParameter("@quyDinhId", quyDinhId)).ToList();
                var lstGroupBy = lst.GroupBy(
                    c => new
                    {
                        c.ChuongTrinhHocId,
                        c.MonHocId,
                        c.TenMonHoc,
                        c.TietTHHKI,
                        c.TietTHHKII,
                        c.TietTNHKI,
                        c.TietTNHKII,
                        c.NamHocBatDau,
                        c.NamHocKetThuc,
                        c.TenChuongTrinhHoc,
                        c.QuyDinhSuDungSoTietId
                    });
                int stt = 1;
                foreach (var item in lstGroupBy.ToList())
                {
                    var model = new QuyDinhSoTietSuDungThietBiViewResVM();
                    model.LstLopIds = new List<int>();
                    model.ThongTin = new QuyDinhSoTietSuDungThietBiResVM();
                    model.QuyDinhId = item.Key.QuyDinhSuDungSoTietId.Value;
                    foreach (var x in item.ToList())
                    {
                        model.LstLopIds.Add(x.LopId.Value);
                        model.Lops += x.TenLop.ToString() + ",";
                    }
                    model.Lops = model.Lops.Substring(0, model.Lops.Length - 1);
                    model.ThongTin.ChuongTrinhHocId = item.Key.ChuongTrinhHocId;
                    model.ThongTin.TenChuongTrinhHoc = item.Key.TenChuongTrinhHoc;
                    model.ThongTin.MonHocId = item.Key.MonHocId;
                    model.ThongTin.TenMonHoc = item.Key.TenMonHoc;
                    model.ThongTin.TietTHHKI = item.Key.TietTHHKI;
                    model.ThongTin.TietTHHKII = item.Key.TietTHHKII;
                    model.ThongTin.TietTNHKI = item.Key.TietTNHKI;
                    model.ThongTin.TietTNHKII = item.Key.TietTNHKII;
                    model.ThongTin.NamHocBatDau = item.Key.NamHocBatDau;
                    model.ThongTin.NamHocKetThuc = item.Key.NamHocKetThuc;
                    model.STT = stt;
                    stt++;
                    rs.Add(model);
                }
                var rsSelect = rs.FirstOrDefault();
                return rsSelect;
            }
            catch (Exception e)
            {

                return new QuyDinhSoTietSuDungThietBiViewResVM();
            }
        }
        public bool Insert(ThietBi model)
        {
            try
            {
                DbContext.ThietBis.Add(model);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public ResponseModel UpdateQuyDinhSoTiet(UpdateQuyDinhSoTietSuDungThietBiReqVM model)
        {
            try
            {
                var lstModelOld = DbContext.QuyDinhSoTietSuDungThietBis.Where(x => x.QuyDinhSuDungSoTietId == model.QuyDinhSuDungSoTietId).ToList();
                DbContext.QuyDinhSoTietSuDungThietBis.RemoveRange(lstModelOld);
                var quyDinhSung = DbContext.QuyDinhSuDungSoTiets.FirstOrDefault(x => x.QuyDinhSuDungSoTietId == model.QuyDinhSuDungSoTietId);
                quyDinhSung.NgayCapNhat = DateTime.Now;
                DbContext.SaveChanges();
                foreach (var item in model.LstLopId)
                {
                    var newModel = new QuyDinhSoTietSuDungThietBi();
                    newModel.MonHocId = model.MonHocId;
                    newModel.TietTHHKI = model.TietTHHKI;
                    newModel.TietTHHKII = model.TietTHHKII;
                    newModel.TietTNHKI = model.TietTNHKI;
                    newModel.TietTNHKII = model.TietTNHKII;
                    newModel.IsDelete = false;
                    newModel.ChuongTrinhHocId = model.ChuongTrinhHocId;
                    newModel.NamHocBatDau = model.NamHocBatDau;
                    newModel.NamHocKetThuc = model.NamHocKetThuc;
                    newModel.LopId = item;
                    newModel.QuyDinhSuDungSoTietId = model.QuyDinhSuDungSoTietId;
                    DbContext.QuyDinhSoTietSuDungThietBis.Add(newModel);
                    DbContext.SaveChanges();
                }
                return new ResponseModel(true, "success", "Cập nhật thành công");

            }
            catch (Exception)
            {

                return new ResponseModel(false, "fail", "Cập nhật thất bại");
            }
        }

        public List<ThietBi> getAllBy(List<int> ids)
        {
            try
            {
                return DbContext.ThietBis.Where(m => ids.Contains(m.ThietBiId)).ToList();

            }
            catch (Exception ex)
            {

                return new List<ThietBi>();
            }
        }
        public List<ThietBi> getAllBy(int? monHocId,string str)
        {
            try
            {
                return DbContext.ThietBis.Where(m => m.MonHocId == monHocId && m.TenThietBi.Contains(str)).ToList();

            }
            catch (Exception ex)
            {

                return new List<ThietBi>();
            }
        }
    }
}
