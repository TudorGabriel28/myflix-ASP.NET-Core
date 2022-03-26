using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers
{
    public class Converter<TSource, TDestination>
    : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    {
        public PagedList<TDestination> Convert(
            PagedList<TSource> source,
            PagedList<TDestination> destination,
            ResolutionContext context) =>
            new PagedList<TDestination>(
                context.Mapper.Map<List<TSource>, List<TDestination>>(source), source.TotalCount, source.CurrentPage, source.PageSize);
    }
}
