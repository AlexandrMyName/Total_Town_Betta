using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Zenject;

public class AbilityProduceUnit : CmdExe<IProduceUnit>
{
    private ProduceUnitsPresenter _produceUnitPresenter;

    [Inject]
    private void Constract(ProduceUnitsPresenter produceUnitPresenter)
    {
        _produceUnitPresenter = produceUnitPresenter;
    }
    protected override void SpecificExecute(IProduceUnit command)
    {
        _produceUnitPresenter.Show();
    }
}
