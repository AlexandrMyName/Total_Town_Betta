using UnityEngine;

public class CmdMove : IMove
{
    public CmdMove(Vector3 target) => Target = target;
    public Vector3 Target { get; }
}
