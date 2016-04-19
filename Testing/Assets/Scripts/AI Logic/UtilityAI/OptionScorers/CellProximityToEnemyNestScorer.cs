namespace AI.Master
{
    using Apex.Serialization;
    using UnityEngine;
    using Apex.AI;

    public sealed class CellProximityToEnemyNestScorer : OptionScorerBase<Cell>
    {
        [ApexSerialization]
        public float maxScore;

        [ApexSerialization]
        public float distanceFactor = 0.1f;

        public override float Score(IAIContext context, Cell option)
        {
            var c = (AIContext)context;
            var enemyBase = c.unit.mainBase.enemyBase;
            var distance = (enemyBase.transform.position - option.position).magnitude * this.distanceFactor;
            return Mathf.Max(0f, this.maxScore - distance);
        }
    }
}