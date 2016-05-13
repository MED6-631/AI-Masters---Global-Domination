namespace AI.Master
{
    using Apex.AI;
    public class MoveToAttackTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            var attackTarget = c.attackTarget;
            if (attackTarget == null)
            {
                // Attack target has not been set, thus unit cannot move to attack target
                return;
            }

            c.unit.MoveTo(attackTarget.transform.position);
        }
    }
}