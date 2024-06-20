namespace ProductManagementApp.Services.Extensions;

public static class QueryableExtensions
{
    public static Task<List<TSource>> ToListAsync<TSource>(
        this IQueryable<TSource> source,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(source.ToList());
    }
}