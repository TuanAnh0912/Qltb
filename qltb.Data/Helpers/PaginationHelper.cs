using qltb.Data.ResVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Helpers
{
    public static class PaginationHelper
    {
        public static List<PaginationResVM> PaginationGeneration(Dictionary<string, string> queryParamsDict, int totalPage, int currentPage, int pageSize)
        {
            try
            {
                int maxSize = 5;
                int side = Convert.ToInt32(Math.Floor(maxSize * 1.0 / 2));
                List<PaginationResVM> pagination = new List<PaginationResVM>();
                string queryParamString = "?{0}";
                List<string> queryParams = new List<string>();
                foreach(var queryParam in queryParamsDict)
                {
                    queryParams.Add(queryParam.Key + "=" + queryParam.Value);
                }
                queryParamString = string.Format(queryParamString, string.Join("&", queryParams.ToArray()));
                if (queryParams.Count != 0)
                {
                    queryParamString += "&";
                }
                pagination.Add(new PaginationResVM()
                {
                    Text = "<i class=\"fa fa-lg fa-angle-left\"></i><i class=\"fa fa-lg fa-angle-left\"></i>",
                    PageIndex = 1,
                    IsActived = false,
                    IsDisabled = false
                }); ;
                pagination.Add(new PaginationResVM()
                {
                    Text = "<i class=\"fa fa-lg fa-angle-left\"></i>",
                    PageIndex = currentPage - 1,
                    IsActived = false,
                    IsDisabled = (currentPage - 1) <= 0
                });
                // 1 2 3 4 
                if (totalPage <= maxSize)
                {
                    for (int i = 1; i <= totalPage; i++)
                    {
                        pagination.Add(new PaginationResVM()
                        {
                            Text = i.ToString(),
                            PageIndex = i,
                            IsActived = i == currentPage,
                            IsDisabled = false
                        });
                    }
                }
                else if ((totalPage > maxSize) && (currentPage + side > totalPage))
                {
                    for (int i = maxSize-1 ; i >= 0; i--)
                    {
                        pagination.Add(new PaginationResVM()
                        {
                            Text = (totalPage - i).ToString(),
                            PageIndex = totalPage - i,
                            IsActived = (totalPage - i) == currentPage,
                            IsDisabled = false
                        });
                    }
                }
                // 8 9 10 11 12
                else if(totalPage > maxSize)
                {
                    if (currentPage - side <= 0)
                    {
                        for (int i = 1; i <= maxSize; i++)
                        {
                            pagination.Add(new PaginationResVM()
                            {
                                Text = i.ToString(),
                                PageIndex = i,
                                IsActived = i == currentPage,
                                IsDisabled = false
                            });
                        }
                    }
                    else
                    {
                        // left
                        for (int i = side; i > 0; i--)
                        {
                            pagination.Add(new PaginationResVM()
                            {
                                Text = (currentPage - i).ToString(),
                                PageIndex = currentPage - i,
                                IsActived = false,
                                IsDisabled = false
                            }); ;
                        }

                        // add middle active
                        pagination.Add(new PaginationResVM()
                        {
                            Text = currentPage.ToString(),
                            PageIndex = currentPage,
                            IsActived = true,
                            IsDisabled = false
                        });


                        // right
                        for (int i = 1; i <= side; i++)
                        {
                            pagination.Add(new PaginationResVM()
                            {
                                Text = (currentPage + i).ToString(),
                                PageIndex = currentPage + i,
                                IsActived = false,
                                IsDisabled = false
                            });
                        }
                    }
                }
                pagination.Add(new PaginationResVM()
                {
                    Text = "<i class=\"fa fa-lg fa-angle-right\"></i>",
                    IsActived = false,
                    PageIndex = currentPage + 1,
                    IsDisabled = (currentPage + 1) > totalPage
                });
                pagination.Add(new PaginationResVM()
                {
                    Text = "<i class=\"fa fa-lg fa-angle-right\"></i><i class=\"fa fa-lg fa-angle-right\"></i>",
                    IsActived = false,
                    PageIndex = totalPage,
                    IsDisabled = false
                });

                // set url
                foreach (var item in pagination)
                {
                    item.Url = queryParamString + "currentPage=" + item.PageIndex + "&pageSize=" + pageSize;
                }

                return pagination;
            }
            catch(Exception ex)
            {
                return new List<PaginationResVM>();
            }
        }
    }
}
