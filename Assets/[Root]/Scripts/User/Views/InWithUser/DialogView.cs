using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour, IProccess, IDialog
{
    
    [SerializeField] private Sprite _spriteAwake;
    
    [Space(20)]
    [SerializeField] private List<GameObject> _hidenObjects;
    [SerializeField] private GameObject _inputScriptsGM;

    [SerializeField] private TMP_Text _dialog;
    [SerializeField] private Image _image;
    [SerializeField] private Button _onAccept;
    [SerializeField] private Button _onCancel;

    private AsyncAwaiterTime _waiterTime;
    private bool _isProccess; 
    private float currentTime;
    public bool IsProccess { get => _isProccess; set => _isProccess = value; }

    public async void SendDialog(List<string> dialogs,int secondsBetween, Action onComplete = null, Sprite icon = null)
    {
        if (_isProccess) return;

        foreach(var hiden in _hidenObjects)
             hiden.SetActive(false);
         
        if(icon == null){

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

        _isProccess = false;
        if (onComplete != null)
        {
            onComplete?.Invoke();
        }

        foreach (var hiden in _hidenObjects)
            hiden.SetActive(true);

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
