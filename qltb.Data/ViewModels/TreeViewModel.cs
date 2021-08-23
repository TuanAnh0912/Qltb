using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class TreeViewModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public string parent { get; set; }
        public bool icon { get; set; }
        public TreeNoteStateViewModel state { get; set; }
    }
}
