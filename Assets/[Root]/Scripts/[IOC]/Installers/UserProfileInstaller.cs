using UnityEngine;
using Zenject;

public class UserProfileInstaller : MonoInstaller<UserProfileInstaller>
{
    [SerializeField] private int _Workers = 5;
    [SerializeField] private int _Woods = 79;
    [SerializeField] private int _Diamonds = 10;
    [SerializeField] private int _Irons = 200;
    [SerializeField] private UserProfile userDataProfile;

    public override void InstallBindings()
    {
        userDataProfile.Init(_Woods, _Diamonds, _Workers, _Irons);
        Container.Bind<IUserProfile>().FromInstance(userDataProfile).AsCached();
        
    }
}
