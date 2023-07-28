using System;
using System.Collections.Generic;
using UnityEngine;

public class ProduceBuilderView : MonoBehaviour, IProccess
{
    [SerializeField] List<ProduceBuilderSlotsView> _slotsBuilding = new();
    [SerializeField] GameObject _slotPrefab;
    [SerializeField] Transform _containerForSlots;


    private bool _isProccess;
    public bool IsProccess { get => _isProccess; set => _isProccess = value; }

    public void CanselOperation(Action onCansel , GameObject @object)
    {
         Destroy(@object);
         onCansel?.Invoke();
        _isProccess = false;
    }
    public void InitView(Action <ProduceBuilderSlotsView> slot , List<IBuildingCnf> configs)
    {

        foreach (var config in configs)
        {
            Debug.Log("BindView");
            GameObject prefab = Instantiate(_slotPrefab, _containerForSlots, false);
            ProduceBuilderSlotsView slotView = prefab.GetComponent<ProduceBuilderSlotsView>();

            _slotsBuilding.Add(slotView);

            slotView.Init(config);

            slotView.ButtonClick.onClick.AddListener(() =>
            {
                slot?.Invoke(slotView);
                _isProccess = true;



            });
        }
    }
    private void OnDestroy()
    {
        foreach (var slotView in _slotsBuilding)
            slotView.ButtonClick.onClick.RemoveAllListeners();
        
    }

  
}
