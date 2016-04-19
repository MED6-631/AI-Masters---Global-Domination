namespace AI.Master
{
    using Apex.AI;
    public sealed class SetResourceTargetToNull : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            ((AIContext)context).resourceTarget = null;
        }
    }
}