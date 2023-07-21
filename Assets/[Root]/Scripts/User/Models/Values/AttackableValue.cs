using UnityEngine;

[CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Values/" + nameof(AttackableValue),order = 1)]
public class AttackableValue : BaseValue<IAttackable>  { }
