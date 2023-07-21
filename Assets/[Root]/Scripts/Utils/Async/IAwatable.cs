using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAwatable<TAwaiter>
{
    IAwaiter<TAwaiter> GetAwaiter();
}
