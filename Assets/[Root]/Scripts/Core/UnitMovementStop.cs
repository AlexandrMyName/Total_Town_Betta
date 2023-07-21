using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementStop : MonoBehaviour, IAwatable<AsyncExt.Void>
{
    [SerializeField] private NavMeshAgent _agent;
    public Action OnStop;

    public IAwaiter<AsyncExt.Void> GetAwaiter() => new UnitStopNotifire(this);


    private void OnValidate()
    {
        _agent ??= GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (!_agent.pathPending)
        {
            if(_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if(!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    OnStop?.Invoke();
                }
            }
        }   
    }
    public class UnitStopNotifire : IAwaiter<AsyncExt.Void>
    {
        private bool _isCompleted;
        private Action _continuation;
        UnitMovementStop _unitMovementStop;
        public bool IsCompleted => _isCompleted;


        public UnitStopNotifire(UnitMovementStop stop)
        {
            _unitMovementStop = stop;
            _unitMovementStop.OnStop += onStop;
        }

        private void onStop()
        {
            _unitMovementStop.OnStop -= onStop;
            _isCompleted = true;
            _continuation?.Invoke();
        }
        public AsyncExt.Void GetResult() => new AsyncExt.Void();
       
        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
            {
                continuation?.Invoke();
            }
            else
            {
                _continuation = continuation;
            }
        }

    }
}
