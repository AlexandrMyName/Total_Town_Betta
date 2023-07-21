using UnityEngine;

public class AbilityHoldPos : CmdExe<IHoldPos>
{

     
    protected override void SpecificExecute(IHoldPos command)
    {
        Debug.Log(  "| Hold |");
    }
}
