namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;
    public sealed class BuildUnit : ActionBase
    {
        [ApexSerialization]
        public UnitType unitType;

        public override void Execute(IAIContext context)
        {
            var c = (ControllerContext)context;
            c.controller.mainBase.SpawnUnit(this.unitType);
        }
    }
}