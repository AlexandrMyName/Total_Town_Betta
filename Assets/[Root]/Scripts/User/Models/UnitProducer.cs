using UnityEngine;

 
public class UnitProducer :  IUnitProducer
{
    private string _name;
    private float _timeInSeconds;
    private float _maxTimeInSeconds;
    private string _nameResource;
    private ProducerType _producerType;

    public string Name => _name;

    public float TimeInSeconds { get => _timeInSeconds; set => _timeInSeconds = value; }
    public float MaxTimeInSeconds { get => _maxTimeInSeconds; }

    public string NameResource => _nameResource;

    public ProducerType ProducerType => _producerType;



    public UnitProducer(string name, float maxTimeInSeconds, string nameResource, ProducerType producerType)
    {
        _name = name;
        _maxTimeInSeconds = maxTimeInSeconds;
        _timeInSeconds = _maxTimeInSeconds;
        _nameResource = nameResource;
        _producerType = producerType;
         
    }
}
