﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ViewModels
{
    public class LoaiKhoPhongViewModel
    {
        public int LoaiKhoPhongId { get; set; }
        public string TenLoaiKhoPhong { get; set; }
        public string MaLoaiKhoPhong { get; set; }
        public Nullable<int> SoLuongPhong { get; set; }
    }
}