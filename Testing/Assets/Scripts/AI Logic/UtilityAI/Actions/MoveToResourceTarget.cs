namespace AI.Master
{
    using Apex.AI;
    public sealed class MoveToResourceTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            if (c.resourceTarget == null)
            {
                // resource target has not been set, no move possible
                return;
            }

            c.unit.MoveTo(c.resourceTarget.GetGatheringPositions(c.unit));
        }
    }
}