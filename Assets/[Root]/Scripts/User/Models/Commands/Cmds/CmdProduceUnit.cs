using UnityEngine;

public class CmdProduceUnit : IProduceUnit
{
    [InjectAsset("Chomper")]  private GameObject _obj;
    public GameObject Object => _obj;
}
