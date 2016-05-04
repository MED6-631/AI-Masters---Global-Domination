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
        private Transform enemyOnCircle;
        public bool Aggressive;
        public bool Defensive;
        public bool Collector;
        public bool Patrol;
        public bool Move;
        public bool playerOutOfRange;
        private GameObject gC;
        private float time;
        private Collider[] nearbyEnemies;
        private Collider[] nearbyObjects;
        public Transform[] rootWaypoints = new Transform[4];
        public GameObject[] spawnerWaypoints = new GameObject[4];

        void Start()
        {
            _steerForPath = this.GetComponent<SteerForPath>();
            gC = GameObject.FindGameObjectWithTag("GameController");
        }

        void FixedUpdate()
        {
            AcquireTarget();
            if(Input.GetMouseButtonDown(1))
            {
                AcquiredTarget(t.position);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;

                ReturnToPlayer = false;
                Defensive = false;
                Aggressive = false;
                Collector = false;
                Patrol = false;
            }

            //if(isMoving == false && ReturnToPlayer == true && Aggressive == false)
            //{
            //    AcquiredTarget(playerReference.position);
            //}

            if(isMoving == false && ReturnToPlayer == true && Aggressive == true)
            {
                
                StartCoroutine(AAT(1));
                
                
            }
            
            if(playerOutOfRange && Defensive == true)
            {
                gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Sad;
                //time += Time.deltaTime;
                //if(time >= 3)
                //{
                //    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.None;
                //    playerOutOfRange = false;
                //    time = 0;
                //}
                
            }
            else if(!playerOutOfRange && Defensive == true)
            {
                gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
            }

            if(resetPath)
            {
                StopMoving();
                resetPath = false;
            }

            if(ReturnToPlayer == false && Aggressive == true)
            {
               
                StartCoroutine(AAT(4));
                
                
            }

            if(isMoving == false && ReturnToPlayer == true && Aggressive == false && Defensive == true && Collector == false && Patrol == false)
            {
                StartCoroutine(DM(1));
            }

            if (isMoving == false && ReturnToPlayer == false && Aggressive == false && Defensive == false && Collector == true && Patrol == false)
            {
                StartCoroutine(SB(1));
            }
            if (isMoving == false && ReturnToPlayer == false && Aggressive == false && Defensive == false && Collector == false && Patrol == true)
            {
                StartCoroutine(PM(1));
            }

            playerReference = GameObject.FindGameObjectWithTag("Player").transform;
            nearbyEnemies = Physics.OverlapSphere(transform.position, this.viewRadius);
            nearbyObjects = Physics.OverlapSphere(transform.position, this.viewRadius);

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

        public void AcquireAttackTarget()
        {
          

            
            for (int i = 0; i < nearbyEnemies.Length; i++)
            {
                if (nearbyEnemies[i].gameObject.tag.Contains("unit") && nearbyEnemies[i].gameObject.GetComponent<UnitBase>().isDead == false)
                {
                    
                    
                    t = nearbyEnemies[i].transform;
                    

                }

            }

            
            MoveTo(t.position);

        }
        
        public void DefensiveMove()
        {
           
            Debug.Log(nearbyObjects.Length);
            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                if (nearbyObjects[i].gameObject.tag.Contains("unit"))
                {


                    t = nearbyObjects[i].transform;


                }
                else if(nearbyObjects[i].gameObject.tag.Contains("Player") && ReturnToPlayer == true)
                {
                    playerOutOfRange = false;
                }
                else
                {
                    playerOutOfRange = true;
                }

            }

            if(nearbyObjects.Length >= 4 && playerOutOfRange == false)
            {
                MoveTo(t.position);
            }
            else if(playerOutOfRange == true)
            {
                MoveTo(playerReference.position);
            }
            else
            {
                MoveTo(playerReference.position);
            }


            

        }

        public void SeekBoosters()
        {
            rootWaypoints = GameObject.FindGameObjectWithTag("GameController").GetComponent<BoosterMaster>().RootPoints;

            AcquiredTarget(rootWaypoints[Random.Range(0, 4)].position);

        }

        public void PatrolMode()
        {
           spawnerWaypoints = GameObject.FindGameObjectWithTag("waveMaster").GetComponent<WaveMaster>().spawnPointRoot;

            AcquiredTarget(spawnerWaypoints[Random.Range(0, 4)].transform.position);
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

        public override void StopMoving()
        {
         if(_steerForPath.path != null)
            {
                _steerForPath.path.Clear();
            }
               
        }

        IEnumerator AAT(float time)
        {
            yield return new WaitForSeconds(time);
            AcquireAttackTarget();

        }

        IEnumerator DM(float time)
        {
            yield return new WaitForSeconds(time);
            DefensiveMove();

        }

        IEnumerator SB(float time)
        {
            yield return new WaitForSeconds(time);
            SeekBoosters();
        }

        IEnumerator PM(float time)
        {
            yield return new WaitForSeconds(time);
            PatrolMode();
        }
    }

}
