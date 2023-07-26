 
public interface IUnitProduceTask  
{
    void Cancel();
    void Add(IUnitProducer producer);
}

public interface IUnitProducer
{
    string Name { get; }    
    float TimeInSeconds { get; set; }
    float MaxTimeInSeconds { get;  }
    string NameResource { get; }

    ProducerType ProducerType { get; }
}
public enum ProducerType
{
    Worker,
    TownerFinder,

}
