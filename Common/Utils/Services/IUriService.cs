using Common.Utils.Filters;
using System;

namespace Common.Utils.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
