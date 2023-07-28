using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class QueueAssemblingExe<T> : CmdExe<T>, IProccess,ICost where T : ICommand
{

    [SerializeField] protected List<GameObject> _interectiveObjects;
    [SerializeField] protected GameObject _gameobjectOnCompleted;
    [SerializeField] protected Slider _proccessSlider;
    [SerializeField] protected float _maxTimeToDo;

    protected bool _inProccessing;
    protected IProfileBinder _profileBinding;
    protected ISelectable _currentSelectable;
    protected Collider _collider;
    private AsyncAwaiterTime _waiter;

    [Inject] protected IUserProfile _profile;
    private IMessege _messege;

    private float _currentTime;


    public bool IsProccess { get => _inProccessing; set => _inProccessing = value; }
    public int Woods { get => _profileBinding.Woods; set => _profileBinding.Woods = value; }
    public int Diamonds { get => _profileBinding.Diamonds; set => _profileBinding.Diamonds = value; }
    public int Workers { get => _profileBinding.Workers; set => _profileBinding.Workers = value; }
    public int Irons { get => _profileBinding.Irons; set => Irons = value; }
    protected AsyncAwaiterTime Waiter { get => _waiter; set => _waiter = value; }
    protected float CurrentTime { get => _currentTime; set => _currentTime = value; }

    protected void Init(IProfileBinder profBinder, ISelectable currentSelectable,IMessege messegeObj,  Collider col)
    {
        _messege = messegeObj;
        _profileBinding = profBinder;
        _currentSelectable = currentSelectable;
        _collider = col;
        _proccessSlider.maxValue = _maxTimeToDo;
    }
    protected override void SpecificExecute(T command) => OnCommandExecute(command);
    protected void UpdateProccess(float value)
    {
        

        _proccessSlider.value = value;
    }

    protected void BegineCommandExe()
    {
        _collider.enabled = false;
        _proccessSlider.gameObject.SetActive(true);
    }
    protected abstract void OnCommandExecute(T command);
    protected abstract void CanselCommandProccess(T command);
    protected void BindMessege(string messege) => _messege.SendMessageToUser(messege, _currentSelectable.Icon);

    private void Update() => RefreshTime();
    protected abstract void RefreshTime();
}
