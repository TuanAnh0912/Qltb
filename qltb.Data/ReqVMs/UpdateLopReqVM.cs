﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ReqVMs
{
    public class UpdateLopReqVM
    {
        public int? LopId { get; set; }

        public string TenLop { get; set; }

        public string MaLop { get; set; }

        public int? KhoiLopId { get; set; }
    }
}