using UnityEngine;
using UnityEngine.AI;
public class AbilityMove : CmdExe<IMove>
{
    [SerializeField] private UnitMovementStop _stop;

    [SerializeField] private Animator _animator;

    private void OnValidate()
    {
         _animator ??= GetComponent<Animator>();
         _stop ??= GetComponent<UnitMovementStop>();
    }
    protected override async void SpecificExecute(IMove command)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = command.Target;
        _animator.SetTrigger("Walk");
        await _stop;
        _animator.SetTrigger("Stop");
    }
}
