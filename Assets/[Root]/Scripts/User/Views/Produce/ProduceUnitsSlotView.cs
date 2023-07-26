using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProduceUnitsSlotView : MonoBehaviour
{
   
    [SerializeField] private Slider _sliderProduce;

    public Button _cansel;
    public  Button _accept;

    [SerializeField] private Transform _containerForIcons;

    [SerializeField] private GameObject _prefabIconQuere;

    [SerializeField] private TMP_Text _textCount;
    [SerializeField] private TMP_Text _textMaxCount;

    private List<GameObject> _iconsObjects = new();


    public void Add()
    {
       _iconsObjects.Add(Instantiate(_prefabIconQuere, _containerForIcons));
    }
    public void RemoveIconSlot()
    {
        if (_iconsObjects.Count < 1) return;
        GameObject gm = _iconsObjects[_iconsObjects.Count - 1];
        _iconsObjects.RemoveAt(_iconsObjects.Count - 1);
        Destroy(gm);
        Debug.Log("обьект уничтожен");
    }
    public void RemoveAllIconsAndRefresh(int countIcons, bool clearAll)
    {
       
        foreach(GameObject gm in _iconsObjects)
        {

            Destroy(gm);
        }
        _iconsObjects.Clear();
        if(clearAll) return;

        for(int i = 0; i < countIcons; i++)
        {
            Add();
        }
    }
    public void RefreshSlider(IUnitProducer producer)
    {
        _sliderProduce.maxValue = producer.MaxTimeInSeconds;
        _sliderProduce.value = producer.TimeInSeconds;

    }
    public void SetText(int currentCountinQuere, int iterator, int max)
    {
       
            _textCount.text = $"{currentCountinQuere}";
        _textMaxCount.text = $"{iterator}/{max}";
    }
}
