using UnityEngine;

public class AbilityPattroll : CmdExe<IPattroll>
{

     
    protected override void SpecificExecute(IPattroll command)
    {
        Debug.Log(  "| Patroll |");
        //
    }
}
