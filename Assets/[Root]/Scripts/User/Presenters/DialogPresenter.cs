using System.Collections;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class DialogPresenter : MonoBehaviour
{
    private DialogObjectValue _model;

    [Inject] private DialogView _view;
    [Inject] private Camera _camera;
    [SerializeField] private InterectionScroll scroll;
    [SerializeField] private bool _isBlockInputAwake;
    [SerializeField, Range(0.1f,1f)] private float _cameraSpeed;

    [Inject(Id = "HidenEndObject")] private GameObject _hidenGameEnd;
    [Inject] private ClipEffector _clipEffector;
    [SerializeField] private ClipType _clipType;
    [SerializeField] private GameObject _ship;
    [SerializeField] private Transform _moveAfterDialog;

    IDialogObject _cachedObj;

    

    private Vector3 _cachedPos;
    private Quaternion _cachedRotation;

    [Inject]
    private void Construct(DialogObjectValue value)
    {
        _model = value;
        _model.OnValueChanged += onClickDialogObject;
    }

    private void onClickDialogObject(IDialogObject obj)
    {
        if(_cachedObj != null && obj == _cachedObj)
        {
            return;
        }
        else
        {
            _cachedRotation = _camera.transform.rotation;
            _cachedPos = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);


           

            _cachedObj = obj;
            StartCoroutine(ChangeCameraToPoint());
             
        }
    }

    private IEnumerator ChangeCameraToPoint()
    {
        scroll.enabled = false;
        float camValue = 0;

        while(_camera.transform.position != _cachedObj.CameraPoint.position)
        {
            Debug.Log("cam");
            camValue += Time.deltaTime /** _cameraSpeed*/;
            _camera.transform.position = Vector3.Lerp(_cachedPos, _cachedObj.CameraPoint.position,camValue);
            _camera.transform.LookAt(_cachedObj.GM.transform.position + Vector3.up * 2);
            if (camValue > 1) break;
            yield return null;  
        }
        _cachedObj.SetAnimation();
        _view.SendDialog(_cachedObj.Dialogs, 5, onCompleteDialog, _cachedObj.Icon);
    }
    private IEnumerator ChangeCameraFromPoint()
    {
        float camValue = 0;

        while (_camera.transform.position != _cachedPos)
        {
            Debug.Log("cam");
            Quaternion currentCamRotation = _camera.transform.rotation;
            camValue += Time.deltaTime /** _cameraSpeed*/;
            _camera.transform.position = Vector3.Lerp(_cachedObj.CameraPoint.position, _cachedPos, camValue);
            _camera.transform.rotation = Quaternion.Lerp(currentCamRotation ,_cachedRotation,camValue);
            if (camValue > 1) break;
            yield return null;
        }
        scroll.enabled = true;
    }
    private void onCompleteDialog()
    {
        StartCoroutine(ChangeCameraFromPoint());
        _cachedObj.GM.SetActive(false);

        if (_cachedObj.IsBegineClipShip)
        {
            if (_clipEffector != null) _clipEffector.Play(_clipType);

            if (_clipType == ClipType.DemoEnd && _moveAfterDialog != null)
            {

                _ship.GetComponent<NavMeshAgent>().destination = _moveAfterDialog.position;
                if (_hidenGameEnd != null)
                    _hidenGameEnd.SetActive(true);
            }
        }
    }
}
