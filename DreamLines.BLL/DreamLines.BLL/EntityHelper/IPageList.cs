using System;
using System.Collections.Generic;
using System.Text;

namespace DreamLines.BLL.EntityHelper
{
    /// <summary>
    /// Interface to page list, contains main properties
    /// </summary>
    public interface IPageList
    {
        int TotalCount { get; }
        int PageCount { get; }
        int Page { get; }
        int PageSize { get; }
    }
}
