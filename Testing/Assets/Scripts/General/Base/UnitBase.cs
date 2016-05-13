#pragma warning disable 0414

namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]

    public abstract class UnitBase : MonoBehaviour, ICanDie
    {

        private const float destinationBufferRadius = 3f;
        private const float destinationCenter = 0.5f;

        [SerializeField]
        private ParticleSystem _deathEffect;

        [SerializeField]
        protected float _maxHealth = 100f;


        [SerializeField]
        protected float _attackRadius = 2f;

        [SerializeField]
        protected float _attackPerSecond = 1f;

        [SerializeField]
        private float _minDamage = 10f;

        [SerializeField]
        private float _maxDamage = 20f;

        [SerializeField]
        private float _viewRadius = 15f;

        [SerializeField]
        protected float _randomWanderRadius = 10f;

        [SerializeField]
        private float _currentHealth;

        [SerializeField]
        private float _unitRadius = 0.6f;

        [SerializeField]
        protected int _teamID;

        protected float _lastAttack;
        protected List<GameObject> _obs;
        private NavMeshAgent _navMeshAgent;


        public abstract UnitType type { get; }

        public int id
        {
            get;
            set;
        }

        public float maxHealth
        {
            get { return _maxHealth; }
        }

        public float currentHealth
        {
            get { return _currentHealth; }
        }

        public float heal
        {
            get;
            set;
        }

        public bool isDead
        {
            get { return _currentHealth <= 0f || !this.gameObject.activeSelf; }
        }

        public float attackRadius
        {
            get { return _attackRadius; }
        }

        public float viewRadius
        {
            get { return _viewRadius; }
        }

        public MainBaseStructure mainBase
        {
            get;
            set;
        }

        public List<GameObject> obs
        {
            get { return _obs; }
        }

        public int teamID
        {
            get { return _teamID; }
        }

        public NavMeshAgent navMeshAgent
        {
            get { return _navMeshAgent; }
        }

        public virtual bool isMoving
        {
            get { return _navMeshAgent != null ? _navMeshAgent.desiredVelocity.sqrMagnitude > 1f : false; }
        }

        public float unitRadius
        {
            get { return _unitRadius; }
        }

        public Vector3 velocity
        {
            get;
            set;
        }

        private void Awake()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            if(_navMeshAgent != null)
            {
                _navMeshAgent.avoidancePriority += Random.Range(-23, 24);
            }
        }

        private void OnEnable()
        {
            _obs = new List<GameObject>(5);
            _currentHealth = _maxHealth;
        }

        private void OnDisable()
        {
            //if(this.mainBase != null)
            //{
            //    this.mainBase.ReturnUnit(this);
            //}

            Destroy(this.gameObject);
          
        }


        public float GetDamage()
        {
            return Random.Range(_minDamage, _maxDamage);
        }

        public void AddOrUpdateObservations(Collider[] colliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                AddOrUpdateObservationInternal(colliders[i].gameObject);
            }
        }

        private void AddOrUpdateObservationInternal(GameObject gameObject)
        {
            if(ReferenceEquals(gameObject, this.gameObject))
            {
                return;
            }

            var idx = _obs.IndexOf(gameObject);
            if(idx >= 0)
            {
                _obs[idx] = gameObject;
                return;
            }

            _obs.Add(gameObject);
        }

        protected void LookAt (Vector3 pos)
        {
            this.transform.LookAt(new Vector3(pos.x, this.transform.position.y, pos.z));
        }

        public bool IsAllied(UnitBase otherUnit)
        {
            return ReferenceEquals(this.teamID, otherUnit.teamID);
        }

        public bool IsAllied(MainBaseStructure mainBase)
        {
            return ReferenceEquals(this.teamID, teamID);
        }

        public virtual void RandomWander()
        {
            var randomPos = this.transform.position + Random.onUnitSphere.normalized * _randomWanderRadius;
            randomPos.y = this.transform.position.y;

            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomPos, out hit, _randomWanderRadius * 0.5f, _navMeshAgent.areaMask))
            {
                MoveTo(hit.position);
            }
        }

        public virtual void MoveTo(Vector3 destination)
        {
            NavMeshHit hit;
            if(NavMesh.SamplePosition(destination, out hit, destinationBufferRadius, _navMeshAgent.areaMask))
            {
                _navMeshAgent.Resume();
                _navMeshAgent.SetDestination(hit.position);
            }
        }

        public virtual void MoveToSpot(Vector3 destination)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(destination, out hit, destinationCenter, _navMeshAgent.areaMask))
            {
                _navMeshAgent.Resume();
                _navMeshAgent.SetDestination(hit.position);
            }
        }

        public virtual void StopMoving()
        {
            _navMeshAgent.Stop();
        }

        public void ReceiveDamage(float dmg)
        {
            _currentHealth -= dmg;
            if(_currentHealth <= 0f)
            {
                PlayEffect(_deathEffect);
                this.gameObject.SetActive(false);
            }

        }

        public void HealDamage(float hp)
        {
            _currentHealth += hp;

            if(_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }
        }

        protected void PlayEffect(ParticleSystem effect)
        {
            effect.transform.SetParent(null);
            effect.Play();
            CoroutineHelper.instance.StartCoroutine(CleanUpEffect(effect));
        }

        private IEnumerator CleanUpEffect(ParticleSystem effect)
        {
            yield return new WaitForSeconds(effect.duration);
            effect.gameObject.SetActive(false);
        }

        public void Attack()
        {
            var time = Time.time;
            if(time - _lastAttack < 1f / _attackPerSecond)
            {
                return;
            }

            _lastAttack = time;
            StopMoving();
            InternalAttack(GetDamage());
        }

        //public void COInternalAttack(float dmg)
        //{
        //    var hits = Physics.OverlapSphere(this.transform.position, _attackRadius, Layers.mortal);
        //    for (int i = 0; i < hits.Length; i++)
        //    {
        //        var hit = hits[i];
        //        if (ReferenceEquals(hit.gameObject, this.gameObject))
        //        {
        //            continue;
        //        }

        //        var unit = hit.GetComponent<UnitBase>();
        //        if (unit != null && unit.teamID != 2)
        //        {
        //            this.LookAt(unit.transform.position);
        //            unit.ReceiveDamage(dmg);
        //            return;
        //        }

        //        var mainBase = hit.GetComponent<MainBaseStructure>();
        //        if (mainBase != null)
        //        {
        //            this.LookAt(mainBase.transform.position);
        //            mainBase.ReceiveDamage(dmg);
        //            return;
        //        }
        //    }
        //}

        public abstract void InternalAttack(float dmg);

    }
}

