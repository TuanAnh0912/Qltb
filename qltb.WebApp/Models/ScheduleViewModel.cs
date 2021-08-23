using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace qltb.WebApp.Models
{
    public class ScheduleViewModel
    {
        public Data.KhoPhong khoPhong { set; get; }
        public List<Data.SuDungPhong> Monday { set; get; }
        public List<Data.SuDungPhong> Tuesday { set; get; }
        public List<Data.SuDungPhong> Wednesday { set; get; }
        public List<Data.SuDungPhong> Thursday { set; get; }
        public List<Data.SuDungPhong> Friday { set; get; }
        public List<Data.SuDungPhong> Saturday { set; get; }
        public List<Data.SuDungPhong> Sunday { set; get; }
    }
}