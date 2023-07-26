using System;
using UnityEngine;

public interface IScreenInit  : IDisposable
{
    void Initialize(IScreenInit hidenObj);

    
}
