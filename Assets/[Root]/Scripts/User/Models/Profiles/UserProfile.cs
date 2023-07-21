using System.Collections.Generic;
using System.Linq;

public class UserProfile : IUserProfile
{
    public List<ICurrencyProfile> Currencies { get; }


    public UserProfile(int woods, int diamonds, int workers, int irons)
    {
        Currencies = new List<ICurrencyProfile>();
        Currencies.Add(new CurrencyProfile(CurrencyType.Wood, woods));
        Currencies.Add(new CurrencyProfile(CurrencyType.Diamond, diamonds));
        Currencies.Add(new CurrencyProfile(CurrencyType.Worker, workers));
        Currencies.Add(new CurrencyProfile(CurrencyType.Iron, irons));
    }

    public ICurrencyProfile GetCurency(CurrencyType type) => Currencies.Where(x=>x.Type == type).First();
    
}
public class CurrencyProfile : ICurrencyProfile
{
    public CurrencyProfile(CurrencyType type, int count)
    {
        Type = type;
        Count = count;
    }

    public CurrencyType Type { get; set; }
    public int Count { get; set; }
}