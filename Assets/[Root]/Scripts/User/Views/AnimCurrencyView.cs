using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimCurrencyView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private string _resourcesPath;
    [SerializeField] private List<SequenceCurrencyType> _currencies;

    private List<GameObject> _prefabs = new List<GameObject>();

    private AsyncAwaiterTime _waiter;
    private float _currentTime;

    [Serializable]
    public class SequenceCurrencyType
    {
        public Sprite Icon;
        public CurrencyType Type;
    }
    private void Update()
    {
        if( _waiter != null)
        {
            _currentTime += Time.deltaTime;
            _waiter.SetValue(_currentTime);
        }
    }
    public void RefreshUI(CurrencyType type, float befor, float now) => SetSlot(type,befor, now);
    
    private void SetSlot(CurrencyType type, float befor, float now)
    {
        GameObject slotGm = Resources.Load<GameObject>(_resourcesPath);

        var slot = GameObject.Instantiate(slotGm, _container);
        _prefabs.Add(slot);
        AnimCurrencySlotView slotView = slot.GetComponent<AnimCurrencySlotView>();
        slotView.SetSlot(_currencies.Where(currecy => currecy.Type == type).First().Icon, befor, now);
    }
    public void SetTimerToDispose(int seconds) => Timer(seconds);
     
    private async void Timer(int maxTime)
    {
        _waiter = new AsyncAwaiterTime(maxTime);
        await _waiter;
        _waiter = null;
        _currentTime = 0;
        ClearView();
    }

    public void ClearView()
    {
       for(int i = 0; i < _prefabs.Count; i++)
            Destroy(_prefabs[i]);
         
    }
}
