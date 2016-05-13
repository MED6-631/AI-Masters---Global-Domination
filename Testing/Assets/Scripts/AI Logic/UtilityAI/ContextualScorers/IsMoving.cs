namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;

    public sealed class IsMoving : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;
            if (c.cs.isMoving)
            {
                // unit is moving
                return this.not ? 0f : this.score;
            }

            return this.not ? this.score : 0f;
        }
    }
}