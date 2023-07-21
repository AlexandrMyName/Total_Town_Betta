using UnityEngine;

public class CmdPattroll : IPattroll
{
    public Vector3 From { get; }

    public Vector3 To { get; }

    public CmdPattroll(Vector3 from, Vector3 to)
    {
        From = from;
        To = to;
    }
}
