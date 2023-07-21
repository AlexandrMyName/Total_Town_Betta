using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimCurrencySlotView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text _befor;
    [SerializeField] private TMP_Text _constanta;
    [SerializeField] private TMP_Text _now;


    public void SetSlot(Sprite icon, float befor, float now)
    {
       
        image.sprite = icon;

        var constanta = now - befor;

        char charConst = constanta > 0 ? '+' : '-';

        _befor.text = befor.ToString();
        _now.text = now.ToString();
        _constanta.text = $"{charConst} {Mathf.Abs(constanta)}";
    }
}
