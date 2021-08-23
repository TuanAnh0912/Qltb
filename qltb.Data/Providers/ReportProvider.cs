using qltb.Data.Helpers;
using qltb.Data.ResVMs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class ReportProvider : ApplicationDbcontext
    {
        public ReportHongMatResVM GetReportHongMat(int? namHoc, string tenThietBi, int? monHocId, int? khoPhongId, int? currentPage, int? pageSize)
        {
            currentPage = currentPage.HasValue ? currentPage : 1;
            pageSize = pageSize.HasValue ? pageSize : 10;
            Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
            queryParamsDict.Add("TenThietBi", string.IsNullOrEmpty(tenThietBi) ? "" : tenThietBi.Trim());
            queryParamsDict.Add("KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value.ToString() : null);
            queryParamsDict.Add("MonHocId", monHocId.HasValue ? monHocId.Value.ToString() : null);
            queryParamsDict.Add("NamHoc", namHoc.HasValue ? namHoc.Value.ToString() : DateTime.Now.Year.ToString());
            try
            {
                ReportHongMatResVM reportHongMatResVM = new ReportHongMatResVM();
                reportHongMatResVM.ThietBis = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    command.CommandType = CommandType.Text;
                    try
                    {
                        // tong so thiet bi hong
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull((Select Sum(SoLuongHong) 
                                                                From ChiTietPhieuGhiHongThietBi as a
                                                                Inner Join PhieuGhiHongThietBi as b
                                                                on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                                                Where Year(b.NgayLap) = @NamHoc), 0) 
                                                        - 
                                                        IsNull((Select Sum(SoLuongSuaChua) 
                                                                From ChiTietPhieuSuaChua as a
                                                                Left Join PhieuSuaChua as b
                                                                on a.PhieuSuaChuaId = b.PhieuSuaChuaId
                                                                Where Year(b.NgayLap) = @NamHoc), 0) ";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    reportHongMatResVM.TongSoThietBiHong = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so thiet bi sua chua
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(SoLuongSuaChua), 0)
                                                From ChiTietPhieuSuaChua as a
                                                Left Join PhieuSuaChua as b
                                                on a.PhieuSuaChuaId = b.PhieuSuaChuaId
                                                Where Year(b.NgayLap) = @NamHoc";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    reportHongMatResVM.TongSoSuaChua = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so thiet bi mat
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(SoLuongMat), 0)
                                                From ChiTietPhieuGhiMatThietBi as a
                                                Left Join PhieuGhiMatThietBi as b
                                                on a.PhieuGhiMatThietBiId = b.PhieuGhiMatThietBiId
                                                Where Year(b.NgayLap) = @NamHoc";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    reportHongMatResVM.TongSoThietBiMat = Convert.ToInt32(reader[0]);
                                }
                            }
                        }


                        // danh sach thiet bi
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select a.*, d.TenThietBi, e.TenMonHoc, f.TenLoaiThietBi, g.TenDonViTinh, h.TenKhoPhong
                                                From (Select IsNull(a.KhoPhongId, IsNull(b.KhoPhongId, IsNull(c.KhoPhongId,0))) as 'KhoPhongId', 
                                                            IsNull(a.ThietBiId, IsNull(b.ThietBiId, IsNull(c.ThietBiId,0))) as 'ThietBiId',
		                                                    (IsNull(a.SoLuongHong, 0) - IsNull(c.SoLuongSuaChua, 0)) as 'SoLuongHong',
		                                                    IsNull(b.SoLuongMat, 0) as 'SoLuongMat', IsNull(c.SoLuongSuaChua, 0) as 'SoLuongSuaChua'
                                                    From (Select a.KhoPhongId, a.ThietBiId, Sum(a.SoLuongHong) as 'SoLuongHong' 
                                                            From ChiTietPhieuGhiHongThietBi as a
                                                            Left Join PhieuGhiHongThietBi as b
                                                            on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                                            Where Year(b.NgayLap) = @NamHoc
			                                                    Group by a.KhoPhongId, a.ThietBiId) as a
                                                    Full Join (Select a.KhoPhongId, a.ThietBiId, Sum(a.SoLuongMat) as 'SoLuongMat' 
                                                        From ChiTietPhieuGhiMatThietBi as a
                                                        Left Join PhieuGhiMatThietBi as b
                                                        on a.PhieuGhiMatThietBiId = b.PhieuGhiMatThietBiId
                                                        Where Year(b.NgayLap) = @NamHoc 
                                                        Group by a.KhoPhongId, a.ThietBiId) as b
                                                    on a.KhoPhongId = b.KhoPhongId and a.ThietBiId = b.ThietBiId
                                                    Full Join (Select a.KhoPhongId, a.ThietBiId, Sum(a.SoLuongSuaChua) as 'SoLuongSuaChua' 
                                                        From ChiTietPhieuSuaChua as a
                                                        Left Join PhieuSuaChua as b
                                                        on a.PhieuSuaChuaId = b.PhieuSuaChuaId
                                                        Where Year(b.NgayLap) = @NamHoc
                                                        Group by a.KhoPhongId, a.ThietBiId) as c
                                                    on a.KhoPhongId = c.KhoPhongId and a.ThietBiId = c.ThietBiId) as a
                                                Left Join ThietBi as d
                                                on a.ThietBiId = d.ThietBiId
                                                Left Join MonHoc as e
                                                on d.MonHocId = e.MonHocId
                                                Left Join LoaiThietBi as f
                                                on d.LoaiThietBiId = f.LoaiThietBiId
                                                Left Join DonViTinh as g
                                                on d.DonViTinhId = g.DonViTinhId
                                                Left Join KhoPhong as h
                                                on a.KhoPhongId = h.KhoPhongId
                                                Where ((@TenThietBi is null) or (d.TenThietBi Like N'%'+@TenThietBi+'%'))
                                                    and ((@KhoPhongId is null) or (a.KhoPhongId = @KhoPhongId))
                                                    and ((@MonHocId is null) or (d.MonHocId = @MonHocId))
                                                Order by d.TenThietBi
                                                Offset @Offset Rows Fetch Next @Next Rows Only";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                            command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                            command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                            command.Parameters.Add(new SqlParameter("@Next", pageSize));
                            using (var reader = command.ExecuteReader())
                            {
                                reportHongMatResVM.ThietBis.Objects = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                            }
                        }
                        int totalRow = 0;
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select Count(*)
                                                From (Select IsNull(a.KhoPhongId, IsNull(b.KhoPhongId, IsNull(c.KhoPhongId,0))) as 'KhoPhongId', 
                                                            IsNull(a.ThietBiId, IsNull(b.ThietBiId, IsNull(c.ThietBiId,0))) as 'ThietBiId',
		                                                    (IsNull(a.SoLuongHong, 0) - IsNull(c.SoLuongSuaChua, 0)) as 'SoLuongHong',
		                                                    IsNull(b.SoLuongMat, 0) as 'SoLuongMat', IsNull(c.SoLuongSuaChua, 0) as 'SoLuongSuaChua'
                                                    From (Select a.KhoPhongId, a.ThietBiId, Sum(a.SoLuongHong) as 'SoLuongHong' 
                                                            From ChiTietPhieuGhiHongThietBi as a
                                                            Left Join PhieuGhiHongThietBi as b
                                                            on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                                            Where Year(b.NgayLap) = @NamHoc
			                                                    Group by a.KhoPhongId, a.ThietBiId) as a
                                                    Full Join (Select a.KhoPhongId, a.ThietBiId, Sum(a.SoLuongMat) as 'SoLuongMat' 
                                                        From ChiTietPhieuGhiMatThietBi as a
                                                        Left Join PhieuGhiMatThietBi as b
                                                        on a.PhieuGhiMatThietBiId = b.PhieuGhiMatThietBiId
                                                        Where Year(b.NgayLap) = @NamHoc 
                                                        Group by a.KhoPhongId, a.ThietBiId) as b
                                                    on a.KhoPhongId = b.KhoPhongId and a.ThietBiId = b.ThietBiId
                                                    Full Join (Select a.KhoPhongId, a.ThietBiId, Sum(a.SoLuongSuaChua) as 'SoLuongSuaChua' 
                                                        From ChiTietPhieuSuaChua as a
                                                        Left Join PhieuSuaChua as b
                                                        on a.PhieuSuaChuaId = b.PhieuSuaChuaId
                                                        Where Year(b.NgayLap) = @NamHoc
                                                        Group by a.KhoPhongId, a.ThietBiId) as c
                                                    on a.KhoPhongId = c.KhoPhongId and a.ThietBiId = c.ThietBiId) as a
                                                Left Join ThietBi as d
                                                on a.ThietBiId = d.ThietBiId
                                                Left Join MonHoc as e
                                                on d.MonHocId = e.MonHocId
                                                Left Join LoaiThietBi as f
                                                on d.LoaiThietBiId = f.LoaiThietBiId
                                                Left Join DonViTinh as g
                                                on d.DonViTinhId = g.DonViTinhId
                                                Left Join KhoPhong as h
                                                on a.KhoPhongId = h.KhoPhongId
                                                Where ((@TenThietBi is null) or (d.TenThietBi Like N'%'+@TenThietBi+'%'))
                                                    and ((@KhoPhongId is null) or (a.KhoPhongId = @KhoPhongId))
                                                    and ((@MonHocId is null) or (d.MonHocId = @MonHocId))";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                            command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                            command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    totalRow = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                        reportHongMatResVM.ThietBis.CurrentQueryParamsDict = queryParamsDict;
                        reportHongMatResVM.ThietBis.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
                return reportHongMatResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReportKiemKeResVM GetReportKiemKe(int? namHoc, int? khoPhongId, int? thang, int? currentPage, int? pageSize)
        {
            currentPage = currentPage.HasValue ? currentPage : 1;
            pageSize = pageSize.HasValue ? pageSize : 10;
            Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
            queryParamsDict.Add("NamHoc", namHoc.HasValue ? namHoc.Value.ToString() : DateTime.Now.Year.ToString());
            queryParamsDict.Add("KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value.ToString() : "");
            queryParamsDict.Add("Thang", thang.HasValue ? thang.Value.ToString() : "");
            try
            {
                ReportKiemKeResVM reportKiemKeResVM = new ReportKiemKeResVM();
                reportKiemKeResVM.ThietBis = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    command.CommandType = CommandType.Text;
                    try
                    {
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select a.*, b.*, c.TenKhoPhong, d.SoPhieu as 'SoPhieuKiemKe', Format(d.NgayKiemKe, 'dd-MM-yyyy') as 'NgayKiemKe', e.TenLoaiThietBi
                                                From ChiTietPhieuKiemKe as a
                                                Left Join ThietBi as b
                                                on a.ThietBiId = b.ThietBiId
                                                Left Join KhoPhong as c
                                                on a.KhoPhongId = c.KhoPhongId
                                                Inner Join PhieuKiemKe as d
                                                on a.PhieuKiemKeId = d.PhieuKiemKeId
                                                Left Join LoaiThietBi as e
                                                on b.LoaiThietBiId = e.LoaiThietBiId
                                                Where ((@KhoPhongId is null) or (a.KhoPhongId = @KhoPhongId))
                                                    and ((@Thang is null) or (Month(d.NgayKiemKe) = @Thang))
                                                    and (Year(d.NgayKiemKe) = @NamHoc)
                                                Order by d.NgayKiemKe Desc, a.PhieuKiemKeId, a.ThietBiId, a.KhoPhongId
                                                Offset @Offset Rows Fetch Next @Next Rows Only";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                            command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@Thang", thang.HasValue ? thang.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                            command.Parameters.Add(new SqlParameter("@Next", pageSize));
                            using (var reader = command.ExecuteReader())
                            {
                                reportKiemKeResVM.ThietBis.Objects = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                            }
                        }
                        int totalRow = 0;
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select Count(*)
                                                From ChiTietPhieuKiemKe as a
                                                Left Join ThietBi as b
                                                on a.ThietBiId = b.ThietBiId
                                                Left Join KhoPhong as c
                                                on a.KhoPhongId = c.KhoPhongId
                                                Inner Join PhieuKiemKe as d
                                                on a.PhieuKiemKeId = d.PhieuKiemKeId
                                                Where ((@KhoPhongId is null) or (a.KhoPhongId = @KhoPhongId))
                                                    and ((@Thang is null) or (Month(d.NgayKiemKe) = @Thang))
                                                    and (Year(d.NgayKiemKe) = @NamHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                            command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@Thang", thang.HasValue ? thang.Value : (object)DBNull.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    totalRow = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                        reportKiemKeResVM.ThietBis.CurrentQueryParamsDict = queryParamsDict;
                        reportKiemKeResVM.ThietBis.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
                return reportKiemKeResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReportMuonTraTheoGiaoVienResVM GetReportMuonTraTheoGiaoVien(int? namHoc, int? trangThaiPhieuMuonId,int? currentPage, int? pageSize)
        {
            try
            {
                currentPage = currentPage.HasValue ? currentPage : 1;
                pageSize = pageSize.HasValue ? pageSize : 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("NamHoc", namHoc.HasValue ? namHoc.Value.ToString() : DateTime.Now.Year.ToString());
                queryParamsDict.Add("TrangThaiPhieuMuonId", trangThaiPhieuMuonId.HasValue ? trangThaiPhieuMuonId.Value.ToString() : "");

                ReportMuonTraTheoGiaoVienResVM reportMuonTraTheoGiaoVienResVM = new ReportMuonTraTheoGiaoVienResVM();

                //reportMuonTraTheoGiaoVienResVM.ThietBis = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    command.CommandType = CommandType.Text;
                    try
                    {
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select (Select IsNull(Sum(a.SoLuong), 0) From LienKetThietBiPhieuMuon as a
                                                        Left Join PhieuMuon as b 
                                                        on a.MaPhieuMuon = b.MaPhieuMuon
                                                        Where (b.TrangThaiPhieuMuonId = 1) and (Year(b.NgayMuon) = @NamHoc)) 
                                                        + 
                                                        (Select IsNull(Sum(a.SoLuong), 0) From LienKetThietBiPhieuMuon as a
                                                        Left Join PhieuMuon as b 
                                                        on a.MaPhieuMuon = b.MaPhieuMuon
                                                        Where (b.TrangThaiPhieuMuonId = 2) and (Year(b.NgayMuon) = @NamHoc))";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    reportMuonTraTheoGiaoVienResVM.TongSoThietBiDaMuon = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(a.SoLuong), 0) From LienKetThietBiPhieuMuon as a
                                                        Left Join PhieuMuon as b 
                                                        on a.MaPhieuMuon = b.MaPhieuMuon
                                                        Where (b.TrangThaiPhieuMuonId = 1) and (Year(b.NgayMuon) = @NamHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    reportMuonTraTheoGiaoVienResVM.TongSoThietBiDangMuon = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(a.SoLuong), 0) From LienKetThietBiPhieuMuon as a
                                                        Left Join PhieuMuon as b 
                                                        on a.MaPhieuMuon = b.MaPhieuMuon
                                                        Where (b.TrangThaiPhieuMuonId = 2) and (Year(b.NgayMuon) = @NamHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    reportMuonTraTheoGiaoVienResVM.TongSoThietBiDaTra = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        //{
                        //    command.Parameters.Clear();
                        //    string sqlString = @"Select * From PhieuMuon as a
                        //                        Left Join GiaoVien as b
                        //                        on a.GiaoVienId = b.GiaoVienId
                        //                        Where (a.LoaiPhieuMuonId = 1)
                        //                        Order by a.NgayMuon Desc
                        //                        Offset @Offset Rows Fetch Next @Next Rows Only";
                        //    command.CommandText = sqlString;
                        //    command.CommandType = CommandType.Text;
                        //    command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                        //    command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        //    command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        //    using (var reader = command.ExecuteReader())
                        //    {
                        //        reportMuonTraTheoGiaoVienResVM.ThietBis.Objects = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        //    }
                        //}
                        //int totalRow = 0;
                        //{
                        //    command.Parameters.Clear();
                        //    string sqlString = @"Select Count(*)
                        //                        From ChiTietPhieuKiemKe as a
                        //                        Left Join ThietBi as b
                        //                        on a.ThietBiId = b.ThietBiId
                        //                        Left Join KhoPhong as c
                        //                        on a.KhoPhongId = c.KhoPhongId
                        //                        Inner Join PhieuKiemKe as d
                        //                        on a.PhieuKiemKeId = d.PhieuKiemKeId
                        //                        Where ((@KhoPhongId is null) or (a.KhoPhongId = @KhoPhongId))
                        //                            and ((@Thang is null) or (Month(d.NgayKiemKe) = @Thang))
                        //                            and (Year(d.NgayKiemKe) = @NamHoc)";
                        //    command.CommandText = sqlString;
                        //    command.CommandType = CommandType.Text;
                        //    command.Parameters.Add(new SqlParameter("@NamHoc", namHoc.HasValue ? namHoc.Value : DateTime.Now.Year));
                        //    command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                        //    command.Parameters.Add(new SqlParameter("@Thang", thang.HasValue ? thang.Value : (object)DBNull.Value));
                        //    using (var reader = command.ExecuteReader())
                        //    {
                        //        if (reader.Read())
                        //        {
                        //            totalRow = Convert.ToInt32(reader[0]);
                        //        }
                        //    }
                        //}

                        //int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                        //reportKiemKeResVM.ThietBis.CurrentQueryParamsDict = queryParamsDict;
                        //reportKiemKeResVM.ThietBis.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return reportMuonTraTheoGiaoVienResVM;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
