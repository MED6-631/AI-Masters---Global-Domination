namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;
    using Apex.Serialization;

    public class HasBoosterWithinRange : ContextualScorerBase
    {
        [ApexSerialization, MemberDependency("useScanRadius", false), MemberDependency("useAttackRadius", false), FriendlyName("Range", "A custom range to use for the evaluation")]
        public float range = 10f;

        [ApexSerialization, MemberDependency("useAttackRadius", false), FriendlyName("Use Scan Radius", "Set to true to use the unit's scanRadius as the range")]
        public bool useScanRadius;

        [ApexSerialization, MemberDependency("useScanRadius", false), FriendlyName("Use Attack Radius", "Set to true to use the unit's attackRadius as the range")]
        public bool useAttackRadius;

        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        public override float Score(IAIContext context)
        {

            var c = (AIContext)context;

            var range = this.range;
            if(this.useScanRadius)
            {
                range = c.unit.viewRadius;
            }
            else if(this.useAttackRadius)
            {
                range = c.unit.attackRadius;
            }

            var hits = Physics.OverlapSphere(c.position, range, Layers.resources);

            for(int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];

                if(ReferenceEquals(hit.gameObject, c.gameObject))
                {
                    return 0f;
                }

                //var health = hit.gameObject.tag.Contains("healthpack");
                //var dmgup = hit.gameObject.tag.Contains("dmgup");
                //var moveup = hit.gameObject.tag.Contains("moveup");
                var booster = hit.gameObject.tag.Contains("booster");

                if(booster != null)
                {
                    Debug.Log("Found Booster");
                    c.pucu.t = hit.gameObject.transform;
                    return this.not ? 0f : this.score;
                }
            }

            Debug.Log("No Booster Found");
            return this.not ? this.score : 0f;
               
        }
    }

}
