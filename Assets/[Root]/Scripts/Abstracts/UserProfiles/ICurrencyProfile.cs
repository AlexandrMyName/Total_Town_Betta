 

public enum CurrencyType { Wood, Iron, Diamond, Worker}
public interface ICurrencyProfile 
{
   public CurrencyType Type { get; set; }
    public int Count { get; set; }
}
