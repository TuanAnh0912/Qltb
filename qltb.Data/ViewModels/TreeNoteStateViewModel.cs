using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class TreeNoteStateViewModel
    {
        public bool selected { get; set; }
        public bool opened { get; set; }
        public TreeNoteStateViewModel(bool selected)
        {
            this.selected = selected;
        }
    }
}
