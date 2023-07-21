 
public class CreatorExe_Attack : CansellExeCmdCreator<IAttack, IAttackable>
{
    protected override IAttack createCmd(IAttackable argument) => new CmdAttack(argument);
    
}
