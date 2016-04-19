namespace AI.Master
{
    using Apex.AI;
    public sealed class MoveToBestCell : ActionWithOptions<Cell>
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            var best = this.GetBest(c, c.sampledCells);
            if (best == null)
            {
                // Best (Highest-scoring) cell is null, so no move is possible
                return;
            }

            c.unit.MoveTo(best.position);
        }
    }
}