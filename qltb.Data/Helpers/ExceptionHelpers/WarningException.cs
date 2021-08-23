using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Helpers.ExceptionHelpers
{
    public class WarningException : Exception
    {
        public WarningException()
        {

        }

        public WarningException(string message) : base(message)
        {

        }
    }
}
