namespace AI.Master
{
    using Apex.AI;
    public sealed class ScanForGridCells : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            Grid.instance.GetUnblockedCells(c.position, c.unit.viewRadius, c.sampledCells);
        }
    }
}