using qltb.WebApp.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qltb.Data.Providers;
using qltb.Data.ViewModels;


namespace qltb.WebApp.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("ALL")]
    [RoutePrefix("quan-ly-phong")]
    public class RoomManagerController : Controller
    {
        KhoPhongProvider _khophong = new KhoPhongProvider();
        // GET: RoomManager
        [Route("tat-ca-loai-phong")]
        public ActionResult Index()
        {
            var model = _khophong.GetAllLoaiKhoPhong();
            return View(model);
        }
        [Route("danh-sach")]
        public ActionResult ListRoom(int LoaiKhoPhongId)
        {
            var model = _khophong.GetByLoaiKhoPhongId(LoaiKhoPhongId);
            return View(model);
        }
        [Route("chi-tiet-phong")]
        public ActionResult RoomDetails()
        {
            return View();
        }
        public ActionResult Schedule(string room, int week)
        {
            ViewBag.WeekNumberInYear = GetWeeksInYear(DateTime.Now.Year);

            ViewBag.room = room;
            ViewBag.week = week;

            InitSchedule();
            Data.Providers.SuDungPhongProvider suDungPhongProvider = new Data.Providers.SuDungPhongProvider();

            DateTime start = FirstDateOfWeekISO8601(DateTime.Now.Year, week);
            List<Models.ScheduleViewModel> scheduleViewModels = new List<Models.ScheduleViewModel>();
            var  suDungPhongs = suDungPhongProvider.getAll(room, start, start.AddDays(7));
            if (suDungPhongs != null)
            {

                var suDungPhongsGroupByKhoPhong = suDungPhongs.GroupBy(user => user.KhoPhong);
                foreach (var group in suDungPhongsGroupByKhoPhong)
                {
                   
                    Models.ScheduleViewModel scheduleViewModel = new Models.ScheduleViewModel()
                    {
                        khoPhong = group.Key,
                        Friday =new List<Data.SuDungPhong>(),
                        Monday =new List<Data.SuDungPhong>(),
                        Saturday =new List<Data.SuDungPhong>(),
                        Sunday =new List<Data.SuDungPhong>(),
                        Thursday =new List<Data.SuDungPhong>(),
                        Tuesday =new List<Data.SuDungPhong>(),
                        Wednesday =new List<Data.SuDungPhong>()
                    };
                    foreach (var suDungPhong in group)
                    {
                        int day = (int)suDungPhong.NgayTao.Value.DayOfWeek;
                        if (day == 1)
                        {
                            scheduleViewModel.Friday.Add(suDungPhong);
                        }
                        else if (day == 2)
                        {
                            scheduleViewModel.Monday.Add(suDungPhong);
                        }
                        else if (day == 3)
                        {
                            scheduleViewModel.Saturday.Add(suDungPhong);
                        }
                        else if (day == 4)
                        {
                            scheduleViewModel.Sunday.Add(suDungPhong);
                        }
                        else if (day == 5)
                        {
                            scheduleViewModel.Thursday.Add(suDungPhong);
                        }
                        else if (day == 6)
                        {
                            scheduleViewModel.Tuesday.Add(suDungPhong);
                        }
                        else 
                        {
                            scheduleViewModel.Wednesday.Add(suDungPhong);
                        }
                    }
                    scheduleViewModels.Add(scheduleViewModel);
                }
            }

            return View(scheduleViewModels);
        }

        public ActionResult SelectTieuChuanPhong(int KhoiTruongId = 0, int value = 0)
        {
            ViewBag.selected = value;
            return View(new Data.Providers.TieuChuanPhongProvider().getAll(KhoiTruongId));
        }
        public int GetWeeksInYear(int year)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(year, 12, 31);
            Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                dfi.FirstDayOfWeek);
        }
        private static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }
        private void InitSchedule()
        {
            Data.Providers.ThoiKhoaBieuPhongProvider thoiKhoaBieuPhongProvider = new Data.Providers.ThoiKhoaBieuPhongProvider();
            var thoiKhoaBieuPhongs = thoiKhoaBieuPhongProvider.getAll();
            if (thoiKhoaBieuPhongs != null)
            {
                Data.Providers.SuDungPhongProvider suDungPhongProvider = new Data.Providers.SuDungPhongProvider();
                foreach (var item in thoiKhoaBieuPhongs)
                {
                    DateTime start = DateTime.ParseExact(item.NgayApDung.Value.ToString("MMddyyyy"), "MMddyyyy", null);
                    DateTime end = DateTime.ParseExact(DateTime.Now.ToString("MMddyyyy"), "MMddyyyy", null);

                    var suDungPhongLast = suDungPhongProvider.getLastItem(item.Id);
                    if (suDungPhongLast != null)
                    {
                        start =  DateTime.ParseExact((suDungPhongLast.NgayTao.Value).AddDays(1).ToString("MMddyyyy"), "MMddyyyy", null);
                    }
                    if (start < end)
                    {
                        for (var date = start; date <= end; date = date.AddDays(1))
                        {
                            var suDungPhong = suDungPhongProvider.getByDateAndThoiKhoaBieuPhongId(item.Id, date);
                            if (suDungPhong == null)
                            {
                                suDungPhong = new Data.SuDungPhong()
                                {
                                    ThoiKhoaBieuPhongId = item.Id,
                                    NgayTao = date,
                                    LopId = item.LopHocId,
                                    NgayTrongTuan = item.NgayTrongTuan,
                                    TietHoc = item.TietHocSuDung,
                                    KhoPhongId = item.KhoPhongId
                                };
                                suDungPhongProvider.Insert(suDungPhong);
                            }
                        }
                    }
                }
            }
        }
    }
}