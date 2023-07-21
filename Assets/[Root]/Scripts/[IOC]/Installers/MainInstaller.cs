using Zenject;

public class MainInstaller : MonoInstaller<MainInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<CommandsModel>().AsSingle();
        
        Container.Bind<CreatorExeCmd<IProduceUnit>>().To<CreatorExe_ProduceUnit>().AsSingle();
        Container.Bind<CreatorExeCmd<IAttack>>().To<CreatorExe_Attack>().AsSingle();
        Container.Bind<CreatorExeCmd<IPattroll>>().To<CreatorExe_Pattroll>().AsSingle();
        Container.Bind<CreatorExeCmd<IMove>>().To<CreatorExe_Move>().AsSingle();
        Container.Bind<CreatorExeCmd<IHoldPos>>().To<CreatorExe_HoldPos>().AsSingle();
        Container.Bind<CreatorExeCmd<IBuildProccess>>().To<CreatorExe_BuildProccess>().AsSingle();
        Container.Bind<CreatorExeCmd<IDelete>>().To<CreatorExe_Delete>().AsSingle();

        UserProfile profile = new UserProfile(79, 12, 5, 200);
        Container.Bind<IUserProfile>().FromInstance(profile).AsTransient();

        Container.BindInterfacesAndSelfTo<TimerModel>().AsSingle();
    }
}
