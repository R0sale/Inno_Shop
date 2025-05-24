using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> list, int count, int currentPage, int pageSize)
        {
            Metadata = new Metadata
            {
                TotalCount = count,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(list.Count / (double)pageSize)
            };
            AddRange(list);
        }

        public Metadata Metadata { get; set; }

        public static PagedList<T> ToPagedList(IEnumerable<T> list, int currentPage, int pageSize)
        {
            var count = list.Count();
            var items = list
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(items, count, currentPage, pageSize);
        }
    }
}
