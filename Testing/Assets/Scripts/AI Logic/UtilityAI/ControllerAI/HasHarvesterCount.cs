namespace AI.Master
{
    using UnityEngine;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Text;
    using Apex.Serialization;
    using Apex.AI;
    public sealed class HasHarvesterCount : ContextualScorerBase
    {

        [ApexSerialization]
        public int desiredMinerCount;

        [ApexSerialization]
        public bool not;

        public override float Score(IAIContext context)
        {
            var c = (ControllerContext)context;
            var controller = c.controller;
            var mainBase = controller.mainBase;

            var minerCount = mainBase.minerCount;

            if(minerCount >= this.desiredMinerCount)
            {
                return this.not ? 0f : this.score;
            }
            return this.not ? this.score : 0f;
        }


    }
}

