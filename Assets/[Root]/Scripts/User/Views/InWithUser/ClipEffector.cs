using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
 

public class ClipEffector : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hidenObjects;
    [SerializeField] private PlayableDirector _onDemoEnd;
    [SerializeField] private ClipType _clipType;

    
    public void Play(ClipType type)
    {
        if(type == ClipType.DemoEnd)
        {
            foreach(var obj in _hidenObjects)
            {
                obj.SetActive(false);
            }
            _onDemoEnd.enabled = true;
            _onDemoEnd.Play();
        }
    }
}
public enum ClipType { DemoEnd = 0, None = 1 }