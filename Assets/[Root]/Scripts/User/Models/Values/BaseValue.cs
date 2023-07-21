using System;
using UnityEngine;

public abstract class BaseValue<T> : ScriptableObject , IAwatable<T>
{
    public class NewValueNotifire<TAwaited> : IAwaiter<TAwaited>
    {

        private bool _isCompleted;
        private readonly BaseValue<TAwaited> _baseValue;
        private Action _continuation;
        private TAwaited _result;

        public bool IsCompleted => _isCompleted;

         
        public NewValueNotifire(BaseValue<TAwaited> baseValue)
        {
            _baseValue = baseValue;
            _baseValue.OnValueChanged += onNewValue;
        }
        private void onNewValue(TAwaited awaited)
        {
            _baseValue.OnValueChanged -= onNewValue;
            _result = awaited;
            _isCompleted = true;
            _continuation?.Invoke();
        }
        public TAwaited GetResult() => _result;

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

    public Action<T> OnValueChanged;

    public T Value;

    public IAwaiter<T> GetAwaiter() => new NewValueNotifire<T>(this);
    

    public void SetValue(T value)
    {
        OnValueChanged?.Invoke(value);
        Value = value;
    }
}
