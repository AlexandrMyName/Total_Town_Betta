using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ”ничтожает все обьекты внесенные в реестр не уничтожаемых
/// </summary>
public class SceneObjectDestroyer : MonoBehaviour
{
    
    private void Awake() => Destroyer.DestroyAll();
    
}

public static class Destroyer
{
    private static List<GameObject> _gameobjects;
    public static void DontDestroyOnLoad(this GameObject gm)
    {
        _gameobjects ??= new List<GameObject>();
        GameObject.DontDestroyOnLoad(gm);
        _gameobjects.Add(gm);
    }
    public static void DestroyAll()
    {
        if (_gameobjects == null) return;
        foreach(var obj in _gameobjects)
        {
            GameObject.Destroy(obj);
        }
        _gameobjects.Clear();
    }
}
