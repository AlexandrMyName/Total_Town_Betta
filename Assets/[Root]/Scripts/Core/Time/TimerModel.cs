using System;
using UniRx;
using UnityEngine;
using Zenject;

public class TimerModel : ITickable, ITimer
{
    
    public IObservable<int> GameTime => _gameTime.Select(f => (int)f);

    private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

    bool _isInitialize;
    
    public void Tick()
    {
        if(!_isInitialize) InitRandomTime();
        _gameTime.Value += Time.deltaTime;
    }

    
    private void InitRandomTime()
    {
        _gameTime.Value = UnityEngine.Random.Range(200,1000);
        _isInitialize = true;
    }
}
