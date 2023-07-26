using System.Collections.Generic;

public interface IUserProfile 
{
     List<ICurrencyProfile> Currencies { get; }
     ICurrencyProfile GetCurency(CurrencyType type);
}
