using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller<UIInstaller>
{
    [SerializeField] private GameObject _hidenEndObject;
    [SerializeField] private Hint_P _helpHintView;
    [SerializeField] private ProduceUnitsPresenter _produceUnitsPresenter;
    [SerializeField] private MessegeView _messegeView;
    [SerializeField] private DialogView _dialogView;
    [SerializeField] private CurrencyView _currencyView;
    [SerializeField] private CommandsView _commandsView;
    [SerializeField] private CommandP _commandPresenter;
    [SerializeField] private LeftUI_P _leftUI;
    [SerializeField] private InterectiveText _interectiveTextEnd;
    [SerializeField] private ClipEffector _clipEffector;

    [SerializeField] private AnimCurrencyView _animCurrencyView;
    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("HidenEndObject").FromInstance(_hidenEndObject).AsCached();
        Container.Bind<Hint_P>().FromInstance(_helpHintView).AsCached();

        Container.Bind<ProduceUnitsPresenter>().FromInstance(_produceUnitsPresenter).AsCached();
        Container.Bind<MessegeView>().FromInstance(_messegeView).AsCached();
        Container.Bind<DialogView>().FromInstance(_dialogView).AsCached();
        Container.Bind<CurrencyView>().FromInstance(_currencyView).AsCached();
        Container.Bind<CommandsView>().FromInstance(_commandsView).AsCached();
        Container.Bind<CommandP>().FromInstance(_commandPresenter).AsCached();
        Container.Bind<LeftUI_P>().FromInstance(_leftUI).AsCached();
        Container.Bind<InterectiveText>().FromInstance(_interectiveTextEnd).AsCached();
        Container.Bind<ClipEffector>().FromInstance(_clipEffector).AsCached();
        Container.Bind<AnimCurrencyView>().FromInstance(_animCurrencyView).AsCached();
    }
}
