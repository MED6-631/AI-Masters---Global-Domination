namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;

    public class MoveToMarkerPoint : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;

            c.pucu.MoveTo(c.pucu.t.position);
        }
    }

}
