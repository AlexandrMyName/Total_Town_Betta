using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AbilityMoveShip : CmdExe<IMove>
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private TMP_Text messageToUser;
    
    private void OnValidate()
    {
        _agent ??= GetComponent<NavMeshAgent>();
    }

    protected override void SpecificExecute(IMove command)
    {
        _agent.destination = command.Target;
    }
}
