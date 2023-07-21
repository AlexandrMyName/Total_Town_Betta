using System.Collections.Generic;
using UnityEngine;

public class CoreTerrain : MonoBehaviour
{
    [SerializeField] private List<GameObject> _planes;   
    

    private List<PlainBuilder> _buildersSet;
    [SerializeField] private GlobalTransformDetector _transformDetector;

    private void Awake()
    {
        _buildersSet ??= new List<PlainBuilder>();
        InitPlanes();
    }
    private void InitPlanes()
    {
        for(int i = 0; i < _planes.Count; i++)
        {
            var plane = _planes[i];

            PlainBuilder builderSet = new PlainBuilder(
               Mathf.FloorToInt( plane.transform.position.x),
                Mathf.FloorToInt(plane.transform.position.y),
                 Mathf.FloorToInt(plane.transform.position.z),
                Mathf.FloorToInt(plane.transform.localScale.x)
                );
            _buildersSet.Add(builderSet);
            _transformDetector.Initialize(builderSet);
        }
    }

    

}


