using UnityEngine;

[CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Values/" + nameof(SelectableValue),order = 1)]
public class SelectableValue : BaseValue<ISelectable> { }
