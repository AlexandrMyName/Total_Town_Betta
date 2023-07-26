using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ProduceBuildsConfigs), menuName = "Configs/" + nameof(ProduceBuildsConfigs),order = 1)]
public class ProduceBuildsConfigs : ScriptableObject
{
    [SerializeField] public List<ProduceBuilderConfig> _configs;

    
}
