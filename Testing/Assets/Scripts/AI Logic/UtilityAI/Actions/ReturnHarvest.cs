namespace AI.Master
{
    using Apex.AI;
    public sealed class ReturnHarvest : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            var unit = c.unit as MinerUnit;
            if (unit == null)
            {
                // Only harvester units may return harvest
                return;
            }

            var mainBase = unit.mainBase;
            var distanceSqr = (mainBase.transform.position - c.position).sqrMagnitude;
            if (distanceSqr > (mainBase.returnGatherRadius * mainBase.returnGatherRadius))
            {
                // the nest is too far away and so this unit cannot return harvest
                return;
            }

            unit.ReturnResources();
        }
    }
}