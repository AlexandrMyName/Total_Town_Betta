using UnityEngine;

public interface IBuilderConfig  :  ICost
{
    public string ResourceID { get; }
    public Sprite Icon { get; }

    public string Name { get; }
 
}
