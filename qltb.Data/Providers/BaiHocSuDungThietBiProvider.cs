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
    public class BaiHocSuDungThietBiProvider : ApplicationDbcontext
    {
        public List<BaiHocCoSuDungThietBiResVM> GettAllBaiHocSuDungTB()
        {
            try
            {
                string commnad = @"select ROW_NUMBER() Over(order by a.BaiHocSuDungThietBiId ASC) as 'STT',a.BaiHocSuDungThietBiId,b.MonHocId,c.ChuongTrinhHocId,d.HocKyId,e.LoaiBaiHocId,f.LopId,a.TenBaiHoc,b.TenMonHoc,f.TenLop,c.TenChuongTrinhHoc,a.TietTheoPPTC,a.SoTiet,CONVERT(varchar,a.ThoiGian,103) as 'ThoiGian',d.TenHocKy,e.TenLoaiBaiHoc from BaiHocSuDungThietBi as a
                                   left join MonHoc as b on a.MonHocId=b.MonHocId
                                   left join ChuongTrinhHoc as c on a.ChuongTrinhHocId=c.ChuongTrinhHocId
                                   left join HocKy as d on a.HocKyId=d.HocKyId
                                   left join LoaiBaiHoc as e on a.LoaiBaiHocId=e.LoaiBaiHocId
                                   left join Lop as f on a.LopId=f.LopId
                                   where a.IsDelete=0";
                var lst = DbContext.Database.SqlQuery<BaiHocCoSuDungThietBiResVM>(commnad).ToList();
                return lst;
            }
            catch (Exception e)
            {

                return new List<BaiHocCoSuDungThietBiResVM>();
            }
        }
        public ResponseModel ChangeThietBiBaiHoc(int BaiHocId, List<AddThietBiBaiHocReqVM> lst)
        {
            using (var DbTransec = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var lstRs = new ResponseModel();
                    lstRs.list_message = new List<string>();
                    var lstBaiHocThietBiOld = DbContext.ChiTietBaiHocSuDungThietBis.Where(t => t.BaiHocSuDungThietBiId == BaiHocId).ToList();
                    if (lstBaiHocThietBiOld != null)
                    {
                        DbContext.ChiTietBaiHocSuDungThietBis.RemoveRange(lstBaiHocThietBiOld);
                        DbContext.SaveChanges();
                    }
                    var lstThietBiBaiHocNew = new List<ChiTietBaiHocSuDungThietBi>();
                    if (lst == null)
                    {
                        DbTransec.Commit();
                        return new ResponseModel(true, "success", "Thêm thành công");
                    }
                    foreach (var item in lst)
                    {
                        var model = new ChiTietBaiHocSuDungThietBi();
                        model.BaiHocSuDungThietBiId = BaiHocId;
                        model.ThietBiId = item.ThietBiId;
                        model.SoLuong = item.SoLuong;
                        model.IsDelete = false;
                        lstThietBiBaiHocNew.Add(model);
                    }
                    DbContext.ChiTietBaiHocSuDungThietBis.AddRange(lstThietBiBaiHocNew);
                    DbContext.SaveChanges();
                    DbTransec.Commit();
                    return new ResponseModel(true, "success", "Thêm thành công");
                }
                catch (Exception e)
                {
                    DbTransec.Rollback();
                    return new ResponseModel(false, "fail", "Thêm thất bại");
                }
            }
        }
        public bool CheckBaiHocThietBi(int id)
        {
            try
            {
                var Check = DbContext.ChiTietBaiHocSuDungThietBis.FirstOrDefault(x => x.BaiHocSuDungThietBiId == id);
                if (Check == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public BaiGiangChuaThietBiResVM GetThietBiCuaBaiHoc(int id, string search, int? loaiThietBiId)
        {
            try
            {
                var rs = new BaiGiangChuaThietBiResVM();
                string command1 = @"select * from BaiHocSuDungThietBi as a 
                                    left join MonHoc as b on a.MonHocId=b.MonHocId
                                    where a.BaiHocSuDungThietBiId=@id";
                rs = DbContext.Database.SqlQuery<BaiGiangChuaThietBiResVM>(command1, new SqlParameter("@id", id)).FirstOrDefault();

                string command = "";
                if (CheckBaiHocThietBi(id))
                {
                    command = @"select ROW_NUMBER() Over(order by a.BaiHocSuDungThietBiId ASC)  as 'STT',c.MaThietBi,a.BaiHocSuDungThietBiId,c.ThietBiId,f.TenMonHoc,c.TenThietBi,a.BaiHocSuDungThietBiId,d.TenLoaiThietBi,e.TenDonViTinh,b.SoLuong,b.IsDelete,IIF(SoLuong is null,0,1) as 'CheckThietBi'  from BaiHocSuDungThietBi as a
                                   inner join ChiTietBaiHocSuDungThietBi as b on b.BaiHocSuDungThietBiId=a.BaiHocSuDungThietBiId
                                   right join ThietBi as c on b.ThietBiId=c.ThietBiId 
                                   left join LoaiThietBi as d on d.LoaiThietBiId=c.LoaiThietBiId
                                   left join DonViTinh as e on e.DonViTinhId=c.DonViTinhId
								   left join MonHoc as f on a.MonHocId=f.MonHocId
								   where ((@loaiThietBiId is null) or (d.LoaiThietBiId = @loaiThietBiId)) and (a.BaiHocSuDungThietBiId is null or a.BaiHocSuDungThietBiId=@id) and ((@search is null) or (c.TenThietBi like N'%'+@search+'%')) 
                                   order by CheckThietBi DESC";
                    rs.lstThietBi = DbContext.Database.SqlQuery<ThietBiCuaBaiGiangResVM>(command, new SqlParameter("@id", id),
                                                                                                   new SqlParameter("@search", search == null ? (object)DBNull.Value : search),
                                                                                                   new SqlParameter("@loaiThietBiId", loaiThietBiId == null ? (object)DBNull.Value : loaiThietBiId)).ToList();

                }
                else
                {
                    command = @"select ROW_NUMBER() Over(order by c.ThietBiId ASC)  as 'STT',c.MaThietBi,c.ThietBiId,f.TenMonHoc,c.TenThietBi,d.TenLoaiThietBi,e.TenDonViTinh ,0 as 'SoLuong',0 as 'CheckThietBi'
								   from ThietBi as c
                                   left join LoaiThietBi as d on d.LoaiThietBiId=c.LoaiThietBiId
                                   left join DonViTinh as e on e.DonViTinhId=c.DonViTinhId
								   left join MonHoc as f on c.MonHocId=f.MonHocId";
                    rs.lstThietBi = DbContext.Database.SqlQuery<ThietBiCuaBaiGiangResVM>(command).ToList();
                }
                return rs;
            }
            catch (Exception e)
            {
                return new BaiGiangChuaThietBiResVM();
            }
        }
        public BaiHocCoSuDungThietBiResVM GettBaiHocSuDungTBById(int id)
        {
            try
            {
                string commnad = @"select ROW_NUMBER() Over(order by a.BaiHocSuDungThietBiId ASC) as 'STT',a.BaiHocSuDungThietBiId,b.MonHocId,c.ChuongTrinhHocId,d.HocKyId,e.LoaiBaiHocId,f.LopId,a.TenBaiHoc,b.TenMonHoc,f.TenLop,c.TenChuongTrinhHoc,a.TietTheoPPTC,a.SoTiet,CONVERT(varchar,a.ThoiGian,103) as 'ThoiGian',d.TenHocKy,e.TenLoaiBaiHoc from BaiHocSuDungThietBi as a
                                   left join MonHoc as b on a.MonHocId=b.MonHocId
                                   left join ChuongTrinhHoc as c on a.ChuongTrinhHocId=c.ChuongTrinhHocId
                                   left join HocKy as d on a.HocKyId=d.HocKyId
                                   left join LoaiBaiHoc as e on a.LoaiBaiHocId=e.LoaiBaiHocId
                                   left join Lop as f on a.LopId=f.LopId
                                   where a.IsDelete=0 and a.BaiHocSuDungThietBiId=@id";
                var lst = DbContext.Database.SqlQuery<BaiHocCoSuDungThietBiResVM>(commnad, new SqlParameter("@id", id)).FirstOrDefault();
                return lst;
            }
            catch (Exception e)
            {
                return new BaiHocCoSuDungThietBiResVM();
            }
        }
        public List<ChuongTrinhHoc> GetAllCTH()
        {
            try
            {
                return DbContext.ChuongTrinhHocs.ToList();
            }
            catch (Exception e)
            {
                return new List<ChuongTrinhHoc>();
            }
        }
        public List<LoaiBaiHoc> GetAllLoaiBaiHoc()
        {
            try
            {
                return DbContext.LoaiBaiHocs.ToList();
            }
            catch (Exception e)
            {
                return new List<LoaiBaiHoc>();
            }
        }
        public List<HocKy> GetAllHocKy()
        {
            try
            {
                return DbContext.HocKies.ToList();
            }
            catch (Exception e)
            {
                return new List<HocKy>();
            }
        }
        public ResponseModel Insert(BaiHocSuDungThietBi model)
        {
            try
            {
                model.IsDelete = false;
                DbContext.BaiHocSuDungThietBis.Add(model);
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Thêm thành công");
            }
            catch (Exception e)
            {
                return new ResponseModel(false, "fail", "Thêm thất bại");
            }
        }
        public ResponseModel DeleteBaiHocSuDungThietBi(int id)
        {
            try
            {
                var model = DbContext.BaiHocSuDungThietBis.FirstOrDefault(x => x.BaiHocSuDungThietBiId == id);
                model.IsDelete = true;
                DbContext.SaveChanges();
                return new ResponseModel(true, "Success", "Xoá thành công");

            }
            catch (Exception)
            {

                return new ResponseModel(false, "fail", "Xoá thất bại");
            }
        }
        public ResponseModel DeleteQuyDinhSuDungThietBi(int id)
        {
            try
            {
                var lst = DbContext.QuyDinhSoTietSuDungThietBis.Where(x => x.QuyDinhSuDungSoTietId == id).ToList();
                foreach (var item in lst)
                {
                    item.IsDelete = true;
                }
                DbContext.SaveChanges();
                return new ResponseModel(true, "Success", "Xoá thành công");

            }
            catch (Exception)
            {

                return new ResponseModel(false, "fail", "Xoá thất bại");
            }
        }
        public ResponseModel Update(UpdateBaiHocSuDungThietBiReqVM model)
        {
            try
            {
                var oldModel = DbContext.BaiHocSuDungThietBis.FirstOrDefault(t => t.BaiHocSuDungThietBiId == model.BaiHocSuDungThietBiId);
                MapData<BaiHocSuDungThietBi>.CopyDataObject(model, ref oldModel);
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Cập nhật thành công");
            }
            catch (Exception e)
            {
                return new ResponseModel(false, "fail", "Cập nhật thất bại");
            }
        }
    }
}
