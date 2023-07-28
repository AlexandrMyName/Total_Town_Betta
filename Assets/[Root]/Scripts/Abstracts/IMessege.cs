using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessege 
{
    void SendMessageToUser(string messege, Sprite iconFrom, float maxTime = 5f);
}
