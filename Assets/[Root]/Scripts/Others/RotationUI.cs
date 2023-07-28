 
using UnityEngine;
using Zenject;

public class RotationUI : MonoBehaviour
{
    private Camera _camera;

    public void Update()
    {
        this.transform.LookAt(_camera.transform);
    }

    [Inject] private void Construct(Camera cam) => _camera = cam; 
}
