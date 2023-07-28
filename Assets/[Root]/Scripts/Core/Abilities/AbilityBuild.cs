using UnityEngine;
using Zenject;

[RequireComponent(typeof(BuildProccesses), typeof(ProfileBinding))]
public class AbilityBuild : QueueAssemblingExe<IBuildProccess>, IProccess, ICost
{
    [Space(10), Header("Workers unit (склад)")]
    [SerializeField] private WorkersBuild _mainWorkersBuilding;//Find [REFACTORING] BUILD IN SPACE
    [SerializeField] private Transform _workerGoTo;

    [Inject] private MessegeView _messegeToUser;
    [Inject] private CurrencyView _currencyView;
    [Inject] private SelectableValue _selectValue;

    private bool _isWorkerGo;
    private int _iterator;

    [Inject]
    private void Construct(MessegeView messegeToUser, CurrencyView currencyView, SelectableValue selectValue)
    {
        _messegeToUser = messegeToUser;
        _currencyView = currencyView;
        _selectValue = selectValue;
        Init(GetComponent<ProfileBinding>(), gameObject.GetComponent<ISelectable>(), _messegeToUser, gameObject.GetComponent<Collider>());
    }
    protected override void RefreshTime()
    {
        if (Waiter != null)
        {
            CurrentTime += Time.deltaTime;
            Waiter.SetValue(CurrentTime);
            UpdateProccess(CurrentTime);
        }
    }
    protected override async void OnCommandExecute(IBuildProccess command)
    {
        if (GetProccessState() == false) return;

        command.IsBuild = true;
        _inProccessing = true;
        _isWorkerGo = true;
        DisposeCurrencyView(null, true);

        bool isHasWorkers = await _mainWorkersBuilding.Move(Workers, _workerGoTo, _currentSelectable);

        if (!isHasWorkers)
        {
            CanselCommandProccess(command);
            return;
        }
        _selectValue.SetValue(null);
        BegineCommandExe();

        _profileBinding.BindAnimCurrencyView(false, true);
        _currencyView.gameObject.SetActive(false); //Find [REFACTORING]

        int countObject = _interectiveObjects.Count;
        float onOneObjectTime = _maxTimeToDo / countObject;
        Waiter ??= new AsyncAwaiterTime(onOneObjectTime);

        while (_iterator < countObject)
        {
            await Waiter;
            _interectiveObjects[_iterator].SetActive(true); 
            _iterator++;
            Waiter = new AsyncAwaiterTime(CurrentTime + onOneObjectTime);
        }

        if (_gameobjectOnCompleted != null)
            _gameobjectOnCompleted.SetActive(true);
        this.gameObject.SetActive(false);

        BindMessege($"{_currentSelectable.Name} закончено!");
        await _mainWorkersBuilding.MoveBack(Workers, _currentSelectable);
        _profileBinding.BindAnimCurrencyView(true, false, false);
    }
    
    protected override void CanselCommandProccess(IBuildProccess command)
    {
        _currencyView.gameObject.SetActive(true);
        BindMessege("Не хватает рабочих!");
        command.IsBuild = false;
        _inProccessing = false;
        _isWorkerGo = false;
    }

    private void DisposeCurrencyView(string messege, bool withoutMessege = false)
    {
        _currencyView.gameObject.SetActive(false);
        if (withoutMessege == false) BindMessege(messege);
    }
    private bool GetProccessState()
    {
        if (Waiter != null)
        {

            DisposeCurrencyView("Уже в процессе!");
            return false;
        }
        else
        {
            if (_isWorkerGo)
            {

                DisposeCurrencyView("Ожидаем материалы...");
                return false;
            }

            if (!HasCost())
            {

                if (_mainWorkersBuilding != null && _mainWorkersBuilding._movementStopWorkers.Count < Workers)
                {

                    BindMessege("Не хватает рабочих!");
                    return false;
                }
                else if (_mainWorkersBuilding == null)
                {

                    BindMessege("Хижина рабочих очень далеко!");
                }

                BindMessege("Не хватает материалов!");
                return false;
            }
            BindMessege("Начинаем!");
            return true;
        }
    }
    private bool HasCost()
    {
        if (
            _profile.GetCurency(CurrencyType.Wood).Count < Woods ||

            _mainWorkersBuilding._movementStopWorkers.Count < Workers ||

            _profile.GetCurency(CurrencyType.Diamond).Count < Diamonds ||

            _profile.GetCurency(CurrencyType.Iron).Count < Irons)

        { return false; }

        return true;
    }
}
