
public class CmdAttack : IAttack
{
    public CmdAttack(IAttackable attackable) => target = attackable;
   
    private IAttackable target;
    IAttackable IAttack.Target => target;
}
