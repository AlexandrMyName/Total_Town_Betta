
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProduceUnitsPresenter : MonoBehaviour
{
    [SerializeField] private Button _onHideView;

    [SerializeField] private ProduceUnitsSlotView _workersSlot;
    [SerializeField] private ProduceUnitsSlotView _townFindersSlot;

    [SerializeField] private Image _imageForHiden;
    [SerializeField] private List<GameObject> _HidensInView;
   

    public void Show() => SetActive(true);
    public void UnShow() => SetActive(false);

    private void Awake() => _onHideView.onClick.AddListener(() => UnShow());
    private void OnDestroy() => _onHideView.onClick.RemoveAllListeners();
    
    private void SetActive(bool isActive)
    {
        _imageForHiden.enabled = isActive;
        foreach (GameObject go in _HidensInView)
        {
            go.SetActive(isActive);
        }
    }
    public void BindViewButtonsAccept(Action<IUnitProducer> method)
    {
        _workersSlot._accept.onClick.AddListener(() =>
        {
            method?.Invoke(new UnitProducer("Рабочий", 10, "Worker_GoMaterial", ProducerType.Worker));
        });
    }
    public void BindViewButtonsCansel(Action method)
    {
        _workersSlot._cansel.onClick.AddListener(() =>
        {
            method?.Invoke();
        });
    }

    public void AddUnit(IUnitProducer producer)
    {
        switch (producer.ProducerType)
        {
            case ProducerType.Worker:

                _workersSlot.Add();
                break;


        }
    }
    
    public void RemoveUnitQuere(IUnitProducer producer)
    {
        switch (producer.ProducerType)
        {
            case ProducerType.Worker:

                _workersSlot.RemoveIconSlot();
                break;


        }
    }
   
    public void RefreshUI(IUnitProducer producer)
    {
        switch (producer.ProducerType)
        {
            case ProducerType.Worker:

                _workersSlot.RefreshSlider(producer);
                break;


        }
    }
    public void AllRefreshUI(int currentQuereCount, bool clearAll = false)
    {
        _workersSlot.RemoveAllIconsAndRefresh(currentQuereCount, clearAll);
    }
    
    public void RefteshUI_Text(IUnitProducer producer ,int currentCountinQuere, int iterator , int max)
    {
        if(producer.ProducerType == ProducerType.Worker) 
         _workersSlot.SetText(currentCountinQuere,iterator, max);
    }
}
