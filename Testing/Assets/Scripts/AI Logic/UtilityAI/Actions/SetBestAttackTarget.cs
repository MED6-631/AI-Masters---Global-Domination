namespace AI.Master
{
    using UnityEngine;
    using Apex.AI;
    using Apex.Utilities;

    public class SetBestAttackTarget : ActionWithOptions<ICanDie>
    {
        public override void Execute(IAIContext context)
        {
            var c = (AIContext)context;
            var unit = c.unit;

            var observations = unit.obs;
            var count = observations.Count;
            if (count == 0)
            {
                // unit has no observations of other things
                return;
            }

            // get a list to populate from the buffer pool
            var list = ListBufferPool.GetBuffer<ICanDie>(Mathf.RoundToInt(count * 0.5f)); // Preallocation could probably be better, currently it is just a rough estimate that half of the unit's observations could be units
            for (int i = 0; i < count; i++)
            {
                var obs = observations[i];
                var canDie = obs.GetComponent<ICanDie>();
                if (canDie == null)
                {
                    // observation is not a "ICanDie", so ignore it
                    continue;
                }

                var obsUnit = obs.GetComponent<UnitBase>();
                if (obsUnit != null)
                {
                    if (unit.teamID == 2)
                    {
                        // ignored allied units
                        continue;
                    }
                }

                var mainBase = obs.GetComponent<MainBaseStructure>();
                if (mainBase != unit)
                {
                    if (unit.IsAllied(mainBase))
                    {
                        // ignore allied nests
                        continue;
                    }
                }

                list.Add(canDie);
            }

            var best = this.GetBest(c, list);
            if (best != null)
            {
                // the highest-scoring candidate is valid, so set it to be the new attack target
                c.attackTarget = best;
            }

            // return the list "borrowed" from the buffer pool
            ListBufferPool.ReturnBuffer(list);
        }
    }
}