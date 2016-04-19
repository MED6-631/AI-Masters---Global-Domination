namespace AI.Master
{
    using Apex.AI;
    public sealed class MoveToOwnNest : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            c.unit.MoveTo(c.unit.mainBase.GetReturningPosition(c.unit));
        }
    }
}