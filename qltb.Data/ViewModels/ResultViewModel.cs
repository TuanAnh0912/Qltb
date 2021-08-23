using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class ResultViewModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Style { get; set; }

        public ResultViewModel(bool status, string mess)
        {
            this.Status = status;
            this.Message = mess;
        }

        public ResultViewModel(bool status, string mess, string style)
        {
            this.Status = status;
            this.Message = mess;
            this.Style = style;
        }
    }
    
}
