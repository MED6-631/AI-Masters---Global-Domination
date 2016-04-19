﻿namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;

    public sealed class CanCarryMoreHarvest : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;
            var unit = c.unit as MinerUnit;
            if (unit == null)
            {
                // this scorer is only relevant for Harvester units
                return 0f;
            }

            if (unit.currentCarriedResources < unit.maxCarriableResources)
            {
                // unit can actually carry more resources
                return this.not ? 0f : this.score;
            }

            return this.not ? this.score : 0f;
        }
    }
}