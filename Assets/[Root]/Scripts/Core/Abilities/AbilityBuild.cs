using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(BuildProccesses),typeof(ProfileBinding))]
public class AbilityBuild : CmdExe<IBuildProccess>, IAmProccess, ICost
{
    [SerializeField] private EffectsView _effects;
    [SerializeField] private List<GameObject> _buildsInterectiveObjects;
   
    [SerializeField] private Slider _buildSlider;
    [SerializeField] private float _maxTimeToBuild;
    [SerializeField] private GameObject _gameobjectOnCompleted;
    [SerializeField] private TMP_Text _textTimeFoUser;
    

    [Space(10),Header("Interectiv with User")]
    [SerializeField] private GameObject _currencyForUser;
    [SerializeField] private DialogView _dialogView;
    [SerializeField] private MessegeView _messegeToUser;
    [SerializeField] private ProfileBinding _profileBinding;

    [Space(10), Header("If you wonna this, this is Effects timeline")]
    [SerializeField] private List<string> _dialogs;
    [SerializeField] private ClipEffector _clipEffector;
    [SerializeField] private ClipType _clipType;
    [SerializeField] private Transform _moveAfterDialog;
    [SerializeField] private GameObject _hidenGameEnd;

    [Space(10)]
    [SerializeField] private CurrencyView _currencyView;
    [Space(10), Header("Workers unit (склад)")]
    [SerializeField] private WorkersBuild _mainWorkersBuilding;
    [SerializeField] private Transform _workerGoTo;

    private float currentTime;
    private int _iterator;
    private AsyncAwaiterTime _waiter;

    
    [Inject] private SelectableValue selectValue;
    [Inject] private IUserProfile _profile;

    private ISelectable buildProccess;

    private bool _inProccessing;
    private bool _isWorkerGo;
    public bool IsProccess => _inProccessing;

    public int Woods { get => _profileBinding.Woods; set => _profileBinding.Woods = value; }
    public int Diamonds { get => _profileBinding.Diamonds; set => _profileBinding.Diamonds = value; }
    public int Workers { get => _profileBinding.Workers; set => _profileBinding.Workers = value; }
    public int Irons { get => _profileBinding.Irons; set => Irons = value; }


    private void OnValidate() => _profileBinding ??= GetComponent<ProfileBinding>();
     
    private void Awake()
    {
        buildProccess = gameObject.GetComponent<ISelectable>();
        
    }
    private void Update()
    {
        if (_waiter != null)
        {
            currentTime += Time.deltaTime;
            _waiter.SetValue(currentTime);
             
            var convertToUser = TimeSpan.FromSeconds(currentTime);
            var convertToUserMax = TimeSpan.FromSeconds(_maxTimeToBuild);
            _textTimeFoUser.text = 
                $"{convertToUser.Minutes:D2}:" +
                $"{convertToUser.Seconds:D2}/ "+
                $"{convertToUserMax.Minutes:D2}:" +
                $"{convertToUserMax.Seconds:D2} ";

            buildProccess.Health = Mathf.RoundToInt(currentTime / _maxTimeToBuild * 100);

            if (selectValue.Value != buildProccess) return;

             
            _buildSlider.value = currentTime;
            _buildSlider.maxValue = _maxTimeToBuild;
        }
    }
    protected override async void SpecificExecute(IBuildProccess command)
    {
        _effects.DeactiveSelectedEffect();

        if (_waiter != null)
        {
            _currencyView.gameObject.SetActive(false);
            _messegeToUser.SendMessageToUser("Уже в процессе шеф!", buildProccess.Icon);
            return;
        }
        else
        {
            if (_isWorkerGo)
            {
                _currencyView.gameObject.SetActive(false);
                _messegeToUser.SendMessageToUser("Ожидаем материалы...", buildProccess.Icon);
                return;
            }

            if (_profile.GetCurency(CurrencyType.Wood).Count < Woods ||
                _profile.GetCurency(CurrencyType.Worker).Count < Workers ||
                _profile.GetCurency(CurrencyType.Diamond).Count < Diamonds ||
                _profile.GetCurency(CurrencyType.Iron).Count < Irons)
            {
                _messegeToUser.SendMessageToUser("Не хватает материалов!", buildProccess.Icon);
                return;
            }

            _messegeToUser.SendMessageToUser("Начинаем!", buildProccess.Icon);
           

        }
        command.IsBuild = true;
        _inProccessing = true;
        _isWorkerGo = true;
        _currencyView.gameObject.SetActive(false);



        bool isHasWorkers = await _mainWorkersBuilding.Move(Workers, _workerGoTo, buildProccess);


        if (!isHasWorkers)
        {
            _currencyView.gameObject.SetActive(true);
            _messegeToUser.SendMessageToUser("Не хватает рабочих!", buildProccess.Icon);
            command.IsBuild = false;
            _inProccessing = false;
            _isWorkerGo = false;
            return;
        }
        _profileBinding.BindAnimCurrencyView(false, true);
        _currencyForUser.SetActive(false);
        _textTimeFoUser.gameObject.SetActive(true);
        int countObject = _buildsInterectiveObjects.Count;
        float onOneObjectTime = _maxTimeToBuild / countObject;
        _waiter ??= new AsyncAwaiterTime(onOneObjectTime);

        while (_iterator < countObject)
        {
            await _waiter;
            _buildsInterectiveObjects[_iterator].SetActive(true);
            _iterator++;
            _waiter = new AsyncAwaiterTime(currentTime + onOneObjectTime);
        }

        if(_gameobjectOnCompleted != null)
            _gameobjectOnCompleted.SetActive(true);
        this.gameObject.SetActive(false);


        _messegeToUser.SendMessageToUser($"{buildProccess.Name} закончено!", buildProccess.Icon);
        await _mainWorkersBuilding.MoveBack(Workers, buildProccess);
        _profileBinding.BindAnimCurrencyView(true, false, false);


        if (_dialogView == null) return;
        else
        {
            selectValue.SetValue(null);
            _dialogView.SendDialog(_dialogs, 4, onEndDialog);
        }
    }

    private void onEndDialog()
    {
        if (_clipEffector != null)
            _clipEffector.Play(_clipType);

        if (_clipType == ClipType.DemoEnd && _moveAfterDialog != null)
        {
            
            _gameobjectOnCompleted.GetComponent<NavMeshAgent>().destination = _moveAfterDialog.position;
            if (_hidenGameEnd != null)
                _hidenGameEnd.SetActive(true);
        }
    }
}
