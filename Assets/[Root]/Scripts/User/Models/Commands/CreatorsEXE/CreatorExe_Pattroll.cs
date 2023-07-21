using UnityEngine;
using Zenject;

public class CreatorExe_Pattroll : CansellExeCmdCreator<IPattroll,Vector3>
{
    [Inject] private Vector3Value _groundClick;

    protected override IPattroll createCmd(Vector3 argument) 
        => new CmdPattroll(_groundClick.Value, argument);
}
