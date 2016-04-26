namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class PathCompanionUnit : CompanionAISteering
    {

        private SteerForPath _steerForPath;

        public bool ReturnToPlayer = false;

        public bool resetPath = false;
        public Transform t;
        public Transform playerReference;


        void Start()
        {
            _steerForPath = this.GetComponent<SteerForPath>();
        }

        void FixedUpdate()
        {
            AcquireTarget();
            if(isMoving == false && ReturnToPlayer == false)
            {
                AcquiredTarget(t.position);
            }

            if(isMoving == false && ReturnToPlayer == true)
            {
                AcquiredTarget(playerReference.position);
            }

            if(resetPath)
            {
                StopMoving();
                resetPath = false;
            }

            playerReference = GameObject.FindGameObjectWithTag("Player").transform;
            
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
            if(cell == null || cell.blocked)
            {
                cell = Grid.instance.GetNearestWalkableCell(destination);
            }

            _steerForPath.SetDestination(cell.position);
        }

        public void AcquiredTarget(Vector3 targetDestination)
        {
            var pos = targetDestination;

            MoveTo(pos);
        }

        public void AcquireTarget()
        {
            //if (ReturnToPlayer == true)
            //{
            //    if(isMoving == true && resetPath == false && Input.GetMouseButtonDown(1))
            //    {
            //        resetPath = true;
            //    }

            //    t = playerReference;
            //}

            if (GameObject.FindGameObjectWithTag("marker") != null)
            {

                if (GameObject.FindGameObjectWithTag("marker") && ReturnToPlayer == false)
                {
                    if (isMoving == true && resetPath == false && Input.GetMouseButtonDown(1))
                    {
                        resetPath = true;
                    }
                    t = GameObject.FindGameObjectWithTag("marker").transform;
                }
                else
                {
                    t = null;
                }
            }


        }

        // Update is called once per frame
        public override void StopMoving()
        {
         if(_steerForPath.path != null)
            {
                _steerForPath.path.Clear();
            }
               
        }
    }

}
