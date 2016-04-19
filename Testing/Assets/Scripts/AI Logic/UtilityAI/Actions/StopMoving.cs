namespace AI.Master
{
    using Apex.AI;
    public sealed class StopMoving : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            ((AIContext)context).unit.StopMoving();
        }
    }
}