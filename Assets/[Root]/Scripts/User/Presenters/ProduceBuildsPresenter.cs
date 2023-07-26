using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProduceBuildsPresenter : MonoBehaviour
{
 

    //[Inject] private UserProfile _profile;

    [SerializeField] private ProduceBuilderView _view;

    private GameObject _cachedPrefab;
    private Outline _cachedRenderer;
    private bool _modelIsActive;

    [SerializeField] private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }
    [Inject]
    public void BindView(CnfsProduceBuilding buildingConfigs)
    {
        Debug.Log("Успешно!");
        List<IBuildingCnf> abstractsBuildersConfig = ConvertToAbstract(buildingConfigs);

        Debug.Log(abstractsBuildersConfig.Count);
        _view.InitView(ProduceBuilding, abstractsBuildersConfig);
    }

    private List<IBuildingCnf> ConvertToAbstract(CnfsProduceBuilding buildingConfigs)
    {
        List<IBuildingCnf> abstractsBuildersConfig = new List<IBuildingCnf>();

        foreach (var config in buildingConfigs._configs)
        {
            abstractsBuildersConfig.Add(config as IBuildingCnf);
        }

        return abstractsBuildersConfig;
    }

    private void ProduceBuilding(ProduceBuilderSlotsView slot)
    {
        var  resource = Resources.Load<GameObject>(slot.Config.ResourceID);
        _cachedPrefab = GameObject.Instantiate(resource);
        _cachedRenderer = _cachedPrefab.gameObject.GetComponent<Outline>();
        _modelIsActive = true;
        _cachedRenderer.enabled = true;


    }

    [SerializeField] private LayerMask _layerMask;

    private void Update()
    {
        if (_modelIsActive)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            

                if (Physics.Raycast(ray, out hit, 1000, _layerMask))
                {
                    _cachedRenderer.OutlineColor = Color.green;
                    Debug.Log(hit.point);
                    _cachedPrefab.transform.position = new Vector3(Mathf.FloorToInt(hit.point.x),hit.point.y,Mathf.FloorToInt(hit.point.z));

                    if (Input.GetMouseButtonDown(0))
                    {
                        _modelIsActive = false;
                        _cachedRenderer.enabled = false;
                    }
                }
                //else if (!Physics.Raycast(ray, out hit, 1000, _layerMask))
                //{
                //    _cachedRenderer.OutlineColor = Color.red;

                //    _cachedPrefab.transform.position = new Vector3(Mathf.FloorToInt(hit.point.x), hit.point.y, Mathf.FloorToInt(hit.point.z));
                //}
            
               
                


                
             
        }
    }
}
