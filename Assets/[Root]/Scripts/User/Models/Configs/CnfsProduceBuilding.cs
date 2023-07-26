using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CnfsProduceBuilding), menuName = "Configs/" + nameof(CnfsProduceBuilding),order = 1)]
public class CnfsProduceBuilding : ScriptableObject
{
    [SerializeField] public List<CnfProduceBuilding> _configs;

    
}
