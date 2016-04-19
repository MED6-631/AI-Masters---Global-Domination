namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;
    public sealed class AttackTargetTypeScorer : OptionScorerBase<ICanDie>
    {
        [ApexSerialization]
        public float score;

        [ApexSerialization]
        public bool onlyBase;

        [ApexSerialization]
        public UnitType unitType;

        public override float Score(IAIContext context, ICanDie option)
        {
            if (this.onlyBase)
            {
                if (option is MainBaseStructure)
                {
                    return this.score;
                }
            }

            var unit = option.gameObject.GetComponent<UnitBase>();
            if (unit != null)
            {
                if (unit.type == this.unitType)
                {
                    return this.score;
                }
            }

            return 0f;
        }
    }
}