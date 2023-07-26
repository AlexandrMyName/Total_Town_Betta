using UnityEngine;

public interface IBuildingCnf  :  ICost
{
    public string ResourceID { get; }
    public Sprite Icon { get; }

    public string Name { get; }
 
}
