﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.ResVMs
{
    public class QuyDinhSoTietSuDungThietBiResVM
    {
        public long STT { get; set; }
        public int? QuyDinhSoTietSuDungThietBiId { get; set; }
        public Nullable<int> LopId { get; set; }
        public string TenLop { get; set; }
        public Nullable<int> ChuongTrinhHocId { get; set; }
        public string TenChuongTrinhHoc { get; set; }
        public Nullable<int> MonHocId { get; set; }
        public string TenMonHoc { get; set; }   
        public Nullable<int> TietTNHKI { get; set; }
        public Nullable<int> TietTNHKII { get; set; }
        public Nullable<int> TietTHHKI { get; set; }
        public Nullable<int> TietTHHKII { get; set; }
        public Nullable<int> NamHocBatDau { get; set; }
        public Nullable<int> NamHocKetThuc { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> QuyDinhSuDungSoTietId { get; set; }

    }
}
