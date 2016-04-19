﻿namespace AI.Master
{
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;


    public sealed class NearestResourceScorer : OptionScorerBase<ResourceComponent>
    {
        [ApexSerialization, FriendlyName("Max Score", "The highest score that this scorer can output to an option")]
        public float maxScore = 100f;

        [ApexSerialization, FriendlyName("Distance Factor", "A factor used to multiply the calculated distance by")]
        public float distanceFactor = 0.1f;

        public override float Score(IAIContext context, ResourceComponent option)
        {
            var c = (AIContext)context;
            var distance = (c.position - option.transform.position).magnitude * this.distanceFactor;
            return Mathf.Max(0f, this.maxScore - distance);
        }
    }
}