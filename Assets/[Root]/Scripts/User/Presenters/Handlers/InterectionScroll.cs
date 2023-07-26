using UnityEngine;
using Zenject;

public class InterectionScroll : MonoBehaviour
{
    
    [field: SerializeField] private bool UseRotation { get; set; }
    [SerializeField] private float sensetivity = 0.5f;
    [Space, Header("Clamp camera values")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    private Plane _plane;

    private Camera _camera;
    

    [Inject]
    private void Constract(Camera camera)
    {
        _camera = camera;
#if !UNITY_ANDROID

sensetivity = 100f;
#endif
    }




    private void Update()
    {
#if UNITY_ANDROID
 

        if(Input.touchCount >= 1)
        {
            _plane.SetNormalAndPosition(transform.up, transform.position);

        }
        Vector3 Delta1 = Vector3.zero;
        Vector3 Delta2 = Vector3.zero;

        //Position
        if(Input.touchCount >= 1)
        {
            Delta1 = GetPlaneDeltaPosition(Input.GetTouch(0));
            if(Input.GetTouch(0).phase == TouchPhase.Moved)
            _camera.transform.Translate(Delta1 * sensetivity, Space.World);
        }
        //Rotation and pinch
        if(Input.touchCount >= 2)
        {
            var xpos1 = GetPlanePosition(Input.GetTouch(0).position);
            var xpos2 = GetPlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var ypos1 = GetPlanePosition(Input.GetTouch(1).position);
            var ypos2 = GetPlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            float zoom = Vector3.Distance(ypos1,xpos1)/ Vector3.Distance(ypos2 ,xpos2);

            if (zoom == 0 || zoom > 10) return;

            _camera.transform.position = Vector3.LerpUnclamped(xpos1,_camera.transform.position, 1/zoom);

            if (UseRotation && ypos2 != ypos1)
            {
                _camera.transform.RotateAround(xpos1,_plane.normal, Vector3.SignedAngle(ypos1 - xpos1, ypos2 - xpos2, _plane.normal));
            }
        }
#endif


#if UNITY_EDITOR
        _camera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 20 ;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, minY, maxY);


       // if (!Input.GetMouseButton(1)) return;
        if (Input.mousePosition.y >= Screen.height - 2)
        {
            _camera.transform.position += Vector3.forward * 0.6f;
        }
        else if (Input.mousePosition.y <= 2)
        {
            _camera.transform.position -= Vector3.forward * 0.6f;
        }
        if(Input.mousePosition.x >= Screen.width - 2)
        {
            _camera.transform.position += Vector3.right * 0.6f;
        }
        else if (Input.mousePosition.x <= 2)
        {
            _camera.transform.position -= Vector3.right * 0.6f;
        }

#endif
    }


    private Vector3 GetPlaneDeltaPosition(Touch touch)
    {
        if(touch.phase != TouchPhase.Moved) return Vector3.zero;

        Ray rayNow = Camera.main.ScreenPointToRay(touch.position);
        Ray rayBefor = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);

        if (_plane.Raycast(rayNow, out var enter) && _plane.Raycast(rayBefor,out var enterBefor))
        {
            return rayBefor.GetPoint(enterBefor) - rayNow.GetPoint(enter);
        }
        return Vector3.zero;
    }

    private Vector3 GetPlanePosition(Vector3 pos)
    {
         
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if(_plane.Raycast(ray,out var enter))
        {
           return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }
}
