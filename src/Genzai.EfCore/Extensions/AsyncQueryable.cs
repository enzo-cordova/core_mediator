namespace Genzai.EfCore.Extensions;

/// <summary>
/// AsyncQueryable extensions
/// </summary>
[ExcludeFromCodeCoverage]
public static class AsyncQueryable
{
    /// <summary>
    /// Returns the input typed as IQueryable that can be queried asynchronously
    /// </summary>
    /// <typeparam name="TEntity">The item type</typeparam>
    /// <param name="source">The input</param>
    public static IQueryable<TEntity> AsAsyncQueryable<TEntity>(this IEnumerable<TEntity> source)
        => new AsyncQueryable<TEntity>(source ?? throw new ArgumentNullException(nameof(source)));
}

/// <summary>
/// AsyncQueryable
/// </summary>
/// <typeparam name="TEntity"></typeparam>
[ExcludeFromCodeCoverage]
public class AsyncQueryable<TEntity> : EnumerableQuery<TEntity>, IAsyncEnumerable<TEntity>, IQueryable<TEntity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncQueryable"/> class.
    /// </summary>
    /// <param name="enumerable">Enumerable</param>
    public AsyncQueryable(IEnumerable<TEntity> enumerable) : base(enumerable) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncQueryable"/> class.
    /// </summary>
    /// <param name="expression">Expression</param>
    public AsyncQueryable(Expression expression) : base(expression) { }

    /// <summary>
    /// Get enumerator
    /// </summary>
    public IAsyncEnumerator<TEntity> GetEnumerator() => new AsyncEnumerator(this.AsEnumerable().GetEnumerator());

    /// <summary>
    /// GetAsyncEnumerator
    /// </summary>
    /// <param name="cancellationToken"></param>
    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator(this.AsEnumerable().GetEnumerator());

    IQueryProvider IQueryable.Provider => new AsyncQueryProvider(this);

    private sealed class AsyncEnumerator : IAsyncEnumerator<TEntity>
    {
        private readonly IEnumerator<TEntity> inner;

        public AsyncEnumerator(IEnumerator<TEntity> inner) => this.inner = inner;

        public void DisposeEnumerator()
        {
            inner.Dispose();
        }

        public TEntity Current => inner.Current;

        public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(inner.MoveNext());

#pragma warning disable CS1998 // Nothing to await

        public async ValueTask DisposeAsync() => inner.Dispose();

#pragma warning restore CS1998
    }

    private sealed class AsyncQueryProvider : IAsyncQueryProvider
    {
        private readonly IQueryProvider inner;

        internal AsyncQueryProvider(IQueryProvider inner) => this.inner = inner;

        public IQueryable CreateQuery(Expression expression) => new AsyncQueryable<TEntity>(expression);

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new AsyncQueryable<TElement>(expression);

#pragma warning disable CS8603

        public object Execute(Expression expression) => inner.Execute(expression);

#pragma warning restore CS8603

        public TResult Execute<TResult>(Expression expression) => inner.Execute<TResult>(expression);

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression) => new AsyncQueryable<TResult>(expression);

#pragma warning disable RCS1047 // Non-asynchronous method name should not end with 'Async'.

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
#pragma warning restore RCS1047 // Non-asynchronous method name should not end with 'Async'.
        {
            return Execute<TResult>(expression);
        }
    }
}