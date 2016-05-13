namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;
    using Apex.Serialization;

    public class IsReturnToPlayer : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;

            if(c.pucu.ReturnToPlayer)
            {
                return this.not ? 0f : this.score;
            }
            else
            {
                return this.not ? this.score : 0f;
            }

        }
    }

}
