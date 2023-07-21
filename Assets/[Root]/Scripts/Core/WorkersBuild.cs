using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WorkersBuild : MonoBehaviour
{
    [Space(10), Header("Workers unit (склад)")]
    [SerializeField] private List<UnitMovementStop> _movementStopWorkers;
    [SerializeField] private Transform _cachedPosition;

    [SerializeField] private TMP_Text textCountWorkers;


    private int workersInProcess;
    private List<Worker> workers = new List<Worker>();
    
    private void Awake()
    {
        textCountWorkers.text = $"{_movementStopWorkers.Count -  workersInProcess}/{_movementStopWorkers.Count}";
    }
    public async Task<bool> Move(int count, Transform workerGoTo)
    {
        if (_movementStopWorkers != null)
        {
            int expand = _movementStopWorkers.Count - workers.Count;
            if (expand < count) return false;

            UnitMovementStop lastWorker = lastWorker = _movementStopWorkers[0];
            textCountWorkers.text = $"{_movementStopWorkers.Count - workersInProcess}/{_movementStopWorkers.Count}";
            if (count > _movementStopWorkers.Count) return false;
            //Message

            while (lastWorker != null)
            {
                for (int i = 0; i < count; i++)
                {

                    var WorkerGo = _movementStopWorkers[i];
                    Worker worker = WorkerGo.GetComponent<Worker>();

                    if (worker.IsProccess) continue;

                    workers.Add(worker);
                    workersInProcess++;
                }

                if (workersInProcess < count) return false;



                foreach (Worker worker in workers)
                {
                    if (worker.IsProccess) continue;

                    worker.gameObject.SetActive(true);
                    worker.IsProccess = true;

                    NavMeshAgent agentWorker = worker.gameObject.GetComponent<NavMeshAgent>();
                    agentWorker.destination = workerGoTo.position;
                    lastWorker = worker.gameObject.GetComponent<UnitMovementStop>();
                }


                textCountWorkers.text = $"{_movementStopWorkers.Count - workersInProcess}/{_movementStopWorkers.Count}";

                await lastWorker;
                foreach (Worker worker in workers)
                {
                    if (!worker.IsProccess) continue;
                    worker.gameObject.SetActive(false);
                }
                break;
            }

         

        }
        return true;
    }
    List<Worker> workersGoBack = new List<Worker>();
    public async Task<bool> MoveBack(int count)
    {
        UnitMovementStop WorkerGo = null;
   
       
        for (int i = 0; i < count; i++)
        {
             WorkerGo = _movementStopWorkers[i];

            Worker worker = WorkerGo.GetComponent<Worker>();

            if (!worker.IsProccess) continue;

            worker.gameObject.SetActive(true);
            NavMeshAgent agentWorker = WorkerGo.GetComponent<NavMeshAgent>();
            agentWorker.destination = worker._cahedPos;

            workers.Remove(worker);
            workersGoBack.Add(worker);
        }
        await WorkerGo;

        foreach (Worker worker in workersGoBack)
        {
            worker.IsProccess = false;
            workersInProcess--;
            worker.gameObject.SetActive(false);
        }

        textCountWorkers.text = $"{_movementStopWorkers.Count - workersInProcess}/{_movementStopWorkers.Count}";
        return true;
    }
}
