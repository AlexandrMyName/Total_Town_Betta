using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(ProfileBinding))]
public class AbilityDelete :  QueueAssemblingExe<IDelete>, IProccess, ICost
{
    [SerializeField] private List<GameObject> _objectsForActive;

    [Space(10), Header("Workers unit (склад)")]
    [SerializeField] private WorkersBuild _mainWorkersBuilding;
    [SerializeField] private Transform _workerGoTo;
   

    [Inject] private SelectableValue _selectableValue;
    [Inject] private MessegeView _messegeToUser;
    [Inject] private CurrencyView _currencyView;

    private int _iterator;
    private bool _isWorkerGo;

    [Inject]
    private void Construct(MessegeView messegeToUser, CurrencyView currencyView, SelectableValue selectValue)
    {
        _messegeToUser = messegeToUser;
        _currencyView = currencyView;
        _selectableValue = selectValue;
        Init(GetComponent<ProfileBinding>(), gameObject.GetComponent<ISelectable>(), _messegeToUser, gameObject.GetComponent<Collider>());
    }

    protected override void RefreshTime()
    {
        if (Waiter != null && IsProccess)
        {
            CurrentTime += Time.deltaTime;
            Waiter.SetValue(CurrentTime);
            UpdateProccess(_maxTimeToDo - CurrentTime);
        }
    }
   
    protected override async void OnCommandExecute(IDelete command)
    {
        if (HasCost() == false) return;

        IsProccess = true;
        _isWorkerGo = true;

        BindMessege($"Рабочие отправлены на снос:  ({_currentSelectable.Name})");

        bool isHasWorkers = await _mainWorkersBuilding.Move(Workers, _workerGoTo, _currentSelectable);

        if (!isHasWorkers)
        {
            CanselCommandProccess(command);
            return;
        }
        BindMessege($"Рабочие приступили  ({_currentSelectable.Name})");
        foreach (GameObject obj in _objectsForActive) obj.SetActive(true);
        _proccessSlider.gameObject.SetActive(true);
        _selectableValue.SetValue(null);
        _collider.enabled = false;


        _currencyView.gameObject.SetActive(false);
        _currentSelectable = gameObject.GetComponent<ISelectable>();
        _proccessSlider.maxValue = _maxTimeToDo;

        float secondsOnOne = _maxTimeToDo / _interectiveObjects.Count;


        Waiter = new AsyncAwaiterTime(secondsOnOne);


        if (_profileBinding != null) _profileBinding.BindAnimCurrencyView(false);

        while (_iterator < _interectiveObjects.Count)
        {
            _interectiveObjects[_iterator].SetActive(false);
            await Waiter;
            Waiter = new AsyncAwaiterTime(CurrentTime + secondsOnOne);
            _iterator++;
        }

        this.gameObject.SetActive(false);
        await _mainWorkersBuilding.MoveBack(Workers, _currentSelectable);

        if (_profileBinding != null) _profileBinding.BindAnimCurrencyView(true, true);

        BindMessege($"Рабочие закончили, добыто Древесины {Woods} шт.");
    }

    protected override void CanselCommandProccess(IDelete command)
    {
        BindMessege("Не хватает рабочих!");

        IsProccess = false;
        _isWorkerGo = false;
    }
    private bool HasCost()
    {
        if (Waiter != null) _currencyView.gameObject.SetActive(false);

        if (_isWorkerGo)
        {
            _currencyView.gameObject.SetActive(false);
            BindMessege($"Ожидание рабочих");
        }

        if (IsProccess) return false;


        if (_profile.GetCurency(CurrencyType.Worker).Count < Workers)
        {
            int noneWorker = Mathf.Abs(_profile.GetCurency(CurrencyType.Worker).Count - Workers);
            noneWorker = Mathf.Min(0, noneWorker);
            BindMessege($"Не хватает рабочих ( {_profile.GetCurency(CurrencyType.Worker).Count - noneWorker})");
            return false;
        }
        return true;
    }
}
