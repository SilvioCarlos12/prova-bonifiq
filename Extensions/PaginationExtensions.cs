namespace ProvaPub.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> PaginationQuery<T>(this IQueryable<T> query, int page, int pageSize = 10)
        {
            if (page < 1)
            {
                page = 1;
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
