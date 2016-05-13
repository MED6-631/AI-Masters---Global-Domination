namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;

    public class MoveToBoosterSpawnTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            var t = c.pucu.rootWaypoints;
            

            if(t == null)
            {
                return;
            }

            c.unit.MoveTo(t[UnityEngine.Random.Range(0,4)].position);
        }
    }

}
