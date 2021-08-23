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
    public class CanBoThietBiProvider:ApplicationDbcontext
    {
        public List<CanBoThietBiResVM> GetAllCanBoThietBi()
        {
            try
            {
                string command= @"select  ROW_NUMBER() OVER(ORDER BY a.CanBoThietBiId ASC) AS STT,a.CanBoThietBiId,b.VaiTroCanBoId,c.GiaoVienId,c.MaGiaoVien,c.TenGiaoVien,d.TenGioiTinh,e.TenVaiTro,a.TrinhDoVanHoa,Convert(varchar,a.ThoiGianBatDauQuanLy,103) as 'ThoiGianBatDauQuanLy',Convert(varchar,a.ThoiGianHetHanQuanLy,103) as 'ThoiGianHetHanQuanLy',a.CoTrinhDoNghiepVu,a.GhiChu from CanBoThietBi as a
                                  left join VaiTroCanBo as b on a.VaiTroCanBoId=b.VaiTroCanBoId
                                  left join GiaoVien as c on a.GiaoVienId=c.GiaoVienId
								  left join GioiTinh as d on c.GioiTinhId=d.GioiTinhId
								  left join VaiTroCanBo as e on a.VaiTroCanBoId=e.VaiTroCanBoId
                                  where a.IsDelete=0";
                var lstCanBoTB = DbContext.Database.SqlQuery<CanBoThietBiResVM>(command).ToList();
                return lstCanBoTB;
            }
            catch (Exception e)
            {

                return new List<CanBoThietBiResVM>();
            }
        }
        public List<VaiTroCanBo> GetAllVaiTro()
        {
            try
            {
                return DbContext.VaiTroCanBoes.ToList();
            }
            catch (Exception e)
            {

                return new List<VaiTroCanBo>();
            }
        }
        public CanBoThietBiResVM GetCanBoThietBiById(int id)
        {
            try
            {
                string command = @"select  ROW_NUMBER() OVER(ORDER BY a.CanBoThietBiId ASC) AS STT,a.CanBoThietBiId,a.GhiChu,b.VaiTroCanBoId,a.IsDelete,c.MaGiaoVien,c.GiaoVienId,c.TenGiaoVien,d.TenGioiTinh,e.TenVaiTro,a.TrinhDoVanHoa,Convert(varchar,a.ThoiGianBatDauQuanLy,103) as 'ThoiGianBatDauQuanLy',Convert(varchar,a.ThoiGianHetHanQuanLy,103) as 'ThoiGianHetHanQuanLy',a.CoTrinhDoNghiepVu from CanBoThietBi as a
                                  left join VaiTroCanBo as b on a.VaiTroCanBoId=b.VaiTroCanBoId
                                  left join GiaoVien as c on a.GiaoVienId=c.GiaoVienId
								  left join GioiTinh as d on c.GioiTinhId=d.GioiTinhId
								  left join VaiTroCanBo as e on a.VaiTroCanBoId=e.VaiTroCanBoId
                                  where a.IsDelete=0 and a.CanBoThietBiId=@id";
                var CanBoTB = DbContext.Database.SqlQuery<CanBoThietBiResVM>(command,new SqlParameter("@id",id)).FirstOrDefault();
                return CanBoTB;
            }
            catch (Exception e)
            {

                return new CanBoThietBiResVM();
            }
        }
        public ResponseModel DeleteCanBoThietBi(int id)
        {
            try
            {
                var model = DbContext.CanBoThietBis.FirstOrDefault(t => t.CanBoThietBiId == id);
                model.IsDelete = true;
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Xoá thành công");
            }
            catch (Exception e)
            {
                return new ResponseModel(false, "fail","Xoá thất bại");
            }
        }
        public ResponseModel UpdateCanBoThietBi(UpdateCanBoThietBiReqVm model)
        {
            try
            {
                var oldModel = DbContext.CanBoThietBis.FirstOrDefault(t => t.CanBoThietBiId == model.CanBoThietBiId);
                MapData<CanBoThietBi>.CopyDataObject(model, ref oldModel);
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Cập nhật thành công");

            }
            catch (Exception e)
            {
                return new ResponseModel(false,"fail","Cập nhật thất bại");
            }
        }
        public ResponseModel InsertCanBoThietBi(CanBoThietBi model)
        {
            try
            {
                model.IsDelete = false;
                model.NgayTao = DateTime.Now;
                DbContext.CanBoThietBis.Add(model);
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Thêm mới thành công");

            }
            catch (Exception e)
            {

                return new ResponseModel(false, "fail", "Thêm mới thất bại");
                
            }
        }
    }
}
