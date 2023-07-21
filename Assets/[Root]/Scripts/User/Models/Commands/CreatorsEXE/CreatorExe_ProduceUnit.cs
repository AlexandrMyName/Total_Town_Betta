using System;
using Zenject;

public class CreatorExe_ProduceUnit : CreatorExeCmd<IProduceUnit>
{
    [Inject] private AssetsContext _customContext;
   
    protected override void SpecificExecute(Action<IProduceUnit> callback)
    => callback?.Invoke(_customContext.Inject(new CmdProduceUnit()));

}
