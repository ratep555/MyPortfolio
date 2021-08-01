using System;
using System.Linq;
using Core.Entities;

namespace Core.Paging
{
   public static class QueryParametersExtensions
    {
        public static bool HasQuery(this QueryParameters queryParameters)
        {
            return !String.IsNullOrEmpty(queryParameters.Query);
        }
    }
}