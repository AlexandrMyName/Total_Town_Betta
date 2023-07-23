using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WorkersBuild : MonoBehaviour
{
    [Space(10), Header("Workers unit (склад)")]
    [SerializeField] private List<UnitMovementStop> _movementStopWorkers; //Global
    [SerializeField] private Transform _cachedPosition;
    [SerializeField] private TMP_Text textCountWorkers;

    private int _workersInProcess;
    private List<Worker> _workers = new List<Worker>(); //InWorkDo
    private List<Worker> _workersGoBack = new List<Worker>(); //InWorkersGoBack
    private List<Worker> _cachedWorkersBackEnd = new List<Worker>();//InWorkerEnd

    private void Awake() => RefreshUI();

    public async Task<bool> Move(int count, Transform workerGoTo, ISelectable selectableCall)
    {
        if (_movementStopWorkers == null) return false;

        int expand = _movementStopWorkers.Count - _workers.Count;
        if (expand < count) return false;

        UnitMovementStop lastWorker = _movementStopWorkers[0];
        RefreshUI();

        if (count > _movementStopWorkers.Count) return false;


        int iteratorCounter = 0;


        while (lastWorker != null)
        {
            for (int i = 0; i < _movementStopWorkers.Count; i++)
            {
                if (iteratorCounter >= count) continue;

                var WorkerGo = _movementStopWorkers[i];
                Worker worker = WorkerGo.GetComponent<Worker>();

                if (worker.Binded_selectable != null) continue;
                if (worker.IsProccess) continue;

                worker.Binded_selectable = selectableCall;
                iteratorCounter++;
                _workers.Add(worker);
                _workersInProcess++;
            }

            if (_workersInProcess < count) return false;



            foreach (Worker worker in _workers)
            {
                if (worker.IsProccess) continue;

                if (worker.Binded_selectable != selectableCall && worker.Binded_selectable != null) continue;
                worker.gameObject.SetActive(true);
                worker.IsProccess = true;

                NavMeshAgent agentWorker = worker.gameObject.GetComponent<NavMeshAgent>();
                agentWorker.destination = workerGoTo.position;
                lastWorker = worker.gameObject.GetComponent<UnitMovementStop>();
            }
            RefreshUI();

            await lastWorker;

            foreach (Worker worker in _workers)
            {
                if (worker.Binded_selectable != selectableCall && worker.Binded_selectable != null) continue;
                if (!worker.IsProccess) continue;
                worker.gameObject.SetActive(false);
            }
            break;
        }
        return true;
    }

    public async Task<bool> MoveBack(int count, ISelectable selectableCall)
    {
        await GoBack(selectableCall,count);
        EndWork(selectableCall);
        RefreshUI();
        return true;
    }
    private void RefreshUI() => textCountWorkers.text = $"{_movementStopWorkers.Count - _workersInProcess}/{_movementStopWorkers.Count}";
    private UnitMovementStop GoBack(ISelectable selectableCall, int count)
    {
        UnitMovementStop WorkerGo = null;
        int iteratorCounter = 0;
        for (int i = 0; i < _movementStopWorkers.Count; i++)
        {
            if (iteratorCounter >= count) continue;

            WorkerGo = _movementStopWorkers[i];

            Worker worker = WorkerGo.GetComponent<Worker>();

            if (worker.Binded_selectable != selectableCall && worker.Binded_selectable != null) continue;
            if (!worker.IsProccess) continue;

            iteratorCounter++;
            worker.gameObject.SetActive(true);
            NavMeshAgent agentWorker = WorkerGo.GetComponent<NavMeshAgent>();
            agentWorker.destination = worker._cahedPos;

            _workers.Remove(worker);
            _workersGoBack.Add(worker);
        }
        return WorkerGo;
    }
    private void EndWork(ISelectable selectableCall)
    {
        foreach (Worker worker in _workersGoBack)
        {
            if (worker.Binded_selectable != selectableCall && worker.Binded_selectable != null) continue;

            worker.IsProccess = false;

            _workersInProcess--;
            worker.gameObject.SetActive(false);
            _cachedWorkersBackEnd.Add(worker);
        }
        foreach (Worker worker in _cachedWorkersBackEnd)
        {
            if (worker.Binded_selectable != selectableCall && worker.Binded_selectable != null) continue;

            _workersGoBack.Remove(worker);
            worker.Binded_selectable = null;
        }
    }
}
