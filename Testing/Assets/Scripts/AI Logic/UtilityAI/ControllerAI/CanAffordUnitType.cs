namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;
    public sealed class CanAffordUnitType : ContextualScorerBase
    {
        [ApexSerialization]
        public UnitType unitType;

        public override float Score(IAIContext context)
        {
            var c = (ControllerContext)context;
            var resources = c.controller.mainBase.currentResources;
            var cost = UnitCostManager.GetCost(this.unitType);
            if (resources >= cost)
            {
                return this.score;
            }

            return 0f;
        }
    }
}