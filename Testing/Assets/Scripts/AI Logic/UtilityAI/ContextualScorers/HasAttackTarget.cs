﻿namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;
    using UnityEngine;

    public class HasAttackTarget : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;
            if (c.attackTarget != null)
            {
                // unit has an attack target
                Debug.Log("Got enemy!");
                return this.not ? 0f : this.score;

                
            }
            Debug.Log("Got No Enemy!");
            return this.not ? this.score : 0f;
        }
    }
}