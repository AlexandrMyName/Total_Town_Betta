 
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ProduceBuilderConfig), menuName = "Configs/" + nameof(ProduceBuilderConfig), order = 1)]
public class ProduceBuilderConfig : ScriptableObject, IBuilderConfig
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public string ResourceID { get; private set; }

    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField] public int Woods { get;  set; }
    [field: SerializeField] public int Diamonds { get;  set; }
    [field: SerializeField] public int Workers { get;  set; }
    [field: SerializeField] public int Irons { get;  set; }
   
}
