using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data
{
    public class ApplicationDbcontext
    {
        qltbEntities db = new qltbEntities();
        public qltbEntities DbContext { get { return db; } }
    }
}
