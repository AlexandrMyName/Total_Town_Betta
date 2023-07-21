using UnityEngine;
using UnityEngine.AI;

public class AbilityAttack : CmdExe<IAttack>
{

     
    protected override void SpecificExecute(IAttack command)
    {
        Component componentCommand = command.Target as Component;
        Component thisComponent = this;

        if (componentCommand.gameObject == thisComponent.gameObject) return;

        Debug.Log((command.Target as Component).gameObject.name + " Attack (gameobject)");

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = componentCommand.transform.position;
    }
}
