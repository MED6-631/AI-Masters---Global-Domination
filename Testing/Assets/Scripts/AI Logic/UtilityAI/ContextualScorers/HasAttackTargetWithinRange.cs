namespace AI.Master
{
    using Apex.Serialization;
    using Apex.AI;
    using UnityEngine;

    public class HasAttackTargetWithinRange : ContextualScorerBase
    {
        [ApexSerialization, MemberDependency("useScanRadius", false), MemberDependency("useAttackRadius", false), FriendlyName("Range", "A custom range to use for the evaluation")]
        public float range = 10f;

        [ApexSerialization, MemberDependency("useAttackRadius", false), FriendlyName("Use Scan Radius", "Set to true to use the unit's scanRadius as the range")]
        public bool useScanRadius;

        [ApexSerialization, MemberDependency("useScanRadius", false), FriendlyName("Use Attack Radius", "Set to true to use the unit's attackRadius as the range")]
        public bool useAttackRadius;

        [ApexSerialization, FriendlyName("Not", "Set to true to inverse the logic of this scorer, e.g. instead of scoring when true, it scores when false.")]
        public bool not;

        [ApexSerialization, FriendlyName("Amount of Units within Radius", "this will count the units found within range")]
        public int UnitCount;

        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;
            var attackTarget = c.attackTarget;
            var range = this.range;
            if (this.useScanRadius)
            {
                range = c.unit.viewRadius;
            }
            else if (this.useAttackRadius)
            {
                range = c.unit.attackRadius;
            }

            var hits = Physics.OverlapSphere(c.position, range, Layers.mortal);

            for (int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                if(ReferenceEquals(hit.gameObject, c.gameObject))
                {
                    return 0f;
                }

                var unit = hit.GetComponent<UnitBase>();

                if(unit != null && unit.teamID != 2)
                {
                    UnitCount++;
                    Debug.Log("Got Enemy in Range");
                    return this.not ? 0f : this.score;
                    
                }
                else if(unit == null && unit.teamID != 2)
                {
                    UnitCount--;
                }

            }
            Debug.Log("Does not Have enemy in Range");
            return this.not ? this.score : 0f;

            //if (attackTarget == null)
            //{
            //    // there is not attack target
            //    return 0f;
            //}

            // get the right range


            //var distanceSqr = (c.unit.transform.position - attackTarget.transform.position).sqrMagnitude;
            //var distancecircle = Vector3.Distance(this..position, attackTarget.transform.position);
            //if (distanceSqr < (range * range))
            //{
            //    // attack target is within range
            //    return this.not ? 0f : this.score;

            //    Debug.Log("Got Enemy in range");
            //}

            //if (distancecircle < (range * range))
            //{
            //    // attack target is within range
            //    return this.not ? 0f : this.score;

            //    Debug.Log("Got Enemy in range");
            //}

            //return this.not ? this.score : 0f;
            //Debug.Log("Does not have enemy in range");
        }
    }
}