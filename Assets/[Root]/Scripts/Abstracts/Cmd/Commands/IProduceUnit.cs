using UnityEngine;

public interface IProduceUnit : ICommand
{
    GameObject Object { get; }
}
