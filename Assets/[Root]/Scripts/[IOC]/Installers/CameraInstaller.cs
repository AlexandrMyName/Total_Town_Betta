using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CameraInstaller : MonoInstaller<CameraInstaller>
{

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private EventSystem _eventSystem;
    public override void InstallBindings()
    {
         Container.Bind<Camera>().FromInstance(_mainCamera);
         Container.Bind<EventSystem>().FromInstance(_eventSystem);
    }
}
