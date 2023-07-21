using UnityEngine;

public class Worker : MonoBehaviour, IAmProccess
{
   
    public bool IsProccess { get; set; }
    public Vector3 _cahedPos;

    private void Awake()
    {
        _cahedPos = this.transform.position;
    }
}
