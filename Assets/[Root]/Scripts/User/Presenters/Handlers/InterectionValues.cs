using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InterectionValues : MonoBehaviour
{
    #region Values
    [Inject] private SelectableValue _selectableValue;
    [Inject] private Vector3Value _groundClickRMB;
    [Inject] private AttackableValue _attackableValue;
    #endregion

    [SerializeField] private Camera _camera;
    [SerializeField] private EventSystem eventSystem;
    private Plane _plane;
    private void OnValidate()
    {
        _camera ??= Camera.main;
        eventSystem ??= GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
    private void Awake() { 
        _camera ??= Camera.main;
        _plane = new Plane(transform.up, 0);
    }
    
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1)) return;

        bool isPointerOverGM = false;

        Ray ray;
        
#if UNITY_EDITOR
        isPointerOverGM = eventSystem.IsPointerOverGameObject();
        ray = _camera.ScreenPointToRay(Input.mousePosition);

#else
        isPointerOverGM = eventSystem.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
         ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        if (isPointerOverGM) return;
        
        RaycastHit[] hitsAll = Physics.RaycastAll(ray);

        if (hitsAll.Length < 0) return;
        
         

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            if (weHit<ISelectable>(hitsAll, out var selectable))
            {
                _selectableValue.SetValue(selectable);
            }

        }
        else
        {
            if (_plane.Raycast(ray, out var enter))
            {
                Vector3 pointer = ray.GetPoint(enter);
                _groundClickRMB.SetValue(pointer);

            }
            if (weHit<IAttackable>(hitsAll, out var attackable))
            {
                _attackableValue.SetValue(attackable);
            }
        }

#elif UNITY_ANDROID
    if ( Input.GetMouseButton(0) ){
            
        if (_plane.Raycast(ray, out var enterA)){
                Vector3 pointer = ray.GetPoint(enterA);
                _groundClickRMB.SetValue(pointer);
                
        }
        if (weHit<IAttackable>(hitsAll, out var attackable))
        {
                _attackableValue.SetValue(attackable);
        }
        if (weHit<ISelectable>(hitsAll, out var selectable))
        {
                _selectableValue.SetValue(selectable);
        }
    }
#endif

    }

    private bool weHit<T>(RaycastHit[] hitsAll, out T result)
    {
       result = hitsAll
            .Select(sell => sell.collider.GetComponent<T>())
            .Where(sell => sell != null)
            .FirstOrDefault();

        return result != null;
    }
}
