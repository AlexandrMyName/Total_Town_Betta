using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AbilityProduceUnit : CmdExe<IProduceUnit>
{
    [SerializeField] private ProduceUnitsPresenter _produceUnitPresenter;


    private void Awake()
    {
        _produceUnitPresenter ??= GameObject.FindGameObjectWithTag("ProduceView").GetComponent<ProduceUnitsPresenter>();
    }
    protected override void SpecificExecute(IProduceUnit command)
    {
        _produceUnitPresenter.Show();
    }
}
