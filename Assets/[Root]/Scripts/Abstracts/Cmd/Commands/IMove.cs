using UnityEngine;

public interface IMove : ICommand
{
   Vector3 Target { get; }
}
