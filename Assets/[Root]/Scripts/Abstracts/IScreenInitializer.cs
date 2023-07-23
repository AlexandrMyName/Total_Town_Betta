using System;
using UnityEngine;

public interface IScreenInitializer  : IDisposable
{
    void Initialize(IScreenInitializer hidenObj);

    
}
