using System;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName =nameof(AssetsContext),menuName = "IoC/Custom/" + nameof(AssetsContext), order = 1)]
public class AssetsContext : ScriptableObject
{
   [SerializeField] private Object[] _objects;
    

    public Object GetObjectOfType(Type type, string name = null) 
    {
        for(int i = 0; i < _objects.Length; i++)
        {
            Object currentObj = _objects[i];

            if(currentObj.name == name || name == null )
                return currentObj;
        }
        return null;
    }
}
