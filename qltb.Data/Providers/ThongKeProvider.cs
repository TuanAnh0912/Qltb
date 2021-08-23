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
    public class ThongKeProvider : ApplicationDbcontext
    {
        public ThongKeChungResVM GetThongKeChung()
        {
            try
            {
                ThongKeChungResVM thongKeChungResVM = new ThongKeChungResVM();
                thongKeChungResVM.TongSoThietBiTrongKho = 0;
                thongKeChungResVM.TongSoThietBiHongMat = 0;
                thongKeChungResVM.TongSoThietBiDangMuon = 0;
                thongKeChungResVM.TongSoThietBiCoTheSuDung = 0;
                thongKeChungResVM.TongSoPhieuDangMuon = 0;
                thongKeChungResVM.TongSoPhieuDaTra = 0;
                thongKeChungResVM.TongSoPhieuDangKy = 0;
                thongKeChungResVM.TongSoThietBiHong = 0;
                thongKeChungResVM.TongSoThietBiMat = 0;
                thongKeChungResVM.TongSoThietBiDaSua = 0;
                using (var command = DbContext.Database.Connection.CreateCommand())
                {
                    bool wasOpen = command.Connection.State == ConnectionState.Open;
                    if (!wasOpen) command.Connection.Open();
                    command.CommandType = CommandType.Text;
                    try
                    {
                        // tong so thiet bi trong kho
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(SoLuong), 0) From KhoThietBi";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoThietBiTrongKho = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so thiet bi hong mat
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull((Select Sum(SoLuongHong) From ChiTietPhieuGhiHongThietBi), 0) - IsNull((Select Sum(SoLuongSuaChua) From ChiTietPhieuSuaChua), 0) 
		                                        +  IsNull((Select Sum(SoLuongMat) From ChiTietPhieuGhiMatThietBi), 0) ";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoThietBiHongMat = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so thiet bi dang muon
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(SoLuong), 0) - IsNull(Sum(SoLuongConLai), 0) From KhoThietBi";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoThietBiDangMuon = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so thiet bi co the su dung
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(SoLuongConLai), 0) From KhoThietBi";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoThietBiCoTheSuDung = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // thong ke phieu muon
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select top 5 a.MaPhieuMuon, a.NgayMuon, b.TenGiaoVien, c.TenTrangThai, c.MaMau
                                                From PhieuMuon as a
                                                Left Join GiaoVien as b
                                                on a.GiaoVienId = b.GiaoVienId
                                                Left Join TrangThaiPhieuMuon as c
                                                on a.TrangThaiPhieuMuonId = c.TrangThaiId
                                                Where a.LoaiPhieuMuonId = 1
                                                Order by a.NgayMuon Desc";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                               thongKeChungResVM.PhieuMuonTBs = MapDataHelper<PhieuMuonResVM>.MapList(reader);
                            }
                        }
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select top 5 a.MaPhieuMuon, a.NgayMuon, b.TenGiaoVien, c.TenTrangThai, k.TenKhoPhong, c.MaMau
                                                From PhieuMuon as a
                                                Left Join GiaoVien as b
                                                on a.GiaoVienId = b.GiaoVienId
                                                Left Join TrangThaiPhieuMuon as c
                                                on a.TrangThaiPhieuMuonId = c.TrangThaiId
												Left join KhoPhong as k on a.KhoPhongId = k.KhoPhongId
                                                Where a.LoaiPhieuMuonId = 2
                                                Order by a.NgayMuon Desc";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                thongKeChungResVM.PhieuMuonPhongs = MapDataHelper<PhieuMuonResVM>.MapList(reader);
                            }
                        }
                        // tong so phieu muon dang muon
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select Count(*) From PhieuMuon Where (TrangThaiPhieuMuonId = 1) and (LoaiPhieuMuonId = 1)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoPhieuDangMuon = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so phieu muon da tra
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select Count(*) From PhieuMuon Where (TrangThaiPhieuMuonId = 2) and (LoaiPhieuMuonId = 1)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoPhieuDaTra = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so phieu muon dang ky
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select Count(*) From PhieuMuon Where (TrangThaiPhieuMuonId = 3) and (LoaiPhieuMuonId = 1)";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoPhieuDangKy = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so thiet bi hong
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull((Select Sum(SoLuongHong) From ChiTietPhieuGhiHongThietBi), 0) - IsNull((Select Sum(SoLuongSuaChua) From ChiTietPhieuSuaChua), 0) ";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoThietBiHong = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so thiet bi mat
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(SoLuongMat), 0) From ChiTietPhieuGhiMatThietBi";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoThietBiMat = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                        // tong so thiet bi da sua
                        {
                            command.Parameters.Clear();
                            string sqlString = @"Select IsNull(Sum(SoLuongSuaChua), 0) From ChiTietPhieuSuaChua";
                            command.CommandText = sqlString;
                            command.CommandType = CommandType.Text;
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    thongKeChungResVM.TongSoThietBiDaSua = Convert.ToInt32(reader[0]);
                                }
                            }
                        }

                    }

                    finally
                    {
                        command.Connection.Close();
                    }
                }

                return thongKeChungResVM;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        
    }
}
