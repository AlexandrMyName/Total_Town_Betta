using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(ProfileBinding))]
public class AbilityDelete : CmdExe<IDelete>, IAmProccess, ICost
{
    [SerializeField] private EffectsView _effects;
    [SerializeField] private List<GameObject> _stackObjectsForInterection;
    [SerializeField] private List<GameObject> _objectsForActive;
    [SerializeField] private int _maxTimeToDeleteInSeconds;

    [Space(10),Header("UI")]
    
    [SerializeField] private Slider _sliderUnbuild;
    [SerializeField] private TMP_Text _timerTMP;
    [SerializeField] private MessegeView _messegeToUser;
    [SerializeField] private CurrencyView _currencyView;
    [SerializeField] private ProfileBinding _profileBinding;

    [Space(10), Header("Workers unit (склад)")]
    [SerializeField] private WorkersBuild _mainWorkersBuilding;
    [SerializeField] private Transform _workerGoTo;
    private void OnValidate() => _profileBinding ??= GetComponent<ProfileBinding>();

    [Inject] private SelectableValue selectableValue;
    [Inject] private IUserProfile _profile;
    

    private AsyncAwaiterTime _waiterTime;
    private ISelectable _thisSelectable;
    private bool _isProccess;

    public bool IsProccess => _isProccess;
    public int Woods { get => _profileBinding.Woods; set => _profileBinding.Woods = value; }
    public int Diamonds { get => _profileBinding.Diamonds; set => _profileBinding.Diamonds = value; }
    public int Workers { get => _profileBinding.Workers; set => _profileBinding.Workers = value; }
    public int Irons { get => _profileBinding.Irons; set => Irons = value; }

    private float _currentTime;
    private int _iterator;
    private bool _isWorkerGo;
    private void Awake() => _thisSelectable = GetComponent<ISelectable>();
    
    private void Update()
    {
        
        if(_waiterTime != null && _isProccess) 
        {
            _currentTime += Time.deltaTime;
            _waiterTime.SetValue(_currentTime);

            TimeSpan timeSpaneMax = TimeSpan.FromSeconds(_maxTimeToDeleteInSeconds);
            TimeSpan timeSpaneCurrent = TimeSpan.FromSeconds(_currentTime);

            _timerTMP.text =
                $"{timeSpaneCurrent.Minutes:D2}:" +
                $"{timeSpaneCurrent.Seconds:D2}/ " +
                $"{timeSpaneMax.Minutes:D2}:" +
                $"{timeSpaneMax.Seconds:D2}/ ";

            if (selectableValue.Value != _thisSelectable) return;
            _sliderUnbuild.value = _maxTimeToDeleteInSeconds-_currentTime;
        }
    }
    protected override async void SpecificExecute(IDelete command) 
    {
        if(_effects != null)
        _effects.DeactiveSelectedEffect();

        if(_waiterTime != null)  _currencyView.gameObject.SetActive(false);

         

        if (_isWorkerGo)
        {
            _currencyView.gameObject.SetActive(false);
            _messegeToUser.SendMessageToUser($"Доставка материалов", _thisSelectable.Icon);
        }

        if (_isProccess) return;


        if ( _profile.GetCurency(CurrencyType.Worker).Count < Workers)
        {
            int noneWorker = Mathf.Abs(_profile.GetCurency(CurrencyType.Worker).Count - Workers);
            noneWorker =Mathf.Min(0, noneWorker);
            _messegeToUser.SendMessageToUser($"Не хватает рабочих ( {_profile.GetCurency(CurrencyType.Worker).Count - noneWorker})", 
                _thisSelectable.Icon);
            return;
        }

        foreach(GameObject obj in _objectsForActive) obj.SetActive(true);
         
        _isProccess = true;
        _isWorkerGo = true;

        bool isHasWorkers = await _mainWorkersBuilding.Move(Workers, _workerGoTo , _thisSelectable);

        if (!isHasWorkers)
        {
            _messegeToUser.SendMessageToUser("Не хватает рабочих!", _thisSelectable.Icon);
            
            _isProccess = false;
            _isWorkerGo = false;
            return;
        }




        _currencyView.gameObject.SetActive(false);
        _thisSelectable = gameObject.GetComponent<ISelectable>();
         _sliderUnbuild.maxValue = _maxTimeToDeleteInSeconds;
        _timerTMP.gameObject.SetActive(true);
        float secondsOnOne = _maxTimeToDeleteInSeconds/_stackObjectsForInterection.Count;

        TimeSpan timeSpaneMax = TimeSpan.FromSeconds(_maxTimeToDeleteInSeconds);
        _waiterTime = new AsyncAwaiterTime(secondsOnOne);


        if (_profileBinding != null)  _profileBinding.BindAnimCurrencyView(false); 

        while (_iterator < _stackObjectsForInterection.Count)
        {
            _stackObjectsForInterection[_iterator].SetActive(false);
            await _waiterTime;
            _waiterTime = new AsyncAwaiterTime(_currentTime + secondsOnOne);
            _iterator++;
        }

        this.gameObject.SetActive(false);
        await _mainWorkersBuilding.MoveBack(Workers, _thisSelectable);

        if (_profileBinding != null)  _profileBinding.BindAnimCurrencyView(true,true);

        _messegeToUser.SendMessageToUser($"Рабочие закончили, добыто Древесины {Woods} шт.",_thisSelectable.Icon);
    }
}
