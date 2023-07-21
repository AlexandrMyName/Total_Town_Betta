using UnityEngine;
 
public class Unit : MonoBehaviour, ISelectable, IAttackable
{
    [SerializeField] private string _name;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Sprite _icon;  


    public Sprite Icon => _icon;

    public string Name => _name;

    public Transform PointerOfTransform => base.transform;

    public float Health { get => _health; set => _health = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
}
