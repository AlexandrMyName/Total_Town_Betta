using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] private float speedRotate;
    [SerializeField] Vector3 axis;

   
    
    private void Update()
    {
        transform.Rotate(axis * speedRotate * Time.deltaTime);
    }
}
