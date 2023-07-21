using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(AssetsInstaller), menuName = "IoC/Installers/" + nameof(AssetsInstaller),order = 1)]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext assetContext;
    [SerializeField] private Vector3Value groundClickRMB;
    [SerializeField] private SelectableValue selectableValue;
    [SerializeField] private AttackableValue attackableValue;


    public override void InstallBindings()
    {
        Container.BindInstances(assetContext,groundClickRMB,selectableValue,attackableValue);
        Container.Bind<IAwatable<IAttackable>>().FromInstance(attackableValue);
        Container.Bind<IAwatable<Vector3>>().FromInstance(groundClickRMB);
        
    }
}
