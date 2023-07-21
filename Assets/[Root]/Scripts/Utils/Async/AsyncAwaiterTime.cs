using System;
public class AsyncAwaiterTime : IAwatable<AsyncExt.Void>
{
    public Action<float> onCompleted;
    public float _maxTime;

    public AsyncAwaiterTime(float maxTime) => _maxTime = maxTime;

    public void SetValue(float time) => onCompleted?.Invoke(time);
    public IAwaiter<AsyncExt.Void> GetAwaiter() => new LoadSceneAsyncOperation(this, _maxTime);

    public class LoadSceneAsyncOperation : IAwaiter<AsyncExt.Void>
    {
        private bool _isCompleted;
        private float _maxValue;
        private Action _continue;
        private AsyncAwaiterTime _notifire;
        public bool IsCompleted => _isCompleted;

        public LoadSceneAsyncOperation(AsyncAwaiterTime notifire, float maxTimeOnComplete)
        {
            _notifire = notifire;
            _maxValue = maxTimeOnComplete;
            _notifire.onCompleted += onCompleted;
        }
        private void onCompleted(float currentValue)
        {
            if (currentValue >= _maxValue)
            {
                _notifire.onCompleted -= onCompleted;
                _isCompleted = true;
                _continue?.Invoke();
            }
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
                _continue = continuation;
            }
        }
    }

}
