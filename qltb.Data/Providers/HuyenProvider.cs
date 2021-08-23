using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class HuyenProvider : ApplicationDbcontext
    {
       
        public List<Huyen> getAllStartWithId(string id)
        {
            try
            {
                return DbContext.Huyens.Where(m => m.HuyenId.StartsWith(id)).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
