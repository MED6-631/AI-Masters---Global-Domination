namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using Apex.Serialization;
    using System;

    public class HasPlayerUnitWithinRange : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Max Distance", "The maximum distance between player and companion to score.")]
        public float maxDistance = 10f;

        public override float Score(IAIContext context)
        {

            var c = (AIContext)context;
            Transform player = c.pucu.playerReference;

            var distance = Vector3.Distance(c.cs.transform.position, player.position);

            if(distance >= maxDistance)
            {
                return this.score;
            }

            return 0f;


        }
    }

}
