namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;

    public class MoveToBoosterTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            var transform = c.pucu.t;

            if(transform == null)
            {
                return;
            }

            c.pucu.MoveTo(transform.position);
        }
    }

}
