namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using System;

    public class CompanionAISteering : UnitBase
    {

        public override UnitType type
        {
            get
            {
                return UnitType.Companion;
            }
        }

        protected override void InternalAttack(float dmg)
        {
            var hits = Physics.OverlapSphere(this.transform.position, _attackRadius, Layers.mortal);
            for (int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                if (ReferenceEquals(hit.gameObject, this.gameObject))
                {
                    continue;
                }

                var unit = hit.GetComponent<UnitBase>();
                if (unit != null && unit.teamID != 2)
                {
                    this.LookAt(unit.transform.position);
                    unit.ReceiveDamage(dmg);
                    return;
                }

                var mainBase = hit.GetComponent<MainBaseStructure>();
                if (mainBase != null)
                {
                    this.LookAt(mainBase.transform.position);
                    mainBase.ReceiveDamage(dmg);
                    return;
                }
            }
        }
    }
}
