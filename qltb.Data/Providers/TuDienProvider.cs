using qltb.Data.Helpers;
using qltb.Data.Helpers.ExceptionHelpers;
using qltb.Data.ReqVMs;
using qltb.Data.ResVMs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class TuDienProvider : ApplicationDbcontext
    {
        #region dghc
        public void NhapDGHC(List<AddTinhReqVM> tinhReqVMs, List<AddHuyenReqVM> huyenReqVMs, List<AddXaReqVM> xaReqVMs)
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
                            string sqlString = @"Delete Tinh; Delete Huyen; Delete Xa;";
                            command.CommandText = sqlString;
                            command.Transaction = dbContextTransaction.UnderlyingTransaction;
                            command.CommandType = CommandType.Text;
                            command.ExecuteNonQuery();

                            List<Tinh> tinhs = tinhReqVMs.Select(x => new Tinh() { TinhId = x.TinhId, TenTinh = x.TenTinh }).ToList();
                            List<Huyen> huyens = huyenReqVMs.Select(x => new Huyen() { HuyenId = x.HuyenId, TenHuyen = x.TenHuyen }).ToList();
                            List<Xa> xas = xaReqVMs.Select(x => new Xa() { XaId = x.XaId, TenXa = x.TenXa }).ToList();
                            DbContext.Tinhs.AddRange(tinhs);
                            DbContext.Huyens.AddRange(huyens);
                            DbContext.Xas.AddRange(xas);
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

        public List<TinhResVM> GetTinhs(string tenTinh)
        {
            try
            {
                List<TinhResVM> tinhResVMs = new List<TinhResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From Tinh
                                    Where (TenTinh Like N'%' + @TenTinh+ '%')
                                    Order by Cast(TinhId as int)";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenTinh", string.IsNullOrEmpty(tenTinh) ? "" : tenTinh.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            tinhResVMs = MapDataHelper<TinhResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return tinhResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TinhResVM GetTinh(string tinhId)
        {
            try
            {
                TinhResVM tinhResVM = new TinhResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From Tinh
                                    Where TinhId = @TinhId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TinhId", string.IsNullOrEmpty(tinhId) ? "" : tinhId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            tinhResVM = MapDataHelper<TinhResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return tinhResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ListWithPaginationResVM GetTinhs(string tenTinh, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenTinh", tenTinh);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From Tinh
                                    Where (TenTinh Like N'%' + @TenTinh+ '%')
                                    Order by Cast(TinhId as int)
                                    Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenTinh", string.IsNullOrEmpty(tenTinh) ? "" : tenTinh.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<TinhResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }


                int totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From Tinh
                                    Where (TenTinh Like N'%' + @TenTinh+ '%')";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenTinh", string.IsNullOrEmpty(tenTinh) ? "" : tenTinh.Trim()));
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

        public ListWithPaginationResVM GetHuyens(string tenHuyen, string tinhId, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenHuyen", tenHuyen);
                queryParamsDict.Add("TinhId", tinhId);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From Huyen as a
                                            Left Join Tinh as b
                                            on Substring(a.HuyenId,1,2) = b.TinhId
                                            Where (a.TenHuyen Like N'%' + @TenHuyen+ '%')
                                                    and ((@TinhId is null) or (Substring(a.HuyenId,1,2) = @TinhId))
                                            Order by Cast(HuyenId as int)
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenHuyen", string.IsNullOrEmpty(tenHuyen) ? "" : tenHuyen.Trim()));
                        command.Parameters.Add(new SqlParameter("@TinhId", string.IsNullOrEmpty(tinhId) ? (object)DBNull.Value : tinhId.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<HuyenResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }


                int totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From Huyen as a
                                            Where (a.TenHuyen Like N'%' + @TenHuyen+ '%')
                                                    and ((@TinhId is null) or (Substring(a.HuyenId,1,2) = @TinhId))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenHuyen", string.IsNullOrEmpty(tenHuyen) ? "" : tenHuyen.Trim()));
                        command.Parameters.Add(new SqlParameter("@TinhId", string.IsNullOrEmpty(tinhId) ? (object)DBNull.Value : tinhId.Trim()));
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

        public List<HuyenResVM> GetHuyens(string tenHuyen, string tinhId)
        {
            try
            {
                List<HuyenResVM> huyenResVMs = new List<HuyenResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From Huyen
                                    Where (TenHuyen Like N'%' + @TenHuyen+ '%')
                                        and (Substring(HuyenId,1,2) Like @TinhId)
                                    Order by Cast(HuyenId as int)";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenHuyen", string.IsNullOrEmpty(tenHuyen) ? "" : tenHuyen.Trim()));
                        command.Parameters.Add(new SqlParameter("@TinhId", string.IsNullOrEmpty(tinhId) ? "" : tinhId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            huyenResVMs = MapDataHelper<HuyenResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return huyenResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HuyenResVM GetHuyen(string huyenId)
        {
            try
            {
                HuyenResVM huyenResVM = new HuyenResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From Huyen
                                    Where HuyenId = @HuyenId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@HuyenId", string.IsNullOrEmpty(huyenId) ? "" : huyenId.Trim()));
                        using (var reader = command.ExecuteReader())
                        {
                            huyenResVM = MapDataHelper<HuyenResVM>.Map(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return huyenResVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ListWithPaginationResVM GetXas(string tenXa, string tinhId, string huyenId, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenXa", tenXa);
                queryParamsDict.Add("TinhId", tinhId);
                queryParamsDict.Add("HuyenId", huyenId);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.TenTinh, c.TenHuyen From Xa as a
                                            Left Join Tinh as b
                                            on Substring(a.XaId,1,2) = b.TinhId
                                            Left Join Huyen as c
                                            on Substring(a.XaId,1,5) = c.HuyenId
                                            Where (a.TenXa Like N'%' + @TenXa+ '%')
                                                and ((@TinhId is null) or (Substring(a.XaId,1,2) = @TinhId))
                                                and ((@HuyenId is null) or (Substring(a.XaId,1,5) = @HuyenId))
                                            Order by Cast(XaId as bigint)
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenXa", string.IsNullOrEmpty(tenXa) ? "" : tenXa.Trim()));
                        command.Parameters.Add(new SqlParameter("@TinhId", string.IsNullOrEmpty(tinhId) ? (object)DBNull.Value : tinhId.Trim()));
                        command.Parameters.Add(new SqlParameter("@HuyenId", string.IsNullOrEmpty(huyenId) ? (object)DBNull.Value : huyenId.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<XaResVM>.MapList(reader);
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
                        string sqlString = @"Select Count(*) From Xa as a
                                            Where (a.TenXa Like N'%' + @TenXa+ '%')
                                                and ((@TinhId is null) or (Substring(a.XaId,1,2) = @TinhId))
                                                and ((@HuyenId is null) or (Substring(a.XaId,1,5) = @HuyenId))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenXa", string.IsNullOrEmpty(tenXa) ? "" : tenXa.Trim()));
                        command.Parameters.Add(new SqlParameter("@TinhId", string.IsNullOrEmpty(tinhId) ? (object)DBNull.Value : tinhId.Trim()));
                        command.Parameters.Add(new SqlParameter("@HuyenId", string.IsNullOrEmpty(huyenId) ? (object)DBNull.Value : huyenId.Trim()));
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
        #endregion

        #region loai thiet bi
        public ListWithPaginationResVM GetLoaiThietBis(string tenLoaiThietBi, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenLoaiThietBi", tenLoaiThietBi);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From LoaiThietBi
                                            Where ((@TenLoaiThietBi is null) or (TenLoaiThietBi Like N'%' + @TenLoaiThietBi + '%'))
                                            Order by TenLoaiThietBi
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLoaiThietBi", string.IsNullOrEmpty(tenLoaiThietBi) ? (object)DBNull.Value : tenLoaiThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<LoaiThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var loaiThietBiResVM in (List<LoaiThietBiResVM>)listWithPaginationResVM.Objects)
                {
                    loaiThietBiResVM.STT = (currentPage - 1) * pageSize + i++;
                }

                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From LoaiThietBi
                                            Where ((@TenLoaiThietBi is null) or (TenLoaiThietBi Like N'%' + @TenLoaiThietBi + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLoaiThietBi", string.IsNullOrEmpty(tenLoaiThietBi) ? (object)DBNull.Value : tenLoaiThietBi.Trim()));
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

        public LoaiThietBiResVM GetLoaiThietBi(int? loaiThietBiId)
        {
            try
            {
                LoaiThietBiResVM loaiThietBiResVM = new LoaiThietBiResVM();
                if (loaiThietBiId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From LoaiThietBi Where LoaiThietBiId = @LoaiThietBiId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LoaiThietBiId", loaiThietBiId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                loaiThietBiResVM = MapDataHelper<LoaiThietBiResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return loaiThietBiResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLoaiThietBi(UpdateLoaiThietBiReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLoaiThietBi))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From LoaiThietBi Where (LoaiThietBiId != @LoaiThietBiId) and (MaLoaiThietBi = @MaLoaiThietBi)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LoaiThietBiId", reqVM.LoaiThietBiId.Value));
                            command.Parameters.Add(new SqlParameter("@MaLoaiThietBi", reqVM.MaLoaiThietBi.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại thiết bị đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update LoaiThietBi
                                            Set MaLoaiThietBi = @MaLoaiThietBi, TenLoaiThietBi = @TenLoaiThietBi
                                            Where LoaiThietBiId = @LoaiThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LoaiThietBiId", reqVM.LoaiThietBiId.Value));
                        command.Parameters.Add(new SqlParameter("@MaLoaiThietBi", string.IsNullOrEmpty(reqVM.MaLoaiThietBi) ? "" : reqVM.MaLoaiThietBi.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenLoaiThietBi", string.IsNullOrEmpty(reqVM.TenLoaiThietBi) ? "" : reqVM.TenLoaiThietBi.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteLoaiThietBi(DeleteLoaiThietBiReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete LoaiThietBi Where LoaiThietBiId = @LoaiThietBiId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LoaiThietBiId", reqVM.LoaiThietBiId.HasValue ? reqVM.LoaiThietBiId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddLoaiThietBi(AddLoaiThietBiReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLoaiThietBi))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From LoaiThietBi Where (MaLoaiThietBi = @MaLoaiThietBi)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaLoaiThietBi", reqVM.MaLoaiThietBi.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại thiết bị đã tồn tại");
                    }
                }

                LoaiThietBi loaiThietBi = new LoaiThietBi();
                loaiThietBi.MaLoaiThietBi = string.IsNullOrEmpty(reqVM.MaLoaiThietBi) ? "" : reqVM.MaLoaiThietBi.Trim();
                loaiThietBi.TenLoaiThietBi = string.IsNullOrEmpty(reqVM.TenLoaiThietBi) ? "" : reqVM.TenLoaiThietBi.Trim();
                DbContext.LoaiThietBis.Add(loaiThietBi);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region loai bai hoc 
        public ListWithPaginationResVM GetLoaiBaiHocs(string tenLoaiBaiHoc, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenLoaiBaiHoc", tenLoaiBaiHoc);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From LoaiBaiHoc
                                            Where ((@TenLoaiBaiHoc is null) or (TenLoaiBaiHoc Like N'%' + @TenLoaiBaiHoc + '%'))
                                            Order by TenLoaiBaiHoc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLoaiBaiHoc", string.IsNullOrEmpty(tenLoaiBaiHoc) ? (object)DBNull.Value : tenLoaiBaiHoc.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<LoaiBaiHocResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var loaiBaiHocResVM in (List<LoaiBaiHocResVM>)listWithPaginationResVM.Objects)
                {
                    loaiBaiHocResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From LoaiBaiHoc
                                            Where ((@TenLoaiBaiHoc is null) or (TenLoaiBaiHoc Like N'%' + @TenLoaiBaiHoc + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLoaiBaiHoc", string.IsNullOrEmpty(tenLoaiBaiHoc) ? (object)DBNull.Value : tenLoaiBaiHoc.Trim()));
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

        public LoaiBaiHocResVM GetLoaiBaiHoc(int? loaiBaiHocId)
        {
            try
            {
                LoaiBaiHocResVM loaiBaiHocResVM = new LoaiBaiHocResVM();
                if (loaiBaiHocId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From LoaiBaiHoc Where LoaiBaiHocId = @LoaiBaiHocId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LoaiBaiHocId", loaiBaiHocId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                loaiBaiHocResVM = MapDataHelper<LoaiBaiHocResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return loaiBaiHocResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLoaiBaiHoc(UpdateLoaiBaiHocReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLoaiBaiHoc))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From LoaiBaiHoc Where (LoaiBaiHocId != @LoaiBaiHocId) and (MaLoaiBaiHoc = @MaLoaiBaiHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LoaiBaiHocId", reqVM.LoaiBaiHocId.Value));
                            command.Parameters.Add(new SqlParameter("@MaLoaiBaiHoc", reqVM.MaLoaiBaiHoc.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại bài học đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update LoaiBaiHoc
                                            Set MaLoaiBaiHoc = @MaLoaiBaiHoc, TenLoaiBaiHoc = @TenLoaiBaiHoc
                                            Where LoaiBaiHocId = @LoaiBaiHocId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LoaiBaiHocId", reqVM.LoaiBaiHocId.Value));
                        command.Parameters.Add(new SqlParameter("@MaLoaiBaiHoc", string.IsNullOrEmpty(reqVM.MaLoaiBaiHoc) ? "" : reqVM.MaLoaiBaiHoc.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenLoaiBaiHoc", string.IsNullOrEmpty(reqVM.TenLoaiBaiHoc) ? "" : reqVM.TenLoaiBaiHoc.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteLoaiBaiHoc(DeleteLoaiBaiHocReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete LoaiBaiHoc Where LoaiBaiHocId = @LoaiBaiHocId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LoaiBaiHocId", reqVM.LoaiBaiHocId.HasValue ? reqVM.LoaiBaiHocId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddLoaiBaiHoc(AddLoaiBaiHocReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLoaiBaiHoc))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From LoaiBaiHoc Where (MaLoaiBaiHoc = @MaLoaiBaiHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaLoaiBaiHoc", reqVM.MaLoaiBaiHoc.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại bài học đã tồn tại");
                    }
                }

                LoaiBaiHoc loaiBaiHoc = new LoaiBaiHoc();
                loaiBaiHoc.MaLoaiBaiHoc = string.IsNullOrEmpty(reqVM.MaLoaiBaiHoc) ? "" : reqVM.MaLoaiBaiHoc.Trim();
                loaiBaiHoc.TenLoaiBaiHoc = string.IsNullOrEmpty(reqVM.TenLoaiBaiHoc) ? "" : reqVM.TenLoaiBaiHoc.Trim();
                DbContext.LoaiBaiHocs.Add(loaiBaiHoc);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region chuong trinh hoc  
        public ListWithPaginationResVM GetChuongTrinhHocs(string tenChuongTrinhHoc, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenChuongTrinhHoc", tenChuongTrinhHoc);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From ChuongTrinhHoc
                                            Where ((@TenChuongTrinhHoc is null) or (TenChuongTrinhHoc Like N'%' + @TenChuongTrinhHoc + '%'))
                                            Order by TenChuongTrinhHoc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenChuongTrinhHoc", string.IsNullOrEmpty(tenChuongTrinhHoc) ? (object)DBNull.Value : tenChuongTrinhHoc.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<ChuongTrinhHocResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var chuongTrinhHocResVM in (List<ChuongTrinhHocResVM>)listWithPaginationResVM.Objects)
                {
                    chuongTrinhHocResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From ChuongTrinhHoc
                                            Where ((@TenChuongTrinhHoc is null) or (TenChuongTrinhHoc Like N'%' + @TenChuongTrinhHoc + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenChuongTrinhHoc", string.IsNullOrEmpty(tenChuongTrinhHoc) ? (object)DBNull.Value : tenChuongTrinhHoc.Trim()));
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

        public ChuongTrinhHocResVM GetChuongTrinhHoc(int? chuongTrinhHocId)
        {
            try
            {
                ChuongTrinhHocResVM chuongTrinhHocResVM = new ChuongTrinhHocResVM();
                if (chuongTrinhHocId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From ChuongTrinhHoc Where ChuongTrinhHocId = @ChuongTrinhHocId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@ChuongTrinhHocId", chuongTrinhHocId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                chuongTrinhHocResVM = MapDataHelper<ChuongTrinhHocResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return chuongTrinhHocResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateChuongTrinhHoc(UpdateChuongTrinhHocReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaChuongTrinhHoc))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From ChuongTrinhHoc Where (ChuongTrinhHocId != @ChuongTrinhHocId) and (MaChuongTrinhHoc = @MaChuongTrinhHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@ChuongTrinhHocId", reqVM.ChuongTrinhHocId.Value));
                            command.Parameters.Add(new SqlParameter("@MaChuongTrinhHoc", reqVM.MaChuongTrinhHoc.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại chương trình học đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update ChuongTrinhHoc
                                            Set MaChuongTrinhHoc = @MaChuongTrinhHoc, TenChuongTrinhHoc = @TenChuongTrinhHoc
                                            Where ChuongTrinhHocId = @ChuongTrinhHocId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@ChuongTrinhHocId", reqVM.ChuongTrinhHocId.Value));
                        command.Parameters.Add(new SqlParameter("@MaChuongTrinhHoc", string.IsNullOrEmpty(reqVM.MaChuongTrinhHoc) ? "" : reqVM.MaChuongTrinhHoc.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenChuongTrinhHoc", string.IsNullOrEmpty(reqVM.TenChuongTrinhHoc) ? "" : reqVM.TenChuongTrinhHoc.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteChuongTrinhHoc(DeleteChuongTrinhHocReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete ChuongTrinhHoc Where ChuongTrinhHocId = @ChuongTrinhHocId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@ChuongTrinhHocId", reqVM.ChuongTrinhHocId.HasValue ? reqVM.ChuongTrinhHocId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddChuongTrinhHoc(AddChuongTrinhHocReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaChuongTrinhHoc))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From ChuongTrinhHoc Where (MaChuongTrinhHoc = @MaChuongTrinhHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaChuongTrinhHoc", reqVM.MaChuongTrinhHoc.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã chương trình học đã tồn tại");
                    }
                }

                ChuongTrinhHoc chuongTrinhHoc = new ChuongTrinhHoc();
                chuongTrinhHoc.MaChuongTrinhHoc = string.IsNullOrEmpty(reqVM.MaChuongTrinhHoc) ? "" : reqVM.MaChuongTrinhHoc.Trim();
                chuongTrinhHoc.TenChuongTrinhHoc = string.IsNullOrEmpty(reqVM.TenChuongTrinhHoc) ? "" : reqVM.TenChuongTrinhHoc.Trim();
                DbContext.ChuongTrinhHocs.Add(chuongTrinhHoc);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region don vi tinh 
        public ListWithPaginationResVM GetDonViTinhs(string tenDonViTinh, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenDonViTinh", tenDonViTinh);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From DonViTinh
                                            Where ((@TenDonViTinh is null) or (TenDonViTinh Like N'%' + @TenDonViTinh + '%'))
                                            Order by TenDonViTinh
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenDonViTinh", string.IsNullOrEmpty(tenDonViTinh) ? (object)DBNull.Value : tenDonViTinh.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<DonViTinhResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var donViTinhResVM in (List<DonViTinhResVM>)listWithPaginationResVM.Objects)
                {
                    donViTinhResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From DonViTinh
                                            Where ((@TenDonViTinh is null) or (TenDonViTinh Like N'%' + @TenDonViTinh + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenDonViTinh", string.IsNullOrEmpty(tenDonViTinh) ? (object)DBNull.Value : tenDonViTinh.Trim()));
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

        public DonViTinhResVM GetDonViTinh(int? donViTinhId)
        {
            try
            {
                DonViTinhResVM donViTinhResVM = new DonViTinhResVM();
                if (donViTinhId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From DonViTinh Where DonViTinhId = @DonViTinhId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@DonViTinhId", donViTinhId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                donViTinhResVM = MapDataHelper<DonViTinhResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return donViTinhResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDonViTinh(UpdateDonViTinhReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaDonViTinh))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From DonViTinh Where (DonViTinhId != @DonViTinhId) and (MaDonViTinh = @MaDonViTinh)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@DonViTinhId", reqVM.DonViTinhId.Value));
                            command.Parameters.Add(new SqlParameter("@MaDonViTinh", reqVM.MaDonViTinh.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã đơn vị tính đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update DonViTinh
                                            Set MaDonViTinh = @MaDonViTinh, TenDonViTinh = @TenDonViTinh
                                            Where DonViTinhId = @DonViTinhId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@DonViTinhId", reqVM.DonViTinhId.Value));
                        command.Parameters.Add(new SqlParameter("@MaDonViTinh", string.IsNullOrEmpty(reqVM.MaDonViTinh) ? "" : reqVM.MaDonViTinh.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenDonViTinh", string.IsNullOrEmpty(reqVM.TenDonViTinh) ? "" : reqVM.TenDonViTinh.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteDonViTinh(DeleteDonViTinhReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete DonViTinh Where DonViTinhId = @DonViTinhId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@DonViTinhId", reqVM.DonViTinhId.HasValue ? reqVM.DonViTinhId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddDonViTinh(AddDonViTinhResVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaDonViTinh))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From DonViTinh Where (MaDonViTinh = @MaDonViTinh)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaDonViTinh", reqVM.MaDonViTinh.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã đơn vị tính đã tồn tại");
                    }
                }

                DonViTinh donViTinh = new DonViTinh();
                donViTinh.MaDonViTinh = string.IsNullOrEmpty(reqVM.MaDonViTinh) ? "" : reqVM.MaDonViTinh.Trim();
                donViTinh.TenDonViTinh = string.IsNullOrEmpty(reqVM.TenDonViTinh) ? "" : reqVM.TenDonViTinh.Trim();
                DbContext.DonViTinhs.Add(donViTinh);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region loai kho phong 
        public ListWithPaginationResVM GetLoaiKhoPhongs(string tenLoaiKhoPhong, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenLoaiKhoPhong", tenLoaiKhoPhong);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From LoaiKhoPhong
                                            Where ((@TenLoaiKhoPhong is null) or (TenLoaiKhoPhong Like N'%' + @TenLoaiKhoPhong + '%'))
                                            Order by TenLoaiKhoPhong
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLoaiKhoPhong", string.IsNullOrEmpty(tenLoaiKhoPhong) ? (object)DBNull.Value : tenLoaiKhoPhong.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<LoaiKhoPhongResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var loaiKhoPhongResVM in (List<LoaiKhoPhongResVM>)listWithPaginationResVM.Objects)
                {
                    loaiKhoPhongResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From LoaiKhoPhong
                                            Where ((@TenLoaiKhoPhong is null) or (TenLoaiKhoPhong Like N'%' + @TenLoaiKhoPhong + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLoaiKhoPhong", string.IsNullOrEmpty(tenLoaiKhoPhong) ? (object)DBNull.Value : tenLoaiKhoPhong.Trim()));
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

        public LoaiKhoPhongResVM GetLoaiKhoPhong(int? loaiKhoPhongId)
        {
            try
            {
                LoaiKhoPhongResVM loaiKhoPhongResVM = new LoaiKhoPhongResVM();
                if (loaiKhoPhongId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From LoaiKhoPhong Where LoaiKhoPhongId = @LoaiKhoPhongId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LoaiKhoPhongId", loaiKhoPhongId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                loaiKhoPhongResVM = MapDataHelper<LoaiKhoPhongResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return loaiKhoPhongResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLoaiKhoPhong(UpdateLoaiKhoPhongReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLoaiKhoPhong))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From LoaiKhoPhong Where (LoaiKhoPhongId != @LoaiKhoPhongId) and (MaLoaiKhoPhong = @MaLoaiKhoPhong)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LoaiKhoPhongId", reqVM.LoaiKhoPhongId.Value));
                            command.Parameters.Add(new SqlParameter("@MaLoaiKhoPhong", reqVM.MaLoaiKhoPhong.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại kho phòng đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update LoaiKhoPhong
                                            Set MaLoaiKhoPhong = @MaLoaiKhoPhong, TenLoaiKhoPhong = @TenLoaiKhoPhong
                                            Where LoaiKhoPhongId = @LoaiKhoPhongId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LoaiKhoPhongId", reqVM.LoaiKhoPhongId.Value));
                        command.Parameters.Add(new SqlParameter("@MaLoaiKhoPhong", string.IsNullOrEmpty(reqVM.MaLoaiKhoPhong) ? "" : reqVM.MaLoaiKhoPhong.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenLoaiKhoPhong", string.IsNullOrEmpty(reqVM.TenLoaiKhoPhong) ? "" : reqVM.TenLoaiKhoPhong.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteLoaiKhoPhong(DeleteLoaiKhoPhongReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete LoaiKhoPhong Where LoaiKhoPhongId = @LoaiKhoPhongId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LoaiKhoPhongId", reqVM.LoaiKhoPhongId.HasValue ? reqVM.LoaiKhoPhongId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddLoaiKhoPhong(AddLoaiKhoPhongReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLoaiKhoPhong))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From LoaiKhoPhong Where (MaLoaiKhoPhong = @MaLoaiKhoPhong)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaLoaiKhoPhong", reqVM.MaLoaiKhoPhong.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại kho phòng đã tồn tại");
                    }
                }

                LoaiKhoPhong loaiKhoPhong = new LoaiKhoPhong();
                loaiKhoPhong.MaLoaiKhoPhong = string.IsNullOrEmpty(reqVM.MaLoaiKhoPhong) ? "" : reqVM.MaLoaiKhoPhong.Trim();
                loaiKhoPhong.TenLoaiKhoPhong = string.IsNullOrEmpty(reqVM.TenLoaiKhoPhong) ? "" : reqVM.TenLoaiKhoPhong.Trim();
                DbContext.LoaiKhoPhongs.Add(loaiKhoPhong);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region loai phu trach
        public ListWithPaginationResVM GetLoaiPhuTrachs(string tenLoaiPhuTrach, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenLoaiPhuTrach", tenLoaiPhuTrach);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From LoaiPhuTrach
                                            Where ((@TenLoaiPhuTrach is null) or (TenLoaiPhuTrach Like N'%' + @TenLoaiPhuTrach + '%'))
                                            Order by TenLoaiPhuTrach
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLoaiPhuTrach", string.IsNullOrEmpty(tenLoaiPhuTrach) ? (object)DBNull.Value : tenLoaiPhuTrach.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<LoaiPhuTrachResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var loaiPhuTrachResVM in (List<LoaiPhuTrachResVM>)listWithPaginationResVM.Objects)
                {
                    loaiPhuTrachResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From LoaiPhuTrach
                                            Where ((@TenLoaiPhuTrach is null) or (TenLoaiPhuTrach Like N'%' + @TenLoaiPhuTrach + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLoaiPhuTrach", string.IsNullOrEmpty(tenLoaiPhuTrach) ? (object)DBNull.Value : tenLoaiPhuTrach.Trim()));
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

        public LoaiPhuTrachResVM GetLoaiPhuTrach(int? loaiPhuTrachId)
        {
            try
            {
                LoaiPhuTrachResVM loaiPhuTrachResVM = new LoaiPhuTrachResVM();
                if (loaiPhuTrachId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From LoaiPhuTrach Where LoaiPhuTrachId = @LoaiPhuTrachId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LoaiPhuTrachId", loaiPhuTrachId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                loaiPhuTrachResVM = MapDataHelper<LoaiPhuTrachResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return loaiPhuTrachResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLoaiPhuTrach(UpdateLoaiPhuTrachReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLoaiPhuTrach))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From LoaiPhuTrach Where (LoaiPhuTrachId != @LoaiPhuTrachId) and (MaLoaiPhuTrach = @MaLoaiPhuTrach)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LoaiPhuTrachId", reqVM.LoaiPhuTrachId.Value));
                            command.Parameters.Add(new SqlParameter("@MaLoaiPhuTrach", reqVM.MaLoaiPhuTrach.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại phụ trách đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update LoaiPhuTrach
                                            Set MaLoaiPhuTrach = @MaLoaiPhuTrach, TenLoaiPhuTrach = @TenLoaiPhuTrach
                                            Where LoaiPhuTrachId = @LoaiPhuTrachId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LoaiPhuTrachId", reqVM.LoaiPhuTrachId.Value));
                        command.Parameters.Add(new SqlParameter("@MaLoaiPhuTrach", string.IsNullOrEmpty(reqVM.MaLoaiPhuTrach) ? "" : reqVM.MaLoaiPhuTrach.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenLoaiPhuTrach", string.IsNullOrEmpty(reqVM.TenLoaiPhuTrach) ? "" : reqVM.TenLoaiPhuTrach.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteLoaiPhuTrach(DeleteLoaiPhuTrachReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete LoaiPhuTrach Where LoaiPhuTrachId = @LoaiPhuTrachId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LoaiPhuTrachId", reqVM.LoaiPhuTrachId.HasValue ? reqVM.LoaiPhuTrachId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddLoaiPhuTrach(AddLoaiPhuTrachReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLoaiPhuTrach))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From LoaiPhuTrach Where (MaLoaiPhuTrach = @MaLoaiPhuTrach)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaLoaiPhuTrach", reqVM.MaLoaiPhuTrach.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại phụ trách đã tồn tại");
                    }
                }

                LoaiPhuTrach loaiPhuTrach = new LoaiPhuTrach();
                loaiPhuTrach.MaLoaiPhuTrach = string.IsNullOrEmpty(reqVM.MaLoaiPhuTrach) ? "" : reqVM.MaLoaiPhuTrach.Trim();
                loaiPhuTrach.TenLoaiPhuTrach = string.IsNullOrEmpty(reqVM.TenLoaiPhuTrach) ? "" : reqVM.TenLoaiPhuTrach.Trim();
                DbContext.LoaiPhuTraches.Add(loaiPhuTrach);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region loai nguon cap
        public ListWithPaginationResVM GetNguonCaps(string tenNguonCap, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenNguonCap", tenNguonCap);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From NguonCap
                                            Where ((@TenNguonCap is null) or (TenNguonCap Like N'%' + @TenNguonCap + '%'))
                                            Order by TenNguonCap
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenNguonCap", string.IsNullOrEmpty(tenNguonCap) ? (object)DBNull.Value : tenNguonCap.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<NguonCapResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var nguonCapResVM in (List<NguonCapResVM>)listWithPaginationResVM.Objects)
                {
                    nguonCapResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From NguonCap
                                            Where ((@TenNguonCap is null) or (TenNguonCap Like N'%' + @TenNguonCap + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenNguonCap", string.IsNullOrEmpty(tenNguonCap) ? (object)DBNull.Value : tenNguonCap.Trim()));
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

        public List<NguonCapResVM> GetNguonCaps()
        {
            try
            {
                List<NguonCapResVM> nguonCapResVMs = new List<NguonCapResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From NguonCap";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            nguonCapResVMs = MapDataHelper<NguonCapResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return nguonCapResVMs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public NguonCapResVM GetNguonCap(int? nguonCapId)
        {
            try
            {
                NguonCapResVM nguonCapResVM = new NguonCapResVM();
                if (nguonCapId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From NguonCap Where NguonCapId = @NguonCapId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NguonCapId", nguonCapId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                nguonCapResVM = MapDataHelper<NguonCapResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return nguonCapResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNguonCap(UpdateNguonCapReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaNguonCap))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From NguonCap Where (NguonCapId != @NguonCapId) and (MaNguonCap = @MaNguonCap)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NguonCapId", reqVM.NguonCapId.Value));
                            command.Parameters.Add(new SqlParameter("@MaNguonCap", reqVM.MaNguonCap.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại nguồn cấp đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update NguonCap
                                            Set MaNguonCap = @MaNguonCap, TenNguonCap = @TenNguonCap
                                            Where NguonCapId = @NguonCapId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@NguonCapId", reqVM.NguonCapId.Value));
                        command.Parameters.Add(new SqlParameter("@MaNguonCap", string.IsNullOrEmpty(reqVM.MaNguonCap) ? "" : reqVM.MaNguonCap.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenNguonCap", string.IsNullOrEmpty(reqVM.TenNguonCap) ? "" : reqVM.TenNguonCap.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteNguonCap(DeleteNguonCapReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete NguonCap Where NguonCapId = @NguonCapId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@NguonCapId", reqVM.NguonCapId.HasValue ? reqVM.NguonCapId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddNguonCap(AddNguonCapReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaNguonCap))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From NguonCap Where (MaNguonCap = @MaNguonCap)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaNguonCap", reqVM.MaNguonCap.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại nguồn cấp đã tồn tại");
                    }
                }

                NguonCap nguonCap = new NguonCap();
                nguonCap.MaNguonCap = string.IsNullOrEmpty(reqVM.MaNguonCap) ? "" : reqVM.MaNguonCap.Trim();
                nguonCap.TenNguonCap = string.IsNullOrEmpty(reqVM.TenNguonCap) ? "" : reqVM.TenNguonCap.Trim();
                DbContext.NguonCaps.Add(nguonCap);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region muc dich su dung
        public ListWithPaginationResVM GetMucDichSuDungs(string tenMucDichSuDung, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenMucDichSuDung", tenMucDichSuDung);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From MucDichSuDung
                                            Where ((@TenMucDichSuDung is null) or (TenMucDichSuDung Like N'%' + @TenMucDichSuDung + '%'))
                                            Order by TenMucDichSuDung
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenMucDichSuDung", string.IsNullOrEmpty(tenMucDichSuDung) ? (object)DBNull.Value : tenMucDichSuDung.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<MucDichSuDungResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var mucDichSuDungResVM in (List<MucDichSuDungResVM>)listWithPaginationResVM.Objects)
                {
                    mucDichSuDungResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From MucDichSuDung
                                            Where ((@TenMucDichSuDung is null) or (TenMucDichSuDung Like N'%' + @TenMucDichSuDung + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenMucDichSuDung", string.IsNullOrEmpty(tenMucDichSuDung) ? (object)DBNull.Value : tenMucDichSuDung.Trim()));
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

        public List<MucDichSuDungResVM> GetMucDichSuDungs()
        {
            try
            {
                List<MucDichSuDungResVM> mucDichSuDungResVMs = new List<MucDichSuDungResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From MucDichSuDung";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            mucDichSuDungResVMs = MapDataHelper<MucDichSuDungResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return mucDichSuDungResVMs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public MucDichSuDungResVM GetMucDichSuDung(int? mucDichSuDungId)
        {
            try
            {
                MucDichSuDungResVM mucDichSuDungResVM = new MucDichSuDungResVM();
                if (mucDichSuDungId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From MucDichSuDung Where MucDichSuDungId = @MucDichSuDungId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MucDichSuDungId", mucDichSuDungId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                mucDichSuDungResVM = MapDataHelper<MucDichSuDungResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return mucDichSuDungResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMucDichSuDung(UpdateMucDichSuDungReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaMucDichSuDung))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From MucDichSuDung Where (MucDichSuDungId != @MucDichSuDungId) and (MaMucDichSuDung = @MaMucDichSuDung)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MucDichSuDungId", reqVM.MucDichSuDungId.Value));
                            command.Parameters.Add(new SqlParameter("@MaMucDichSuDung", reqVM.MaMucDichSuDung.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã mục đích sử dụng đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update MucDichSuDung
                                            Set MaMucDichSuDung = @MaMucDichSuDung, TenMucDichSuDung = @TenMucDichSuDung
                                            Where MucDichSuDungId = @MucDichSuDungId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@MucDichSuDungId", reqVM.MucDichSuDungId.Value));
                        command.Parameters.Add(new SqlParameter("@MaMucDichSuDung", string.IsNullOrEmpty(reqVM.MaMucDichSuDung) ? "" : reqVM.MaMucDichSuDung.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenMucDichSuDung", string.IsNullOrEmpty(reqVM.TenMucDichSuDung) ? "" : reqVM.TenMucDichSuDung.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteMucDichSuDung(DeleteMucDichSuDungReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete MucDichSuDung Where MucDichSuDungId = @MucDichSuDungId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@MucDichSuDungId", reqVM.MucDichSuDungId.HasValue ? reqVM.MucDichSuDungId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddMucDichSuDung(AddMucDichSuDungReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaMucDichSuDung))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From MucDichSuDung Where (MaMucDichSuDung = @MaMucDichSuDung)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaMucDichSuDung", reqVM.MaMucDichSuDung.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã mục đích sử dụng đã tồn tại");
                    }
                }

                MucDichSuDung mucDichSuDung = new MucDichSuDung();
                mucDichSuDung.MaMucDichSuDung = string.IsNullOrEmpty(reqVM.MaMucDichSuDung) ? "" : reqVM.MaMucDichSuDung.Trim();
                mucDichSuDung.TenMucDichSuDung = string.IsNullOrEmpty(reqVM.TenMucDichSuDung) ? "" : reqVM.TenMucDichSuDung.Trim();
                DbContext.MucDichSuDungs.Add(mucDichSuDung);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region nguon ngan sach
        public ListWithPaginationResVM GetNguonNganSachs(string tenNguonNganSach, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenNguonNganSach", tenNguonNganSach);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From NguonNganSach
                                            Where ((@TenNguonNganSach is null) or (TenNguonNganSach Like N'%' + @TenNguonNganSach + '%'))
                                            Order by TenNguonNganSach
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenNguonNganSach", string.IsNullOrEmpty(tenNguonNganSach) ? (object)DBNull.Value : tenNguonNganSach.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<NguonNganSachResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var nguonNganSachResVM in (List<NguonNganSachResVM>)listWithPaginationResVM.Objects)
                {
                    nguonNganSachResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From NguonNganSach
                                            Where ((@TenNguonNganSach is null) or (TenNguonNganSach Like N'%' + @TenNguonNganSach + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenNguonNganSach", string.IsNullOrEmpty(tenNguonNganSach) ? (object)DBNull.Value : tenNguonNganSach.Trim()));
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

        public NguonNganSachResVM GetNguonNganSach(int? nguonNganSachId)
        {
            try
            {
                NguonNganSachResVM nguonNganSachResVM = new NguonNganSachResVM();
                if (nguonNganSachId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From NguonNganSach Where NguonNganSachId = @NguonNganSachId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NguonNganSachId", nguonNganSachId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                nguonNganSachResVM = MapDataHelper<NguonNganSachResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return nguonNganSachResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNguonNganSach(UpdateNguonNganSachReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaNguonNganSach))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From NguonNganSach Where (NguonNganSachId != @NguonNganSachId) and (MaNguonNganSach = @MaNguonNganSach)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NguonNganSachId", reqVM.NguonNganSachId.Value));
                            command.Parameters.Add(new SqlParameter("@MaNguonNganSach", reqVM.MaNguonNganSach.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại nguồn ngân sách đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update NguonNganSach
                                            Set MaNguonNganSach = @MaNguonNganSach, TenNguonNganSach = @TenNguonNganSach
                                            Where NguonNganSachId = @NguonNganSachId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@NguonNganSachId", reqVM.NguonNganSachId.Value));
                        command.Parameters.Add(new SqlParameter("@MaNguonNganSach", string.IsNullOrEmpty(reqVM.MaNguonNganSach) ? "" : reqVM.MaNguonNganSach.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenNguonNganSach", string.IsNullOrEmpty(reqVM.TenNguonNganSach) ? "" : reqVM.TenNguonNganSach.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteNguonNganSach(DeleteNguonNganSachReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete NguonNganSach Where NguonNganSachId = @NguonNganSachId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@NguonNganSachId", reqVM.NguonNganSachId.HasValue ? reqVM.NguonNganSachId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddNguonNganSach(AddNguonNganSachReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaNguonNganSach))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From NguonNganSach Where (MaNguonNganSach = @MaNguonNganSach)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaNguonNganSach", reqVM.MaNguonNganSach.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại nguồn ngân sách đã tồn tại");
                    }
                }

                NguonNganSach nguonNganSach = new NguonNganSach();
                nguonNganSach.MaNguonNganSach = string.IsNullOrEmpty(reqVM.MaNguonNganSach) ? "" : reqVM.MaNguonNganSach.Trim();
                nguonNganSach.TenNguonNganSach = string.IsNullOrEmpty(reqVM.TenNguonNganSach) ? "" : reqVM.TenNguonNganSach.Trim();
                DbContext.NguonNganSaches.Add(nguonNganSach);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region nguon kinh phi cap tren
        public ListWithPaginationResVM GetNguonKinhPhiCapTrens(string tenNguonKinhPhiCapTren, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenNguonKinhPhiCapTren", tenNguonKinhPhiCapTren);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From NguonKinhPhiCapTren
                                            Where ((@TenNguonKinhPhiCapTren is null) or (TenNguonKinhPhiCapTren Like N'%' + @TenNguonKinhPhiCapTren + '%'))
                                            Order by TenNguonKinhPhiCapTren
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenNguonKinhPhiCapTren", string.IsNullOrEmpty(tenNguonKinhPhiCapTren) ? (object)DBNull.Value : tenNguonKinhPhiCapTren.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<NguonKinhPhiCapTrenResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var nguonKinhPhiCapTrenResVM in (List<NguonKinhPhiCapTrenResVM>)listWithPaginationResVM.Objects)
                {
                    nguonKinhPhiCapTrenResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From NguonKinhPhiCapTren
                                            Where ((@TenNguonKinhPhiCapTren is null) or (TenNguonKinhPhiCapTren Like N'%' + @TenNguonKinhPhiCapTren + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenNguonKinhPhiCapTren", string.IsNullOrEmpty(tenNguonKinhPhiCapTren) ? (object)DBNull.Value : tenNguonKinhPhiCapTren.Trim()));
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

        public List<NguonKinhPhiCapTrenResVM> GetNguonKinhPhiCapTrens()
        {
            try
            {
                List<NguonKinhPhiCapTrenResVM> nguonKinhPhiCapTrenResVMs = new List<NguonKinhPhiCapTrenResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From NguonKinhPhiCapTren";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            nguonKinhPhiCapTrenResVMs = MapDataHelper<NguonKinhPhiCapTrenResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return nguonKinhPhiCapTrenResVMs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public NguonKinhPhiCapTrenResVM GetNguonKinhPhiCapTren(int? nguonKinhPhiCapTrenId)
        {
            try
            {
                NguonKinhPhiCapTrenResVM nguonKinhPhiCapTrenResVM = new NguonKinhPhiCapTrenResVM();
                if (nguonKinhPhiCapTrenId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From NguonKinhPhiCapTren Where NguonKinhPhiCapTrenId = @NguonKinhPhiCapTrenId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NguonKinhPhiCapTrenId", nguonKinhPhiCapTrenId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                nguonKinhPhiCapTrenResVM = MapDataHelper<NguonKinhPhiCapTrenResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return nguonKinhPhiCapTrenResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNguonKinhPhiCapTren(UpdateNguonKinhPhiCapTrenReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaNguonKinhPhiCapTren))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From NguonKinhPhiCapTren Where (NguonKinhPhiCapTrenId != @NguonKinhPhiCapTrenId) and (MaNguonKinhPhiCapTren = @MaNguonKinhPhiCapTren)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@NguonKinhPhiCapTrenId", reqVM.NguonKinhPhiCapTrenId.Value));
                            command.Parameters.Add(new SqlParameter("@MaNguonKinhPhiCapTren", reqVM.MaNguonKinhPhiCapTren.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại nguồn kinh phí cấp trên đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update NguonKinhPhiCapTren
                                            Set MaNguonKinhPhiCapTren = @MaNguonKinhPhiCapTren, TenNguonKinhPhiCapTren = @TenNguonKinhPhiCapTren
                                            Where NguonKinhPhiCapTrenId = @NguonKinhPhiCapTrenId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@NguonKinhPhiCapTrenId", reqVM.NguonKinhPhiCapTrenId.Value));
                        command.Parameters.Add(new SqlParameter("@MaNguonKinhPhiCapTren", string.IsNullOrEmpty(reqVM.MaNguonKinhPhiCapTren) ? "" : reqVM.MaNguonKinhPhiCapTren.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenNguonKinhPhiCapTren", string.IsNullOrEmpty(reqVM.TenNguonKinhPhiCapTren) ? "" : reqVM.TenNguonKinhPhiCapTren.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteNguonKinhPhiCapTren(DeleteNguonKinhPhiCapTrenReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete NguonKinhPhiCapTren Where NguonKinhPhiCapTrenId = @NguonKinhPhiCapTrenId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@NguonKinhPhiCapTrenId", reqVM.NguonKinhPhiCapTrenId.HasValue ? reqVM.NguonKinhPhiCapTrenId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddNguonKinhPhiCapTren(AddNguonKinhPhiCapTrenReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaNguonKinhPhiCapTren))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From NguonKinhPhiCapTren Where (MaNguonKinhPhiCapTren = @MaNguonKinhPhiCapTren)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaNguonKinhPhiCapTren", reqVM.MaNguonKinhPhiCapTren.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại nguồn kinh phí cấp trên đã tồn tại");
                    }
                }

                NguonKinhPhiCapTren nguonKinhPhiCapTren = new NguonKinhPhiCapTren();
                nguonKinhPhiCapTren.MaNguonKinhPhiCapTren = string.IsNullOrEmpty(reqVM.MaNguonKinhPhiCapTren) ? "" : reqVM.MaNguonKinhPhiCapTren.Trim();
                nguonKinhPhiCapTren.TenNguonKinhPhiCapTren = string.IsNullOrEmpty(reqVM.TenNguonKinhPhiCapTren) ? "" : reqVM.TenNguonKinhPhiCapTren.Trim();
                DbContext.NguonKinhPhiCapTrens.Add(nguonKinhPhiCapTren);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region khoi lop
        public ListWithPaginationResVM GetKhoiLops(string tenKhoiLop, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenKhoiLop", tenKhoiLop);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From KhoiLop
                                            Where ((@TenKhoiLop is null) or (TenKhoiLop Like N'%' + @TenKhoiLop + '%'))
                                            Order by TenKhoiLop
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenKhoiLop", string.IsNullOrEmpty(tenKhoiLop) ? (object)DBNull.Value : tenKhoiLop.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<KhoiLopResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var khoiLopResVM in (List<KhoiLopResVM>)listWithPaginationResVM.Objects)
                {
                    khoiLopResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From KhoiLop
                                            Where ((@TenKhoiLop is null) or (TenKhoiLop Like N'%' + @TenKhoiLop + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenKhoiLop", string.IsNullOrEmpty(tenKhoiLop) ? (object)DBNull.Value : tenKhoiLop.Trim()));
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

        public List<KhoiLopResVM> GetKhoiLops()
        {
            try
            {
                List<KhoiLopResVM> khoiLopResVMs = new List<KhoiLopResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From KhoiLop";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            khoiLopResVMs = MapDataHelper<KhoiLopResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return khoiLopResVMs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public KhoiLopResVM GetKhoiLop(int? khoiLopId)
        {
            try
            {
                KhoiLopResVM khoiLopResVM = new KhoiLopResVM();
                if (khoiLopId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From KhoiLop Where KhoiLopId = @KhoiLopId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@KhoiLopId", khoiLopId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                khoiLopResVM = MapDataHelper<KhoiLopResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return khoiLopResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateKhoiLop(UpdateKhoiLopReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaKhoiLop))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From KhoiLop Where (KhoiLopId != @KhoiLopId) and (MaKhoiLop = @MaKhoiLop)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@KhoiLopId", reqVM.KhoiLopId.Value));
                            command.Parameters.Add(new SqlParameter("@MaKhoiLop", reqVM.MaKhoiLop.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã khối lớp đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update KhoiLop
                                            Set MaKhoiLop = @MaKhoiLop, TenKhoiLop = @TenKhoiLop
                                            Where KhoiLopId = @KhoiLopId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@KhoiLopId", reqVM.KhoiLopId.Value));
                        command.Parameters.Add(new SqlParameter("@MaKhoiLop", string.IsNullOrEmpty(reqVM.MaKhoiLop) ? "" : reqVM.MaKhoiLop.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenKhoiLop", string.IsNullOrEmpty(reqVM.TenKhoiLop) ? "" : reqVM.TenKhoiLop.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteKhoiLop(DeleteKhoiLopReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete KhoiLop Where KhoiLopId = @KhoiLopId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@KhoiLopId", reqVM.KhoiLopId.HasValue ? reqVM.KhoiLopId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddKhoiLop(AddKhoiLopReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaKhoiLop))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From KhoiLop Where (MaKhoiLop = @MaKhoiLop)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaKhoiLop", reqVM.MaKhoiLop.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã khối lớp đã tồn tại");
                    }
                }

                KhoiLop khoiLop = new KhoiLop();
                khoiLop.MaKhoiLop = string.IsNullOrEmpty(reqVM.MaKhoiLop) ? "" : reqVM.MaKhoiLop.Trim();
                khoiLop.TenKhoiLop = string.IsNullOrEmpty(reqVM.TenKhoiLop) ? "" : reqVM.TenKhoiLop.Trim();
                DbContext.KhoiLops.Add(khoiLop);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region mon hoc
        public ListWithPaginationResVM GetMonHocs(string tenMonHoc, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenMonHoc", tenMonHoc);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From MonHoc
                                            Where ((@TenMonHoc is null) or (TenMonHoc Like N'%' + @TenMonHoc + '%'))
                                            Order by TenMonHoc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenMonHoc", string.IsNullOrEmpty(tenMonHoc) ? (object)DBNull.Value : tenMonHoc.Trim()));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<MonHocResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var monHocResVM in (List<MonHocResVM>)listWithPaginationResVM.Objects)
                {
                    monHocResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From MonHoc
                                            Where ((@TenMonHoc is null) or (TenMonHoc Like N'%' + @TenMonHoc + '%'))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenMonHoc", string.IsNullOrEmpty(tenMonHoc) ? (object)DBNull.Value : tenMonHoc.Trim()));
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

        public List<MonHocResVM> GetMonHocs()
        {
            try
            {
                List<MonHocResVM> monHocResVMs = new List<MonHocResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From MonHoc Order by TenMonHoc";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            monHocResVMs = MapDataHelper<MonHocResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return monHocResVMs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public MonHocResVM GetMonHoc(int? monHocId)
        {
            try
            {
                MonHocResVM monHocResVM = new MonHocResVM();
                if (monHocId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From MonHoc Where MonHocId = @MonHocId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                monHocResVM = MapDataHelper<MonHocResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return monHocResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateMonHoc(UpdateMonHocReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaMonHoc))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From MonHoc Where (MonHocId != @MonHocId) and (MaMonHoc = @MaMonHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MonHocId", reqVM.MonHocId.Value));
                            command.Parameters.Add(new SqlParameter("@MaMonHoc", reqVM.MaMonHoc.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã môn học đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update MonHoc
                                            Set TenMonHoc = @TenMonHoc, MaMonHoc = @MaMonHoc
                                            Where MonHocId = @MonHocId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@MonHocId", reqVM.MonHocId.Value));
                        command.Parameters.Add(new SqlParameter("@MaMonHoc", string.IsNullOrEmpty(reqVM.MaMonHoc) ? "" : reqVM.MaMonHoc.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenMonHoc", string.IsNullOrEmpty(reqVM.TenMonHoc) ? "" : reqVM.TenMonHoc.Trim()));
                        command.ExecuteNonQuery();
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

        public void DeleteMonHoc(DeleteMonHocReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete MonHoc Where MonHocId = @MonHocId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@MonHocId", reqVM.MonHocId.HasValue ? reqVM.MonHocId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddMonHoc(AddMonHocReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaMonHoc))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From MonHoc Where (MaMonHoc = @MaMonHoc)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaMonHoc", reqVM.MaMonHoc.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã môn học đã tồn tại");
                    }
                }

                MonHoc monHoc = new MonHoc();
                monHoc.MaMonHoc = string.IsNullOrEmpty(reqVM.MaMonHoc) ? "" : reqVM.MaMonHoc.Trim();
                monHoc.TenMonHoc = string.IsNullOrEmpty(reqVM.TenMonHoc) ? "" : reqVM.TenMonHoc.Trim();
                DbContext.MonHocs.Add(monHoc);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region lop
        public ListWithPaginationResVM GetLops(string tenLop, int? khoiLopId, int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();
                queryParamsDict.Add("TenLop", tenLop);
                queryParamsDict.Add("KhoiLopId", khoiLopId.HasValue ? khoiLopId.Value.ToString() : null);

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select a.*, b.TenKhoiLop From Lop as a
                                            Left Join KhoiLop as b
                                            on a.KhoiLopId = b.KhoiLopId
                                            Where ((@TenLop is null) or (a.TenLop Like N'%' + @TenLop + '%'))
                                                and ((@KhoiLopId is null) or (a.KhoiLopId = @KhoiLopId))
                                            Order by TenLop
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLop", string.IsNullOrEmpty(tenLop) ? (object)DBNull.Value : tenLop.Trim()));
                        command.Parameters.Add(new SqlParameter("@KhoiLopId", khoiLopId.HasValue ? khoiLopId.Value : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<LopResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var lopResVM in (List<LopResVM>)listWithPaginationResVM.Objects)
                {
                    lopResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From Lop
                                            Where ((@TenLop is null) or (TenLop Like N'%' + @TenLop + '%'))
                                                and ((@KhoiLopId is null) or (KhoiLopId = @KhoiLopId))";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TenLop", string.IsNullOrEmpty(tenLop) ? (object)DBNull.Value : tenLop.Trim()));
                        command.Parameters.Add(new SqlParameter("@KhoiLopId", khoiLopId.HasValue ? khoiLopId.Value : (object)DBNull.Value));
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

        public LopResVM GetLop(int? lopId)
        {
            try
            {
                LopResVM lopResVM = new LopResVM();
                if (lopId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From Lop Where LopId = @LopId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LopId", lopId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                lopResVM = MapDataHelper<LopResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return lopResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLop(UpdateLopReqVM reqVM)
        {
            try
            {
                // check trung ma
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLop))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From Lop Where (LopId != @LopId) and (MaLop = @MaLop)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@LopId", reqVM.LopId.Value));
                            command.Parameters.Add(new SqlParameter("@MaLop", reqVM.MaLop.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã lớp đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update Lop
                                            Set TenLop = @TenLop, MaLop = @MaLop, KhoiLopId = @KhoiLopId
                                            Where LopId = @LopId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LopId", reqVM.LopId.Value));
                        command.Parameters.Add(new SqlParameter("@MaLop", string.IsNullOrEmpty(reqVM.MaLop) ? "" : reqVM.MaLop.Trim()));
                        command.Parameters.Add(new SqlParameter("@TenLop", string.IsNullOrEmpty(reqVM.TenLop) ? "" : reqVM.TenLop.Trim()));
                        command.Parameters.Add(new SqlParameter("@KhoiLopId", reqVM.KhoiLopId.HasValue ? reqVM.KhoiLopId: (object)DBNull.Value));
                        command.ExecuteNonQuery();
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

        public void DeleteLop(DeleteLopReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete Lop Where LopId = @LopId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@LopId", reqVM.LopId.HasValue ? reqVM.LopId.Value : -1));
                        command.ExecuteNonQuery();
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

        public void AddLop(AddLopReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (!string.IsNullOrEmpty(reqVM.MaLop))
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From Lop Where (MaLop = @MaLop)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@MaLop", reqVM.MaLop.Trim()));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã lớp đã tồn tại");
                    }
                }

                Lop lop = new Lop();
                lop.MaLop = string.IsNullOrEmpty(reqVM.MaLop) ? "" : reqVM.MaLop.Trim();
                lop.TenLop = string.IsNullOrEmpty(reqVM.TenLop) ? "" : reqVM.TenLop.Trim();
                lop.KhoiLopId = reqVM.KhoiLopId;
                DbContext.Lops.Add(lop);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region kho phong
        public List<KhoPhongResVM> GetKhoPhongs()
        {
            try
            {
                List<KhoPhongResVM> khoPhongResVMs = new List<KhoPhongResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From KhoPhong Order by TenKhoPhong";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            khoPhongResVMs = MapDataHelper<KhoPhongResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return khoPhongResVMs;
            }
            catch(Exception ex){
                throw ex;
            }
        }
        #endregion

        #region thiet bi
        public List<ThietBiResVM> GetThietBis(int? monHocId)
        {
            try
            {
                List<ThietBiResVM> thietBiResVMs = new List<ThietBiResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From ThietBi Where MonHocId = @MonHocId ";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@MonHocId", monHocId.HasValue ? monHocId.Value : -1));
                        using (var reader = command.ExecuteReader())
                        {
                            thietBiResVMs = MapDataHelper<ThietBiResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return thietBiResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region tiet hoc
        public ListWithPaginationResVM GetTietHocs(int? currentPage, int? pageSize)
        {
            try
            {
                if (!currentPage.HasValue) currentPage = 1;
                if (!pageSize.HasValue) pageSize = 10;
                Dictionary<string, string> queryParamsDict = new Dictionary<string, string>();

                ListWithPaginationResVM listWithPaginationResVM = new ListWithPaginationResVM();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From TietHoc
                                            Order By BuoiTrongNgay, TenTietHoc
                                            Offset @Offset Rows Fetch Next @Next Rows Only";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@Offset", (currentPage.Value - 1) * pageSize.Value));
                        command.Parameters.Add(new SqlParameter("@Next", pageSize.Value));
                        using (var reader = command.ExecuteReader())
                        {
                            listWithPaginationResVM.Objects = MapDataHelper<TietHocResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                // add stt
                long i = 1;
                foreach (var tietHocResVM in (List<TietHocResVM>)listWithPaginationResVM.Objects)
                {
                    tietHocResVM.STT = (currentPage - 1) * pageSize + i++;
                }


                long totalRow = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select Count(*) From TietHoc";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
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

        public List<TietHocResVM> GetTietHocs()
        {
            try
            {
                List<TietHocResVM> tietHocResVMs = new List<TietHocResVM>();
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Select * From TietHoc
                                            Order By BuoiTrongNgay, TenTietHoc";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            tietHocResVMs = MapDataHelper<TietHocResVM>.MapList(reader);
                        }
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return tietHocResVMs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TietHocResVM GetTietHoc(int? tietHocId)
        {
            try
            {
                TietHocResVM tietHocResVM = new TietHocResVM();
                if (tietHocId.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select * From TietHoc Where TietHocId = @TietHocId";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@TietHocId", tietHocId.Value));
                            using (var reader = command.ExecuteReader())
                            {
                                tietHocResVM = MapDataHelper<TietHocResVM>.Map(reader);
                            }
                        }
                        finally
                        {
                            command.Connection.Close();
                        }
                    }

                    return tietHocResVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateTietHoc(UpdateTietHocReqVM reqVM)
        {
            try
            {
                // check trung ma loai loai bai hoc
                int totalRow = 0;
                if (reqVM.TenTietHoc.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From TietHoc Where (TietHocId != @TietHocId) and (TenTietHoc = @TenTietHoc) and (BuoiTrongNgay = @BuoiTrongNgay)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@TietHocId", reqVM.TietHocId.Value));
                            command.Parameters.Add(new SqlParameter("@TenTietHoc", reqVM.TenTietHoc.Value));
                            command.Parameters.Add(new SqlParameter("@BuoiTrongNgay", reqVM.BuoiTrongNgay.Value));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Tiết học đã tồn tại");
                    }
                }

                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Update TietHoc
                                            Set TenTietHoc = @TenTietHoc, BuoiTrongNgay = @BuoiTrongNgay
                                            Where TietHocId = @TietHocId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TietHocId", reqVM.TietHocId.HasValue ? reqVM.TietHocId : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@TenTietHoc", reqVM.TenTietHoc.HasValue ? reqVM.TenTietHoc : (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@BuoiTrongNgay", reqVM.BuoiTrongNgay.HasValue ? reqVM.BuoiTrongNgay : (object)DBNull.Value));
                        command.ExecuteNonQuery();
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

        public void DeleteTietHoc(DeleteTietHocReqVM reqVM)
        {
            try
            {
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();

                    try
                    {
                        string sqlString = @"Delete TietHoc Where TietHocId = @TietHocId";
                        command.CommandText = sqlString;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@TietHocId", reqVM.TietHocId.HasValue ? reqVM.TietHocId : (object)DBNull.Value));
                        command.ExecuteNonQuery();
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

        public void AddTietHoc(AddTietHocReqVM reqVM)
        {
            try
            {
                // check trung ma loai thiet bi
                int totalRow = 0;
                if (reqVM.TenTietHoc.HasValue)
                {
                    using (var command = DbContext.Database.Connection.CreateCommand())
                    {
                        bool wasOpen = command.Connection.State == ConnectionState.Open;
                        if (!wasOpen) command.Connection.Open();

                        try
                        {
                            string sqlString = @"Select Count(*) From TietHoc Where (TenTietHoc = @TenTietHoc) and (BuoiTrongNgay = @BuoiTrongNgay)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            command.Parameters.Add(new SqlParameter("@TenTietHoc", reqVM.TenTietHoc.Value));
                            command.Parameters.Add(new SqlParameter("@BuoiTrongNgay", reqVM.BuoiTrongNgay.Value));
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

                    if (totalRow > 0)
                    {
                        throw new WarningException("Mã loại nguồn cấp đã tồn tại");
                    }
                }

                TietHoc tietHoc = new TietHoc();
                tietHoc.TenTietHoc = reqVM.TenTietHoc;
                tietHoc.BuoiTrongNgay = reqVM.BuoiTrongNgay;
                DbContext.TietHocs.Add(tietHoc);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
