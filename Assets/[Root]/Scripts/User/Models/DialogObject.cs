using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DialogObject : MonoBehaviour, IDialogObject
{
    [SerializeField] private List<string> _dialogs = new();
    [SerializeField] private Sprite _icon;
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private bool _isBegineDialog;
    [SerializeField] private Animator _animator;
    [SerializeField] AnimationObjectType type;
    private float _weightForLook = 0;

    [Inject] Camera _camera;
    private void OnValidate() => _animator = GetComponent<Animator>();
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimation( )
    {
        _weightForLook = 1;
        switch (type)
        {
            case AnimationObjectType.Hello:

                _animator.SetTrigger("Hello");
                break;
            
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (_weightForLook == 0) return;
        _animator.SetLookAtWeight(_weightForLook);
        _animator.SetLookAtPosition(_camera.transform.position);
    }
    public List<string> Dialogs =>_dialogs; 

    public Sprite Icon => _icon;

    public GameObject GM => gameObject;

    public Transform CameraPoint => _cameraPoint;

    public bool IsBegineClipShip =>_isBegineDialog;
}

 