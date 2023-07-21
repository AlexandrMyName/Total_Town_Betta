using System;

public abstract class CreatorExeCmd <T> where T : ICommand
{
    public void CreateExe(IExecutor exe, Action<T> callback)
    {
       CmdExe<T> cmdExe = exe as CmdExe<T>;
        if (cmdExe != null)
            SpecificExecute(callback);
    }

    protected abstract void SpecificExecute(Action<T> callback);

    public virtual void ProccessCansel() { }
}
