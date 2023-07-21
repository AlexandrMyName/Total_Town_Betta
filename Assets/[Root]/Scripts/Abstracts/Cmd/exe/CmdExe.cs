using UnityEngine;

public abstract class CmdExe<T> : MonoBehaviour, IExecutor where T : ICommand
{
    public void Execute(object command) => SpecificExecute((T)command);
    

    protected abstract void SpecificExecute(T command);
}
