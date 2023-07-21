using System;
 

public interface ITimer 
{
    IObservable<int> GameTime { get; }
    

}
