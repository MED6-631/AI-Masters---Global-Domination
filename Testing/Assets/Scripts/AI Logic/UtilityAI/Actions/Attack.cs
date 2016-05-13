namespace AI.Master
{
    using Apex.AI;
    using UnityEngine;

    public class Attack : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            ((AIContext)context).unit.Attack();
            //var c = (AIContext)context;
            
          //      c.cs.ReceiveDamage(c.cs.GetDamage());
            Debug.Log("Attacked!");
            
        }


    }
}