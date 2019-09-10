using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLines.BLL.EntityHelper
{
    /// <summary>
    /// PageList class, is helper class wich extend the Generic List class and IPageList interface
    /// It will help selecting a page from large list of items asynchcronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T> : List<T>, IPageList
    {
        public int TotalCount { get; private set; }
        public int PageCount { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public PageList()
        {

        }
        /// <summary>
        /// return a page from large list of items asynchcronously.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task CreateAsync(IQueryable<T> source, int page, int pageSize)
        {
            // async to get count
            TotalCount = await source.CountAsync(); 
            PageCount = GetPageCount(pageSize, TotalCount);
            Page = page < 1 ? 0 : page - 1;
            PageSize = pageSize;
            // async to get the page
            AddRange(await source.Skip(Page * PageSize)
                                 .Take(PageSize)
                                 .ToListAsync()); 
        }

        /// <summary>
        /// Calculate total page count
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns>total page count</returns>
        private int GetPageCount(int pageSize, int totalCount)
        {
            if (pageSize == 0)
                return 0;

            var remainder = totalCount % pageSize;
            return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
        }
    }
}
