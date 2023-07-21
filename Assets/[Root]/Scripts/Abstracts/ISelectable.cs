using UnityEngine;

public interface ISelectable : IDoHealth
{
    Sprite Icon { get; }
    string Name { get; }
    Transform PointerOfTransform { get; }
}
