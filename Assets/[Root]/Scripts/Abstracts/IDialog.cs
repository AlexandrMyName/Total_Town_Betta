using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDialog
{
   
     void SendDialog(List<string> dialogs, int secondsBetween, Action onComplete, Sprite icon = null);
}
