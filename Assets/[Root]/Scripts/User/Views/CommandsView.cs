using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CommandsView : MonoBehaviour
{
    [Header("Buttons")]
    public Action<IExecutor> OnCommandButtonClick;
    [SerializeField] private GameObject _attackButtonGM;
    [SerializeField] private GameObject _moveButtonGM;
    [SerializeField] private GameObject _pattrollButtonGM;
    [SerializeField] private GameObject _holdPosButtonGM;
    [SerializeField] private GameObject _produceUnitButtonGM;
    [SerializeField] private GameObject _buildButtonGM;
    [SerializeField] private GameObject _deleteButtonGM;
    [Space(10)]
    [SerializeField] private GameObject _currencyView;

    private Dictionary<Type, GameObject> _cmdButtons;


    [Inject] private IUserProfile _userProfile;
    private void AddButtons()
    {
        _cmdButtons.Add(typeof(CmdExe<IAttack>), _attackButtonGM);
        _cmdButtons.Add(typeof(CmdExe<IMove>), _moveButtonGM);
        _cmdButtons.Add(typeof(CmdExe<IPattroll>), _pattrollButtonGM);
        _cmdButtons.Add(typeof(CmdExe<IHoldPos>), _holdPosButtonGM);
        _cmdButtons.Add(typeof(CmdExe<IProduceUnit>), _produceUnitButtonGM);
        _cmdButtons.Add(typeof(CmdExe<IBuildProccess>), _buildButtonGM);
        _cmdButtons.Add(typeof(CmdExe<IDelete>), _deleteButtonGM);
    }
    private void Awake()
    {
        _cmdButtons = new Dictionary<Type, GameObject>();
        AddButtons();
    }

    #region View_Logic
    public void MakeLayout(List<IExecutor> exexutors)
    {
        for(int i = 0; i < exexutors.Count; i++)
        {
            IExecutor executor = exexutors[i];

            Button button = GetSelectableOfType(executor.GetType()) as Button;
            button.gameObject.SetActive(true);
            button.onClick.AddListener(() => OnCommandButtonClick?.Invoke(executor));

            _currencyView.SetActive(false);
             
            
            if (executor is CmdExe<IBuildProccess> || executor is CmdExe<IDelete>)
            {
                BuildProccessBindCosts(executor);
            }

        }
    }

    private void BuildProccessBindCosts(IExecutor executor)
    {
        var view = _currencyView.GetComponent<CurrencyView>();
        IAmProccess proccess = (IAmProccess)executor;
        ICost costs = (ICost)executor;
        _currencyView.SetActive(proccess.IsProccess != true);

        var diamondCount = _userProfile.Currencies.Where(x => x.Type == CurrencyType.Diamond).FirstOrDefault().Count;
        var woodCount = _userProfile.Currencies.Where(x => x.Type == CurrencyType.Wood).FirstOrDefault().Count;
        var workerCount = _userProfile.Currencies.Where(x => x.Type == CurrencyType.Worker).FirstOrDefault().Count;
        var ironCount = _userProfile.Currencies.Where(x => x.Type == CurrencyType.Iron).FirstOrDefault().Count;
 

        char charCurrent = executor is CmdExe<IBuildProccess> ? '-' : '+';
        
            if (view.Diamonds)
            {
                view.Diamonds.text = $"{diamondCount}/ {charCurrent}{costs.Diamonds} ";
            if (diamondCount != 0 && costs.Diamonds != 0)
            {
                if(executor is CmdExe<IBuildProccess>)
                view.Diamonds.color = Color.Lerp(Color.red, Color.green, diamondCount / costs.Diamonds);
                else
                {
                    view.Diamonds.color = Color.Lerp(Color.red, Color.green,  costs.Diamonds);
                }
            }
            }
            if (view.Woods)
            {
                view.Woods.text = $"{woodCount}/ {charCurrent}{costs.Woods} ";
           
                if (woodCount != 0 && costs.Woods != 0)
                {
                    if (executor is CmdExe<IBuildProccess>)
                        view.Woods.color = Color.Lerp(Color.red, Color.green, woodCount / costs.Woods);
                    else
                    {
                        view.Woods.color = Color.Lerp(Color.red, Color.green, costs.Woods);
                    }
                }
            }
            if (view.Workers)
            {
                view.Workers.text = $"{workerCount}/ - {costs.Workers} ";
                if (workerCount != 0 && costs.Workers != 0)
                {
                    if (executor is CmdExe<IBuildProccess>)
                        view.Workers.color = Color.Lerp(Color.red, Color.green, workerCount / costs.Workers);
                else
                {
                    view.Workers.color = Color.Lerp(Color.red, Color.green, workerCount / costs.Workers);
                }
            }
            }
            if (view.Irons)
            {
             view.Irons.text = $"{ironCount}/ {charCurrent}{costs.Irons} ";
                if (ironCount != 0 && costs.Irons != 0)
                {
                    if (executor is CmdExe<IBuildProccess>)
                        view.Irons.color = Color.Lerp(Color.red, Color.green, ironCount / costs.Irons);
                    else
                    {
                        view.Irons.color = Color.Lerp(Color.red, Color.green, costs.Irons);
                    }
                }
            }
        
        
    }

    public void BlockInteraction(IExecutor exe)
    {
        Selectable button = GetSelectableOfType(exe.GetType());
        SetInterectable(true);
        button.interactable = false;
    }
    public void UnblockAllInteractions() => SetInterectable(true);
   
    private void SetInterectable(bool isInteractable)
    {
        foreach (var gm in _cmdButtons.Values)
        {
            GameObject currentGm = gm;
            Selectable currentButton = currentGm.GetComponent<Selectable>();
            currentButton.interactable = isInteractable;
        }
    }
    private Selectable GetSelectableOfType(Type type)
    {
        var cmd =  _cmdButtons.Where(cmd => cmd.Key.IsAssignableFrom(type)).First().Value;
         
        return cmd.GetComponent<Selectable>(); ;
    }
    public void Clear()
    {
        _currencyView.SetActive(false);
        if (_cmdButtons == null) return;
        foreach(var gm in _cmdButtons)
        {
            GameObject currentGm = gm.Value;
            Button currentButton = currentGm.GetComponent<Button>();
            currentButton.onClick.RemoveAllListeners();
            currentGm.SetActive(false);
        }
    }
    #endregion
}
