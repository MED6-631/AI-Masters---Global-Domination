namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public sealed class PathUtilityCompanionUnit : CompanionAISteering
    {

        private SteerForPath _steerForPath;

        public bool ReturnToPlayer = false;

        public bool resetPath = false;
        public Transform t;
        public Transform playerReference;
        private Transform enemyOnCircle;
        public bool Aggressive;
        public bool Defensive;
        public bool Collector;
        public bool Patrol;
        public bool Move;
        private GameObject gC;
        private float time;
        //        private Collider[] nearbyEnemies;
        //        private Collider[] nearbyObjects;
        public Transform[] rootWaypoints = new Transform[4];
        public GameObject[] spawnerWaypoints = new GameObject[4];

        void Start()
        {
            _steerForPath = this.GetComponent<SteerForPath>();
            gC = GameObject.FindGameObjectWithTag("GameController");
            playerReference = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            AcquireTarget();



        }

        public override bool isMoving
        {
            get
            {
                return _steerForPath.path != null;
            }
        }

        public override void RandomWander()
        {
            var randomPos = this.transform.position + Random.onUnitSphere.normalized * _randomWanderRadius;
            randomPos.y = this.transform.position.y;
            MoveTo(randomPos);
        }



        public override void MoveTo(Vector3 destination)
        {
            var cell = Grid.instance.GetCell(destination);
            if (cell == null || cell.blocked)
            {
                cell = Grid.instance.GetNearestWalkableCell(destination);
            }

            _steerForPath.SetDestination(cell.position);
        }



        public void AcquireTarget()
        {

            if(Input.GetMouseButtonDown(1))
            {
                t = GameObject.FindGameObjectWithTag("marker").transform;
            }
            if(Vector3.Distance(this.gameObject.transform.position, t.position) <= 3)
            {
                t = null;
            }

            playerReference = GameObject.FindGameObjectWithTag("Player").transform;


        }

        public override void StopMoving()
        {
            if (_steerForPath.path != null)
            {
                _steerForPath.path.Clear();
            }

        }

    }

}
