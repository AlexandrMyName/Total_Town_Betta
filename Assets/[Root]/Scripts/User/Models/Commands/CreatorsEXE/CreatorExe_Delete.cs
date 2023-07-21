using System;
using Zenject;

public class CreatorExe_Delete: CreatorExeCmd<IDelete>
{
    [Inject] private AssetsContext _customContext;
   
    protected override void SpecificExecute(Action<IDelete> callback)
    => callback?.Invoke(_customContext.Inject(new CmdDelete()));

}
