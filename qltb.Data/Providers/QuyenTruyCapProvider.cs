using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class QuyenTruyCapProvider : ApplicationDbcontext
    {
        public bool Insert(int ChucVuId, List<int> ChucNangs)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var lst = DbContext.QuyenTruyCaps.Where(q => q.ChucVuId == ChucVuId).ToList();
                    DbContext.QuyenTruyCaps.RemoveRange(lst);
                    DbContext.SaveChanges();

                    var quyentruycaps = new List<QuyenTruyCap>();
                    if(ChucNangs !=null && ChucNangs.Count() > 0)
                    {
                        foreach (var i in ChucNangs)
                        {
                            var obj = new QuyenTruyCap();
                            obj.ChucNangId = i;
                            obj.ChucVuId = ChucVuId;
                            quyentruycaps.Add(obj);
                        }
                        DbContext.QuyenTruyCaps.AddRange(quyentruycaps);
                        DbContext.SaveChanges();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
