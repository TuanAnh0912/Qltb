using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class TinhProvider : ApplicationDbcontext
    {
        public List<Tinh> getAll()
        {
            try
            {
                return DbContext.Tinhs.ToList();
            }
            catch (Exception e)
            {
                return new List<Tinh>();
            }
        }
    }
}
