using System;
using Zenject;

public class CreatorExe_HoldPos : CreatorExeCmd<IHoldPos>
{
    [Inject] private AssetsContext _customContext;
    private Action<IHoldPos> _action;
    protected override void SpecificExecute(Action<IHoldPos> callback)
    => callback?.Invoke(_customContext.Inject(new CmdHoldPos()));
     
}
