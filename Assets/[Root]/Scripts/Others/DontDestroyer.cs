using UnityEngine;

public class DontDestroyer : MonoBehaviour
{
    
    private void Start() => Destroyer.DontDestroyOnLoad(this.gameObject);
    
}
