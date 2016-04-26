namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class PathCompanionUnit : CompanionAISteering
    {

        private SteerForPath _steerForPath;

        public bool ReturnToPlayer = false;

        public Transform t;


        void Start()
        {
            _steerForPath = this.GetComponent<SteerForPath>();
        }

        void FixedUpdate()
        {
            AcquireTarget();
            if(isMoving == false)
            {
                AcquiredTarget(t.position);
            }

            
        }

        public override bool isMoving
        {
            get
            {
                return _steerForPath.path != null;
            }
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
            if (ReturnToPlayer == true)
            {
                t = GameObject.FindGameObjectWithTag("Player").transform;
            }

            if (GameObject.FindGameObjectWithTag("marker") != null)
            {

                if (GameObject.FindGameObjectWithTag("marker") && ReturnToPlayer == false)
                {
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
