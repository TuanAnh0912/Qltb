using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Helpers
{
    public class ParseStringToDateTime
    {
        public DateTime ParseStringToTime(string stringParse)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(stringParse, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return dt;
            }
            catch (Exception)
            {

                return new DateTime();
            }
           
        }
    }
}
