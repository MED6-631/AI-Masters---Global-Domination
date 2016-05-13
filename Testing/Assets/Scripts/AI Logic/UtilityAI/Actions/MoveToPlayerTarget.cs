namespace AI.Master
{
    using System;
    using Apex.AI;

    public class MoveToPlayerTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;

            if(c.pucu.playerReference == null)
            {
                return;
            }

            c.unit.MoveTo(c.pucu.playerReference.position);
        }
    }

}
