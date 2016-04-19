namespace AI.Master
{
    using Apex.Serialization;
    using UnityEngine;
    using Apex.AI;
    public sealed class HasUnitCount : ContextualScorerBase
    {
        [ApexSerialization]
        public UnitType unitType;

        [ApexSerialization]
        public int desiredCount = 10;

        [ApexSerialization]
        public bool not;

        public override float Score(IAIContext context)
        {
            var c = (ControllerContext)context;
            var mainBase = c.controller.mainBase;
            var count = 0;
            switch (this.unitType)
            {
                case UnitType.Miner:
                {
                    count = mainBase.minerCount;
                    break;
                }

                case UnitType.Soldier:
                {
                    count = mainBase.soldierCount;
                    break;
                }



                default:
                {
                    Debug.LogWarning(this.ToString() + " Unsupported unit type => " + this.unitType);
                    break;
                }
            }

            if (count >= this.desiredCount)
            {
                return this.not ? 0f : this.score;
            }

            return this.not ? this.score : 0f;
        }
    }
}