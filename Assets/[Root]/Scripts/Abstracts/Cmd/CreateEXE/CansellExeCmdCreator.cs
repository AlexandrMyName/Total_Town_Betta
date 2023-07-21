using System;
using System.Threading;
using Zenject;

public abstract class CansellExeCmdCreator<TCommand, TArgument> : CreatorExeCmd<TCommand> where TCommand : ICommand
{

    [Inject] private AssetsContext _customContext;
    private CancellationTokenSource _cansellationSource;

    [Inject] private IAwatable<TArgument> _awatable;

    protected override async void SpecificExecute(Action<TCommand> callback)
    {
        _cansellationSource = new CancellationTokenSource();

        try
        {
            var argument = await _awatable.WithCancellation(_cansellationSource.Token);
            callback?.Invoke(_customContext.Inject(createCmd(argument)));
        }
        catch { }
    }

    protected abstract TCommand createCmd(TArgument argument);

    public sealed override void ProccessCansel()
    {
        _cansellationSource?.Cancel();
        _cansellationSource?.Dispose();
        _cansellationSource = null;
    }
}
