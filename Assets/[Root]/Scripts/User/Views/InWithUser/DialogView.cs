using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DialogView : MonoBehaviour, IAmProccess
{
    [Header("Input this, if wonna play on awake"),Space(2)]
    [SerializeField] private List<string> dialogs;
    [SerializeField] private Sprite _spriteAwake;
    [SerializeField] private EffectsView _effects;
    [Space(20)]
    [SerializeField] private List<GameObject> _hidenObjects;
    [SerializeField] private GameObject _inputScriptsGM;

    [SerializeField] private TMP_Text _dialog;
    [SerializeField] private Image _image;
    [SerializeField] private Button _onAccept;
    [SerializeField] private Button _onCancel;


   
    private AsyncAwaiterTime _waiterTime;

    private bool _isProccess;
   
    float currentTime;
    public bool IsProccess => _isProccess;

    private void Awake()
    {
        if (dialogs.Count == 0)
        {
            this.gameObject.SetActive(false);
            return;

        }
        _inputScriptsGM.SetActive(false);
        _image.sprite = _spriteAwake;
        SendAwakeDialog(4);
    }
    public void SendDialog(List<string> dialogs, Sprite iconFromWho)
    {
        if (_isProccess) return;
    }
    public async void SendAwakeDialog(int secondsBetween)
    {
        if(_isProccess) return;

        _isProccess = true;
        _onAccept.onClick.AddListener(OnClickAccept);
        this.gameObject.SetActive(true);

        foreach(var dialog in dialogs)
        {
            _waiterTime = new AsyncAwaiterTime(secondsBetween);
            _dialog.text = dialog;
            await _waiterTime;
            _onAccept.gameObject.SetActive(true);
            
            _waiterTime = new AsyncAwaiterTime(100);
            await _waiterTime;

        }
        _onAccept.onClick.RemoveAllListeners();
        _onAccept.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        _inputScriptsGM.SetActive(true);
         
        _effects.ActiveSelectedEffect();
        _isProccess = false;
    }


    public async void SendDialog(List<string> dialogs,int secondsBetween, Action onComplete, Sprite icon = null)
    {
        if (_isProccess) return;

        foreach(var hiden in _hidenObjects)
        {
            hiden.SetActive(false);
        }
        if(icon == null)
        {
            _image.sprite = _spriteAwake;
            
            
        }else _image.sprite = icon;

        _inputScriptsGM.SetActive(false);
        _isProccess = true;
        _onAccept.onClick.AddListener(OnClickAccept);
        this.gameObject.SetActive(true);

        foreach (var dialog in dialogs)
        {
            _waiterTime = new AsyncAwaiterTime(secondsBetween);
            _dialog.text = dialog;
            await _waiterTime;
            _onAccept.gameObject.SetActive(true);

            _waiterTime = new AsyncAwaiterTime(100);
            await _waiterTime;

        }
        _onAccept.onClick.RemoveAllListeners();
        _onAccept.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        
        _inputScriptsGM.SetActive(true);

        _effects.ActiveSelectedEffect();
        _isProccess = false;
        onComplete?.Invoke();
        
    }
    private void OnClickAccept()
    {
        _waiterTime.SetValue(_waiterTime._maxTime);
        _onAccept.gameObject.SetActive(false);
    }
    private void Update()
    {
         
        if (_isProccess && _waiterTime != null)
        {
             currentTime += Time.deltaTime;
            _waiterTime.SetValue(currentTime);
        }
    }
}
