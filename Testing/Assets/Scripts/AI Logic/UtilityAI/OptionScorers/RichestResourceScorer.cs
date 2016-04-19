namespace AI.Master
{
    using Apex.Serialization;
    using UnityEngine;
    using Apex.AI;

    public sealed class RichestResourceScorer : OptionScorerBase<ResourceComponent>
    {
        [ApexSerialization, FriendlyName("Max Score", "The highest score that this scorer can output to an option")]
        public float maxScore = 100f;

        [ApexSerialization, FriendlyName("Factor", "A factor used to multiply the option's current resources count by")]
        public float factor = 1f;

        public override float Score(IAIContext context, ResourceComponent option)
        {
            var resources = option.currentResources * this.factor;
            return Mathf.Min(this.maxScore, resources);
        }
    }
}