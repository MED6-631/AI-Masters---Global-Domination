namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;

    public class MoveToEnemySpawnerTarget : ActionBase
    {

        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            var t = c.pucu.spawnerWaypoints;

            if(t == null)
            {
                return;
            }

            c.pucu.MoveTo(t[UnityEngine.Random.Range(0,4)].transform.position);

        }


    }

}
