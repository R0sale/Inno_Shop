using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestFeatures
{
    public class RequestParams
    {
        const int maxPageSize = 20;
        public int CurrentPage { get; set; }
        private int _pagesize = 10;

        public int PageSize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = value <= maxPageSize ? value :  maxPageSize;
            }
        }
    }
}
