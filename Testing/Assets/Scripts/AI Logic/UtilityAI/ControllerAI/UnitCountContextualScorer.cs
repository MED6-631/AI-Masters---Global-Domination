namespace AI.Master
{
    using Apex.Serialization;
    using UnityEngine;
    using Apex.AI;
    public sealed class UnitCountContextualScorer : IContextualScorer
    {
        [ApexSerialization]
        public UnitType unitType;

        [ApexSerialization]
        public float maxScore = 100f;

        public bool isDisabled
        {
            get;
            set;
        }

        public float Score(IAIContext context)
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

            return Mathf.Max(0f, this.maxScore - count);
        }
    }
}