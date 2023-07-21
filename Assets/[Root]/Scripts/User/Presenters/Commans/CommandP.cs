using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CommandsView))]
public class CommandP : MonoBehaviour
{
    [Inject] private CommandsModel _model;
    [Inject] private SelectableValue _selectableValue;
    [SerializeField] private CommandsView _view;
    

    private void OnValidate()
    {
        _view ??= GetComponent<CommandsView>();
    }
    private void Awake()
    {
        _selectableValue.OnValueChanged += bindView;
        _view.OnCommandButtonClick += _model.OnCommandButtonClick;
        _model.OnCansel += _view.UnblockAllInteractions;
        _model.OnAccept += _view.BlockInteraction;
        _model.OnSent += _view.UnblockAllInteractions;
        bindView(null);
        
    }
   
    private void bindView(ISelectable selectable)
    {
        _view.Clear();
        if (selectable == null) return;
 
        List<IExecutor> executorsOnObject = new List<IExecutor>();
        executorsOnObject.AddRange((selectable as Component).GetComponentsInParent<IExecutor>());
        if(executorsOnObject.Count == 0) return;
        _view.MakeLayout(executorsOnObject);
    }
    private void OnDestroy()
    {
        _selectableValue.OnValueChanged -= bindView;
        _model.OnCansel -= _view.UnblockAllInteractions;
        _model.OnAccept -= _view.BlockInteraction;
        _model.OnSent -= _view.UnblockAllInteractions;
    }
}
