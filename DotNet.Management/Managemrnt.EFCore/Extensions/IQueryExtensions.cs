using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Management.Domain
{
    public static class IQueryExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source, 
            bool condition, 
            Expression<Func<TSource, bool>> predicate)
        {
            if (condition && predicate != null)
            {
                return source.Where(predicate);
            }
            return source;
        }

        public static IQueryable<TEntity> IncludeIf<TEntity, TProperty>(
            this IQueryable<TEntity> source,
            bool condititon,
            Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        {
            if (condititon && navigationPropertyPath != null)
            {
                source.Include(navigationPropertyPath);
            }
            return source;
        }

        public static IQueryable<TSource> PageIf<TSource>(this IQueryable<TSource> source, bool page, PageDto pageDto)
        {
            if (page)
            {
                return source.Skip(pageDto.SkipCount).Take(pageDto.MaxResultCount);
            }
            return source;
        }
    }
}
