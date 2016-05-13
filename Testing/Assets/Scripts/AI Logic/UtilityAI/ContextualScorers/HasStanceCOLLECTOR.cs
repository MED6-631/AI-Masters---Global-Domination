﻿namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;

    public sealed class HasStanceCOLLECTOR : ContextualScorerBase
    {
        public override float Score(IAIContext context)
        {

            var c = (AIContext)context;

            if (c.pucu.Collector)
            {
                return this.score;
            }
            else
            {
                return 0;
            }

        }
    }

}

