using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ProduceBuilderSlotsView : MonoBehaviour
{
    public Image Icon;
    public Button ButtonClick;

    public TMP_Text CurrencyCostDiamonds;
    public TMP_Text CurrencyCostMoney;

    public IBuilderConfig Config;


    public void Init(IBuilderConfig config)
    {
        Icon.sprite = config.Icon;
        CurrencyCostDiamonds.text = $" {config.Diamonds}";

        CurrencyCostMoney.text = $" "; //

        Config = config;
    }
}