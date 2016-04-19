namespace AI.Master
{
    using Apex.AI;

    public sealed class Attack : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            ((AIContext)context).unit.Attack();
        }
    }
}