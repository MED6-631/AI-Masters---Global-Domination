namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class MainBaseStructure : MonoBehaviour, ICanDie
    {

        private const int maxUnitsReturning = 9;

        public float returnGatherRadius = 9f;

        [SerializeField]
        private float _maxHealth = 1000f;

        [SerializeField]
        private float _spawnDistance = 10f;

        [SerializeField]
        private float _buildCooldown = 0.5f;

        [SerializeField]
        private int _startMiners = 0;

        [SerializeField]
        private int _startSoldiers = 0;

        //[SerializeField]
        //private int _startDemolishers = 0;

        [SerializeField]
        private int _initialInstanceCount = 30;

        [SerializeField]
        private GameObject _minerPrefab;

        [SerializeField]
        private GameObject _soldierPrefab;

        //[SerializeField]
        //private GameObject _demolisherPrefab;

        [SerializeField]
        private int _currentResources;

        [SerializeField]
        private float _anglePerSpawn = 40f;

        private Dictionary<UnitType, UnitPool> _entityPools;
        private List<UnitBase> _units;
        private float _lastBuild;

        private readonly Vector3[] _returnPositions = new Vector3[maxUnitsReturning];

        public AIController controller
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
            get;
            private set;
        }


        public bool isDead
        {
            get { return this.currentHealth <= 0f; }
        }

        public int minerCount
        {
            get { return _entityPools[UnitType.Miner].count; }
        }


        public int soldierCount
        {
            get { return _entityPools[UnitType.Soldier].count; }
        }

        //public int demolisherCount
        //{
        //    get { return _entityPools[UnitType.Demolisher].count; }
        //}

        public int currentResources
        {
            get { return _currentResources; }
            set { _currentResources = value; }
        }

        public List<UnitBase> units
        {
            get { return _units; }
        }

        public MainBaseStructure enemyBase
        {
            get;
            private set;
        }

        private int _lastSpawnIndex;

        private void Awake()
        {
            _entityPools = new Dictionary<UnitType, UnitPool>(new UnitTypeComparer())
            {
                {UnitType.Miner, new UnitPool(_minerPrefab, this.gameObject, _initialInstanceCount) },
                {UnitType.Soldier, new UnitPool(_soldierPrefab, this.gameObject, _initialInstanceCount) }
                //{UnitType.Demolisher, new UnitPool(_demolisherPrefab, this.gameObject, _initialInstanceCount) }
            };

            _units = new List<UnitBase>(_initialInstanceCount);

            var bases = FindObjectsOfType<MainBaseStructure>();
            for (int i = 0; i < bases.Length; i++)
            {
                if(ReferenceEquals(bases[i], this))
                {
                    continue;
                }

                this.enemyBase = bases[i];
                break;
            }
        }

        private void OnEnable()
        {
            this.currentHealth = _maxHealth;
            StartCoroutine(BuildInitialUnits());

            var angle = 360f / maxUnitsReturning;
            for (int i = 0; i < maxUnitsReturning; i++)
            {
                _returnPositions[i] = CircleHelpers.GetPointOnCircle(this.transform.position, returnGatherRadius / 2f, angle, i);
            }
        }

        private void OnDisable()
        {
            var count = _units.Count;
            for (int i = 0; i < count; i++)
            {

                ReturnUnit(_units[i]);
            }

            Grid.instance.UpdateCellsBlockedStatus(this.GetComponent<Collider>());
        }

        private IEnumerator BuildInitialUnits()
        {
            //if(_startMiners > 0)
            //{
            //    yield return new WaitForSeconds(1f);
            //    StartCoroutine(BuildUnitsGradually(_startMiners, UnitType.Miner));
            //}

            if(_startSoldiers > 0)
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(BuildUnitsGradually(_startSoldiers, UnitType.Soldier));
            }

            //if(_startDemolishers > 0)
            //{
            //    yield return new WaitForSeconds(1f);
            //    StartCoroutine(BuildUnitsGradually(_startDemolishers, UnitType.Demolisher));
            //}
        }

        private IEnumerator BuildUnitsGradually (int count, UnitType type)
        {
            for (int i = 0; i < count; i++)
            {
                InternalBuildUnit(type);
                yield return new WaitForSeconds(_buildCooldown);
            }
        }


        public void SpawnUnit(UnitType type)
        {
            if(type == UnitType.None)
            {
                Debug.LogError(this.ToString() + " Cannot build units of type 'None' ");
                return;
            }

            var cost = UnitCostManager.GetCost(type);
            if(cost > _currentResources)
            {
                return;
            }

            var time = Time.time;
            if(time - _lastBuild < _buildCooldown)
            {
                return;
            }

            _lastBuild = time;
            _currentResources -= cost;
            InternalBuildUnit(type);
        }

        private void InternalBuildUnit(UnitType type)
        {
            var pos = CircleHelpers.GetPointOnCircle(this.transform.position, _spawnDistance, _anglePerSpawn, _lastSpawnIndex++);
            var unit = _entityPools[type].Get(pos, Quaternion.identity);
            unit.mainBase = this;

            var color = this.controller.color;
            var renderers = unit.GetComponentsInChildren<Renderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                if(renderers[i].GetComponent<ParticleSystem>() == null)
                {
                    renderers[i].material.color = color;
                }
            }

            unit.gameObject.name += string.Concat(" ", this.units.Count);

            _units.Add(unit);
        }




        public void ReturnUnit(UnitBase unit)
        {
            if(unit.type == UnitType.None)
            {
                Debug.LogError(this.ToString() + " Cannot return units of type 'None'");
                return;
            }

            _units.Remove(unit);
            _entityPools[unit.type].Return(unit);
        }

        public void ReceiveDamage(float dmg)
        {
            this.currentHealth -= dmg;
            if(this.currentHealth <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }

        public Vector3 GetReturningPosition(UnitBase unit)
        {
            var unitPos = unit.transform.position;
            var nearest = _returnPositions[0];
            for (int i = 1; i < _returnPositions.Length; i++)
            {
                var pos = _returnPositions[i];
                if((unitPos - pos).sqrMagnitude < (unitPos - nearest).sqrMagnitude)
                {
                    nearest = pos;
                }
            }

            return nearest;
        }

    }

}
