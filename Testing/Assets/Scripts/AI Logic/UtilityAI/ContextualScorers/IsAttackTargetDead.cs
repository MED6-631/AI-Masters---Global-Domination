namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;

    public sealed class IsAttackTargetDead : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;
            //if (c.attackTarget == null)
            //{
            //    // unit has no attack target
            //    return 0f;
            //}

            if (c.attackTarget == null)
            {
                // the attack target is dead
                return this.not ? 0f : this.score;
            }

            return this.not ? this.score : 0f;
        }
    }
}