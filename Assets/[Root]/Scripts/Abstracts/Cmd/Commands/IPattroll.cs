using UnityEngine;

public interface IPattroll : ICommand
{
    Vector3 From { get; }
    Vector3 To { get; }
}
