namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;

    public sealed class AttackTargetAliveScorer : OptionScorerBase<ICanDie>
    {
        [ApexSerialization, FriendlyName("Score", "The score to give to ICanDies that are alive")]
        public float score = 10f;

        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        public override float Score(IAIContext context, ICanDie option)
        {
            if (option != null)
            {
                // The option being evaluated is not dead
                return this.not ? 0f : this.score;
            }

            return this.not ? this.score : 0f;
        }
    }
}