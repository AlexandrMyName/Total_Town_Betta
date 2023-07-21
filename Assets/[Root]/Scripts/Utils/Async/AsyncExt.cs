using System.Threading;
using System.Threading.Tasks;
 

public static class AsyncExt  
{
    public struct Void { }
    public static Task<TResult> AsTask<TResult>(this IAwatable<TResult> awaitable)
    {
        return Task.Run(async () => await awaitable);
    }

    public static async Task<T> WhithCansellathionToken<T>(this Task<T> originalTask, CancellationToken ct)
    {

        var CanselTask = new TaskCompletionSource<Void>();

        using (ct.Register(t => ((TaskCompletionSource<Void>)t).TrySetResult(new Void()), CanselTask))
        {
            var any = await Task.WhenAny(originalTask, CanselTask.Task);

            if(any == CanselTask.Task)
            {
                ct.ThrowIfCancellationRequested();
            }
        }

        return await originalTask;
    }
    public static async Task<TResult> WithCancellation<TResult>(this IAwatable<TResult>
    originalTask, CancellationToken ct) => await WhithCansellathionToken(originalTask.AsTask(), ct);
}
