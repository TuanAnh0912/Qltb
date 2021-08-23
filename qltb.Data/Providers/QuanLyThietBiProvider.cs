using qltb.Data.Helpers;
using qltb.Data.Helpers.ExceptionHelpers;
using qltb.Data.ReqVMs;
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
    public class QuanLyThietBiProvider : ApplicationDbcontext
    {
        #region kho thiet bi
        public List<ChiTietThietBiResVM> GetThietBiTrongKhos(string tenThietBi, int? monHocId)
        {
            try
            {
                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select b.*, a.SoLuongConLai, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong, a.KhoPhongId
                                            From KhoThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Where ((@TenThietBi is null) or (b.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (b.MonHocId = @MonHocId)) and (a.SoLuongConLai > 0)
                                            Order by b.TenThietBi";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChiTietThietBiResVM GetThietBiTrongKho(int? thietBiId, int? khoPhongId)
        {
            try
            {
                ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select b.*, a.SoLuongConLai, a.KhoPhongId,c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong 
                                            From KhoThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Where (a.ThietBiId = @ThietbiId) and (a.KhoPhongId = @KhoPhongId)";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@ThietBiId", thietBiId.HasValue ? thietBiId.Value : -1));
                        command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : -1));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVM = MapDataHelper<ChiTietThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChiTietThietBiResVM GetThietBiTrongKho(int? thietBiId, int? khoPhongId, string phieuGhiMatThietBiId, string phieuGhiHongThietBiId)
        {
            try
            {
                ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = "";
                        if (string.IsNullOrEmpty(phieuGhiMatThietBiId) && string.IsNullOrEmpty(phieuGhiHongThietBiId))
                        {
                            sqlString = @"Select b.*, a.SoLuongConLai, a.KhoPhongId,c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong 
                                            From KhoThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Where (a.ThietBiId = @ThietbiId) and (a.KhoPhongId = @KhoPhongId)";
                        }
                        else if (!string.IsNullOrEmpty(phieuGhiMatThietBiId))
                        {
                            sqlString = @"Select b.*, a.*, g.SoPhieu as 'SoPhieuGhiMatThietBi', 
                                                c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong 
                                            From ChiTietPhieuGhiMatThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Left Join PhieuGhiMatThietBi as g
                                            on a.PhieuGhiMatThietBiId = g.PhieuGhiMatThietBiId
                                            Where (a.ThietBiId = @ThietbiId) and (a.KhoPhongId = @KhoPhongId) and (a.PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId)";
                        }
                        else
                        {
                            sqlString = @"Select b.*, a.*, g.SoPhieu as 'SoPhieuGhiHongThietBi', 
                                                c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong 
                                            From ChiTietPhieuGhiHongThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Left Join PhieuGhiHongThietBi as g
                                            on a.PhieuGhiHongThietBiId = g.PhieuGhiHongThietBiId
                                            Where (a.ThietBiId = @ThietbiId) and (a.KhoPhongId = @KhoPhongId) and (a.PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId)";
                        }
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@ThietBiId", thietBiId.HasValue ? thietBiId.Value : -1));
                        command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : -1));
                        command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", phieuGhiHongThietBiId));
                        command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", phieuGhiMatThietBiId));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVM = MapDataHelper<ChiTietThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChiTietThietBiResVM> GetThietBiTrongKhos(string tenThietBi, int? monHocId, int? loaiId)
        {
            try
            {
                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = "";
                        if (loaiId.HasValue && loaiId.Value == 1)
                        {
                            sqlString = @"Select a.*, b.TenKhoPhong, f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
											Left Join DonViTinh as h
											on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))
                                            Order by f.TenThietBi";
                        }
                        else if (loaiId.HasValue && loaiId.Value == 2)
                        {
                            sqlString = @"Select a.*, b.TenKhoPhong, f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*, k.SoLuongMat, k.LyDo, l.SoPhieu as 'SoPhieuGhiMatThietBi', l.PhieuGhiMatThietBiId
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
                                            Left Join DonViTinh as h
                                            on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Inner Join ChiTietPhieuGhiMatThietBi as k
                                            on a.ThietBiId = k.ThietBiId and a.KhoPhongId = k.KhoPhongId
                                            Inner Join PhieuGhiMatThietbi as l
                                            on k.PhieuGhiMatThietBiId = l.PhieuGhiMatThietBiId
                                            Left Join ChiTietPhieuGhiGiamThietBi as m
                                            on k.PhieuGhiMatThietBiId = m.PhieuGhiMatThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))
                                                and (m.PhieuGhiMatThietBiId is null)
                                            Order by l.NgayLap Desc";
                        }
                        else if (loaiId.HasValue && loaiId.Value == 3)
                        {
                            sqlString = @"Select a.*, b.TenKhoPhong, f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*, k.SoLuongHong, k.LyDo, l.SoPhieu as 'SoPhieuGhiHongThietBi', l.PhieuGhiHongThietBiId
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
                                            Left Join DonViTinh as h
                                            on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Inner Join ChiTietPhieuGhiHongThietBi as k
                                            on a.ThietBiId = k.ThietBiId and a.KhoPhongId = k.KhoPhongId
                                            Inner Join PhieuGhiHongThietbi as l
                                            on k.PhieuGhiHongThietBiId = l.PhieuGhiHongThietBiId
                                            Left Join ChiTietPhieuGhiGiamThietBi as m
                                            on k.PhieuGhiHongThietBiId = m.PhieuGhiHongThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))
                                                and (m.PhieuGhiHongThietBiId is null)
                                            Order by l.NgayLap Desc";
                        }
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region chi tiet thiet bi
        public ListWithPaginationResVM GetChiTietThietBis(string tenThietBi, int? khoPhongId, int? monHocId, int? currentPage, int? pageSize)
        {
            try
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenThietBi", tenThietBi);
                queryParamsDict.Add("KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value.ToString() : null);
                queryParamsDict.Add("MonHocId", monHocId.HasValue ? monHocId.Value.ToString() : null);

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.TenKhoPhong,
                                            f.TenThietBi, f.MaThietBi, g.TenMonHoc, h.*
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
                                            Left Join LoaiThietBI as h
                                            on f.LoaiThietBiId = h.LoaiThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@KhoPhongId is null) or (b.KhoPhongId = @KhoPhongId)) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))
                                            Order by f.TenThietBi Desc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        using (var reader = command.ExecuteReader())

                        {
                            listWithPaginationResVM.Objects = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*)
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
                                            Left Join LoaiThietBI as h
                                            on f.LoaiThietBiId = h.LoaiThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@KhoPhongId is null) or (b.KhoPhongId = @KhoPhongId)) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt64(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var chiTietThietBiResVM in (List<ChiTietThietBiResVM>)listWithPaginationResVM.Objects)
                {
                    chiTietThietBiResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                listWithPaginationResVM.CurrentQueryParamsDict = queryParamsDict;
                listWithPaginationResVM.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);

                return listWithPaginationResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // get ds thiet bi de khi phieu tang
        public List<ChiTietThietBiResVM> GetChiTietThietBis(string tenThietBi, int? monHocId)
        {
            try
            {
                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.TenKhoPhong, 
                                                    f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
                                            Left Join DonViTinh as h
                                            on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))
                                            Order by f.TenThietBi";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChiTietThietBiResVM> GetChiTietThietBis(string tenThietBi, int? monHocId, int? loaiId)
        {
            // loaiId = 1: con dung duoc ; loaiId = 2: mat, loaiId= 3: hong
            try
            {
                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = "";
                        if (loaiId.HasValue && loaiId.Value == 1)
                        {
                            sqlString = @"Select a.*, b.TenKhoPhong, c.TenNguonCap, d.TenNguonKinhPhiCapTren, e.TenMucDichSuDung, 
                                                    f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join NguonCap as c
                                            on a.NguonCapId = c.NguonCapId
                                            Left Join NguonKinhPhiCapTren as d
                                            on a.NguonKinhPhiCapTrenId = d.NguonKinhPhiCapTrenId
                                            Left Join MucDichSuDung as e
                                            on a.MucDichSuDungId = e.MucDichSuDungId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
											Left Join DonViTinh as h
											on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))
                                            Order by b.TenThietBi";
                        }
                        else if (loaiId.HasValue && loaiId.Value == 2)
                        {
                            sqlString = @"Select a.*, b.TenKhoPhong, f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*, k.SoLuongMat, l.SoPhieu as 'SoPhieuGhiMatThietBi', l.PhieuGhiMatThietBiId
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
                                            Left Join DonViTinh as h
                                            on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Inner Join ChiTietPhieuGhiMatThietBi as k
                                            on a.ThietBiId = k.ThietBiId and a.KhoPhongId = k.KhoPhongId
                                            Inner Join PhieuGhiMatThietbi as l
                                            on k.PhieuGhiMatThietBiId = l.PhieuGhiMatThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))
                                            Order by b.TenThietBi";
                        }
                        else if (loaiId.HasValue && loaiId.Value == 3)
                        {
                            sqlString = @"Select a.*, b.TenKhoPhong, f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*, k.SoLuongMat, l.SoPhieu as 'SoPhieuGhiHongThietBi', l.PhieuGhiHongThietBiId
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
                                            Left Join DonViTinh as h
                                            on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Inner Join ChiTietPhieuGhiMatThietBi as k
                                            on a.ThietBiId = k.ThietBiId and a.KhoPhongId = k.KhoPhongId
                                            Inner Join PhieuGhiHongThietbi as l
                                            on k.PhieuGhiHongThietBiId = l.PhieuGhiHongThietBiId
                                            Where ((@TenThietBi is null) or (f.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (g.MonHocId = @MonHocId))
                                            Order by b.TenThietBi";
                        }

                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ChiTietThietBiResVM GetChiTietThietBi(string khoThietBiId)
        {
            try
            {
                ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.TenKhoPhong,
                                                    f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*
                                            From KhoThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
											Left Join DonViTinh as h
											on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Where a.KhoThietBiId = @KhoThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@KhoThietBiId", string.IsNullOrEmpty(khoThietBiId) ? "" : khoThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())

                        {
                            chiTietThietBiResVM = MapDataHelper<ChiTietThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
                return chiTietThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChiTietThietBiResVM GetChiTietThietBi(string chiTietThietBiId, string phieuGhiHongMatThietBiId, int? loaiId)
        {
            try
            {
                ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*,b.TenKhoPhong, c.TenNguonCap, d.TenNguonKinhPhiCapTren, e.TenMucDichSuDung, 
                                                    f.TenThietBi, f.MaThietBi,g.TenMonHoc, h.*, i.*, k.PhieuGhiHongMatThietBiId, k.LyDo, k.SoLuongHongMat, k.SoLuongHong, k.SoLuongMat,
                                                    l.SoPhieu as 'SoPhieuGhiHongMatThietBi'
                                            From ChiTietThietBi as a
                                            Left Join KhoPhong as b
                                            on a.KhoPhongId = b.KhoPhongId
                                            Left Join NguonCap as c
                                            on a.NguonCapId = c.NguonCapId
                                            Left Join NguonKinhPhiCapTren as d
                                            on a.NguonKinhPhiCapTrenId = d.NguonKinhPhiCapTrenId
                                            Left Join MucDichSuDung as e
                                            on a.MucDichSuDungId = e.MucDichSuDungId
                                            Left Join ThietBi as f
                                            on a.ThietBiId  = f.ThietBiId
                                            Left Join MonHoc as g
                                            on f.MonHocId = g.MonHocId
                                            Left Join DonViTinh as h
                                            on f.DonViTinhId = h.DonViTinhId
                                            Left Join LoaiThietBi as i
                                            on f.LoaiThietBiId = i.LoaiThietBiId
                                            Left Join PhieuGhiHongMatThietBi_ChiTietThietBi as k
                                            on a.ChiTietThietBiId = k.ChiTietThietBiId
                                            Left Join PhieuGhiHongMatThietBi as l
                                            on k.PhieuGhiHongMatThietBiId = l.PhieuGhiHongMatThietBiId
                                            Where (a.ChiTietThietBiId = @ChiTietThietBiId) and ((@PhieuGhiHongMatThietBiId is null) or (k.PhieuGhiHongMatThietBiId = @PhieuGhiHongMatThietBiId))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@ChiTietThietBiId", string.IsNullOrEmpty(chiTietThietBiId) ? "" : chiTietThietBiId.Trim()));
                        command.Parameters.Add(new SqlParameter("@PhieuGhiHongMatThietBiId", string.IsNullOrEmpty(phieuGhiHongMatThietBiId) ? (object)DBNull.Value : phieuGhiHongMatThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())

                        {
                            chiTietThietBiResVM = MapDataHelper<ChiTietThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
                //if (loaiId.HasValue && loaiId.Value == 2)
                //{
                //    chiTietThietBiResVM.PhieuGhiHongMatThietBiId = null;
                //    chiTietThietBiResVM.LyDo = null;
                //    chiTietThietBiResVM.SoPhieuGhiHongMatThietBi = null;
                //    chiTietThietBiResVM.SoLuongHongMat = null;
                //}

                chiTietThietBiResVM.HanDungString = chiTietThietBiResVM.HanDung.HasValue ? chiTietThietBiResVM.HanDung.Value.ToString("yyyy-MM-dd") : null;
                chiTietThietBiResVM.NgaySanXuatString = chiTietThietBiResVM.NgaySanXuat.HasValue ? chiTietThietBiResVM.NgaySanXuat.Value.ToString("yyyy-MM-dd") : null;
                chiTietThietBiResVM.NgaySuDungString = chiTietThietBiResVM.NgaySuDung.HasValue ? chiTietThietBiResVM.NgaySuDung.Value.ToString("yyyy-MM-dd") : null;
                return chiTietThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddChiTietThietBi(AddChiTietThietBiReqVM reqVM)
        {
            try
            {
                if (!reqVM.KhoPhongId.HasValue)
                {
                    throw new Exception("Chọn kho phòng");
                }
                int totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    try
                    {
                        string sqlString = @"Select Count(*) From KhoThietBi Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@ThietBiId", reqVM.ThietBiId));
                        command.Parameters.Add(new SqlParameter("@KhoPhongId", reqVM.KhoPhongId.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt32(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                if (totalRow == 1)
                {
                    throw new Exception("Thiết bị đã có trong kho");
                }
                else
                {
                    KhoThietBi khoThietBi = new KhoThietBi();
                    khoThietBi.KhoThietBiId = Guid.NewGuid().ToString();
                    khoThietBi.KhoPhongId = reqVM.KhoPhongId;
                    khoThietBi.ThietBiId = reqVM.ThietBiId;
                    khoThietBi.SoLuong = reqVM.SoLuong.HasValue ? reqVM.SoLuong.Value : 0;
                    khoThietBi.SoLuongConLai = reqVM.SoLuong.HasValue ? reqVM.SoLuong.Value : 0;
                    khoThietBi.SoHieu = reqVM.SoHieu;
                    DbContext.KhoThietBis.Add(khoThietBi);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateChiTietThietBi(UpdateChiTietThietBiReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    command.CommandType = CommandType.Text;

                    try
                    {
                        int totalRow = 0;
                        {

                            string sqlString = @"Select Count(*) From KhoThietBi Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId) and (KhoThietBiId != @KhoThietBiId)";
                            command.CommandText = sqlString;
                            command.Parameters.Add(new SqlParameter("@KhoThietBiId", string.IsNullOrEmpty(reqVM.KhoThietBiId) ? "" : reqVM.KhoThietBiId.Trim()));
                            command.Parameters.Add(new SqlParameter("@ThietBiId", reqVM.ThietBiId.HasValue ? reqVM.ThietBiId.Value : 0));
                            command.Parameters.Add(new SqlParameter("@KhoPhongId", reqVM.KhoPhongId.HasValue ? reqVM.KhoPhongId.Value : 0));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    totalRow = Convert.ToInt32(reader[0]);
                                }
                            }
                        }
                        if (totalRow == 0)
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Update KhoThietBi 
                                            Set SoLuong = @SoLuong, SoLuongConLai = @SoLuong, SoHieu = @SoHieu, ThietBiId = @ThietBiId, KhoPhongId = @KhoPhongId
                                            Where KhoThietBiId = @KhoThietBiId";
                            command.CommandText = sqlString;
                            command.Parameters.Add(new SqlParameter("@KhoThietBiId", string.IsNullOrEmpty(reqVM.KhoThietBiId) ? "" : reqVM.KhoThietBiId.Trim()));
                            command.Parameters.Add(new SqlParameter("@SoHieu", string.IsNullOrEmpty(reqVM.SoHieu) ? "" : reqVM.SoHieu.Trim()));
                            command.Parameters.Add(new SqlParameter("@SoLuong", reqVM.SoLuong.HasValue ? reqVM.SoLuong.Value : 0));
                            command.Parameters.Add(new SqlParameter("@ThietBiId", reqVM.ThietBiId.HasValue ? reqVM.ThietBiId.Value : 0));
                            command.Parameters.Add(new SqlParameter("@KhoPhongId", reqVM.KhoPhongId.HasValue ? reqVM.KhoPhongId.Value : 0));
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            throw new Exception("Thiết bị đã có sẵn trong kho, không thể cập nhật");
                        }

                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteChiTietThietBi(DeleteChiTietThietBiReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete KhoThietBi Where KhoThietBiId = @KhoThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@KhoThietBiId", string.IsNullOrEmpty(reqVM.KhoThietBiId) ? "" : reqVM.KhoThietBiId.Trim()));
                        command.ExecuteReader();
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region phieu ghi tang thiet bi
        public void AddPhieuGhiTangThietBi(AddPhieuGhiTangThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();
                        try
                        {


                            PhieuGhiTangThietBi phieuGhiTangThietBi = new PhieuGhiTangThietBi();
                            phieuGhiTangThietBi.PhieuGhiTangThietBiId = Guid.NewGuid().ToString();
                            phieuGhiTangThietBi.NgayNhap = reqVM.NgayNhap;
                            phieuGhiTangThietBi.NgayTao = DateTime.Now;
                            phieuGhiTangThietBi.SoPhieu = reqVM.SoPhieu;
                            phieuGhiTangThietBi.NoiDung = reqVM.NoiDung;
                            phieuGhiTangThietBi.ChungTuLienQuan = reqVM.ChungTuLienQuan;
                            phieuGhiTangThietBi.NguoiCapNhat = reqVM.NguoiCapNhat;
                            DbContext.PhieuGhiTangThietBis.Add(phieuGhiTangThietBi);
                            DbContext.SaveChanges();

                            if (reqVM.ChiTietThietBis != null && reqVM.ChiTietThietBis.Count() > 0)
                            {
                                List<LogThietBi> logThietBis = new List<LogThietBi>();
                                List<ChiTietThietBi> chiTietThietBis = new List<ChiTietThietBi>();
                                List<PhieuGhiTangThietBi_ChiTietThietBi> phieuGhiTangThietBi_ChiTietThietBis = new List<PhieuGhiTangThietBi_ChiTietThietBi>();
                                foreach (var chiTietThietBiReqVM in reqVM.ChiTietThietBis)
                                {
                                    string sqlString = @"Update KhoThietBi  
                                                        Set SoLuong = SoLuong + @SoLuong, SoLuongConLai = SoLuongConLai + @SoLuong
                                                        Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                    command.CommandText = sqlString;
                                    command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiReqVM.ThietBiId));
                                    command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiReqVM.KhoPhongId.Value));
                                    command.Parameters.Add(new SqlParameter("@SoLuong", chiTietThietBiReqVM.SoLuong.HasValue ? chiTietThietBiReqVM.SoLuong.Value : 0));
                                    command.ExecuteNonQuery();

                                    ChiTietThietBi chiTietThietBi = MapDataHelper<ChiTietThietBi>.CopyDataObject(chiTietThietBiReqVM);
                                    chiTietThietBi.ChiTietThietBiId = Guid.NewGuid().ToString();
                                    chiTietThietBi.NgayCapNhat = DateTime.Now;
                                    chiTietThietBis.Add(chiTietThietBi);

                                    PhieuGhiTangThietBi_ChiTietThietBi phieuGhiTangThietBi_ChiTietThietBi = new PhieuGhiTangThietBi_ChiTietThietBi();
                                    phieuGhiTangThietBi_ChiTietThietBi.PhieuGhiTangThietBi_ChiTietThietBi_Id = Guid.NewGuid().ToString();
                                    phieuGhiTangThietBi_ChiTietThietBi.PhieuGhiTangThietBiId = phieuGhiTangThietBi.PhieuGhiTangThietBiId;
                                    phieuGhiTangThietBi_ChiTietThietBi.ChiTietThietBiId = chiTietThietBi.ChiTietThietBiId;
                                    phieuGhiTangThietBi_ChiTietThietBis.Add(phieuGhiTangThietBi_ChiTietThietBi);

                                    //LogThietBi logThietBi = new LogThietBi();
                                    //logThietBi.ThoiGian = DateTime.Now;
                                    //logThietBi.ThietBiId = chiTietThietBiReqVM.ThietBiId;
                                    //logThietBi.KhoPhongId = chiTietThietBiReqVM.KhoPhongId;
                                    //logThietBi.SoLuong = chiTietThietBiReqVM.SoLuong;
                                    //logThietBi.MaLoaiThayDoi = "ADD_TANG";
                                    //logThietBi.SoPhieuId = phieuGhiTangThietBi.PhieuGhiTangThietBiId;
                                    //logThietBi.NguoiDungId = Guid.Parse(reqVM.NguoiCapNhat);

                                    //logThietBis.Add(logThietBi);

                                }
                                DbContext.ChiTietThietBis.AddRange(chiTietThietBis);
                                DbContext.SaveChanges();

                                DbContext.LogThietBis.AddRange(logThietBis);
                                DbContext.SaveChanges();

                                DbContext.PhieuGhiTangThietBi_ChiTietThietBi.AddRange(phieuGhiTangThietBi_ChiTietThietBis);
                                DbContext.SaveChanges();

                            }

                            // tai lieu dinh kem
                            if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                            {
                                List<FileTrongPhieuGhiTang> fileTrongPhieuGhiTangs = new List<FileTrongPhieuGhiTang>();

                                foreach (var item in reqVM.TaiLieuDinhKems)
                                {
                                    FileTrongPhieuGhiTang fileTrongPhieuGhiTang = new FileTrongPhieuGhiTang();
                                    fileTrongPhieuGhiTang.FileTrongPhieuGhiTangId = item.FileId;
                                    fileTrongPhieuGhiTang.Ext = item.Ext;
                                    fileTrongPhieuGhiTang.Icon = item.Icon;
                                    fileTrongPhieuGhiTang.TenFile = item.TenFile;
                                    fileTrongPhieuGhiTang.Url = item.Url;
                                    fileTrongPhieuGhiTang.PhieuGhiTangThietBiId = phieuGhiTangThietBi.PhieuGhiTangThietBiId;
                                    fileTrongPhieuGhiTangs.Add(fileTrongPhieuGhiTang);
                                }
                                DbContext.FileTrongPhieuGhiTangs.AddRange(fileTrongPhieuGhiTangs);
                                DbContext.SaveChanges();
                            }
                            dbContextTransaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw new WarningException(ex.Message);
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PhieuGhiTangThietBiResVM GetPhieuGhiTangThietBi(string phieuGhiTangThietBiId)
        {
            try
            {
                PhieuGhiTangThietBiResVM phieuGhiTangThietBiResVM = new PhieuGhiTangThietBiResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From PhieuGhiTangThietBi Where PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", string.IsNullOrEmpty(phieuGhiTangThietBiId) ? "" : phieuGhiTangThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiTangThietBiResVM = MapDataHelper<PhieuGhiTangThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
                if (phieuGhiTangThietBiResVM == null)
                {
                    throw new WarningException("Không tìm thấy phiếu ghi tăng thiết bị");
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select b.*, c.TenKhoPhong, d.TenNguonCap, e.TenNguonKinhPhiCapTren, f.TenMucDichSuDung, 
                                                    g.TenThietBi, g.MaThietBi, h.TenMonHoc, i.*, k.*
                                            From PhieuGhiTangThietBi_ChiTietThietBi as a
                                            Inner Join ChiTietThietBi as b
                                            on a.ChiTietThietBiId = b.ChiTietThietBiId
                                            Left Join KhoPhong as c
                                            on b.KhoPhongId = c.KhoPhongId
                                            Left Join NguonCap as d
                                            on b.NguonCapId = d.NguonCapId
                                            Left Join NguonKinhPhiCapTren as e
                                            on b.NguonKinhPhiCapTrenId = e.NguonKinhPhiCapTrenId
                                            Left Join MucDichSuDung as f
                                            on b.MucDichSuDungId = f.MucDichSuDungId
                                            Left Join ThietBi as g
                                            on b.ThietBiId  = g.ThietBiId
                                            Left Join MonHoc as h
                                            on g.MonHocId = h.MonHocId
											Left Join DonViTinh as i
											on g.DonViTinhId = i.DonViTinhId
                                            Left Join LoaiThietBi as k
                                            on g.LoaiThietBiId = k.LoaiThietBiId
                                            Where a.PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", string.IsNullOrEmpty(phieuGhiTangThietBiId) ? "" : phieuGhiTangThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiTangThietBiResVM.ChiTietThietBis = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }


                foreach (var chiTietThietBiResVM in phieuGhiTangThietBiResVM.ChiTietThietBis)
                {
                    chiTietThietBiResVM.HanDungString = chiTietThietBiResVM.HanDung.HasValue ? chiTietThietBiResVM.HanDung.Value.ToString("yyyy-MM-dd") : null;
                    chiTietThietBiResVM.NgaySanXuatString = chiTietThietBiResVM.NgaySanXuat.HasValue ? chiTietThietBiResVM.NgaySanXuat.Value.ToString("yyyy-MM-dd") : null;
                    chiTietThietBiResVM.NgaySuDungString = chiTietThietBiResVM.NgaySuDung.HasValue ? chiTietThietBiResVM.NgaySuDung.Value.ToString("yyyy-MM-dd") : null;
                    if (chiTietThietBiResVM.DonGia.HasValue && chiTietThietBiResVM.SoLuong.HasValue)
                    {
                        chiTietThietBiResVM.ThanhTien = chiTietThietBiResVM.DonGia * chiTietThietBiResVM.SoLuong;
                    }
                    else
                    {
                        chiTietThietBiResVM.ThanhTien = 0;
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select FileTrongPhieuGhiTangId as 'FileId', TenFile, Ext, Url, Icon From FileTrongPhieuGhiTang
                                            Where PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", string.IsNullOrEmpty(phieuGhiTangThietBiId) ? "" : phieuGhiTangThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiTangThietBiResVM.FileTrongPhieuGhiTangs = MapDataHelper<FileResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }


                return phieuGhiTangThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ListWithPaginationResVM GetPhieuGhiTangThietBis(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("SoPhieu", soPhieu);
                queryParamsDict.Add("TuNgay", tuNgay.HasValue ? tuNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("DenNgay", denNgay.HasValue ? denNgay.Value.ToString("yyyy-MM-dd") : null);
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.HoTen From PhieuGhiTangThietBi as a
                                            Left Join NguoiDung as b
                                            on a.NguoiCapNhat = b.NguoiDungId
                                            Where ((@SoPhieu is null) or (a.SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (a.NgayNhap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (a.NgayNhap <= @DenNgay))
                                            Order by a.NgayTao Desc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<PhieuGhiTangThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From PhieuGhiTangThietBi
                                            Where ((@SoPhieu is null) or (SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (NgayNhap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (NgayNhap <= @DenNgay))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt64(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var phieuGhiTangThietBiResVM in (List<PhieuGhiTangThietBiResVM>)listWithPaginationResVM.Objects)
                {
                    phieuGhiTangThietBiResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                listWithPaginationResVM.CurrentQueryParamsDict = queryParamsDict;
                listWithPaginationResVM.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);

                return listWithPaginationResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePhieuGhiTangThietBi(UpdatePhieuGhiTangThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            try
                            {
                                command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                command.CommandType = CommandType.Text;
                                {
                                    string sqlString = @"Update PhieuGhiTangThietBi
                                                    Set SoPhieu = @SoPhieu, NgayNhap = @NgayNhap, ChungTuLienQuan = @ChungTuLienQuan, @NoiDung = @NoiDung
                                                    Where PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", reqVM.PhieuGhiTangThietBiId));
                                    command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(reqVM.SoPhieu) ? "" : reqVM.SoPhieu.Trim()));
                                    command.Parameters.Add(new SqlParameter("@NgayNhap", reqVM.NgayNhap.HasValue ? reqVM.NgayNhap.Value : (object)DBNull.Value));
                                    command.Parameters.Add(new SqlParameter("@ChungTuLienQuan", string.IsNullOrEmpty(reqVM.ChungTuLienQuan) ? "" : reqVM.ChungTuLienQuan.Trim()));
                                    command.Parameters.Add(new SqlParameter("@NoiDung", string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim()));
                                    command.ExecuteNonQuery();
                                }

                                //update so luong kho
                                {
                                    List<ChiTietThietBiResVM> chiTietThietBis = new List<ChiTietThietBiResVM>();
                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Select b.ChiTietThietBiId, b.ThietBiId, b.KhoPhongId, b.SoLuong
                                                        From PhieuGhiTangThietBi_ChiTietThietBi as a
                                                        Inner Join ChiTietThietBi as b
                                                        on a.ChiTietThietBiId = b.ChiTietThietBiId
                                                        Where a.PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId";
                                        command.CommandText = sqlString;
                                        command.CommandType = CommandType.Text;
                                        command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", reqVM.PhieuGhiTangThietBiId));
                                        using (var reader = command.ExecuteReader())
                                        {
                                            chiTietThietBis = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                                        }

                                        foreach (var item in reqVM.ChiTietThietBis)
                                        {
                                            ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
                                            chiTietThietBiResVM.ChiTietThietBiId = item.ChiTietThietBiId;
                                            chiTietThietBiResVM.ThietBiId = item.ThietBiId;
                                            chiTietThietBiResVM.KhoPhongId = item.KhoPhongId;
                                            chiTietThietBis.Add(chiTietThietBiResVM);
                                        }
                                    }

                                    List<LogThietBi> logDeleteThietBis = new List<LogThietBi>();
                                    foreach (var item in chiTietThietBis.GroupBy(x => new { x.ThietBiId, x.KhoPhongId, x.SoLuong }))
                                    {
                                        //LogThietBi logThietBi = new LogThietBi();
                                        //logThietBi.ThoiGian = DateTime.Now;
                                        //logThietBi.ThietBiId = item.Key.ThietBiId;
                                        //logThietBi.KhoPhongId = item.Key.KhoPhongId;
                                        //logThietBi.SoLuong = item.Key.SoLuong;
                                        //logThietBi.MaLoaiThayDoi = "DELETE_TANG";
                                        //logThietBi.SoPhieuId = reqVM.PhieuGhiTangThietBiId;
                                        //logThietBi.NguoiDungId = Guid.Parse(reqVM.NguoiCapNhat);
                                        //logDeleteThietBis.Add(logThietBi)

                                        command.Parameters.Clear();
                                        int soLuongTang = reqVM.ChiTietThietBis.Where(x => x.ThietBiId == item.Key.ThietBiId && x.KhoPhongId == item.Key.KhoPhongId).Select(x => x.SoLuong.Value).Sum();
                                        string sqlString = @"Update KhoThietBi
                                                            Set SoLuong = SoLuong + @SoLuongTang - (Select ISNULL(Sum(b.SoLuong), 0)
                                                                                                    From PhieuGhiTangThietBi_ChiTietThietBi as a
                                                                                                    Inner Join ChiTietThietBi as b
                                                                                                    on a.ChiTietThietBiId = b.ChiTietThietBiId
                                                                                                    Where (PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId) 
                                                                                                        and (b.ThietBiId = @ThietBiId) and (b.KhoPhongId = @KhoPhongId)),
                                                                SoLuongConLai = SoLuongConLai + @SoLuongTang - (Select ISNULL(Sum(b.SoLuong), 0)
                                                                                                    From PhieuGhiTangThietBi_ChiTietThietBi as a
                                                                                                    Inner Join ChiTietThietBi as b
                                                                                                    on a.ChiTietThietBiId = b.ChiTietThietBiId
                                                                                                    Where (PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId) 
                                                                                                        and (b.ThietBiId = @ThietBiId) and (b.KhoPhongId = @KhoPhongId))
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                        command.CommandText = sqlString;
                                        command.CommandType = CommandType.Text;
                                        command.Parameters.Add(new SqlParameter("@ThietBiId", item.Key.ThietBiId.Value));
                                        command.Parameters.Add(new SqlParameter("@KhoPhongId", item.Key.KhoPhongId.Value));
                                        command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", reqVM.PhieuGhiTangThietBiId));
                                        command.Parameters.Add(new SqlParameter("@SoLuongTang", soLuongTang));
                                        command.ExecuteNonQuery();
                                    }
                                    DbContext.LogThietBis.AddRange(logDeleteThietBis);
                                    DbContext.SaveChanges();
                                }
                                // xoa thiet bi cu
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete ChiTietThietBi 
                                                        Where ChiTietThietBiId in (Select ChiTietThietBiId From PhieuGhiTangThietbi_ChiTietThietBi Where PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId);
                                                        Delete PhieuGhiTangThietbi_ChiTietThietBi Where PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId;";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", reqVM.PhieuGhiTangThietBiId));
                                    command.ExecuteNonQuery();
                                }

                                // them thiet bi moi
                                if (reqVM.ChiTietThietBis != null && reqVM.ChiTietThietBis.Count() > 0)
                                {
                                    List<ChiTietThietBi> chiTietThietBis = new List<ChiTietThietBi>();
                                    List<PhieuGhiTangThietBi_ChiTietThietBi> phieuGhiTangThietBi_ChiTietThietBis = new List<PhieuGhiTangThietBi_ChiTietThietBi>();
                                    foreach (var chiTietThietBiReqVM in reqVM.ChiTietThietBis)
                                    {
                                        ChiTietThietBi chiTietThietBi = MapDataHelper<ChiTietThietBi>.CopyDataObject(chiTietThietBiReqVM);
                                        chiTietThietBi.ChiTietThietBiId = Guid.NewGuid().ToString();
                                        chiTietThietBi.NgayCapNhat = DateTime.Now;
                                        chiTietThietBis.Add(chiTietThietBi);

                                        PhieuGhiTangThietBi_ChiTietThietBi phieuGhiTangThietBi_ChiTietThietBi = new PhieuGhiTangThietBi_ChiTietThietBi();
                                        phieuGhiTangThietBi_ChiTietThietBi.PhieuGhiTangThietBi_ChiTietThietBi_Id = Guid.NewGuid().ToString();
                                        phieuGhiTangThietBi_ChiTietThietBi.PhieuGhiTangThietBiId = reqVM.PhieuGhiTangThietBiId;
                                        phieuGhiTangThietBi_ChiTietThietBi.ChiTietThietBiId = chiTietThietBi.ChiTietThietBiId;
                                        phieuGhiTangThietBi_ChiTietThietBis.Add(phieuGhiTangThietBi_ChiTietThietBi);
                                    }
                                    DbContext.ChiTietThietBis.AddRange(chiTietThietBis);
                                    DbContext.SaveChanges();

                                    DbContext.PhieuGhiTangThietBi_ChiTietThietBi.AddRange(phieuGhiTangThietBi_ChiTietThietBis);
                                    DbContext.SaveChanges();

                                }
                                
                                // xoa tai lieu dinh kem cu
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete FileTrongPhieuGhiTang Where PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId;";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", reqVM.PhieuGhiTangThietBiId));
                                    command.ExecuteNonQuery();
                                }

                                // them tai lieu dinh kem moi
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuGhiTang> fileTrongPhieuGhiTangs = new List<FileTrongPhieuGhiTang>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuGhiTang fileTrongPhieuGhiTang = new FileTrongPhieuGhiTang();
                                        fileTrongPhieuGhiTang.FileTrongPhieuGhiTangId = item.FileId;
                                        fileTrongPhieuGhiTang.Ext = item.Ext;
                                        fileTrongPhieuGhiTang.Icon = item.Icon;
                                        fileTrongPhieuGhiTang.TenFile = item.TenFile;
                                        fileTrongPhieuGhiTang.Url = item.Url;
                                        fileTrongPhieuGhiTang.PhieuGhiTangThietBiId = reqVM.PhieuGhiTangThietBiId;
                                        fileTrongPhieuGhiTangs.Add(fileTrongPhieuGhiTang);
                                    }
                                    DbContext.FileTrongPhieuGhiTangs.AddRange(fileTrongPhieuGhiTangs);
                                    DbContext.SaveChanges();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePhieuGhiTangThietBi(DeletePhieuGhiTangThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {

                                List<ChiTietThietBiResVM> chiTietThietBis = new List<ChiTietThietBiResVM>();
                                {
                                    string sqlString = @"Select b.ChiTietThietBiId, b.ThietBiId, b.KhoPhongId, b.SoLuong
                                                        From PhieuGhiTangThietBi_ChiTietThietBi as a
                                                        Inner Join ChiTietThietBi as b
                                                        on a.ChiTietThietBiId = b.ChiTietThietBiId
                                                        Where a.PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", reqVM.PhieuGhiTangThietBiId));
                                    using (var reader = command.ExecuteReader())
                                    {
                                        chiTietThietBis = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                                    }

                                }

                                foreach (var item in chiTietThietBis.GroupBy(x => new { x.ThietBiId, x.KhoPhongId }))
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update KhoThietBi
                                                            Set SoLuong = SoLuong  - (Select ISNULL(Sum(b.SoLuong), 0)
                                                                                                    From PhieuGhiTangThietBi_ChiTietThietBi as a
                                                                                                    Inner Join ChiTietThietBi as b
                                                                                                    on a.ChiTietThietBiId = b.ChiTietThietBiId
                                                                                                    Where (PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId) 
                                                                                                        and (b.ThietBiId = @ThietBiId) and (b.KhoPhongId = @KhoPhongId)),
                                                                SoLuongConLai = SoLuongConLai  - (Select ISNULL(Sum(b.SoLuong), 0)
                                                                                                    From PhieuGhiTangThietBi_ChiTietThietBi as a
                                                                                                    Inner Join ChiTietThietBi as b
                                                                                                    on a.ChiTietThietBiId = b.ChiTietThietBiId
                                                                                                    Where (PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId) 
                                                                                                        and (b.ThietBiId = @ThietBiId) and (b.KhoPhongId = @KhoPhongId))
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@ThietBiId", item.Key.ThietBiId.Value));
                                    command.Parameters.Add(new SqlParameter("@KhoPhongId", item.Key.KhoPhongId.Value));
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", reqVM.PhieuGhiTangThietBiId));
                                    command.ExecuteNonQuery();
                                }

                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete ChiTietThietBi
                                            Where ChiTietThietBiId in (Select ChiTietThietBiId From PhieuGhiTangThietBi_ChiTietThietBi
							                                            Where (PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId));
                                            Delete PhieuGhiTangThietBi_ChiTietThietBi
                                            Where (PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId);
                                            Delete PhieuGhiTangThietBi Where (PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId);
                                            Delete FileTrongPhieuGhiTang Where PhieuGhiTangThietBiId = @PhieuGhiTangThietBiId;";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiTangThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiTangThietBiId) ? "" : reqVM.PhieuGhiTangThietBiId.Trim()));
                                    command.ExecuteNonQuery();
                                }

                                dbContextTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                throw new WarningException(ex.Message);
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Phieu ghi hong thiet bi
        public ListWithPaginationResVM GetPhieuGhiHongThietBis(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("SoPhieu", soPhieu);
                queryParamsDict.Add("TuNgay", tuNgay.HasValue ? tuNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("DenNgay", denNgay.HasValue ? denNgay.Value.ToString("yyyy-MM-dd") : null);
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select x.*, ((Select IsNull(Sum(SoLuongHong),0) 
                                                        From ChiTietPhieuGhiHongThietBi 
                                                        Where PhieuGhiHongThietBiId = x.PhieuGhiHongThietBiId)
                                                        -
                                                        (Select IsNull(Sum(SoLuongSuaChua),0) 
                                                        From ChiTietPhieuSuaChua Where PhieuGhiHongThietBiId = x.PhieuGhiHongThietBiId)) as 'SoLuongHongConLai',
                                                        (Select IsNull(Sum(SoLuongHong),0) 
                                                        From ChiTietPhieuGhiHongThietBi 
                                                        Where PhieuGhiHongThietBiId = x.PhieuGhiHongThietBiId) as 'SoLuongHong', b.HoTen
                                            From PhieuGhiHongThietBi as x
                                            Left Join NguoiDung as b
                                            on x.NguoiCapNhat = b.NguoiDungId
                                            Where ((@SoPhieu is null) or (x.SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (x.NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (x.NgayLap <= @DenNgay))
                                            Order by NgayLap Desc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<PhieuGhiHongThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From PhieuGhiHongThietBi
                                            Where ((@SoPhieu is null) or (SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (NgayLap <= @DenNgay))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt64(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var phieuGhiHongThietBiResVM in (List<PhieuGhiHongThietBiResVM>)listWithPaginationResVM.Objects)
                {
                    phieuGhiHongThietBiResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                listWithPaginationResVM.CurrentQueryParamsDict = queryParamsDict;
                listWithPaginationResVM.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);

                return listWithPaginationResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddPhieuGhiHongThietBi(AddPhieuGhiHongThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                PhieuGhiHongThietBi phieuGhiHongThietBi = new PhieuGhiHongThietBi();
                                phieuGhiHongThietBi.PhieuGhiHongThietBiId = Guid.NewGuid().ToString();
                                phieuGhiHongThietBi.NoiDung = string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim();
                                phieuGhiHongThietBi.NgayLap = reqVM.NgayLap;
                                phieuGhiHongThietBi.SoPhieu = reqVM.SoPhieu;
                                phieuGhiHongThietBi.NguoiCapNhat = reqVM.NguoiCapNhat;
                                DbContext.PhieuGhiHongThietBis.Add(phieuGhiHongThietBi);
                                DbContext.SaveChanges();

                                if (reqVM.ChiTietThietBiHongs != null && reqVM.ChiTietThietBiHongs.Count() > 0)
                                {
                                    List<ChiTietPhieuGhiHongThietBi> chiTietPhieuGhiHongThietBis = new List<ChiTietPhieuGhiHongThietBi>();
                                    foreach (var chiTietThietBiHongReqVM in reqVM.ChiTietThietBiHongs)
                                    {
                                        int soLuongConLai = 0;
                                        {
                                            string sqlString = @"Select SoLuongConLai From KhoThietBi Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiHongReqVM.ThietBiId.Value));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiHongReqVM.KhoPhongId.Value));
                                            using (var reader = command.ExecuteReader())
                                            {
                                                if (reader.Read())
                                                {
                                                    soLuongConLai = Convert.ToInt32(reader[0]);
                                                }
                                            }
                                        }

                                        if (soLuongConLai < chiTietThietBiHongReqVM.SoLuongHong)
                                        {
                                            dbContextTransaction.Rollback();
                                            throw new WarningException("Số lượng hỏng không lớn hơn số lượng ban đầu");
                                        }

                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Update KhoThietBi
                                                            Set SoLuongConLai = SoLuongConLai - @SoLuongHong, SoLuong = SoLuong - @SoLuongHong
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiHongReqVM.ThietBiId.Value));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiHongReqVM.KhoPhongId.Value));
                                            command.Parameters.Add(new SqlParameter("@SoLuongHong", chiTietThietBiHongReqVM.SoLuongHong.Value));
                                            command.ExecuteNonQuery();
                                        }


                                        ChiTietPhieuGhiHongThietBi chiTietPhieuGhiHongThietBi = new ChiTietPhieuGhiHongThietBi();
                                        chiTietPhieuGhiHongThietBi.ChiTietPhieuGhiHongThietBiId = Guid.NewGuid().ToString();
                                        chiTietPhieuGhiHongThietBi.PhieuGhiHongThietBiId = phieuGhiHongThietBi.PhieuGhiHongThietBiId;
                                        chiTietPhieuGhiHongThietBi.ThietBiId = chiTietThietBiHongReqVM.ThietBiId;
                                        chiTietPhieuGhiHongThietBi.SoLuongHong = chiTietThietBiHongReqVM.SoLuongHong.HasValue ? chiTietThietBiHongReqVM.SoLuongHong : 0;
                                        chiTietPhieuGhiHongThietBi.KhoPhongId = chiTietThietBiHongReqVM.KhoPhongId;
                                        chiTietPhieuGhiHongThietBi.LyDo = string.IsNullOrEmpty(chiTietThietBiHongReqVM.LyDo) ? "" : chiTietThietBiHongReqVM.LyDo.Trim();
                                        chiTietPhieuGhiHongThietBis.Add(chiTietPhieuGhiHongThietBi);
                                    }
                                    DbContext.ChiTietPhieuGhiHongThietBis.AddRange(chiTietPhieuGhiHongThietBis);
                                    DbContext.SaveChanges();
                                }

                                // tai lieu dinh kem
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuGhiHong> fileTrongPhieuGhiHongs = new List<FileTrongPhieuGhiHong>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuGhiHong fileTrongPhieuGhiHong = new FileTrongPhieuGhiHong();
                                        fileTrongPhieuGhiHong.FileTrongPhieuGhiHongId = item.FileId;
                                        fileTrongPhieuGhiHong.Ext = item.Ext;
                                        fileTrongPhieuGhiHong.Icon = item.Icon;
                                        fileTrongPhieuGhiHong.TenFile = item.TenFile;
                                        fileTrongPhieuGhiHong.Url = item.Url;
                                        fileTrongPhieuGhiHong.PhieuGhiHongThietBiId = phieuGhiHongThietBi.PhieuGhiHongThietBiId;
                                        fileTrongPhieuGhiHongs.Add(fileTrongPhieuGhiHong);
                                    }
                                    DbContext.FileTrongPhieuGhiHongs.AddRange(fileTrongPhieuGhiHongs);
                                    DbContext.SaveChanges();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PhieuGhiHongThietBiResVM GetPhieuGhiHongThietBi(string phieuGhiHongThietBiId)
        {
            try
            {
                PhieuGhiHongThietBiResVM phieuGhiHongThietBiResVM = new PhieuGhiHongThietBiResVM();

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = "Select * From PhieuGhiHongThietBi Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(phieuGhiHongThietBiId) ? "" : phieuGhiHongThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiHongThietBiResVM = MapDataHelper<PhieuGhiHongThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.*, g.SoLuongConLai, g.KhoPhongId, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong, h.SoLuongSuaChua
                                            From ChiTietPhieuGhiHongThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Left Join KhoThietBi as g
                                            on a.ThietBiId = g.ThietBiId and a.KhoPhongId = g.KhoPhongId
                                            Left Join (Select PhieuGhiHongThietBiId, IsNull(Sum(SoLuongSuaChua),0) as 'SoLuongSuaChua' From ChiTietPhieuSuaChua
                                                        Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId
                                                        Group by PhieuGhiHongThietBiId) as h
                                            on a.PhieuGhiHongThietBiId = h.PhieuGhiHongThietBiId
                                            Where a.PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(phieuGhiHongThietBiId) ? "" : phieuGhiHongThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiHongThietBiResVM.ChiTietThietBis = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select FileTrongPhieuGhiHongId as 'FileId', TenFile, Ext, Url, Icon From FileTrongPhieuGhiHong
                                            Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(phieuGhiHongThietBiId) ? "" : phieuGhiHongThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiHongThietBiResVM.FileTrongPhieuGhiHongs = MapDataHelper<FileResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return phieuGhiHongThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePhieuGhiHongThietBi(UpdatePhieuGhiHongThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();
                        command.CommandType = CommandType.Text;
                        command.Transaction = dbContextTransaction.UnderlyingTransaction;
                        try
                        {
                            //check can update
                            int soLuongSuaChua = 0;
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Select IsNull(Sum(SoLuongSuaChua), 0) From ChiTietPhieuSuaChua 
                                                    Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                                command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                                command.CommandText = sqlString;
                                using (var reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        soLuongSuaChua = Convert.ToInt32(reader[0]);
                                    }
                                }
                            }

                            if (soLuongSuaChua > 0)
                            {
                                throw new WarningException("Không thể cập nhập phiếu có thiết bị đã được sửa chữa");
                            }

                            int totalRow = 0;
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Select Count(*) From ChiTietPhieuGhiGiamThietBi as a
                                                    Inner Join ChiTietPhieuGhiHongThietBi as b
                                                    on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                                    Where b.PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                                command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                                command.CommandText = sqlString;
                                using(var reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        totalRow = Convert.ToInt32(reader[0]);
                                    }
                                }
                            }

                            if (totalRow > 0)
                            {
                                throw new WarningException("Không thể cập nhật phiếu ghi hỏng đã được chọn trong phiếu ghi giảm");
                            }

                            // update thong tin co ban
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Update PhieuGhiHongThietBi
                                                    Set SoPhieu = @SoPhieu, NoiDung = @NoiDung, NgayLap = @NgayLap
                                                    Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                                command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                                command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(reqVM.SoPhieu) ? "" : reqVM.SoPhieu.Trim()));
                                command.Parameters.Add(new SqlParameter("@NoiDung", string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim()));
                                command.Parameters.Add(new SqlParameter("@NgayLap", reqVM.NgayLap.HasValue ? reqVM.NgayLap.Value : (object)DBNull.Value));
                                command.CommandText = sqlString;
                                command.ExecuteNonQuery();
                            }

                            // update so luong
                            if (reqVM.ChiTietThietBiHongs != null && reqVM.ChiTietThietBiHongs.Count() > 0)
                            {
                                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Select * From ChiTietPhieuGhiHongThietBi Where (PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId)";
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                                    command.CommandText = sqlString;
                                    using (var reader = command.ExecuteReader())
                                    {
                                        chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                                    }
                                }

                                foreach (var item in chiTietThietBiResVMs.Where(x => !reqVM.ChiTietThietBiHongs.Any(k => (k.ThietBiId == x.ThietBiId) && (k.KhoPhongId == x.KhoPhongId))).ToList())
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update KhoThietBi
                                                        Set SoLuong = SoLuong + b.SoLuongHong, 
                                                            SoLuongConLai = SoLuongConLai + b.SoLuongHong
                                                        From KhoThietBi as a
                                                        Inner Join ChiTietPhieuGhiHongThietBi as b
                                                        on a.ThietBiId = b.ThietBiId and a.KhoPhongId = b.KhoPhongId
                                                        Where (b.PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId) and (b.ThietBiId = @ThietBiId) and (b.KhoPhongId = @KhoPhongId)";
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                                    command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.HasValue ? item.ThietBiId.Value : 0));
                                    command.Parameters.Add(new SqlParameter("@KhoPhongId", item.KhoPhongId.HasValue ? item.KhoPhongId.Value : 0));
                                    command.CommandText = sqlString;
                                    command.ExecuteNonQuery();
                                }

                                List<ChiTietPhieuGhiHongThietBi> chiTietPhieuGhiHongThietBis = new List<ChiTietPhieuGhiHongThietBi>();
                                foreach (var chiTietThietBiHongReqVM in reqVM.ChiTietThietBiHongs)
                                {

                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Update KhoThietBi
                                                Set SoLuong = SoLuong + (Select IsNull((Select SoLuongHong
                                                                                        From ChiTietPhieuGhiHongThietBi 
                                                                                         Where (PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId) 
                                                                                            and (ThietBiId = @ThietBiId and (KhoPhongId = @KhoPhongId))), 0)) - @SoLuongHong, 
                                                    SoLuongConLai = SoLuongConLai + (Select IsNull((Select SoLuongHong
                                                                                        From ChiTietPhieuGhiHongThietBi 
                                                                                         Where (PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId) 
                                                                                            and (ThietBiId = @ThietBiId and (KhoPhongId = @KhoPhongId))), 0)) - @SoLuongHong
                                                Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                        command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                                        command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiHongReqVM.ThietBiId.HasValue ? chiTietThietBiHongReqVM.ThietBiId.Value : 0));
                                        command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiHongReqVM.KhoPhongId.HasValue ? chiTietThietBiHongReqVM.KhoPhongId.Value : 0));
                                        command.Parameters.Add(new SqlParameter("@SoLuongHong", chiTietThietBiHongReqVM.SoLuongHong.HasValue ? chiTietThietBiHongReqVM.SoLuongHong.Value : 0));
                                        command.CommandText = sqlString;
                                        command.ExecuteNonQuery();
                                    }

                                    ChiTietPhieuGhiHongThietBi chiTietPhieuGhiHongThietBi = new ChiTietPhieuGhiHongThietBi();
                                    chiTietPhieuGhiHongThietBi.ChiTietPhieuGhiHongThietBiId = Guid.NewGuid().ToString();
                                    chiTietPhieuGhiHongThietBi.PhieuGhiHongThietBiId = reqVM.PhieuGhiHongThietBiId;
                                    chiTietPhieuGhiHongThietBi.ThietBiId = chiTietThietBiHongReqVM.ThietBiId;
                                    chiTietPhieuGhiHongThietBi.SoLuongHong = chiTietThietBiHongReqVM.SoLuongHong.HasValue ? chiTietThietBiHongReqVM.SoLuongHong : 0;
                                    chiTietPhieuGhiHongThietBi.KhoPhongId = chiTietThietBiHongReqVM.KhoPhongId;
                                    chiTietPhieuGhiHongThietBi.LyDo = string.IsNullOrEmpty(chiTietThietBiHongReqVM.LyDo) ? "" : chiTietThietBiHongReqVM.LyDo.Trim();
                                    chiTietPhieuGhiHongThietBis.Add(chiTietPhieuGhiHongThietBi);
                                }

                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete ChiTietPhieuGhiHongThietBi Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                                    command.CommandText = sqlString;
                                    command.ExecuteNonQuery();
                                }

                                // xoa tai lieu dinh kem cu
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete FileTrongPhieuGhiHong Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId;";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", reqVM.PhieuGhiHongThietBiId));
                                    command.ExecuteNonQuery();
                                }

                                // tai lieu dinh kem
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuGhiHong> fileTrongPhieuGhiHongs = new List<FileTrongPhieuGhiHong>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuGhiHong fileTrongPhieuGhiHong = new FileTrongPhieuGhiHong();
                                        fileTrongPhieuGhiHong.FileTrongPhieuGhiHongId = item.FileId;
                                        fileTrongPhieuGhiHong.Ext = item.Ext;
                                        fileTrongPhieuGhiHong.Icon = item.Icon;
                                        fileTrongPhieuGhiHong.TenFile = item.TenFile;
                                        fileTrongPhieuGhiHong.Url = item.Url;
                                        fileTrongPhieuGhiHong.PhieuGhiHongThietBiId = reqVM.PhieuGhiHongThietBiId;
                                        fileTrongPhieuGhiHongs.Add(fileTrongPhieuGhiHong);
                                    }
                                    DbContext.FileTrongPhieuGhiHongs.AddRange(fileTrongPhieuGhiHongs);
                                    DbContext.SaveChanges();
                                }

                                DbContext.ChiTietPhieuGhiHongThietBis.AddRange(chiTietPhieuGhiHongThietBis);
                                DbContext.SaveChanges();
                            }
                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw new WarningException(ex.Message);
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePhieuGhiHongThietBi(DeletePhieuGhiHongThietBiReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    command.CommandType = CommandType.Text;
                    try
                    {
                        // check can delete
                        int soLuongSuaChua = 0;
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(SoLuongSuaChua), 0) From ChiTietPhieuSuaChua 
                                                    Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                            command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                            command.CommandText = sqlString;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    soLuongSuaChua = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        if (soLuongSuaChua > 0)
                        {
                            throw new WarningException("Không thể xóa phiếu có thiết bị đã được sửa chữa");
                        }

                        int totalRow = 0;
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select Count(*) From ChiTietPhieuGhiGiamThietBi as a
                                                    Inner Join ChiTietPhieuGhiHongThietBi as b
                                                    on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                                    Where b.PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId";
                            command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                            command.CommandText = sqlString;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    totalRow = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        if (totalRow > 0)
                        {
                            throw new WarningException("Không thể xóa phiếu ghi hỏng đã được chọn trong phiếu ghi giảm");
                        }

                        {
                            command.Parameters.Clear();
                            string sqlString = @"Update KhoThietBi
                                            Set SoLuong = SoLuong + b.SoLuongHong, 
                                                SoLuongConLai = SoLuongConLai + b.SoLuongHong
                                            From KhoThietBi as a
                                            Inner Join ChiTietPhieuGhiHongThietBi as b
                                            on a.ThietBiId = b.ThietBiId and a.KhoPhongId = b.KhoPhongId
                                            Where (b.PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId);
                                            Delete ChiTietPhieuGhiHongThietBi Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId;   
                                            Delete PhieuGhiHongThietBi Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId;
                                            Delete FileTrongPhieuGhiHong Where PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId;";
                            command.CommandText = sqlString;
                            command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiHongThietBiId) ? "" : reqVM.PhieuGhiHongThietBiId.Trim()));
                            command.ExecuteNonQuery();
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Phieu ghi mat thiet bi
        public ListWithPaginationResVM GetPhieuGhiMatThietBis(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("SoPhieu", soPhieu);
                queryParamsDict.Add("TuNgay", tuNgay.HasValue ? tuNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("DenNgay", denNgay.HasValue ? denNgay.Value.ToString("yyyy-MM-dd") : null);
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select x.*, b.HoTen
                                            From PhieuGhiMatThietBi as x
                                            Left Join NguoiDung as b
                                            on x.NguoiCapNhat = b.NguoiDungId
                                            Where ((@SoPhieu is null) or (x.SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (x.NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (x.NgayLap <= @DenNgay))
                                            Order by NgayLap Desc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<PhieuGhiMatThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From PhieuGhiMatThietBi
                                            Where ((@SoPhieu is null) or (SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (NgayLap <= @DenNgay))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt64(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var phieuGhiMatThietBiResVM in (List<PhieuGhiMatThietBiResVM>)listWithPaginationResVM.Objects)
                {
                    phieuGhiMatThietBiResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                listWithPaginationResVM.CurrentQueryParamsDict = queryParamsDict;
                listWithPaginationResVM.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);

                return listWithPaginationResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddPhieuGhiMatThietBi(AddPhieuGhiMatThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                PhieuGhiMatThietBi phieuGhiMatThietBi = new PhieuGhiMatThietBi();
                                phieuGhiMatThietBi.PhieuGhiMatThietBiId = Guid.NewGuid().ToString();
                                phieuGhiMatThietBi.NoiDung = string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim();
                                phieuGhiMatThietBi.NgayLap = reqVM.NgayLap;
                                phieuGhiMatThietBi.SoPhieu = reqVM.SoPhieu;
                                phieuGhiMatThietBi.NguoiCapNhat = reqVM.NguoiCapNhat;
                                DbContext.PhieuGhiMatThietBis.Add(phieuGhiMatThietBi);
                                DbContext.SaveChanges();

                                if (reqVM.ChiTietThietBiMats != null && reqVM.ChiTietThietBiMats.Count() > 0)
                                {
                                    List<ChiTietPhieuGhiMatThietBi> chiTietPhieuGhiMatThietBis = new List<ChiTietPhieuGhiMatThietBi>();
                                    foreach (var chiTietThietBiMatReqVM in reqVM.ChiTietThietBiMats)
                                    {
                                        int soLuongConLai = 0;
                                        {
                                            string sqlString = @"Select SoLuongConLai From KhoThietBi Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiMatReqVM.ThietBiId.Value));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiMatReqVM.KhoPhongId.Value));
                                            using (var reader = command.ExecuteReader())
                                            {
                                                if (reader.Read())
                                                {
                                                    soLuongConLai = Convert.ToInt32(reader[0]);
                                                }
                                            }
                                        }

                                        if (soLuongConLai < chiTietThietBiMatReqVM.SoLuongMat)
                                        {
                                            dbContextTransaction.Rollback();
                                            throw new WarningException("Số lượng mất không lớn hơn số lượng ban đầu");
                                        }

                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Update KhoThietBi
                                                            Set SoLuongConLai = SoLuongConLai - @SoLuongMat, SoLuong = SoLuong - @SoLuongMat
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiMatReqVM.ThietBiId.Value));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiMatReqVM.KhoPhongId.Value));
                                            command.Parameters.Add(new SqlParameter("@SoLuongMat", chiTietThietBiMatReqVM.SoLuongMat.Value));
                                            command.ExecuteNonQuery();
                                        }


                                        ChiTietPhieuGhiMatThietBi chiTietPhieuGhiMatThietBi = new ChiTietPhieuGhiMatThietBi();
                                        chiTietPhieuGhiMatThietBi.ChiTietPhieuGhiMatThietBiId = Guid.NewGuid().ToString();
                                        chiTietPhieuGhiMatThietBi.PhieuGhiMatThietBiId = phieuGhiMatThietBi.PhieuGhiMatThietBiId;
                                        chiTietPhieuGhiMatThietBi.ThietBiId = chiTietThietBiMatReqVM.ThietBiId;
                                        chiTietPhieuGhiMatThietBi.SoLuongMat = chiTietThietBiMatReqVM.SoLuongMat.HasValue ? chiTietThietBiMatReqVM.SoLuongMat : 0;
                                        chiTietPhieuGhiMatThietBi.KhoPhongId = chiTietThietBiMatReqVM.KhoPhongId;
                                        chiTietPhieuGhiMatThietBi.LyDo = string.IsNullOrEmpty(chiTietThietBiMatReqVM.LyDo) ? "" : chiTietThietBiMatReqVM.LyDo.Trim();
                                        chiTietPhieuGhiMatThietBis.Add(chiTietPhieuGhiMatThietBi);
                                    }
                                    DbContext.ChiTietPhieuGhiMatThietBis.AddRange(chiTietPhieuGhiMatThietBis);
                                    DbContext.SaveChanges();
                                }

                                // tai lieu dinh kem
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuGhiMat> fileTrongPhieuGhiMats = new List<FileTrongPhieuGhiMat>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuGhiMat fileTrongPhieuGhiMat = new FileTrongPhieuGhiMat();
                                        fileTrongPhieuGhiMat.FileTrongPhieuGhiMatId = item.FileId;
                                        fileTrongPhieuGhiMat.Ext = item.Ext;
                                        fileTrongPhieuGhiMat.Icon = item.Icon;
                                        fileTrongPhieuGhiMat.TenFile = item.TenFile;
                                        fileTrongPhieuGhiMat.Url = item.Url;
                                        fileTrongPhieuGhiMat.PhieuGhiMatThietBiId = phieuGhiMatThietBi.PhieuGhiMatThietBiId;
                                        fileTrongPhieuGhiMats.Add(fileTrongPhieuGhiMat);
                                    }
                                    DbContext.FileTrongPhieuGhiMats.AddRange(fileTrongPhieuGhiMats);
                                    DbContext.SaveChanges();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PhieuGhiMatThietBiResVM GetPhieuGhiMatThietBi(string phieuGhiMatThietBiId)
        {
            try
            {
                PhieuGhiMatThietBiResVM phieuGhiMatThietBiResVM = new PhieuGhiMatThietBiResVM();

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = "Select * From PhieuGhiMatThietBi Where PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(phieuGhiMatThietBiId) ? "" : phieuGhiMatThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiMatThietBiResVM = MapDataHelper<PhieuGhiMatThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.*, g.SoLuongConLai, g.KhoPhongId, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong
                                            From ChiTietPhieuGhiMatThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Left Join KhoThietBi as g
                                            on a.ThietBiId = g.ThietBiId and a.KhoPhongId = g.KhoPhongId
                                            Where a.PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(phieuGhiMatThietBiId) ? "" : phieuGhiMatThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiMatThietBiResVM.ChiTietThietBis = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select FileTrongPhieuGhiMatId as 'FileId', TenFile, Ext, Url, Icon From FileTrongPhieuGhiMat
                                            Where PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(phieuGhiMatThietBiId) ? "" : phieuGhiMatThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiMatThietBiResVM.FileTrongPhieuGhiMats = MapDataHelper<FileResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return phieuGhiMatThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePhieuGhiMatThietBi(UpdatePhieuGhiMatThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();
                        command.CommandType = CommandType.Text;
                        command.Transaction = dbContextTransaction.UnderlyingTransaction;
                        try
                        {
                            // update thong tin co ban
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Update PhieuGhiMatThietBi
                                                    Set SoPhieu = @SoPhieu, NoiDung = @NoiDung, NgayLap = @NgayLap
                                                    Where PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId";
                                command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiMatThietBiId) ? "" : reqVM.PhieuGhiMatThietBiId.Trim()));
                                command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(reqVM.SoPhieu) ? "" : reqVM.SoPhieu.Trim()));
                                command.Parameters.Add(new SqlParameter("@NoiDung", string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim()));
                                command.Parameters.Add(new SqlParameter("@NgayLap", reqVM.NgayLap.HasValue ? reqVM.NgayLap.Value : (object)DBNull.Value));
                                command.CommandText = sqlString;
                                command.ExecuteNonQuery();
                            }

                            // check can update
                            int totalRow = 0;
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Select Count(*) From ChiTietPhieuGhiGiamThietBi as a
                                                    Inner Join ChiTietPhieuGhiMatThietBi as b
                                                    on a.PhieuGhiMatThietBiId = b.PhieuGhiMatThietBiId
                                                    Where b.PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId";
                                command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiMatThietBiId) ? "" : reqVM.PhieuGhiMatThietBiId.Trim()));
                                command.CommandText = sqlString;
                                using (var reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        totalRow = Convert.ToInt32(reader[0]);
                                    }
                                }
                            }

                            if(totalRow > 0)
                            {
                                throw new WarningException("Không thể cập nhật phiếu ghi mất đã được chọn trong phiếu ghi giảm");
                            }

                            // update so luong
                            if (reqVM.ChiTietThietBiMats != null && reqVM.ChiTietThietBiMats.Count() > 0)
                            {
                                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Select * From ChiTietPhieuGhiMatThietBi Where (PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId)";
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiMatThietBiId) ? "" : reqVM.PhieuGhiMatThietBiId.Trim()));
                                    command.CommandText = sqlString;
                                    using (var reader = command.ExecuteReader())
                                    {
                                        chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                                    }
                                }

                                foreach (var item in chiTietThietBiResVMs.Where(x => !reqVM.ChiTietThietBiMats.Any(k => (k.ThietBiId == x.ThietBiId) && (k.KhoPhongId == x.KhoPhongId))).ToList())
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update KhoThietBi
                                                        Set SoLuong = SoLuong + b.SoLuongMat, 
                                                            SoLuongConLai = SoLuongConLai + b.SoLuongMat
                                                        From KhoThietBi as a
                                                        Inner Join ChiTietPhieuGhiMatThietBi as b
                                                        on a.ThietBiId = b.ThietBiId and a.KhoPhongId = b.KhoPhongId
                                                        Where (b.PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId) and (b.ThietBiId = @ThietBiId) and (b.KhoPhongId = @KhoPhongId)";
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiMatThietBiId) ? "" : reqVM.PhieuGhiMatThietBiId.Trim()));
                                    command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.HasValue ? item.ThietBiId.Value : 0));
                                    command.Parameters.Add(new SqlParameter("@KhoPhongId", item.KhoPhongId.HasValue ? item.KhoPhongId.Value : 0));
                                    command.CommandText = sqlString;
                                    command.ExecuteNonQuery();
                                }

                                List<ChiTietPhieuGhiMatThietBi> chiTietPhieuGhiMatThietBis = new List<ChiTietPhieuGhiMatThietBi>();
                                foreach (var chiTietThietBiMatReqVM in reqVM.ChiTietThietBiMats)
                                {

                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Update KhoThietBi
                                                Set SoLuong = SoLuong + (Select IsNull((Select SoLuongMat
                                                                                        From ChiTietPhieuGhiMatThietBi 
                                                                                         Where (PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId) 
                                                                                            and (ThietBiId = @ThietBiId and (KhoPhongId = @KhoPhongId))), 0)) - @SoLuongMat, 
                                                    SoLuongConLai = SoLuongConLai + (Select IsNull((Select SoLuongMat
                                                                                        From ChiTietPhieuGhiMatThietBi 
                                                                                         Where (PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId) 
                                                                                            and (ThietBiId = @ThietBiId and (KhoPhongId = @KhoPhongId))), 0)) - @SoLuongMat
                                                Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                        command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiMatThietBiId) ? "" : reqVM.PhieuGhiMatThietBiId.Trim()));
                                        command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiMatReqVM.ThietBiId.HasValue ? chiTietThietBiMatReqVM.ThietBiId.Value : 0));
                                        command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiMatReqVM.KhoPhongId.HasValue ? chiTietThietBiMatReqVM.KhoPhongId.Value : 0));
                                        command.Parameters.Add(new SqlParameter("@SoLuongMat", chiTietThietBiMatReqVM.SoLuongMat.HasValue ? chiTietThietBiMatReqVM.SoLuongMat.Value : 0));
                                        command.CommandText = sqlString;
                                        command.ExecuteNonQuery();
                                    }

                                    ChiTietPhieuGhiMatThietBi chiTietPhieuGhiMatThietBi = new ChiTietPhieuGhiMatThietBi();
                                    chiTietPhieuGhiMatThietBi.ChiTietPhieuGhiMatThietBiId = Guid.NewGuid().ToString();
                                    chiTietPhieuGhiMatThietBi.PhieuGhiMatThietBiId = reqVM.PhieuGhiMatThietBiId;
                                    chiTietPhieuGhiMatThietBi.ThietBiId = chiTietThietBiMatReqVM.ThietBiId;
                                    chiTietPhieuGhiMatThietBi.SoLuongMat = chiTietThietBiMatReqVM.SoLuongMat.HasValue ? chiTietThietBiMatReqVM.SoLuongMat : 0;
                                    chiTietPhieuGhiMatThietBi.KhoPhongId = chiTietThietBiMatReqVM.KhoPhongId;
                                    chiTietPhieuGhiMatThietBi.LyDo = string.IsNullOrEmpty(chiTietThietBiMatReqVM.LyDo) ? "" : chiTietThietBiMatReqVM.LyDo.Trim();
                                    chiTietPhieuGhiMatThietBis.Add(chiTietPhieuGhiMatThietBi);
                                }

                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete ChiTietPhieuGhiMatThietBi Where PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId";
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiMatThietBiId) ? "" : reqVM.PhieuGhiMatThietBiId.Trim()));
                                    command.CommandText = sqlString;
                                    command.ExecuteNonQuery();
                                }

                                DbContext.ChiTietPhieuGhiMatThietBis.AddRange(chiTietPhieuGhiMatThietBis);
                                DbContext.SaveChanges();
                            }

                            // xoa tai lieu dinh kem cu
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Delete FileTrongPhieuGhiMat Where PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId;";
                                command.CommandText = sqlString;
                                command.CommandType = CommandType.Text;
                                command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", reqVM.PhieuGhiMatThietBiId));
                                command.ExecuteNonQuery();
                            }

                            // tai lieu dinh kem
                            if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                            {
                                List<FileTrongPhieuGhiMat> fileTrongPhieuGhiMats = new List<FileTrongPhieuGhiMat>();

                                foreach (var item in reqVM.TaiLieuDinhKems)
                                {
                                    FileTrongPhieuGhiMat fileTrongPhieuGhiMat = new FileTrongPhieuGhiMat();
                                    fileTrongPhieuGhiMat.FileTrongPhieuGhiMatId = item.FileId;
                                    fileTrongPhieuGhiMat.Ext = item.Ext;
                                    fileTrongPhieuGhiMat.Icon = item.Icon;
                                    fileTrongPhieuGhiMat.TenFile = item.TenFile;
                                    fileTrongPhieuGhiMat.Url = item.Url;
                                    fileTrongPhieuGhiMat.PhieuGhiMatThietBiId = reqVM.PhieuGhiMatThietBiId;
                                    fileTrongPhieuGhiMats.Add(fileTrongPhieuGhiMat);
                                }
                                DbContext.FileTrongPhieuGhiMats.AddRange(fileTrongPhieuGhiMats);
                                DbContext.SaveChanges();
                            }

                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw new WarningException(ex.Message);
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePhieuGhiMatThietBi(DeletePhieuGhiMatThietBiReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    command.CommandType = CommandType.Text;
                    try
                    {
                        // check can update
                        int totalRow = 0;
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select Count(*) From ChiTietPhieuGhiGiamThietBi as a
                                                    Inner Join ChiTietPhieuGhiMatThietBi as b
                                                    on a.PhieuGhiMatThietBiId = b.PhieuGhiMatThietBiId
                                                    Where b.PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId";
                            command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiMatThietBiId) ? "" : reqVM.PhieuGhiMatThietBiId.Trim()));
                            command.CommandText = sqlString;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    totalRow = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        if (totalRow > 0)
                        {
                            throw new WarningException("Không thể xóa phiếu ghi mất đã được chọn trong phiếu ghi giảm");
                            }

                        {
                            command.Parameters.Clear();
                            string sqlString = @"Update KhoThietBi
                                            Set SoLuong = SoLuong + b.SoLuongMat, 
                                                SoLuongConLai = SoLuongConLai + b.SoLuongMat
                                            From KhoThietBi as a
                                            Inner Join ChiTietPhieuGhiMatThietBi as b
                                            on a.ThietBiId = b.ThietBiId and a.KhoPhongId = b.KhoPhongId
                                            Where (b.PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId);
                                            Delete ChiTietPhieuGhiMatThietBi Where PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId;   
                                            Delete PhieuGhiMatThietBi Where PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId;
                                            Delete FileTrongPhieuGhiMat Where PhieuGhiMatThietBiId = @PhieuGhiMatThietBiId;";
                            command.CommandText = sqlString;
                            command.Parameters.Add(new SqlParameter("@PhieuGhiMatThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiMatThietBiId) ? "" : reqVM.PhieuGhiMatThietBiId.Trim()));
                            command.ExecuteNonQuery();
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Phieu Ghi Giam Thiet Bi
        public ListWithPaginationResVM GetPhieuGhiGiamThietBis(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("SoPhieu", soPhieu);
                queryParamsDict.Add("TuNgay", tuNgay.HasValue ? tuNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("DenNgay", denNgay.HasValue ? denNgay.Value.ToString("yyyy-MM-dd") : null);
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.HoTen From PhieuGhiGiamThietBi as a
                                            Left Join NguoiDung as b
                                            on a.NguoiCapNhat = b.NguoiDungId
                                            Where ((@SoPhieu is null) or (a.SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (a.NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (a.NgayLap <= @DenNgay))
                                            Order by a.NgayLap Desc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<PhieuGhiGiamThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From PhieuGhiGiamThietBi
                                            Where ((@SoPhieu is null) or (SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (NgayLap <= @DenNgay))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt64(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var phieuGhiGiamThietBiResVM in (List<PhieuGhiGiamThietBiResVM>)listWithPaginationResVM.Objects)
                {
                    phieuGhiGiamThietBiResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                listWithPaginationResVM.CurrentQueryParamsDict = queryParamsDict;
                listWithPaginationResVM.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);

                return listWithPaginationResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddPhieuGhiGiamThietBi(AddPhieuGhiGiamThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                PhieuGhiGiamThietBi phieuGhiGiamThietBi = new PhieuGhiGiamThietBi();
                                phieuGhiGiamThietBi.PhieuGhiGiamThietBiId = Guid.NewGuid().ToString();
                                phieuGhiGiamThietBi.NoiDung = string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim();
                                phieuGhiGiamThietBi.NgayLap = reqVM.NgayLap;
                                phieuGhiGiamThietBi.SoPhieu = reqVM.SoPhieu;
                                phieuGhiGiamThietBi.NguoiCapNhat = reqVM.NguoiCapNhat;
                                DbContext.PhieuGhiGiamThietBis.Add(phieuGhiGiamThietBi);
                                DbContext.SaveChanges();

                                if (reqVM.ChiTietThietBiGiams != null && reqVM.ChiTietThietBiGiams.Count() > 0)
                                {
                                    List<ChiTietPhieuGhiGiamThietBi> chiTietPhieuGhiGiamThietBis = new List<ChiTietPhieuGhiGiamThietBi>();
                                    foreach (var chiTietThietBiGiamReqVM in reqVM.ChiTietThietBiGiams)
                                    {
                                        if (string.IsNullOrEmpty(chiTietThietBiGiamReqVM.PhieuGhiHongThietBiId) && string.IsNullOrEmpty(chiTietThietBiGiamReqVM.PhieuGhiMatThietBiId))
                                        {
                                            int soLuongConLai = 0;
                                            {
                                                command.Parameters.Clear();
                                                string sqlString = @"Select SoLuongConLai From KhoThietBi Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                                command.CommandText = sqlString;
                                                command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiGiamReqVM.ThietBiId.HasValue ? chiTietThietBiGiamReqVM.ThietBiId.Value : -1));
                                                command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiGiamReqVM.KhoPhongId.HasValue ? chiTietThietBiGiamReqVM.KhoPhongId.Value : -1));
                                                using (var reader = command.ExecuteReader())
                                                {
                                                    if (reader.Read())
                                                    {
                                                        soLuongConLai = Convert.ToInt32(reader[0]);
                                                    }
                                                }
                                            }
                                            if (soLuongConLai < chiTietThietBiGiamReqVM.SoLuongGiam)
                                            {
                                                throw new Exception("Số lượng giảm vượt quá số lượng trong kho");
                                            }

                                            {
                                                command.Parameters.Clear();
                                                string sqlString = @"Update KhoThietBi
                                                            Set SoLuong = SoLuong - @SoLuongGiam, SoLuongConLai = SoLuongConLai - @SoLuongGiam
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                                command.CommandText = sqlString;
                                                command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiGiamReqVM.ThietBiId.HasValue ? chiTietThietBiGiamReqVM.ThietBiId.Value : -1));
                                                command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiGiamReqVM.KhoPhongId.HasValue ? chiTietThietBiGiamReqVM.KhoPhongId.Value : -1));
                                                command.Parameters.Add(new SqlParameter("@SoLuongGiam", chiTietThietBiGiamReqVM.SoLuongGiam.HasValue ? chiTietThietBiGiamReqVM.SoLuongGiam.Value : 0));
                                                command.ExecuteNonQuery();
                                            }

                                        }

                                        ChiTietPhieuGhiGiamThietBi chiTietPhieuGhiGiamThietBi = new ChiTietPhieuGhiGiamThietBi();
                                        chiTietPhieuGhiGiamThietBi.ChiTietPhieuGhiGiamThietBiId = Guid.NewGuid().ToString();
                                        chiTietPhieuGhiGiamThietBi.PhieuGhiGiamThietBiId = phieuGhiGiamThietBi.PhieuGhiGiamThietBiId;
                                        chiTietPhieuGhiGiamThietBi.SoLuongGiam = chiTietThietBiGiamReqVM.SoLuongGiam;
                                        chiTietPhieuGhiGiamThietBi.LyDo = chiTietThietBiGiamReqVM.LyDo;
                                        chiTietPhieuGhiGiamThietBi.ThietBiId = chiTietThietBiGiamReqVM.ThietBiId;
                                        chiTietPhieuGhiGiamThietBi.KhoPhongId = chiTietThietBiGiamReqVM.KhoPhongId;
                                        chiTietPhieuGhiGiamThietBi.PhieuGhiMatThietBiId = chiTietThietBiGiamReqVM.PhieuGhiMatThietBiId;
                                        chiTietPhieuGhiGiamThietBi.PhieuGhiHongThietBiId = chiTietThietBiGiamReqVM.PhieuGhiHongThietBiId;
                                        chiTietPhieuGhiGiamThietBis.Add(chiTietPhieuGhiGiamThietBi);
                                    }
                                    DbContext.ChiTietPhieuGhiGiamThietBis.AddRange(chiTietPhieuGhiGiamThietBis);
                                    DbContext.SaveChanges();
                                }

                                // tai lieu dinh kem
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuGhiGiam> fileTrongPhieuGhiGiams = new List<FileTrongPhieuGhiGiam>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuGhiGiam fileTrongPhieuGhiGiam = new FileTrongPhieuGhiGiam();
                                        fileTrongPhieuGhiGiam.FileTrongPhieuGhiGiamId = item.FileId;
                                        fileTrongPhieuGhiGiam.Ext = item.Ext;
                                        fileTrongPhieuGhiGiam.Icon = item.Icon;
                                        fileTrongPhieuGhiGiam.TenFile = item.TenFile;
                                        fileTrongPhieuGhiGiam.Url = item.Url;
                                        fileTrongPhieuGhiGiam.PhieuGhiGiamThietBiId = phieuGhiGiamThietBi.PhieuGhiGiamThietBiId;
                                        fileTrongPhieuGhiGiams.Add(fileTrongPhieuGhiGiam);
                                    }
                                    DbContext.FileTrongPhieuGhiGiams.AddRange(fileTrongPhieuGhiGiams);
                                    DbContext.SaveChanges();
                                }

                                dbContextTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                throw new WarningException(ex.Message);
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PhieuGhiGiamThietBiResVM GetPhieuGhiGiamThietBi(string phieuGhiGiamThietBiId)
        {
            try
            {
                PhieuGhiGiamThietBiResVM phieuGhiGiamThietBiResVM = new PhieuGhiGiamThietBiResVM();

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = "Select * From PhieuGhiGiamThietBi Where PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(phieuGhiGiamThietBiId) ? "" : phieuGhiGiamThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiGiamThietBiResVM = MapDataHelper<PhieuGhiGiamThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*,
	                                            b.*, g.SoLuongConLai, g.KhoPhongId, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong,
                                                h.SoPhieu as 'SoPhieuGhiMatThietBi', i.SoPhieu as 'SoPhieuGhiHongThietBi'
                                            From ChiTietPhieuGhiGiamThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Left Join KhoThietBi as g
                                            on a.ThietBiId = g.ThietBiId and a.KhoPhongId = g.KhoPhongId
                                            Left Join PhieuGhiMatThietBi as h
                                            on a.PhieuGhiMatThietBiId = h.PhieuGhiMatThietBiId
                                            Left Join PhieuGhiHongThietBi as i
                                            on a.PhieuGhiHongThietBiId = i.PhieuGhiHongThietBiId
                                            Where a.PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(phieuGhiGiamThietBiId) ? "" : phieuGhiGiamThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiGiamThietBiResVM.ChiTietThietBis = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select FileTrongPhieuGhiGiamId as 'FileId', TenFile, Ext, Url, Icon From FileTrongPhieuGhiGiam
                                            Where PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(phieuGhiGiamThietBiId) ? "" : phieuGhiGiamThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuGhiGiamThietBiResVM.FileTrongPhieuGhiGiams = MapDataHelper<FileResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return phieuGhiGiamThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePhieuGhiGiamThietBi(UpdatePhieuGhiGiamThietBiReqVM reqVM)
        {
            try
            {

                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                // update thong tin co ban
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update PhieuGhiGiamThietBi
                                                    Set SoPhieu = @SoPhieu, NoiDung = @NoiDung, NgayLap = @NgayLap
                                                    Where PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId";
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiGiamThietBiId) ? "" : reqVM.PhieuGhiGiamThietBiId.Trim()));
                                    command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(reqVM.SoPhieu) ? "" : reqVM.SoPhieu.Trim()));
                                    command.Parameters.Add(new SqlParameter("@NoiDung", string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim()));
                                    command.Parameters.Add(new SqlParameter("@NgayLap", reqVM.NgayLap.HasValue ? reqVM.NgayLap.Value : (object)DBNull.Value));
                                    command.CommandText = sqlString;
                                    command.ExecuteNonQuery();
                                }

                                int totalRow = 0;
                                // check can update
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Select Count(*) From ChiTietPhieuGhiGiamThietBi as a
                                                        Inner Join ChiTietPhieuGhiHongThietBi as b
                                                        on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                                        Inner Join ChiTietPhieuSuaChua as c
                                                        on b.PhieuGhiHongThietBiId = c.PhieuGhiHongThietBiId
                                                        Where a.ChiTietPhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId";
                                    command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiGiamThietBiId) ? "" : reqVM.PhieuGhiGiamThietBiId.Trim()));
                                    command.CommandText = sqlString;
                                    using (var reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            totalRow = Convert.ToInt32(reader[0]);
                                        }
                                    }
                                }

                                if (totalRow > 0)
                                {
                                    throw new WarningException("Không thể cập nhật phiếu đã có thiết bị hỏng được sửa chữa");
                                }

                                // update so luong
                                if (reqVM.ChiTietThietBiGiams != null && reqVM.ChiTietThietBiGiams.Count() > 0)
                                {
                                    List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Select * From ChiTietPhieuGhiGiamThietBi Where (PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId) and (PhieuGhiMatThietBiId is null) and (PhieuGhiHongThietBiId is null)";
                                        command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiGiamThietBiId) ? "" : reqVM.PhieuGhiGiamThietBiId.Trim()));
                                        command.CommandText = sqlString;
                                        using (var reader = command.ExecuteReader())
                                        {
                                            chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                                        }
                                    }

                                    foreach (var item in chiTietThietBiResVMs.Where(x => !reqVM.ChiTietThietBiGiams.Any(k => (k.ThietBiId == x.ThietBiId) && (k.KhoPhongId == x.KhoPhongId))).ToList())
                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Update KhoThietBi
                                                        Set SoLuong = SoLuong + b.SoLuongGiam, 
                                                            SoLuongConLai = SoLuongConLai + b.SoLuongGiam
                                                        From KhoThietBi as a
                                                        Inner Join ChiTietPhieuGhiGiamThietBi as b
                                                        on a.ThietBiId = b.ThietBiId and a.KhoPhongId = b.KhoPhongId
                                                        Where (b.PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId) and (b.ThietBiId = @ThietBiId) and (b.KhoPhongId = @KhoPhongId)";
                                        command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiGiamThietBiId) ? "" : reqVM.PhieuGhiGiamThietBiId.Trim()));
                                        command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.HasValue ? item.ThietBiId.Value : 0));
                                        command.Parameters.Add(new SqlParameter("@KhoPhongId", item.KhoPhongId.HasValue ? item.KhoPhongId.Value : 0));
                                        command.CommandText = sqlString;
                                        command.ExecuteNonQuery();
                                    }

                                    List<ChiTietPhieuGhiGiamThietBi> chiTietPhieuGhiGiamThietBis = new List<ChiTietPhieuGhiGiamThietBi>();
                                    foreach (var chiTietThietBiGiamReqVM in reqVM.ChiTietThietBiGiams)
                                    {
                                        if (string.IsNullOrEmpty(chiTietThietBiGiamReqVM.PhieuGhiMatThietBiId) && string.IsNullOrEmpty(chiTietThietBiGiamReqVM.PhieuGhiHongThietBiId))
                                        {
                                            int soLuongConLai = 0;
                                            {
                                                command.Parameters.Clear();
                                                string sqlString = @"Select SoLuongConLai From KhoThietBi Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                                command.CommandText = sqlString;
                                                command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiGiamReqVM.ThietBiId.HasValue ? chiTietThietBiGiamReqVM.ThietBiId.Value : -1));
                                                command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiGiamReqVM.KhoPhongId.HasValue ? chiTietThietBiGiamReqVM.KhoPhongId.Value : -1));
                                                using (var reader = command.ExecuteReader())
                                                {
                                                    if (reader.Read())
                                                    {
                                                        soLuongConLai = Convert.ToInt32(reader[0]);
                                                    }
                                                }
                                            }
                                            if (soLuongConLai < chiTietThietBiGiamReqVM.SoLuongGiam)
                                            {
                                                throw new Exception("Số lượng giảm vượt quá số lượng trong kho");
                                            }

                                            {
                                                command.Parameters.Clear();
                                                string sqlString = @"Update KhoThietBi
                                                                    Set SoLuong = SoLuong + (Select IsNull((Select SoLuongGiam
                                                                                                            From ChiTietPhieuGhiGiamThietBi
                                                                                                             Where (PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId) 
                                                                                                                and (ThietBiId = @ThietBiId and (KhoPhongId = @KhoPhongId)
                                                                                                                and (PhieuGhiHongThietBiId is null) and (PhieuGhiMatThietBiId is null))), 0)) - @SoLuongGiam, 
                                                                        SoLuongConLai = SoLuongConLai + (Select IsNull((Select SoLuongGiam
                                                                                                            From ChiTietPhieuGhiGiamThietBi 
                                                                                                             Where (PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId) 
                                                                                                                and (ThietBiId = @ThietBiId and (KhoPhongId = @KhoPhongId)
                                                                                                                and (PhieuGhiHongThietBiId is null) and (PhieuGhiMatThietBiId is null))), 0)) - @SoLuongGiam
                                                                    Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                                command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiGiamThietBiId) ? "" : reqVM.PhieuGhiGiamThietBiId.Trim()));
                                                command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietThietBiGiamReqVM.ThietBiId.HasValue ? chiTietThietBiGiamReqVM.ThietBiId.Value : 0));
                                                command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietThietBiGiamReqVM.KhoPhongId.HasValue ? chiTietThietBiGiamReqVM.KhoPhongId.Value : 0));
                                                command.Parameters.Add(new SqlParameter("@SoLuongGiam", chiTietThietBiGiamReqVM.SoLuongGiam.HasValue ? chiTietThietBiGiamReqVM.SoLuongGiam.Value : 0));
                                                command.CommandText = sqlString;
                                                command.ExecuteNonQuery();
                                            }

                                        }

                                        ChiTietPhieuGhiGiamThietBi chiTietPhieuGhiGiamThietBi = new ChiTietPhieuGhiGiamThietBi();
                                        chiTietPhieuGhiGiamThietBi.ChiTietPhieuGhiGiamThietBiId = Guid.NewGuid().ToString();
                                        chiTietPhieuGhiGiamThietBi.PhieuGhiGiamThietBiId = reqVM.PhieuGhiGiamThietBiId;
                                        chiTietPhieuGhiGiamThietBi.SoLuongGiam = chiTietThietBiGiamReqVM.SoLuongGiam;
                                        chiTietPhieuGhiGiamThietBi.LyDo = chiTietThietBiGiamReqVM.LyDo;
                                        chiTietPhieuGhiGiamThietBi.ThietBiId = chiTietThietBiGiamReqVM.ThietBiId;
                                        chiTietPhieuGhiGiamThietBi.KhoPhongId = chiTietThietBiGiamReqVM.KhoPhongId;
                                        chiTietPhieuGhiGiamThietBi.PhieuGhiMatThietBiId = chiTietThietBiGiamReqVM.PhieuGhiMatThietBiId;
                                        chiTietPhieuGhiGiamThietBi.PhieuGhiHongThietBiId = chiTietThietBiGiamReqVM.PhieuGhiHongThietBiId;
                                        chiTietPhieuGhiGiamThietBis.Add(chiTietPhieuGhiGiamThietBi);
                                    }

                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Delete ChiTietPhieuGhiGiamThietBi Where PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId";
                                        command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiGiamThietBiId) ? "" : reqVM.PhieuGhiGiamThietBiId.Trim()));
                                        command.CommandText = sqlString;
                                        command.ExecuteNonQuery();
                                    }

                                    // xoa tai lieu dinh kem cu
                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Delete FileTrongPhieuGhiGiam Where PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId;";
                                        command.CommandText = sqlString;
                                        command.CommandType = CommandType.Text;
                                        command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", reqVM.PhieuGhiGiamThietBiId));
                                        command.ExecuteNonQuery();
                                    }

                                    // tai lieu dinh kem
                                    if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                    {
                                        List<FileTrongPhieuGhiGiam> fileTrongPhieuGhiGiams = new List<FileTrongPhieuGhiGiam>();

                                        foreach (var item in reqVM.TaiLieuDinhKems)
                                        {
                                            FileTrongPhieuGhiGiam fileTrongPhieuGhiGiam = new FileTrongPhieuGhiGiam();
                                            fileTrongPhieuGhiGiam.FileTrongPhieuGhiGiamId = item.FileId;
                                            fileTrongPhieuGhiGiam.Ext = item.Ext;
                                            fileTrongPhieuGhiGiam.Icon = item.Icon;
                                            fileTrongPhieuGhiGiam.TenFile = item.TenFile;
                                            fileTrongPhieuGhiGiam.Url = item.Url;
                                            fileTrongPhieuGhiGiam.PhieuGhiGiamThietBiId = reqVM.PhieuGhiGiamThietBiId;
                                            fileTrongPhieuGhiGiams.Add(fileTrongPhieuGhiGiam);
                                        }
                                        DbContext.FileTrongPhieuGhiGiams.AddRange(fileTrongPhieuGhiGiams);
                                        DbContext.SaveChanges();
                                    }

                                    DbContext.ChiTietPhieuGhiGiamThietBis.AddRange(chiTietPhieuGhiGiamThietBis);
                                    DbContext.SaveChanges();

                                    dbContextTransaction.Commit();
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                throw new WarningException(ex.Message);
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePhieuGhiGiamThietBi(DeletePhieuGhiGiamThietBiReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            int totalRow = 0;
                            // check can update
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Select Count(*) From ChiTietPhieuGhiGiamThietBi as a
                                                        Inner Join ChiTietPhieuGhiHongThietBi as b
                                                        on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                                        Inner Join ChiTietPhieuSuaChua as c
                                                        on b.PhieuGhiHongThietBiId = c.PhieuGhiHongThietBiId
                                                        Where a.ChiTietPhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId";
                                command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiGiamThietBiId) ? "" : reqVM.PhieuGhiGiamThietBiId.Trim()));
                                command.CommandText = sqlString;
                                using (var reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        totalRow = Convert.ToInt32(reader[0]);
                                    }
                                }
                            }

                            if (totalRow > 0)
                            {
                                throw new WarningException("Không thể xóa phiếu đã có thiết bị hỏng được sửa chữa");
                            }

                            {
                                string sqlString = @"Update KhoThietBi
                                            Set SoLuong = SoLuong + b.SoLuongGiam, 
                                                SoLuongConLai = SoLuongConLai + b.SoLuongGiam
                                            From KhoThietBi as a
                                            Inner Join ChiTietPhieuGhiGiamThietBi as b
                                            on a.ThietBiId = b.ThietBiId and a.KhoPhongId = b.KhoPhongId
                                            Where (b.PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId) and (b.PhieuGhiMatThietBiId is null) and (b.PhieuGhiHongThietBiId is null);
                                            Delete ChiTietPhieuGhiGiamThietBi Where PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId;   
                                            Delete PhieuGhiGiamThietBi Where PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId;
                                            Delete FileTrongPhieuGhiGiam Where PhieuGhiGiamThietBiId = @PhieuGhiGiamThietBiId;";
                                command.CommandText = sqlString;
                                command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                command.CommandType = CommandType.Text;
                                command.Parameters.Add(new SqlParameter("@PhieuGhiGiamThietBiId", string.IsNullOrEmpty(reqVM.PhieuGhiGiamThietBiId) ? "" : reqVM.PhieuGhiGiamThietBiId.Trim()));
                                command.Parameters.Add(new SqlParameter("@NgayCapNhat", DateTime.Now));
                                command.ExecuteNonQuery();
                            }
                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw new WarningException(ex.Message);
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region kiem ke
        public List<ChiTietThietBiResVM> GetThietBiDeKiemKes(string tenThietBi, int? khoPhongId, int? monHocId, string phieuKiemKeId)
        {
            try
            {
                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        DateTime? ngayKiemKe = null;
                        // get ngay kiem ke gan nhat
                        {
                            string sqlString = @"Select Max(NgayKiemKe) From PhieuKiemKe Where ((@PhieuKiemKeId is null) or (PhieuKiemKeId != @PhieuKiemKeId))";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", string.IsNullOrEmpty(phieuKiemKeId) ? (object)DBNull.Value : phieuKiemKeId.Trim()));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    ngayKiemKe = reader.IsDBNull(0) ? (DateTime?)null : Convert.ToDateTime(reader[0]);
                                }
                            }
                        }

                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select a.KhoPhongId, b.*, a.SoLuong, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong,
			                                            Case
				                                            When g.SoLuongHong is null Then 0
				                                            Else g.SoLuongHong
			                                            End as 'SoLuongHong',
			                                            Case
				                                            When h.SoLuongMat is null Then 0
				                                            Else h.SoLuongMat
			                                            End as 'SoLuongMat'
                                            From KhoThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Left Join (Select a.ThietBiid, a.KhoPhongId, Sum(a.SoLuongHong) as 'SoLuongHong' From ChiTietPhieuGhiHongThietBi as a
                                            Inner Join PhieuGhiHongThietBi as b
                                            on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                            Where (a.KhoPhongId = @KhoPhongId) and (b.NgayLap > @NgayKiemKe)
                                            Group by a.ThietBiId, a.KhoPhongId) as g
                                            on a.KhoPhongId = g.KhoPhongId and a.ThietBiId = g.ThietBiId											
											Left Join (Select a.ThietBiid, a.KhoPhongId, Sum(a.SoLuongMat) as 'SoLuongMat' From ChiTietPhieuGhiMatThietBi as a
                                            Inner Join PhieuGhiMatThietBi as b
                                            on a.PhieuGhiMatThietBiId = b.PhieuGhiMatThietBiId
                                            Where (a.KhoPhongId = @KhoPhongId) and (b.NgayLap > @NgayKiemKe)
                                            Group by a.ThietBiId, a.KhoPhongId) as h
                                            on a.KhoPhongId = h.KhoPhongId and a.ThietBiId = h.ThietBiId
                                            Where ((@TenThietBi is null) or (b.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (b.MonHocId = @MonHocId))
                                                and ((@KhoPhongId is null) or (a.KhoPhongId = @KhoPhongId))
                                            Order by b.TenThietBi";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                            command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@NgayKiemKe", ngayKiemKe.HasValue ? ngayKiemKe.Value : (object)""));
                            using (var reader = command.ExecuteReader())
                            {
                                chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChiTietThietBiResVM GetThietBiDeKiemKe(int? thietBiId, int? khoPhongId, string phieuKiemKeId)
        {
            try
            {
                ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        DateTime? ngayKiemKe = null;
                        // get ngay kiem ke gan nhat
                        {
                            string sqlString = @"Select Max(NgayKiemKe) From PhieuKiemKe Where ((@PhieuKiemKeId is null) or (PhieuKiemKeId != @PhieuKiemKeId))";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", string.IsNullOrEmpty(phieuKiemKeId) ? (object)DBNull.Value : phieuKiemKeId.Trim()));
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    ngayKiemKe = reader.IsDBNull(0) ? (DateTime?)null : Convert.ToDateTime(reader[0]);
                                }
                            }
                        }

                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select a.KhoPhongId, b.*, a.SoLuong, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong,
			                                            Case
				                                            When g.SoLuongHong is null Then 0
				                                            Else g.SoLuongHong
			                                            End as 'SoLuongHong',
			                                            Case
				                                            When h.SoLuongMat is null Then 0
				                                            Else h.SoLuongMat
			                                            End as 'SoLuongMat'
                                            From KhoThietBi as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Left Join (Select a.ThietBiid, a.KhoPhongId, Sum(a.SoLuongHong) as 'SoLuongHong' From ChiTietPhieuGhiHongThietBi as a
                                            Inner Join PhieuGhiHongThietBi as b
                                            on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId
                                            Where (a.KhoPhongId = @KhoPhongId) and (b.NgayLap > @NgayKiemKe)
                                            Group by a.ThietBiId, a.KhoPhongId) as g
                                            on a.KhoPhongId = g.KhoPhongId and a.ThietBiId = g.ThietBiId											
											Left Join (Select a.ThietBiid, a.KhoPhongId, Sum(a.SoLuongMat) as 'SoLuongMat' From ChiTietPhieuGhiMatThietBi as a
                                            Inner Join PhieuGhiMatThietBi as b
                                            on a.PhieuGhiMatThietBiId = b.PhieuGhiMatThietBiId
                                            Where (a.KhoPhongId = @KhoPhongId) and (b.NgayLap > @NgayKiemKe)
                                            Group by a.ThietBiId, a.KhoPhongId) as h
                                            on a.KhoPhongId = h.KhoPhongId and a.ThietBiId = h.ThietBiId
                                            Where ((@KhoPhongId is null) or (a.KhoPhongId = @KhoPhongId))
                                                    and ((@ThietBiId is null) or (a.ThietBiId = @ThietBiId))";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@ThietBiId", thietBiId.HasValue ? thietBiId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@NgayKiemKe", ngayKiemKe.HasValue ? ngayKiemKe.Value : (object)""));
                            using (var reader = command.ExecuteReader())
                            {
                                chiTietThietBiResVM = MapDataHelper<ChiTietThietBiResVM>.Map(reader);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ListWithPaginationResVM GetPhieuKiemKes(string soPhieu, int? khoPhongId, DateTime? ngayLapTuNgay, 
                                                    DateTime? ngayLapDenNgay, DateTime? ngayKiemKeTuNgay, DateTime? ngayKiemKeDenNgay, int? currentPage, int? pageSize)
        {
            try
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("SoPhieu", string.IsNullOrEmpty(soPhieu) ? null : soPhieu.Trim());
                queryParamsDict.Add("KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value.ToString() : null);
                queryParamsDict.Add("NgayLapTuNgay", ngayLapTuNgay.HasValue ? ngayLapTuNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("NgayLapDenNgay", ngayLapDenNgay.HasValue ? ngayLapDenNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("NgayKiemKeTuNgay", ngayKiemKeTuNgay.HasValue ? ngayKiemKeTuNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("NgayKiemKeDenNgay", ngayKiemKeDenNgay.HasValue ? ngayKiemKeDenNgay.Value.ToString("yyyy-MM-dd") : null);
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.HoTen From PhieuKiemKe as a
                                            Left Join NguoiDung as b
                                            on a.NguoiCapNhat = b.NguoiDungId
                                            Where ((@SoPhieu is null) or (a.SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@NgayLapTuNgay is null) or (a.NgayLap >= @NgayLapTuNgay)) 
                                                and ((@NgayLapDenNgay is null) or (a.NgayLap <= @NgayLapDenNgay))
                                                and ((@NgayKiemKeTuNgay is null) or (a.NgayKiemKe >= @NgayKiemKeTuNgay))
                                                and ((@NgayKiemKeDenNgay is null) or (a.NgayKiemKe <= @NgayKiemKeDenNgay))
                                                and ((@KhoPhongId is null) or (a.KhoPhongId = @KhoPhongId))
                                            Order by a.NgayLap Desc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@NgayLapTuNgay", ngayLapTuNgay.HasValue ? ngayLapTuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@NgayLapDenNgay", ngayLapDenNgay.HasValue ? ngayLapDenNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@NgayKiemKeTuNgay", ngayKiemKeTuNgay.HasValue ? ngayKiemKeTuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@NgayKiemKeDenNgay", ngayKiemKeDenNgay.HasValue ? ngayKiemKeDenNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<PhieuKiemKeResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From PhieuKiemKe
                                             Where ((@SoPhieu is null) or (SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@NgayLapTuNgay is null) or (NgayLap >= @NgayLapTuNgay)) 
                                                and ((@NgayLapDenNgay is null) or (NgayLap <= @NgayLapDenNgay))
                                                and ((@NgayKiemKeTuNgay is null) or (NgayKiemKe >= @NgayKiemKeTuNgay))
                                                and ((@NgayKiemKeDenNgay is null) or (NgayKiemKe <= @NgayKiemKeDenNgay))
                                                and ((@KhoPhongId is null) or (KhoPhongId = @KhoPhongId))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@NgayLapTuNgay", ngayLapTuNgay.HasValue ? ngayLapTuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@NgayLapDenNgay", ngayLapDenNgay.HasValue ? ngayLapDenNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@NgayKiemKeTuNgay", ngayKiemKeTuNgay.HasValue ? ngayKiemKeTuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@NgayKiemKeDenNgay", ngayKiemKeDenNgay.HasValue ? ngayKiemKeDenNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt64(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var phieuKiemKeResVM in (List<PhieuKiemKeResVM>)listWithPaginationResVM.Objects)
                {
                    phieuKiemKeResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                listWithPaginationResVM.CurrentQueryParamsDict = queryParamsDict;
                listWithPaginationResVM.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);

                return listWithPaginationResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddPhieuKiemKe(AddPhieuKiemKeReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();
                        try
                        {
                            PhieuKiemKe phieuKiemKe = new PhieuKiemKe();
                            phieuKiemKe.PhieuKiemKeId = Guid.NewGuid().ToString();
                            phieuKiemKe.NgayLap = reqVM.NgayLap;
                            phieuKiemKe.NgayKiemKe = reqVM.NgayKiemKe;
                            phieuKiemKe.KhoPhongId = reqVM.KhoPhongId;
                            phieuKiemKe.SoPhieu = reqVM.SoPhieu;
                            phieuKiemKe.GhiChu = reqVM.GhiChu;
                            phieuKiemKe.NguoiCapNhat = reqVM.NguoiCapNhat;

                            DbContext.PhieuKiemKes.Add(phieuKiemKe);
                            DbContext.SaveChanges();

                            int countKiemKe = 0;
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Select Count(*) From PhieuKiemKe 
                                                    Where (NgayKiemKe > @NgayKiemKe) and (KhoPhongId = @KhoPhongId)";
                                command.CommandText = sqlString;
                                command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                command.CommandType = CommandType.Text;
                                command.Parameters.Add(new SqlParameter("@KhoPhongId", phieuKiemKe.KhoPhongId.Value));
                                command.Parameters.Add(new SqlParameter("@NgayKiemKe", reqVM.NgayKiemKe.Value));
                                using (var reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        countKiemKe = Convert.ToInt32(reader[0]);
                                    }
                                }
                            }

                            if (reqVM.ChiTietKiemKes != null && reqVM.ChiTietKiemKes.Count() > 0)
                            {
                                List<ChiTietPhieuKiemKe> chiTietPhieuKiemKes = new List<ChiTietPhieuKiemKe>();
                                foreach (var item in reqVM.ChiTietKiemKes)  
                                {
                                    int soLuong = 0;
                                    int soLuongConLai = 0;
                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Select IsNull(SoLuong, 0) as 'SoLuong', IsNull(SoLuongConLai, 0) as 'SoLuongConLai' From KhoThietBi
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                        command.CommandText = sqlString;
                                        command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                        command.CommandType = CommandType.Text;
                                        command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.Value));
                                        command.Parameters.Add(new SqlParameter("@KhoPhongId", phieuKiemKe.KhoPhongId.Value));
                                        using (var reader = command.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                soLuong = Convert.ToInt32(reader["SoLuong"]);
                                                soLuongConLai = Convert.ToInt32(reader["SoLuongConLai"]);
                                            }
                                        }
                                    }

                                    if ((soLuong - soLuongConLai) > item.SoLuongConDungDuoc)
                                    {
                                        dbContextTransaction.Rollback();
                                        throw new Exception("Số lượng còn dùng được không thể bé hơn số lượng đang mượn");
                                    }          

                                    ChiTietPhieuKiemKe chiTietPhieuKiemKe = new ChiTietPhieuKiemKe();
                                    chiTietPhieuKiemKe.ChiTietPhieuKiemKeId = Guid.NewGuid().ToString();
                                    chiTietPhieuKiemKe.PhieuKiemKeId = phieuKiemKe.PhieuKiemKeId;
                                    chiTietPhieuKiemKe.SoLuongConDungDuoc = item.SoLuongConDungDuoc;
                                    chiTietPhieuKiemKe.SoLuongHong = item.SoLuongHong;
                                    chiTietPhieuKiemKe.SoLuongMat = item.SoLuongMat;
                                    chiTietPhieuKiemKe.SoLuongConDungDuoc = soLuong;
                                    chiTietPhieuKiemKe.GhiChu = item.GhiChu;
                                    chiTietPhieuKiemKe.ThietBiId = item.ThietBiId;
                                    chiTietPhieuKiemKe.KhoPhongId = phieuKiemKe.KhoPhongId;

                                    chiTietPhieuKiemKes.Add(chiTietPhieuKiemKe);

                                    if (countKiemKe == 0)
                                    {
                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Update KhoThietBi  
                                                        Set SoLuong = @SoLuongConDungDuoc , SoLuongConLai =  @SoLuongConDungDuoc - @SoLuongMuon
                                                        Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                            command.CommandType = CommandType.Text;
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.Value));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", phieuKiemKe.KhoPhongId.Value));
                                            command.Parameters.Add(new SqlParameter("@SoLuongConDungDuoc", item.SoLuongConDungDuoc.HasValue ? item.SoLuongConDungDuoc.Value : 0));
                                            command.Parameters.Add(new SqlParameter("@SoLuongMuon", soLuong - soLuongConLai));
                                            command.ExecuteNonQuery();
                                        }
                                    }
                                    
                                }
                                DbContext.ChiTietPhieuKiemKes.AddRange(chiTietPhieuKiemKes);
                                DbContext.SaveChanges();

                            }

                            // tai lieu dinh kem
                            if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                            {
                                List<FileTrongPhieuKiemKe> fileTrongPhieuKiemKes = new List<FileTrongPhieuKiemKe>();

                                foreach (var item in reqVM.TaiLieuDinhKems)
                                {
                                    FileTrongPhieuKiemKe fileTrongPhieuKiemKe = new FileTrongPhieuKiemKe();
                                    fileTrongPhieuKiemKe.FileTrongPhieuKiemKeId = item.FileId;
                                    fileTrongPhieuKiemKe.Ext = item.Ext;
                                    fileTrongPhieuKiemKe.Icon = item.Icon;
                                    fileTrongPhieuKiemKe.TenFile = item.TenFile;
                                    fileTrongPhieuKiemKe.Url = item.Url;
                                    fileTrongPhieuKiemKe.PhieuKiemKeId = phieuKiemKe.PhieuKiemKeId;
                                    fileTrongPhieuKiemKes.Add(fileTrongPhieuKiemKe);
                                }
                                DbContext.FileTrongPhieuKiemKes.AddRange(fileTrongPhieuKiemKes);
                                DbContext.SaveChanges();
                            }
                            dbContextTransaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw new WarningException(ex.Message);
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PhieuKiemKeResVM GetPhieuKiemKe(string phieuKiemKeId)
        {
            try
            {
                PhieuKiemKeResVM phieuKiemKeResVM = new PhieuKiemKeResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From PhieuKiemKe Where PhieuKiemKeId = @PhieuKiemKeId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", string.IsNullOrEmpty(phieuKiemKeId) ? "" : phieuKiemKeId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuKiemKeResVM = MapDataHelper<PhieuKiemKeResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.*, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong
                                            From ChiTietPhieuKiemKe as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join KhoPhong as f
                                            on a.KhoPhongId = f.KhoPhongId
                                            Where a.PhieuKiemKeId = @PhieuKiemKeId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", string.IsNullOrEmpty(phieuKiemKeId) ? "" : phieuKiemKeId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuKiemKeResVM.ChiTietKiemKes = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select FileTrongPhieuKiemKeId as 'FileId', TenFile, Ext, Url, Icon From FileTrongPhieuKiemKe
                                            Where PhieuKiemKeId = @PhieuKiemKeId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", string.IsNullOrEmpty(phieuKiemKeId) ? "" : phieuKiemKeId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuKiemKeResVM.FileTrongPhieuKiemKes = MapDataHelper<FileResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return phieuKiemKeResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePhieuKiemKe(UpdatePhieuKiemKeReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();
                        command.Transaction = dbContextTransaction.UnderlyingTransaction;
                        command.CommandType = CommandType.Text;
                        try
                        {
                            // update thong tin co ban
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Update PhieuKiemKe
                                                    Set SoPhieu = @SoPhieu, GhiChu = @GhiChu, NgayLap = @NgayLap, NgayKiemKe = @NgayKiemKe
                                                    Where PhieuKiemKeId = @PhieuKiemKeId";
                                command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", string.IsNullOrEmpty(reqVM.PhieuKiemKeId) ? "" : reqVM.PhieuKiemKeId.Trim()));
                                command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(reqVM.SoPhieu) ? "" : reqVM.SoPhieu.Trim()));
                                command.Parameters.Add(new SqlParameter("@GhiChu", string.IsNullOrEmpty(reqVM.GhiChu) ? "" : reqVM.GhiChu.Trim()));
                                command.Parameters.Add(new SqlParameter("@NgayLap", reqVM.NgayLap.HasValue ? reqVM.NgayLap.Value : (object)DBNull.Value));
                                command.Parameters.Add(new SqlParameter("@NgayKiemKe", reqVM.NgayKiemKe.HasValue ? reqVM.NgayKiemKe.Value : (object)DBNull.Value));
                                command.CommandText = sqlString;
                                command.ExecuteNonQuery();
                            }

                            int countKiemKe = 0;
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Select Count(*) From PhieuKiemKe 
                                                    Where (NgayKiemKe > @NgayKiemKe) and (KhoPhongId = @KhoPhongId)";
                                command.CommandText = sqlString;
                                command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                command.CommandType = CommandType.Text;
                                command.Parameters.Add(new SqlParameter("@KhoPhongId", reqVM.KhoPhongId.Value));
                                command.Parameters.Add(new SqlParameter("@NgayKiemKe", reqVM.NgayKiemKe.Value));
                                using (var reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        countKiemKe = Convert.ToInt32(reader[0]);
                                    }
                                }
                            }

                            List<int> thietBiIds = new List<int>();
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Select ThietBiId From ChiTietPhieuKiemKe
                                                    Where PhieuKiemKeId = @PhieuKiemKeId";
                                command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", string.IsNullOrEmpty(reqVM.PhieuKiemKeId) ? "" : reqVM.PhieuKiemKeId.Trim()));
                                command.CommandText = sqlString;
                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        thietBiIds.Add(Convert.ToInt32(reader[0]));
                                    }
                                }
                            }

                            // update so luong
                            List<ChiTietPhieuKiemKe> chiTietPhieuKiemKes = new List<ChiTietPhieuKiemKe>();
                            if (reqVM.ChiTietKiemKes != null && reqVM.ChiTietKiemKes.Count() > 0 )
                            {
                                if (countKiemKe == 0)
                                {
                                    foreach (var item in thietBiIds.Where(x => !reqVM.ChiTietKiemKes.Any(k => x == k.ThietBiId.Value)))
                                    {
                                        int soLuong = 0;
                                        int soLuongConLai = 0;
                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Select IsNull(SoLuong, 0) as 'SoLuong', IsNull(SoLuongConLai, 0) as 'SoLuongConLai' From KhoThietBi
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", item));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", reqVM.KhoPhongId.Value));
                                            using (var reader = command.ExecuteReader())
                                            {
                                                if (reader.Read())
                                                {
                                                    soLuong = Convert.ToInt32(reader["SoLuong"]);
                                                    soLuongConLai = Convert.ToInt32(reader["SoLuongConLai"]);
                                                }
                                            }
                                        }

                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Update KhoThietBi
                                                            Set SoLuong = b.SoLuongConDungDuocOld, SoLuongConLai = b.SoLuongConDungDuocOld - @SoLuongMuon
                                                            From KhoThietBi as a
                                                            Left Join ChiTietPhieuKiemKe as b
                                                            on a.ThietBiId = b.ThietBiId
                                                            Where (a.ThietBiId = @ThietBiId) and (a.KhoPhongId = @KhoPhongId) and (b.PhieuKiemKeId = @PhieuKiemKeId)";
                                            command.CommandText = sqlString;
                                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", item));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", reqVM.KhoPhongId.Value));
                                            command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", reqVM.PhieuKiemKeId));
                                            command.Parameters.Add(new SqlParameter("@SoLuongMuon", soLuong - soLuongConLai));
                                            command.ExecuteNonQuery();
                                        }
                                    }
                                }
                                
                                foreach (var item in reqVM.ChiTietKiemKes)
                                {
                                    int soLuong = 0;
                                    int soLuongConLai = 0;
                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Select IsNull(SoLuong, 0) as 'SoLuong', IsNull(SoLuongConLai, 0) as 'SoLuongConLai' From KhoThietBi
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                        command.CommandText = sqlString;
                                        command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.Value));
                                        command.Parameters.Add(new SqlParameter("@KhoPhongId", reqVM.KhoPhongId.Value));
                                        using (var reader = command.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                soLuong = Convert.ToInt32(reader["SoLuong"]);
                                                soLuongConLai = Convert.ToInt32(reader["SoLuongConLai"]);
                                            }
                                        }
                                    }

                                    if (countKiemKe == 0)
                                    {
                                        if ((soLuong - soLuongConLai) > item.SoLuongConDungDuoc)
                                        {
                                            dbContextTransaction.Rollback();
                                            throw new Exception("Số lượng còn dùng được không thể bé hơn số lượng đang mượn");
                                        }

                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Update KhoThietBi  
                                                        Set SoLuong = @SoLuongConDungDuoc , SoLuongConLai =  @SoLuongConDungDuoc - @SoLuongMuon
                                                        Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.CommandType = CommandType.Text;
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.Value));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", reqVM.KhoPhongId.Value));
                                            command.Parameters.Add(new SqlParameter("@SoLuongConDungDuoc", item.SoLuongConDungDuoc.HasValue ? item.SoLuongConDungDuoc.Value : 0));
                                            command.Parameters.Add(new SqlParameter("@SoLuongMuon", soLuong - soLuongConLai));
                                            command.ExecuteNonQuery();
                                        }
                                    }

                                    ChiTietPhieuKiemKe chiTietPhieuKiemKe = new ChiTietPhieuKiemKe();
                                    chiTietPhieuKiemKe.ChiTietPhieuKiemKeId = Guid.NewGuid().ToString();
                                    chiTietPhieuKiemKe.PhieuKiemKeId = reqVM.PhieuKiemKeId;
                                    chiTietPhieuKiemKe.SoLuongConDungDuoc = item.SoLuongConDungDuoc;
                                    chiTietPhieuKiemKe.SoLuongHong = item.SoLuongHong;
                                    chiTietPhieuKiemKe.SoLuongMat = item.SoLuongMat;
                                    chiTietPhieuKiemKe.SoLuongConDungDuocOld = soLuong;
                                    chiTietPhieuKiemKe.GhiChu = item.GhiChu;
                                    chiTietPhieuKiemKe.ThietBiId = item.ThietBiId;
                                    chiTietPhieuKiemKe.KhoPhongId = reqVM.KhoPhongId;

                                    chiTietPhieuKiemKes.Add(chiTietPhieuKiemKe);
                                }
                            }

                            {
                                command.Parameters.Clear();
                                string sqlString = @"Delete ChiTietPhieuKiemKe
                                                        Where PhieuKiemKeId = @PhieuKiemKeId";
                                command.CommandText = sqlString;
                                command.Transaction = dbContextTransaction.UnderlyingTransaction;
                                command.CommandType = CommandType.Text;
                                command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", reqVM.PhieuKiemKeId));
                                command.ExecuteNonQuery();
                            }

                            // xoa tai lieu dinh kem cu
                            {
                                command.Parameters.Clear();
                                string sqlString = @"Delete FileTrongPhieuKiemKe Where PhieuKiemKeId = @PhieuKiemKeId;";
                                command.CommandText = sqlString;
                                command.CommandType = CommandType.Text;
                                command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", reqVM.PhieuKiemKeId));
                                command.ExecuteNonQuery();
                            }

                            // tai lieu dinh kem
                            if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                            {
                                List<FileTrongPhieuKiemKe> fileTrongPhieuKiemKes = new List<FileTrongPhieuKiemKe>();

                                foreach (var item in reqVM.TaiLieuDinhKems)
                                {
                                    FileTrongPhieuKiemKe fileTrongPhieuKiemKe = new FileTrongPhieuKiemKe();
                                    fileTrongPhieuKiemKe.FileTrongPhieuKiemKeId = item.FileId;
                                    fileTrongPhieuKiemKe.Ext = item.Ext;
                                    fileTrongPhieuKiemKe.Icon = item.Icon;
                                    fileTrongPhieuKiemKe.TenFile = item.TenFile;
                                    fileTrongPhieuKiemKe.Url = item.Url;
                                    fileTrongPhieuKiemKe.PhieuKiemKeId = reqVM.PhieuKiemKeId;
                                    fileTrongPhieuKiemKes.Add(fileTrongPhieuKiemKe);
                                }
                                DbContext.FileTrongPhieuKiemKes.AddRange(fileTrongPhieuKiemKes);
                                DbContext.SaveChanges();
                            }

                            DbContext.ChiTietPhieuKiemKes.AddRange(chiTietPhieuKiemKes);
                            DbContext.SaveChanges();

                            dbContextTransaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw new WarningException(ex.Message);
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePhieuKiemKe(DeletePhieuKiemKeReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();
                        command.Transaction = dbContextTransaction.UnderlyingTransaction;
                        command.CommandType = CommandType.Text;
                        try
                        {
                            List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                            {
                                string sqlString = @"Select a.*, b.KhoPhongId From ChiTietPhieuKiemKe as a
                                                Left Join PhieuKiemKe as b
                                                on a.PhieuKiemKeId = b.PhieuKiemKeId
                                                Where a.PhieuKiemKeId = @PhieuKiemKeId";
                                command.CommandText = sqlString;
                                command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", string.IsNullOrEmpty(reqVM.PhieuKiemKeId) ? "" : reqVM.PhieuKiemKeId.Trim()));
                                using (var reader = command.ExecuteReader())
                                {
                                    chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                                }
                            }

                            foreach (var item in chiTietThietBiResVMs)
                            {
                                int soLuong = 0;
                                int soLuongConLai = 0;
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Select IsNull(SoLuong, 0) as 'SoLuong', IsNull(SoLuongConLai, 0) as 'SoLuongConLai' From KhoThietBi
                                                            Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                    command.CommandText = sqlString;
                                    command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.Value));
                                    command.Parameters.Add(new SqlParameter("@KhoPhongId", item.KhoPhongId.Value));
                                    using (var reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            soLuong = Convert.ToInt32(reader["SoLuong"]);
                                            soLuongConLai = Convert.ToInt32(reader["SoLuongConLai"]);
                                        }
                                    }
                                }

                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update KhoThietBi
                                                            Set SoLuong = b.SoLuongConDungDuocOld, SoLuongConLai = b.SoLuongConDungDuocOld - @SoLuongMuon
                                                            From KhoThietBi as a
                                                            Left Join ChiTietPhieuKiemKe as b
                                                            on a.ThietBiId = b.ThietBiId
                                                            Where (a.ThietBiId = @ThietBiId) and (a.KhoPhongId = @KhoPhongId) and (b.PhieuKiemKeId = @PhieuKiemKeId)";
                                    command.CommandText = sqlString;
                                    command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.Value));
                                    command.Parameters.Add(new SqlParameter("@KhoPhongId", item.KhoPhongId.Value));
                                    command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", reqVM.PhieuKiemKeId));
                                    command.Parameters.Add(new SqlParameter("@SoLuongMuon", soLuong - soLuongConLai));
                                    command.ExecuteNonQuery();
                                }
                            }

                            {
                                command.Parameters.Clear();
                                string sqlString = @"Delete ChiTietPhieuKiemKe Where PhieuKiemKeId = @PhieuKiemKeId;
                                                    Delete PhieuKiemKe Where PhieuKiemKeId = @PhieuKiemKeId;
                                                    Delete FileTrongPhieuKiemKe Where PhieuKiemKeId = @PhieuKiemKeId;";
                                command.CommandText = sqlString;
                                command.Parameters.Add(new SqlParameter("@PhieuKiemKeId", reqVM.PhieuKiemKeId));
                                command.ExecuteNonQuery();
                            }
                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            throw new WarningException(ex.Message);
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region phieu sua chua
        public ListWithPaginationResVM GetPhieuSuaChuas(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? currentPage, int? pageSize)
        {
            try
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("SoPhieu", soPhieu);
                queryParamsDict.Add("TuNgay", tuNgay.HasValue ? tuNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("DenNgay", denNgay.HasValue ? denNgay.Value.ToString("yyyy-MM-dd") : null);
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.HoTen From PhieuSuaChua as a
                                            Left Join NguoiDung as b
                                            on a.NguoiCapNhat = b.NguoiDungId
                                            Where ((@SoPhieu is null) or (a.SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (a.NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (a.NgayLap <= @DenNgay))
                                            Order by a.NgayLap Desc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<PhieuSuaChuaResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From PhieuSuaChua
                                            Where ((@SoPhieu is null) or (SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (NgayLap <= @DenNgay))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt64(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var phieuGhiHongMatThietBiResVM in (List<PhieuSuaChuaResVM>)listWithPaginationResVM.Objects)
                {
                    phieuGhiHongMatThietBiResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                listWithPaginationResVM.CurrentQueryParamsDict = queryParamsDict;
                listWithPaginationResVM.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);

                return listWithPaginationResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChiTietThietBiResVM> GetThietBiDeSuaChuas(string tenThietBi, int? monHocId)
        {
            try
            {
                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        command.Parameters.Clear();
                        string sqlString = @"Select a.KhoPhongId, a.PhieuGhiHongThietBiId, c.*, d.TenMonHoc, e.TenLoaiThietBi, f.TenDonViTinh,g.TenKhoPhong, a.SoLuongHong - ISNULL(b.SoLuongSuaChua, 0) as 'SoLuongHongConLai',
                                                    h.SoPhieu as 'SoPhieuGhiHongThietBi'
                                            From ChiTietPhieuGhiHongThietBi as a
                                            Left Join ChiTietPhieuSuaChua as b
                                            on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId 
                                            Left Join ThietBi as c
                                            on a.ThietBiId = c.ThietBiId
                                            Left Join MonHoc as d
                                            on c.MonHocId = d.MonHocId
                                            Left Join LoaiThietBi as e
                                            on c.LoaiThietBiId = e.LoaiThietBiId
                                            Left Join DonViTinh as f
                                            on c.DonViTinhId = f.DonViTinhId
                                            Left Join KhoPhong as g
                                            on a.KhoPhongId = g.KhoPhongId
                                            Left Join PhieuGhiHongThietBi as h
                                            on a.PhieuGhiHongThietBiId = h.PhieuGhiHongThietBiId
                                            Where (a.SoLuongHong > ISNULL(b.SoLuongSuaChua, 0))
                                                and ((@TenThietBi is null) or (c.TenThietBi Like N'%' +@TenThietBi+ '%')) 
                                                and ((@MonHocId is null) or (c.MonHocId = @MonHocId))
                                            Order by c.TenThietBi";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChiTietThietBiResVM GetThietBiDeSuaChua(int? thietBiId, int? khoPhongId, string phieuGhiHongThietBiId)
        {
            try
            {
                ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        command.Parameters.Clear();
                        string sqlString = @"Select a.KhoPhongId, a.PhieuGhiHongThietBiId, c.*, d.TenMonHoc, e.TenLoaiThietBi, f.TenDonViTinh,g.TenKhoPhong, a.SoLuongHong - ISNULL(b.SoLuongSuaChua, 0) as 'SoLuongHongConLai',
                                                    h.SoPhieu as 'SoPhieuGhiHongThietBi'
                                            From ChiTietPhieuGhiHongThietBi as a
                                            Left Join ChiTietPhieuSuaChua as b
                                            on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId 
                                            Left Join ThietBi as c
                                            on a.ThietBiId = c.ThietBiId
                                            Left Join MonHoc as d
                                            on c.MonHocId = d.MonHocId
                                            Left Join LoaiThietBi as e
                                            on c.LoaiThietBiId = e.LoaiThietBiId
                                            Left Join DonViTinh as f
                                            on c.DonViTinhId = f.DonViTinhId
                                            Left Join KhoPhong as g
                                            on a.KhoPhongId = g.KhoPhongId
                                            Left Join PhieuGhiHongThietBi as h
                                            on a.PhieuGhiHongThietBiId = h.PhieuGhiHongThietBiId
                                            Where (a.ThietBiId = @ThietBiId) and (a.KhoPhongId = @KhoPhongId) and (a.PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId)
                                            Order by c.TenThietBi";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@ThietBiId", thietBiId.HasValue ? thietBiId.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@KhoPhongId", khoPhongId.HasValue ? khoPhongId.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("PhieuGhiHongThietBiId", string.IsNullOrEmpty(phieuGhiHongThietBiId) ? "" : phieuGhiHongThietBiId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVM = MapDataHelper<ChiTietThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddPhieuSuaChua(AddPhieuSuaChuaReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                PhieuSuaChua phieuSuaChua = new PhieuSuaChua();
                                phieuSuaChua.PhieuSuaChuaId = Guid.NewGuid().ToString();
                                phieuSuaChua.GhiChu = string.IsNullOrEmpty(reqVM.GhiChu) ? "" : reqVM.GhiChu.Trim();
                                phieuSuaChua.NgayLap = reqVM.NgayLap;
                                phieuSuaChua.SoPhieu = reqVM.SoPhieu;
                                phieuSuaChua.NguoiCapNhat = reqVM.NguoiCapNhat;
                                DbContext.PhieuSuaChuas.Add(phieuSuaChua);
                                DbContext.SaveChanges();

                                List<ChiTietPhieuSuaChua> chiTietPhieuSuaChuas = new List<ChiTietPhieuSuaChua>();
                                if (reqVM.ChiTietPhieuSuaChuas != null && reqVM.ChiTietPhieuSuaChuas.Count() > 0)
                                {
                                    foreach (var item in reqVM.ChiTietPhieuSuaChuas)
                                    {
                                        // check so luong
                                        int soLuongHong = 0;
                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Select SoLuongHong From ChiTietPhieuGhiHongThietBi 
                                                                Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId) and (PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId)";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(item.PhieuGhiHongThietBiId) ? "" : item.PhieuGhiHongThietBiId.Trim()));
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.HasValue ? item.ThietBiId.Value : -1));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", item.KhoPhongId.HasValue ? item.KhoPhongId.Value : -1));
                                            using (var reader = command.ExecuteReader())
                                            {
                                                if (reader.Read())
                                                {
                                                    soLuongHong = Convert.ToInt32(reader[0]);
                                                }
                                            }
                                        }

                                        if (soLuongHong < item.SoLuongSuaChua)
                                        {
                                            dbContextTransaction.Rollback();
                                            throw new Exception("Số lượng sửa chữa không vượt quá số lượng hỏng");
                                        }

                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Update KhoThietBi
                                                                Set Soluong = SoLuong + @SoluongSuaChua, SoLuongConLai = SoLuongConLai + @SoLuongSuaChua
                                                                Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@SoLuongSuaChua", item.SoLuongSuaChua.HasValue ? item.SoLuongSuaChua.Value : 0));
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.HasValue ? item.ThietBiId.Value : -1));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", item.KhoPhongId.HasValue ? item.KhoPhongId.Value : -1));
                                            command.ExecuteNonQuery();
                                        }

                                        ChiTietPhieuSuaChua chiTietPhieuSuaChua = new ChiTietPhieuSuaChua();
                                        chiTietPhieuSuaChua.ChiTietPhieuSuaChuaId = Guid.NewGuid().ToString();
                                        chiTietPhieuSuaChua.PhieuSuaChuaId = phieuSuaChua.PhieuSuaChuaId;
                                        chiTietPhieuSuaChua.KhoPhongId = item.KhoPhongId;
                                        chiTietPhieuSuaChua.SoLuongSuaChua = item.SoLuongSuaChua;
                                        chiTietPhieuSuaChua.ThietBiId = item.ThietBiId;
                                        chiTietPhieuSuaChua.PhieuGhiHongThietBiId = item.PhieuGhiHongThietBiId;
                                        chiTietPhieuSuaChua.DonGia = item.DonGia;
                                        chiTietPhieuSuaChuas.Add(chiTietPhieuSuaChua);
                                    }
                                    DbContext.ChiTietPhieuSuaChuas.AddRange(chiTietPhieuSuaChuas);
                                    DbContext.SaveChanges();
                                }

                                // tai lieu dinh kem
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuSuaChua> fileTrongPhieuSuaChuas = new List<FileTrongPhieuSuaChua>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuSuaChua fileTrongPhieuSuaChua = new FileTrongPhieuSuaChua();
                                        fileTrongPhieuSuaChua.FileTrongPhieuSuaChuaId = item.FileId;
                                        fileTrongPhieuSuaChua.Ext = item.Ext;
                                        fileTrongPhieuSuaChua.Icon = item.Icon;
                                        fileTrongPhieuSuaChua.TenFile = item.TenFile;
                                        fileTrongPhieuSuaChua.Url = item.Url;
                                        fileTrongPhieuSuaChua.PhieuSuaChuaId = phieuSuaChua.PhieuSuaChuaId;
                                        fileTrongPhieuSuaChuas.Add(fileTrongPhieuSuaChua);
                                    }
                                    DbContext.FileTrongPhieuSuaChuas.AddRange(fileTrongPhieuSuaChuas);
                                    DbContext.SaveChanges();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PhieuSuaChuaResVM GetPhieuSuaChua(string phieuSuaChuaId)
        {
            try
            {
                PhieuSuaChuaResVM phieuSuaChuaResVM = new PhieuSuaChuaResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        command.Parameters.Clear();
                        string sqlString = @"Select * From PhieuSuaChua Where PhieuSuaChuaId = @PhieuSuaChuaId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(phieuSuaChuaId) ? (object)DBNull.Value : phieuSuaChuaId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuSuaChuaResVM = MapDataHelper<PhieuSuaChuaResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        command.Parameters.Clear();
                        string sqlString = @"Select a.*, g.SoPhieu as 'SoPhieuGhiHongThietBi',
                                                        b.*, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, f.TenKhoPhong, h.SoLuongHongConLai + a.SoLuongSuaChua as 'SoLuongHongConLai'
                                                From ChiTietPhieuSuaChua as a
                                                Left Join ThietBi as b
                                                on a.ThietBiId = b.ThietBiId
                                                Left Join MonHoc as c
                                                on b.MonHocId = c.MonHocId
                                                Left Join LoaiThietBi as d
                                                on b.LoaiThietBiId = d.LoaiThietBiId
                                                Left Join DonViTinh as e
                                                on b.DonViTinhId = e.DonViTinhId
                                                Left Join KhoPhong as f
                                                on a.KhoPhongId = f.KhoPhongId
                                                Left Join PhieuGhiHongThietBi as g
                                                on a.PhieuGhiHongThietBiId = g.PhieuGhiHongThietBiId
                                                left Join (Select (b.SoLuongHong - ISNULL(Sum(SoLuongSuaChua), 0)) as 'SoLuongHongConLai', a.ThietBiId, a.KhoPhongId, a.PhieuGhiHongThietBiId
			                                                From ChiTietPhieuSuaChua as a
			                                                Left Join ChiTietPhieuGhiHongThietBi as b
			                                                on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId and a.KhoPhongId = b.KhoPhongId and a.ThietBiId = b.ThietBiId
			                                                Group by a.PhieuGhiHongThietBiId, a.ThietBiId, a.KhoPhongId, b.SoLuongHong) as h
                                                on a.ThietBiId = h.ThietBiId and a.KhoPhongId = h.KhoPhongId and h.PhieuGhiHongThietBiId = a.PhieuGhiHongThietBiId
                                                Where a.PhieuSuaChuaId = @PhieuSuaChuaId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(phieuSuaChuaId) ? (object)DBNull.Value : phieuSuaChuaId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuSuaChuaResVM.ChiTietThietBis = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select FileTrongPhieuSuaChuaId as 'FileId', TenFile, Ext, Url, Icon From FileTrongPhieuSuaChua
                                            Where PhieuSuaChuaId = @PhieuSuaChuaId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(phieuSuaChuaId) ? "" : phieuSuaChuaId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuSuaChuaResVM.FileTrongPhieuSuaChuas = MapDataHelper<FileResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return phieuSuaChuaResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePhieuSuaChua(UpdatePhieuSuaChuaReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                // update thong tin co ban
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update PhieuSuaChua
                                                    Set SoPhieu = @SoPhieu, GhiChu = @GhiChu, NgayLap = @NgayLap
                                                    Where PhieuSuaChuaId = @PhieuSuaChuaId";
                                    command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(reqVM.PhieuSuaChuaId) ? "" : reqVM.PhieuSuaChuaId.Trim()));
                                    command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(reqVM.SoPhieu) ? "" : reqVM.SoPhieu.Trim()));
                                    command.Parameters.Add(new SqlParameter("@GhiChu", string.IsNullOrEmpty(reqVM.GhiChu) ? "" : reqVM.GhiChu.Trim()));
                                    command.Parameters.Add(new SqlParameter("@NgayLap", reqVM.NgayLap.HasValue ? reqVM.NgayLap.Value : (object)DBNull.Value));
                                    command.CommandText = sqlString;
                                    command.ExecuteNonQuery();
                                }

                                List<ChiTietPhieuSuaChua> chiTietPhieuSuaChuas = new List<ChiTietPhieuSuaChua>();
                                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                                // update so luong
                                if (reqVM.ChiTietPhieuSuaChuas != null && reqVM.ChiTietPhieuSuaChuas.Count() > 0)
                                {

                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Select * From ChiTietPhieuSuaChua Where PhieuSuaChuaId = @PhieuSuaChuaId";
                                        command.CommandText = sqlString;
                                        command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(reqVM.PhieuSuaChuaId) ? "" : reqVM.PhieuSuaChuaId.Trim()));
                                        using (var reader = command.ExecuteReader())
                                        {
                                            chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                                        }
                                    }

                                    foreach (var item in chiTietThietBiResVMs.Where(x => !reqVM.ChiTietPhieuSuaChuas.Any(k => (k.ThietBiId == x.ThietBiId) && (k.KhoPhongId == x.KhoPhongId))).ToList())
                                    {
                                        command.Parameters.Clear();
                                        string sqlString = @"Update KhoThietBi
                                                        Set SoLuong = SoLuong - b.SoLuongSuaChua, 
                                                            SoLuongConLai = SoLuongConLai - b.SoLuongSuaChua
                                                        From KhoThietBi as a
                                                        Inner Join ChiTietPhieuSuaChua as b
                                                        on a.ThietBiId = b.ThietBiId and a.KhoPhongId = b.KhoPhongId
                                                        Where (b.PhieuSuaChuaId = @PhieuSuaChuaId) and (b.ThietBiId = @ThietBiId) and (b.KhoPhongId = @KhoPhongId)";
                                        command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(reqVM.PhieuSuaChuaId) ? "" : reqVM.PhieuSuaChuaId.Trim()));
                                        command.Parameters.Add(new SqlParameter("@ThietBiId", item.ThietBiId.HasValue ? item.ThietBiId.Value : 0));
                                        command.Parameters.Add(new SqlParameter("@KhoPhongId", item.KhoPhongId.HasValue ? item.KhoPhongId.Value : 0));
                                        command.CommandText = sqlString;
                                        command.ExecuteNonQuery();
                                    }


                                    foreach (var chiTietPhieuSuaChuaReqVM in reqVM.ChiTietPhieuSuaChuas)
                                    {
                                        // check so luong
                                        int soLuongConLai = 0;
                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Select (b.SoLuongHong - ISNULL(Sum(a.SoLuongSuaChua), 0) + IsNUll(c.SoLuongSuaChua, 0)) as 'SoLuongHongConLai', a.ThietBiId, a.KhoPhongId, a.PhieuGhiHongThietBiId
                                                                From ChiTietPhieuSuaChua as a
                                                                Left Join ChiTietPhieuGhiHongThietBi as b
                                                                on a.PhieuGhiHongThietBiId = b.PhieuGhiHongThietBiId and a.KhoPhongId = b.KhoPhongId and a.ThietBiId = b.ThietBiId
                                                                Left Join (Select * From ChiTietPhieuSuaChua Where PhieuSuaChuaId = @PhieuSuaChuaId) as c
                                                                on a.ChiTietPhieuSuaChuaId = c.ChiTietPhieuSuaChuaId
                                                                Where (a.ThietBiId = @ThietBiId) and (a.KhoPhongId = @KhoPhongId) and (a.PhieuGhiHongThietBiId = @PhieuGhiHongThietBiId)
                                                                Group by a.PhieuGhiHongThietBiId, a.ThietBiId, a.KhoPhongId, b.SoLuongHong, c.SoLuongSuaChua";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@PhieuGhiHongThietBiId", string.IsNullOrEmpty(chiTietPhieuSuaChuaReqVM.PhieuGhiHongThietBiId) ? "" : chiTietPhieuSuaChuaReqVM.PhieuGhiHongThietBiId.Trim()));
                                            command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(reqVM.PhieuSuaChuaId) ? "" : reqVM.PhieuSuaChuaId.Trim()));
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietPhieuSuaChuaReqVM.ThietBiId.HasValue ? chiTietPhieuSuaChuaReqVM.ThietBiId.Value : -1));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietPhieuSuaChuaReqVM.KhoPhongId.HasValue ? chiTietPhieuSuaChuaReqVM.KhoPhongId.Value : -1));
                                            using (var reader = command.ExecuteReader())
                                            {
                                                if (reader.Read())
                                                {
                                                    soLuongConLai = Convert.ToInt32(reader[0]);
                                                }
                                            }
                                        }

                                        if (soLuongConLai < chiTietPhieuSuaChuaReqVM.SoLuongSuaChua)
                                        {
                                            dbContextTransaction.Rollback();
                                            throw new Exception("Số lượng sửa chữa không vượt quá số lượng hỏng");
                                        }

                                        {
                                            command.Parameters.Clear();
                                            string sqlString = @"Update KhoThietBi
                                                                Set Soluong = SoLuong + @SoluongSuaChua - IsNull((Select SoLuongSuaChua From ChiTietPhieuSuaChua 
                                                                                                                    Where (PhieuSuaChuaId = @PhieuSuaChuaId) and (ThietBiId = @ThietBiId) 
                                                                                                                            and (KhoPhongId = @KhoPhongId)), 0), 
                                                                    SoLuongConLai = SoLuongConLai + @SoLuongSuaChua - IsNull((Select SoLuongSuaChua From ChiTietPhieuSuaChua 
                                                                                                                    Where (PhieuSuaChuaId = @PhieuSuaChuaId) and (ThietBiId = @ThietBiId) 
                                                                                                                            and (KhoPhongId = @KhoPhongId)), 0)
                                                                Where (ThietBiId = @ThietBiId) and (KhoPhongId = @KhoPhongId)";
                                            command.CommandText = sqlString;
                                            command.Parameters.Add(new SqlParameter("@SoLuongSuaChua", chiTietPhieuSuaChuaReqVM.SoLuongSuaChua.HasValue ? chiTietPhieuSuaChuaReqVM.SoLuongSuaChua.Value : 0));
                                            command.Parameters.Add(new SqlParameter("@ThietBiId", chiTietPhieuSuaChuaReqVM.ThietBiId.HasValue ? chiTietPhieuSuaChuaReqVM.ThietBiId.Value : -1));
                                            command.Parameters.Add(new SqlParameter("@KhoPhongId", chiTietPhieuSuaChuaReqVM.KhoPhongId.HasValue ? chiTietPhieuSuaChuaReqVM.KhoPhongId.Value : -1));
                                            command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(reqVM.PhieuSuaChuaId) ? "" : reqVM.PhieuSuaChuaId));
                                            command.ExecuteNonQuery();
                                        }

                                        ChiTietPhieuSuaChua chiTietPhieuSuaChua = new ChiTietPhieuSuaChua();
                                        chiTietPhieuSuaChua.ChiTietPhieuSuaChuaId = Guid.NewGuid().ToString();
                                        chiTietPhieuSuaChua.PhieuSuaChuaId = reqVM.PhieuSuaChuaId;
                                        chiTietPhieuSuaChua.KhoPhongId = chiTietPhieuSuaChuaReqVM.KhoPhongId;
                                        chiTietPhieuSuaChua.SoLuongSuaChua = chiTietPhieuSuaChuaReqVM.SoLuongSuaChua;
                                        chiTietPhieuSuaChua.ThietBiId = chiTietPhieuSuaChuaReqVM.ThietBiId;
                                        chiTietPhieuSuaChua.PhieuGhiHongThietBiId = chiTietPhieuSuaChuaReqVM.PhieuGhiHongThietBiId;
                                        chiTietPhieuSuaChua.DonGia = chiTietPhieuSuaChuaReqVM.DonGia;
                                        chiTietPhieuSuaChuas.Add(chiTietPhieuSuaChua);
                                    }

                                }

                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete ChiTietPhieuSuaChua Where PhieuSuaChuaId = @PhieuSuaChuaId";
                                    command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(reqVM.PhieuSuaChuaId) ? "" : reqVM.PhieuSuaChuaId.Trim()));
                                    command.CommandText = sqlString;
                                    command.ExecuteNonQuery();
                                }

                                DbContext.ChiTietPhieuSuaChuas.AddRange(chiTietPhieuSuaChuas);
                                DbContext.SaveChanges();

                                // xoa tai lieu dinh kem cu
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete FileTrongPhieuSuaChua Where PhieuSuaChuaId = @PhieuSuaChuaId;";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", reqVM.PhieuSuaChuaId));
                                    command.ExecuteNonQuery();
                                }

                                // tai lieu dinh kem
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuSuaChua> fileTrongPhieuSuaChuas = new List<FileTrongPhieuSuaChua>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuSuaChua fileTrongPhieuSuaChua = new FileTrongPhieuSuaChua();
                                        fileTrongPhieuSuaChua.FileTrongPhieuSuaChuaId = item.FileId;
                                        fileTrongPhieuSuaChua.Ext = item.Ext;
                                        fileTrongPhieuSuaChua.Icon = item.Icon;
                                        fileTrongPhieuSuaChua.TenFile = item.TenFile;
                                        fileTrongPhieuSuaChua.Url = item.Url;
                                        fileTrongPhieuSuaChua.PhieuSuaChuaId = reqVM.PhieuSuaChuaId;
                                        fileTrongPhieuSuaChuas.Add(fileTrongPhieuSuaChua);
                                    }
                                    DbContext.FileTrongPhieuSuaChuas.AddRange(fileTrongPhieuSuaChuas);
                                    DbContext.SaveChanges();
                                }

                                dbContextTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                throw new WarningException(ex.Message);
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePhieuSuaChua(DeletePhieuSuaChuaReqVM reqVM)
        {
            try
            {

                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update KhoThietBi
                                                        Set SoLuong = SoLuong - b.SoLuongSuaChua, 
                                                            SoLuongConLai = SoLuongConLai - b.SoLuongSuaChua
                                                        From KhoThietBi as a
                                                        Inner Join ChiTietPhieuSuaChua as b
                                                        on a.ThietBiId = b.ThietBiId and a.KhoPhongId = b.KhoPhongId
                                                        Where (b.PhieuSuaChuaId = @PhieuSuaChuaId);
                                                        Delete ChiTietPhieuSuaChua Where PhieuSuaChuaId = @PhieuSuaChuaId;
                                                        Delete PhieuSuaChua Where PhieuSuaChuaId = @PhieuSuaChuaId;
                                                        Delete FileTrongPhieuSuaChua Where PhieuSuaChuaId = @PhieuSuaChuaId;";
                                    command.Parameters.Add(new SqlParameter("@PhieuSuaChuaId", string.IsNullOrEmpty(reqVM.PhieuSuaChuaId) ? "" : reqVM.PhieuSuaChuaId.Trim()));
                                    command.CommandText = sqlString;
                                    command.ExecuteNonQuery();
                                }
                                dbContextTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                throw new WarningException(ex.Message);
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region phieu de nghi mua sam
        public ListWithPaginationResVM GetPhieuDeNghiMuaSams(string soPhieu, DateTime? tuNgay, DateTime? denNgay, int? daXuLy, int? currentPage, int? pageSize)
        {
            try
            {
                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("SoPhieu", soPhieu);
                queryParamsDict.Add("TuNgay", tuNgay.HasValue ? tuNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("DenNgay", denNgay.HasValue ? denNgay.Value.ToString("yyyy-MM-dd") : null);
                queryParamsDict.Add("DaXuLy", daXuLy.HasValue ? daXuLy.Value.ToString() : null);
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.HoTen
                                            From PhieuDeNghiMuaSam as a
                                            Left Join NguoiDung as b
                                            on a.NguoiCapNhat = b.NguoiDungId
                                            Where ((@SoPhieu is null) or (a.SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (a.NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (a.NgayLap <= @DenNgay))
                                                and ((@DaXuLy is null) or (a.DaXuLy = @DaXuLy))
                                            Order by a.NgayLap Desc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DaXuLy", daXuLy.HasValue ? daXuLy.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage - 1) * pageSize));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<PhieuDeNghiMuaSamResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From PhieuDeNghiMuaSam
                                            Where ((@SoPhieu is null) or (SoPhieu Like '%' + @SoPhieu + '%'))
                                                and ((@TuNgay is null) or (NgayLap >= @TuNgay)) 
                                                and ((@DenNgay is null) or (NgayLap <= @DenNgay))
                                                and ((@DaXuLy is null) or (DaXuLy = @DaXuLy))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(soPhieu) ? (object)DBNull.Value : soPhieu.Trim()));
                        command.Parameters.Add(new SqlParameter("@TuNgay", tuNgay.HasValue ? tuNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", denNgay.HasValue ? denNgay.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@DaXuLy", daXuLy.HasValue ? daXuLy.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRow = Convert.ToInt64(reader[0]);
                            }
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var phieuDeNghiMuaSamResVM in (List<PhieuDeNghiMuaSamResVM>)listWithPaginationResVM.Objects)
                {
                    phieuDeNghiMuaSamResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                int totalPage = Convert.ToInt32(Math.Ceiling(totalRow * 1.0 / pageSize.Value));
                listWithPaginationResVM.CurrentQueryParamsDict = queryParamsDict;
                listWithPaginationResVM.Paginations = PaginationHelper.PaginationGeneration(queryParamsDict, totalPage, currentPage.Value, pageSize.Value);

                return listWithPaginationResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChiTietThietBiResVM> GetThietBiTonTrongKhos(string tenThietBi, int? monHocId)
        {
            try
            {
                List<ChiTietThietBiResVM> chiTietThietBiResVMs = new List<ChiTietThietBiResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select b.*, a.SoLuong, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh
                                            From (Select ThietBiid, Sum(SoLuong) as 'SoLuong' From KhoThietBi
		                                            Group by ThietBiId) as a
                                            Right Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Where((@TenThietBi is null) or(b.TenThietBi Like N'%' + @TenThietBi + '%'))
                                                and((@MonHocId is null) or(b.MonHocId = @MonHocId))
                                            Order by b.TenThietBi";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenThietBi", string.IsNullOrEmpty(tenThietBi) ? (object)DBNull.Value : tenThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVMs = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChiTietThietBiResVM GetThietBiTonTrongKho(int? thietBiId)
        {
            try
            {
                ChiTietThietBiResVM chiTietThietBiResVM = new ChiTietThietBiResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select b.*, IsNull(a.SoLuong, 0) as 'SoLuong', c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh
                                            From (Select ThietBiid, Sum(SoLuong) as 'SoLuong' From KhoThietBi
		                                            Group by ThietBiId) as a
                                            Right Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Where b.ThietBiId = @ThietBiId
                                            Order by b.TenThietBi";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@ThietBiId", thietBiId.HasValue ? thietBiId.Value : (object)DBNull.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            chiTietThietBiResVM = MapDataHelper<ChiTietThietBiResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return chiTietThietBiResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddPhieuDeNghiMuaSam(AddPhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                PhieuDeNghiMuaSam phieuDeNghiMuaSam = new PhieuDeNghiMuaSam();
                                phieuDeNghiMuaSam.PhieuDeNghiMuaSamId = Guid.NewGuid().ToString();
                                phieuDeNghiMuaSam.NoiDung = string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim();
                                phieuDeNghiMuaSam.NgayLap = reqVM.NgayLap;
                                phieuDeNghiMuaSam.SoPhieu = reqVM.SoPhieu;
                                phieuDeNghiMuaSam.ChotPhieu = false;
                                phieuDeNghiMuaSam.DaXuLy = false;
                                phieuDeNghiMuaSam.NguoiCapNhat = reqVM.NguoiCapNhat;
                                DbContext.PhieuDeNghiMuaSams.Add(phieuDeNghiMuaSam);
                                DbContext.SaveChanges();

                                if (reqVM.ChiTietPhieuDeNghiMuaSams != null && reqVM.ChiTietPhieuDeNghiMuaSams.Count() > 0)
                                {
                                    List<ChiTietPhieuDeNghiMuaSam> chiTietPhieuDeNghiMuaSams = new List<ChiTietPhieuDeNghiMuaSam>();
                                    foreach (var item in reqVM.ChiTietPhieuDeNghiMuaSams)
                                    {
                                        ChiTietPhieuDeNghiMuaSam chiTietPhieuDeNghiMuaSam = new ChiTietPhieuDeNghiMuaSam();
                                        chiTietPhieuDeNghiMuaSam.ChiTietPhieuDeNghiMuaSamId = Guid.NewGuid().ToString();
                                        chiTietPhieuDeNghiMuaSam.PhieuDeNghiMuaSamId = phieuDeNghiMuaSam.PhieuDeNghiMuaSamId;
                                        chiTietPhieuDeNghiMuaSam.KinhPhiId = item.KinhPhiId;
                                        chiTietPhieuDeNghiMuaSam.DonGia = item.DonGia;
                                        chiTietPhieuDeNghiMuaSam.SoLuongDeNghi = item.SoLuongDeNghi;
                                        chiTietPhieuDeNghiMuaSam.ThietBiId = item.ThietBiId;

                                        chiTietPhieuDeNghiMuaSams.Add(chiTietPhieuDeNghiMuaSam);
                                    }
                                    DbContext.ChiTietPhieuDeNghiMuaSams.AddRange(chiTietPhieuDeNghiMuaSams);
                                    DbContext.SaveChanges();
                                }

                                // tai lieu dinh kem
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuDeNghiMuaSam> fileTrongPhieuDeNghiMuaSams = new List<FileTrongPhieuDeNghiMuaSam>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuDeNghiMuaSam fileTrongPhieuDeNghiMuaSam = new FileTrongPhieuDeNghiMuaSam();
                                        fileTrongPhieuDeNghiMuaSam.FileTrongPhieuDeNghiMuaSamId = item.FileId;
                                        fileTrongPhieuDeNghiMuaSam.Ext = item.Ext;
                                        fileTrongPhieuDeNghiMuaSam.Icon = item.Icon;
                                        fileTrongPhieuDeNghiMuaSam.TenFile = item.TenFile;
                                        fileTrongPhieuDeNghiMuaSam.Url = item.Url;
                                        fileTrongPhieuDeNghiMuaSam.PhieuDeNghiMuaSamId = phieuDeNghiMuaSam.PhieuDeNghiMuaSamId;
                                        fileTrongPhieuDeNghiMuaSams.Add(fileTrongPhieuDeNghiMuaSam);
                                    }
                                    DbContext.FileTrongPhieuDeNghiMuaSams.AddRange(fileTrongPhieuDeNghiMuaSams);
                                    DbContext.SaveChanges();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PhieuDeNghiMuaSamResVM GetPhieuDeNghiMuaSam(string phieuDeNghiMuaSamId)
        {
            try
            {
                PhieuDeNghiMuaSamResVM phieuDeNghiMuaSamResVM = new PhieuDeNghiMuaSamResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From PhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId =  @PhieuDeNghiMuaSamId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", phieuDeNghiMuaSamId));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuDeNghiMuaSamResVM = MapDataHelper<PhieuDeNghiMuaSamResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                if (phieuDeNghiMuaSamResVM == null)
                {
                    throw new Exception("Không tìm thấy thông tin phiếu đề nghị mua sắm");
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select b.*, c.TenMonHoc, d.TenLoaiThietBi, e.TenDonViTinh, IsNull(f.SoLuong, 0) as 'SoLuong',
                                                    a.SoLuongDeNghi, a.DonGia, a.KinhPhiId
                                            From ChiTietPhieuDeNghiMuaSam as a
                                            Left Join ThietBi as b
                                            on a.ThietBiId = b.ThietBiId
                                            Left Join MonHoc as c
                                            on b.MonHocId = c.MonHocId
                                            Left Join LoaiThietBi as d
                                            on b.LoaiThietBiId = d.LoaiThietBiId
                                            Left Join DonViTinh as e
                                            on b.DonViTinhId = e.DonViTinhId
                                            Left Join (Select ThietBiid, Sum(SoLuong) as 'SoLuong' From KhoThietBi
		                                                Group by ThietBiId) as f
                                            on a.ThietBiId = f.ThietBiId
                                            Where a.PhieuDeNghiMuaSamId =  @PhieuDeNghiMuaSamId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", phieuDeNghiMuaSamId));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuDeNghiMuaSamResVM.ChiTietThietBis = MapDataHelper<ChiTietThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select FileTrongPhieuDeNghiMuaSamId as 'FileId', TenFile, Ext, Url, Icon From FileTrongPhieuDeNghiMuaSam
                                            Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(phieuDeNghiMuaSamId) ? "" : phieuDeNghiMuaSamId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            phieuDeNghiMuaSamResVM.FileTrongPhieuDeNghiMuaSams = MapDataHelper<FileResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }


                return phieuDeNghiMuaSamResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePhieuDeNghiMuaSam(UpdatePhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                // check da chot chua
                                bool chotPhieu = false;
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Select ChotPhieu From PhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    using (var reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            chotPhieu = Convert.ToBoolean(reader[0]);
                                        }
                                    }
                                }

                                if (chotPhieu)
                                {
                                    throw new WarningException("Phiếu đã chốt không thể cập nhật");
                                }

                                // update thong tin co ban
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update PhieuDeNghiMuaSam
                                                        Set SoPhieu = @SoPhieu, NgayLap = @NgayLap, NoiDung = @NoiDung
                                                        Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    command.Parameters.Add(new SqlParameter("@SoPhieu", string.IsNullOrEmpty(reqVM.SoPhieu) ? "" : reqVM.SoPhieu.Trim()));
                                    command.Parameters.Add(new SqlParameter("@NoiDung", string.IsNullOrEmpty(reqVM.NoiDung) ? "" : reqVM.NoiDung.Trim()));
                                    command.Parameters.Add(new SqlParameter("@NgayLap", reqVM.NgayLap.HasValue ? reqVM.NgayLap.Value : (object)DBNull.Value));
                                    command.ExecuteNonQuery();
                                }

                                // xoa chi tiet phieu de nghi sua chua cu
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete ChiTietPhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    command.ExecuteNonQuery();
                                }

                                if (reqVM.ChiTietPhieuDeNghiMuaSams != null && reqVM.ChiTietPhieuDeNghiMuaSams.Count() > 0)
                                {
                                    List<ChiTietPhieuDeNghiMuaSam> chiTietPhieuDeNghiMuaSams = new List<ChiTietPhieuDeNghiMuaSam>();
                                    foreach (var item in reqVM.ChiTietPhieuDeNghiMuaSams)
                                    {
                                        ChiTietPhieuDeNghiMuaSam chiTietPhieuDeNghiMuaSam = new ChiTietPhieuDeNghiMuaSam();
                                        chiTietPhieuDeNghiMuaSam.ChiTietPhieuDeNghiMuaSamId = Guid.NewGuid().ToString();
                                        chiTietPhieuDeNghiMuaSam.PhieuDeNghiMuaSamId = reqVM.PhieuDeNghiMuaSamId;
                                        chiTietPhieuDeNghiMuaSam.KinhPhiId = item.KinhPhiId;
                                        chiTietPhieuDeNghiMuaSam.DonGia = item.DonGia;
                                        chiTietPhieuDeNghiMuaSam.SoLuongDeNghi = item.SoLuongDeNghi;
                                        chiTietPhieuDeNghiMuaSam.ThietBiId = item.ThietBiId;

                                        chiTietPhieuDeNghiMuaSams.Add(chiTietPhieuDeNghiMuaSam);
                                    }
                                    DbContext.ChiTietPhieuDeNghiMuaSams.AddRange(chiTietPhieuDeNghiMuaSams);
                                    DbContext.SaveChanges();
                                }

                                // xoa tai lieu dinh kem cu
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete FileTrongPhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId;";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", reqVM.PhieuDeNghiMuaSamId));
                                    command.ExecuteNonQuery();
                                }

                                // tai lieu dinh kem
                                if (reqVM.TaiLieuDinhKems != null && reqVM.TaiLieuDinhKems.Count() > 0)
                                {
                                    List<FileTrongPhieuDeNghiMuaSam> fileTrongPhieuDeNghiMuaSams = new List<FileTrongPhieuDeNghiMuaSam>();

                                    foreach (var item in reqVM.TaiLieuDinhKems)
                                    {
                                        FileTrongPhieuDeNghiMuaSam fileTrongPhieuDeNghiMuaSam = new FileTrongPhieuDeNghiMuaSam();
                                        fileTrongPhieuDeNghiMuaSam.FileTrongPhieuDeNghiMuaSamId = item.FileId;
                                        fileTrongPhieuDeNghiMuaSam.Ext = item.Ext;
                                        fileTrongPhieuDeNghiMuaSam.Icon = item.Icon;
                                        fileTrongPhieuDeNghiMuaSam.TenFile = item.TenFile;
                                        fileTrongPhieuDeNghiMuaSam.Url = item.Url;
                                        fileTrongPhieuDeNghiMuaSam.PhieuDeNghiMuaSamId = reqVM.PhieuDeNghiMuaSamId;
                                        fileTrongPhieuDeNghiMuaSams.Add(fileTrongPhieuDeNghiMuaSam);
                                    }
                                    DbContext.FileTrongPhieuDeNghiMuaSams.AddRange(fileTrongPhieuDeNghiMuaSams);
                                    DbContext.SaveChanges();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (WarningException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChotPhieuDeNghiMuaSam(ChotPhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                // check da chot chua
                                bool chotPhieu = false;
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Select ChotPhieu From PhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    using (var reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            chotPhieu = Convert.ToBoolean(reader[0]);
                                        }
                                    }
                                }

                                if (chotPhieu)
                                {
                                    throw new WarningException("Phiếu đã chốt không thể cập nhật");
                                }

                                // update thong tin co ban
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update PhieuDeNghiMuaSam
                                                        Set ChotPhieu = 1
                                                        Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    command.ExecuteNonQuery();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void XuLyPhieuDeNghiMuaSam(XuLyPhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                // check da chot chua
                                bool daXuLy = false;
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Select DaXuLy From PhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    using (var reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            daXuLy = Convert.ToBoolean(reader[0]);
                                        }
                                    }
                                }

                                if (daXuLy)
                                {
                                    throw new WarningException("Phiếu đã được xử lý");
                                }

                                // update thong tin co ban
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Update PhieuDeNghiMuaSam
                                                        Set DaXuLy = 1
                                                        Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    command.ExecuteNonQuery();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePhieuDeNghiMuaSam(DeletePhieuDeNghiMuaSamReqVM reqVM)
        {
            try
            {
                using (var dbContextTransaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        using (var command = DbContext.Database.Connection.CreateCommand())
                        {
                            bool wasOpen = command.Connection.State == ConnectionState.Open;
                            if (!wasOpen) command.Connection.Open();
                            command.CommandType = CommandType.Text;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            try
                            {
                                // check da chot chua
                                bool daXuLy = false;
                                bool chotPhieu = false;
                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Select ChotPhieu, DaXuLy From PhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    using (var reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            chotPhieu = Convert.ToBoolean(reader[0]);
                                            daXuLy = Convert.ToBoolean(reader[1]);
                                        }
                                    }
                                }
                                if (daXuLy)
                                {
                                    throw new WarningException("Phiếu đã được xử lý");
                                }

                                if (chotPhieu)
                                {
                                    throw new WarningException("Phiếu đã được chốt");
                                }

                                {
                                    command.Parameters.Clear();
                                    string sqlString = @"Delete ChiTietPhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId;
                                                        Delete PhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId;
                                                        Delete FileTrongPhieuDeNghiMuaSam Where PhieuDeNghiMuaSamId = @PhieuDeNghiMuaSamId; ";
                                    command.CommandText = sqlString;
                                    command.CommandType = CommandType.Text;
                                    command.Parameters.Add(new SqlParameter("@PhieuDeNghiMuaSamId", string.IsNullOrEmpty(reqVM.PhieuDeNghiMuaSamId) ? "" : reqVM.PhieuDeNghiMuaSamId.Trim()));
                                    command.ExecuteNonQuery();
                                }

                                dbContextTransaction.Commit();
                            }
                            finally
                            {
                                command.Connection.Close();
                            }
                        }
                    }
                    catch (WarningException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new WarningException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
