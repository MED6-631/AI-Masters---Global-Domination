namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;

    public sealed class HasAttackTargetWithinRange : ContextualScorerBase
    {
        [ApexSerialization, MemberDependency("useScanRadius", false), MemberDependency("useAttackRadius", false), FriendlyName("Range", "A custom range to use for the evaluation")]
        public float range = 10f;

        [ApexSerialization, MemberDependency("useAttackRadius", false), FriendlyName("Use Scan Radius", "Set to true to use the unit's scanRadius as the range")]
        public bool useScanRadius;

        [ApexSerialization, MemberDependency("useScanRadius", false), FriendlyName("Use Attack Radius", "Set to true to use the unit's attackRadius as the range")]
        public bool useAttackRadius;

        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;
            var attackTarget = c.attackTarget;
            if (attackTarget == null)
            {
                // there is not attack target
                return 0f;
            }

            // get the right range
            var range = this.range;
            if (this.useScanRadius)
            {
                range = c.unit.viewRadius;
            }
            else if (this.useAttackRadius)
            {
                range = c.unit.attackRadius;
            }

            var distanceSqr = (c.position - attackTarget.transform.position).sqrMagnitude;
            if (distanceSqr < (range * range))
            {
                // attack target is within range
                return this.not ? 0f : this.score;
            }

            return this.not ? this.score : 0f;
        }
    }
}