namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using System;

    public class MinerUnit : UnitBase
    {
        [SerializeField]
        private int _maxCarriableResources = 25;

        [SerializeField]
        private int _currentCarriedResources = 0;

        [SerializeField]
        private float _fleeRadius = 10f;

        [SerializeField]
        private float _returnHarvestRadius = 4f;

        public override UnitType type
        {
            get { return UnitType.Miner; }
        }

        public float returnHarvestRadius
        {
            get { return _returnHarvestRadius; }
        }

        public float fleeRadius
        {
            get { return _fleeRadius; }
        }

        public int currentCarriedResources
        {
            get { return _currentCarriedResources; }
            set { _currentCarriedResources = Mathf.Min(value, _maxCarriableResources); }
        }

        public int maxCarriableResources
        {
            get { return _maxCarriableResources; }
        }

        public override void InternalAttack(float dmg)
        {
            var hits = Physics.OverlapSphere(this.transform.position, _attackRadius, Layers.mortal);
            for (int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                if(ReferenceEquals(hit.gameObject, this.gameObject))
                {
                    continue;
                }

                var unit = hit.GetComponent<UnitBase>();
                if(unit != null)
                {
                    this.LookAt(unit.transform.position);
                    unit.ReceiveDamage(dmg);
                    return;
                }

                var mainBase = hit.GetComponent<MainBaseStructure>();
                if(mainBase != null)
                {
                    this.LookAt(mainBase.transform.position);
                    mainBase.ReceiveDamage(dmg);
                    return;
                }
            }
        }


        public void Gather(ResourceComponent resource)
        {
            if(resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            var time = Time.time;
            if(time - _lastAttack < 1f / _attackPerSecond)
            {
                return;
            }

            _lastAttack = time;
            this.LookAt(resource.transform.position);
            resource.Gather(this);
        }

        public void ReturnResources()
        {
            this.LookAt(this.mainBase.transform.position);
            this.mainBase.currentResources += _currentCarriedResources;
            _currentCarriedResources = 0;
        }
    }
}

