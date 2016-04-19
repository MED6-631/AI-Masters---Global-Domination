namespace AI.Master
{
    using Apex.AI;
    public sealed class RandomWander : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            ((AIContext)context).unit.RandomWander();
        }
    }
}