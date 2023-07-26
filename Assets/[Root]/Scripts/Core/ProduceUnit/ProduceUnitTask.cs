using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class ProduceUnitTask : MonoBehaviour, IUnitProduceTask
{
    
    private ReactiveCollection<IUnitProducer> _unitProducers = new();

    [SerializeField] private int _maxUnitsWorkers;
    [Inject] private ProduceUnitsPresenter _presenter;// Find ?
    [Inject] private IUserProfile _userProfile;
    [Inject] private SelectableValue _selectableValue;

   
    private ISelectable _thisSelectable;

    private WorkersBuild _mainBuilding;

    private void Awake()
    {
        _presenter ??= GameObject.FindGameObjectWithTag("ProduceView").GetComponent<ProduceUnitsPresenter>();
        _thisSelectable = gameObject.GetComponent<ISelectable>();
        _selectableValue.OnValueChanged += onNewSelectable;
        _presenter.BindViewButtonsAccept(Add);
        _presenter.BindViewButtonsCansel(Cancel);

        _mainBuilding = GetComponent<WorkersBuild>();
        _unitProducers.ObserveAdd().Subscribe(@event=> { Debug.Log(@event.Value.Name); });
        _unitProducers.ObserveRemove().Subscribe(@event=> { Debug.Log(@event.Value.Name + " Remove"); });
    }
    public void Add(IUnitProducer producer)
    {
        if (_maxUnitsWorkers <= _mainBuilding._movementStopWorkers.Count) return;
        //Переделать в View
        _presenter.RefteshUI_Text(producer,_unitProducers.Count + 1, _mainBuilding._movementStopWorkers.Count, _maxUnitsWorkers);

        if (_selectableValue.Value != _thisSelectable) return;

        _unitProducers.Add(producer);
        _presenter.AddUnit(producer);
        _iteratorCounter++;
        
    }

    public void Cancel()
    {
        
        if (_selectableValue.Value != _thisSelectable) return;

        if(_unitProducers.Count <= 1) return;
        _unitProducers.RemoveAt(_unitProducers.Count - 1); 
        _presenter.RemoveUnitQuere(_unitProducers[_unitProducers.Count - 1]);
        //Во View
        _presenter.RefteshUI_Text(_unitProducers[_unitProducers.Count - 1],_unitProducers.Count, _mainBuilding._movementStopWorkers.Count,  _maxUnitsWorkers);
    }
    private void onNewSelectable(ISelectable selectable)
    {
        WorkersBuild build = null;
        if (selectable != null)
            build = selectable.PointerOfTransform.gameObject.GetComponent<WorkersBuild>();

        if(build == null) _presenter.UnShow();
         
        if(selectable == _thisSelectable)
        {
            _presenter.AllRefreshUI(_unitProducers.Count, isStopWorkersProduce);


        }
    }
    bool isStopWorkersProduce;
    int _iteratorCounter = 1;
    private void Update()
    {

        if(_unitProducers.Count == 0 ) return;

        if (_unitProducers[0].ProducerType == ProducerType.Worker)
        {
            if (isStopWorkersProduce) return;
            if (_maxUnitsWorkers <= _mainBuilding._movementStopWorkers.Count)
            {
                if(_selectableValue.Value == _thisSelectable)
                _presenter.AllRefreshUI(0, true);
                isStopWorkersProduce = true;
               
                return;

            }
        }
       
       
         _unitProducers[0].TimeInSeconds -= Time.deltaTime;

        if(_selectableValue.Value == _thisSelectable)
        _presenter.RefreshUI(_unitProducers[0]);

        Debug.Log(_unitProducers[0].TimeInSeconds);

        if (_unitProducers[0].TimeInSeconds <= 0)
        {
            OnUnitProducerComplete(_unitProducers[0]);
            _unitProducers.Remove(_unitProducers[0]);
        }
    }

    private void OnUnitProducerComplete(IUnitProducer producer)
    {
        switch(producer.ProducerType)
        {
            case ProducerType.Worker:

                GameObject newWorker =  Instantiate(Resources.Load<GameObject>(producer.NameResource),
                    _mainBuilding.ContainerForWorkers,false);
                newWorker.transform.position += new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                _mainBuilding.AddWorker(newWorker.GetComponent<UnitMovementStop>());
                _userProfile.GetCurency(CurrencyType.Worker).Count++;
                if(_selectableValue.Value == _thisSelectable)
                _presenter.RemoveUnitQuere(producer);
                _presenter.RefteshUI_Text(producer,_unitProducers.Count -1, _mainBuilding._movementStopWorkers.Count, _maxUnitsWorkers);
                break;


            case ProducerType.TownerFinder:

                break;

           
        }
    }
}
