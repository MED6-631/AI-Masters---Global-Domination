namespace AI.Master
{
    using UnityEngine;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Text;
    using Apex.Serialization;
    using Apex.AI;
    public sealed class CanAfford : ContextualScorerBase
    {

        [ApexSerialization]
        public UnitType unitType;


        public override float Score(IAIContext context)
        {
            var c = (ControllerContext)context;
            var resources = c.controller.mainBase.currentResources;
            var cost = UnitCostManager.GetCost(this.unitType);

            if(resources >= cost)
            {
                return this.score;
            }

            return 0f;
        }

    }
}


