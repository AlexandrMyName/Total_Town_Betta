using UnityEngine;

public class AbilityProduceUnit : CmdExe<IProduceUnit>
{

    [SerializeField] private Transform _containerForUnits;
    protected override void SpecificExecute(IProduceUnit command)
    {
        Instantiate(command.Object, 
            new Vector3(Random.Range(-3, 3), transform.position.y + 1, Random.Range(-3, 3)),
            Quaternion.identity,
            _containerForUnits);
    }
}
