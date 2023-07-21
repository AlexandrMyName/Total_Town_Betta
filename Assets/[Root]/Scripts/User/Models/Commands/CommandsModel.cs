using System;
using Zenject;

public class CommandsModel
{
    public Action<IExecutor> OnAccept;
    public Action OnCansel;
    public Action OnSent;

    private bool _isPadding;

    [Inject] private CreatorExeCmd<IProduceUnit> _produceUnit;
    [Inject] private CreatorExeCmd<IHoldPos> _stop;
    [Inject] private CreatorExeCmd<IAttack> _attack;
    [Inject] private CreatorExeCmd<IMove> _move;
    [Inject] private CreatorExeCmd<IPattroll> _pattroll;
    [Inject] private CreatorExeCmd<IBuildProccess> _buildProccess;
    [Inject] private CreatorExeCmd<IDelete> _delete;

    //None commands in right space
    public void OnSpecificCommandButtonClick(IExecutor exe)
    {
        _buildProccess.CreateExe(exe, command => ExecuteAccept(command, exe));
    }
    public void OnCommandButtonClick(IExecutor exe)
    {
        if (_isPadding) ProccessesCansel();
        _isPadding = true;

        OnAccept?.Invoke(exe);

        SentToExecute(exe);
    }
    private void SentToExecute(IExecutor exe)
    {
        _produceUnit.CreateExe(exe, command => ExecuteAccept(command, exe));
        _stop.CreateExe(exe, command => ExecuteAccept(command, exe));
        _attack.CreateExe(exe, command => ExecuteAccept(command, exe));
        _move.CreateExe(exe, command => ExecuteAccept(command, exe));
        _pattroll.CreateExe(exe, command => ExecuteAccept(command, exe));
        _buildProccess.CreateExe(exe, command => ExecuteAccept(command, exe));
        _delete.CreateExe(exe, command => ExecuteAccept(command, exe));
    }
    private void ExecuteAccept(object command, IExecutor exe)
    {
        exe.Execute(command);
        _isPadding = false;
        OnSent?.Invoke();
    }
    public void OnSellectionChanged() => ProccessesCansel();
    
    public void ProccessesCansel()
    {
        _produceUnit.ProccessCansel();
        _stop.ProccessCansel();
        _attack.ProccessCansel();
        _move.ProccessCansel();
        _pattroll.ProccessCansel();
        _isPadding = false;
    }
}
