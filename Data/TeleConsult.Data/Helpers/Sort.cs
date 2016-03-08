namespace TeleConsult.Data.Helpers
{
    using Common;
    using Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    [Flags]
    public enum SortDirection
    {
        Asc = 1,
        Desc = 2
    }

    public static class Sort
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, false);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, false);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, true);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, true);
        }

        public static IOrderedQueryable<T> OrderQCGrids<T>(this IQueryable<T> source, IOrderedQueryable<T> ascendingOrder, IOrderedQueryable<T> descendingOrder, string sortOrder)
        {
            if (sortOrder == GlobalConstants.SortDirections.Ascending)
            {
                return ascendingOrder;
            }
            else
            {
                return descendingOrder;
            }
        }

        public static SortDirection GetDirection(string direction)
        {
            SortDirection sortDirection;
            switch (direction)
            {
                case null:
                case "":
                case "asc":
                case "ascending":
                    sortDirection = SortDirection.Asc;
                    break;
                case "desc":
                case "descending":
                    sortDirection = SortDirection.Desc;
                    break;
                default:
                    throw new ArgumentException("Invalid parameter", "sortDirection");
            }

            return sortDirection;
        }

        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        public static List<T> SortAndFilterList<T>(List<T> source, int start, int limit, string sortBy, SortDirection sortDirection)
        {
            var list = !string.IsNullOrEmpty(sortBy)
                ? OrderingHelper(source.AsQueryable(), sortBy, (sortDirection == SortDirection.Desc), false).ToList()
                : source;

            return ((start == 0) && (limit <= 0 || limit == int.MaxValue))
                ? list : list.Skip(start).Take(limit > 0 ? limit : int.MaxValue).ToList();
        }

        /// <summary>
        /// Applies dynamic sorting based on the Paging Filter's SortBy and SortDirection properties. The expression is updated when SortBy has a non-blank value.
        /// </summary>
        /// <param name="filter">An instance inheriting PagingFilter.</param>
        /// <returns>A LINQ to Entities context expression.</returns>
        public static IQueryable<TSource> OrderByFilter<TSource>(this IQueryable<TSource> source, PagingFilter filter) where TSource : class
        {
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                source = filter.SortDirection == SortDirection.Asc
                       ? Sort.OrderBy(source, filter.SortBy)
                       : Sort.OrderByDescending(source, filter.SortBy);
            }

            return source;
        }

        /// <summary>
        /// Applies dynamic paging based on the Paging Filter's Start and Limit properties. The expression is always enumerated in the end. Paging is only applied when Limit is greater than 0.
        /// </summary>
        /// <param name="filter">An instance inheriting PagingFilter.</param>
        /// <returns>The enumerated result. The returned list belongs to the LINQ to Objects context.</returns>
        public static List<TSource> PageByFilter<TSource>(this IQueryable<TSource> source, PagingFilter filter) where TSource : class
        {
            return filter.Limit > 0 && filter.Page > 0 ? source.Skip(filter.Start).Take(filter.Limit).ToList() : source.ToList();
        }
    }
}
