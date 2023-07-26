using UnityEngine;
using Zenject;

public class ProfileBinding : MonoBehaviour,  IProfileBinder
{
    
    [SerializeField] private int _woodCount;
    [SerializeField] private int _ironCount;
    [SerializeField] private int _workerCount;
    [SerializeField] private int _diamondCount;

    [Inject] private IUserProfile _profile;
    [Inject] private AnimCurrencyView _animCurrency;
    public int Woods { get => _woodCount; set => _woodCount = value; }
    public int Diamonds { get => _diamondCount; set => _diamondCount = value; }
    public int Workers { get => _workerCount; set => _workerCount = value; }
    public int Irons { get => _ironCount; set => _ironCount = value; }

    
    /// <summary>
    /// False - minus ; True - plus (Profile)
    /// </summary>
    /// <param name="end"></param>
    public void BindAnimCurrencyView(bool plus = false, bool activeMaterials = false, bool onlyShowView = false)
    {
        _animCurrency.ClearView();

        float beforeWorker = _profile.GetCurency(CurrencyType.Worker).Count;
        float nowWorker = plus == true ? beforeWorker + _workerCount : beforeWorker - _workerCount;
        float beforeWood = _profile.GetCurency(CurrencyType.Wood).Count;
        float nowWood = plus == true ? beforeWood + _woodCount : beforeWood - _woodCount;
        float beforeDiamond = _profile.GetCurency(CurrencyType.Diamond).Count;
        float nowDiamond = plus == true ? beforeDiamond + _diamondCount : beforeDiamond - _diamondCount;
        float beforeIron = _profile.GetCurency(CurrencyType.Iron).Count;
        float nowIron = plus == true ? beforeIron + _ironCount : beforeIron - _ironCount;

        if (Workers != 0)
            _animCurrency.RefreshUI(CurrencyType.Worker, beforeWorker, nowWorker);

        if (Diamonds != 0 && plus != false && activeMaterials != false)
            _animCurrency.RefreshUI(CurrencyType.Diamond, beforeDiamond, nowDiamond);

        if (Woods != 0 && plus != false && activeMaterials != false)
            _animCurrency.RefreshUI(CurrencyType.Wood, beforeWood, nowWood);

        if (Irons != 0 && plus != false && activeMaterials != false)
            _animCurrency.RefreshUI(CurrencyType.Iron, beforeIron, nowIron);

        _animCurrency.SetTimerToDispose(4);

        if (onlyShowView) return;

        if(plus == false)
        {
            _profile.GetCurency(CurrencyType.Worker).Count -= Workers;

            if (activeMaterials)
            {
                _profile.GetCurency(CurrencyType.Diamond).Count -= Diamonds;
                _profile.GetCurency(CurrencyType.Wood).Count -= Woods;
                _profile.GetCurency(CurrencyType.Iron).Count -= Irons;
            }
        }
        else
        {
            _profile.GetCurency(CurrencyType.Worker).Count += Workers;

            if (activeMaterials)
            {
                _profile.GetCurency(CurrencyType.Diamond).Count += Diamonds;
                _profile.GetCurency(CurrencyType.Wood).Count += Woods;
                _profile.GetCurency(CurrencyType.Iron).Count += Irons;
            }
        }
    }
}
