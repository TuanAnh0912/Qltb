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
    public class ToBoMonProvider : ApplicationDbcontext
    {
        public List<ToBoMonResVM> GetAllToBoMon()
        {
            try
            {
                string command = @"select ROW_NUMBER() OVER(order by ToBoMonId ASC) as 'STT',* from ToBoMon as a 
                                   left join PhongBan as b on a.PhongBanId=b.PhongBanId
                                   where IsDelete=0";
                var lstToBoMon = DbContext.Database.SqlQuery<ToBoMonResVM>(command).ToList();
                return lstToBoMon;
            }
            catch (Exception e)
            {

                return new List<ToBoMonResVM>();
            }
        }
        public ResponseModel Delete(int id)
        {
            try
            {
                var model = DbContext.ToBoMons.FirstOrDefault(x => x.ToBoMonId == id);
                model.IsDelete = true;
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "Xoá thành công");
            }
            catch (Exception e)
            {

                return new ResponseModel(false, "fail", "Xoá thất bại");

            }
        }
        public ToBoMonResVM GetAllToBoMonById(int id)
        {
            try
            {
                string command = @"select ROW_NUMBER() OVER(order by ToBoMonId ASC) as 'STT',* from ToBoMon where ToBoMonId=@id";
                var lstToBoMon = DbContext.Database.SqlQuery<ToBoMonResVM>(command, new SqlParameter("@id", id)).FirstOrDefault();
                return lstToBoMon;
            }
            catch (Exception e)
            {

                return new ToBoMonResVM();
            }
        }
        public ResponseModel Insert(ToBoMon model)
        {
            try
            {
                var check = DbContext.ToBoMons.Where(x => x.MaBoMon == model.MaBoMon && x.IsDelete == false).ToList();
                if (check.Count > 0) return new ResponseModel(false, "fail", "Mã bộ môn đã được sử dụng");
                model.IsDelete = false;
                DbContext.ToBoMons.Add(model);
                DbContext.SaveChanges();
                return new ResponseModel(true, "success", "thêm thành công");
            }
            catch (Exception e)
            {
                return new ResponseModel(false, "fail", "thêm thất bại");
            }
        }
        public ResponseModel UpdateToBoMon(ToBoMonReqVM model)
        {
            try
            {
                var oldModel = DbContext.ToBoMons.FirstOrDefault(t => t.ToBoMonId == model.ToBoMonId);
                MapData<ToBoMon>.CopyDataObject(model, ref oldModel);
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
