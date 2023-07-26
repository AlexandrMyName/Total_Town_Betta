using UnityEngine;

public interface ISelectable : IHealth
{
    Sprite Icon { get; }
    string Name { get; }
    Transform PointerOfTransform { get; }
}
