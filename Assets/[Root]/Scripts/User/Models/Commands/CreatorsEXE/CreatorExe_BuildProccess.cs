using System;
using Zenject;

public class CreatorExe_BuildProccess : CreatorExeCmd<IBuildProccess>
{
    [Inject] private AssetsContext _customContext;

    protected override void SpecificExecute(Action<IBuildProccess> callback)
    {
        
        callback?.Invoke(_customContext.Inject(new CmdBuildProccess()));

    }

}
