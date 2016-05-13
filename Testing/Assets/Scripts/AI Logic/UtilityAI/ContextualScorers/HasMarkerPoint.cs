namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;

    public class HasMarkerPoint : ContextualScorerBase
    {
        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;
            
            if(c.pucu.t == GameObject.FindGameObjectWithTag("marker").transform)
            {
                return this.score;
            }

            return 0f;

        }
    }

}
