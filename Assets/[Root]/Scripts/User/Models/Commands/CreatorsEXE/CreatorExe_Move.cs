using UnityEngine;
using Zenject;

public class CreatorExe_Move : CansellExeCmdCreator<IMove,Vector3>
{
    [Inject] private Vector3Value _groundClickRMB;

    protected override IMove createCmd(Vector3 argument) => new CmdMove(argument);
}
