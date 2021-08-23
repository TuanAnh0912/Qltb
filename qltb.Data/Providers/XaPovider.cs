using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Providers
{
    public class XaPovider : ApplicationDbcontext
    {
        public List<Xa> getAllStartWithId(string id)
        {
            try
            {
                return DbContext.Xas.Where(m => m.XaId.StartsWith(id)).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
