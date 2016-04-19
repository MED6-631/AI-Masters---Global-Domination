namespace AI.Master
{
    using Apex.AI;
    public sealed class SetAttackTargetToNull : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            ((AIContext)context).attackTarget = null;
        }
    }
}