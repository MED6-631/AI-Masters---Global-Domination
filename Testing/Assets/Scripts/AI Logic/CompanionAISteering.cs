namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class CompanionAISteering : MonoBehaviour
    {

        public float moveSpeed = 5f;
        public Transform t;

        public float seperationW = 1000.0f;
        public float cohesionW = 0.001f;

        public float avoidanceW = 1000.0f;
        public float alignmentW = 0.0001f;
        public float pursuitW = 0.001f;

        public Vector3 moveDir = Vector3.zero;


        void FixedUpdate()
        {



            AcquireTarget();

            if (t != null)
            {
                moveDir = ((t.position + t.forward) - transform.position).normalized * pursuitW;
            }





            Vector3 avgNeighborPos = Vector3.zero;
            Vector3 avgNeighborDir = Vector3.zero;

            int NearbyUnits = 0;

            Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 30);

            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                if (nearbyObjects[i].gameObject.tag.Contains("Player") && nearbyObjects[i] != GetComponent<Collider>())
                {
                    avgNeighborPos += nearbyObjects[i].transform.position;

                    NearbyUnits++;

                    avgNeighborDir += nearbyObjects[i].transform.forward;

                    Vector3 offset = nearbyObjects[i].transform.position - transform.position;
                    moveDir += (offset / -offset.sqrMagnitude) * seperationW;

                }

            }

            if (NearbyUnits > 0)
            {
                avgNeighborPos /= NearbyUnits;
                avgNeighborDir /= NearbyUnits;

                moveDir += (avgNeighborPos - transform.position).normalized * cohesionW;
                moveDir += avgNeighborDir.normalized * alignmentW;
            }

            RaycastHit hInfo;
            int layerMask = 1 << 10;

            if (Physics.SphereCast(new Ray(transform.position + moveDir.normalized * 1.6f, moveDir), 1f, out hInfo, 3))
            {
                if (hInfo.transform.gameObject.tag.Contains("obstacle") || hInfo.transform.gameObject.tag.Contains("resource"))
                {
                    Vector3 vectorToCenterOfObstacle = hInfo.transform.position - transform.position;
                    moveDir -= Vector3.Project(vectorToCenterOfObstacle, transform.right).normalized * (1f / vectorToCenterOfObstacle.magnitude) * avoidanceW;
                }

            }


            if (t == null)
            {
                moveDir = Vector3.zero;
            }

            moveDir = moveDir.normalized * moveSpeed;

            transform.position += moveDir * Time.fixedDeltaTime;

            //if (moveDir != Vector3.zero)
            //{
            //    transform.forward = moveDir;
            //}

            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        }


        public void AcquireTarget()
        {


            if (GameObject.FindGameObjectWithTag("Player"))
            {
                t = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else
            {
                t = null;
            }

        }


    }
}
