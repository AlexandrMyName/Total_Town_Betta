using System.Runtime.CompilerServices;

public interface IAwaiter<TAwaitor> : INotifyCompletion
{
     bool IsCompleted { get; }
     TAwaitor GetResult();
}
