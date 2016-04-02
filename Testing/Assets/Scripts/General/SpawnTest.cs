namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class SpawnTest : MonoBehaviour
    {

        public GameObject tunit;

        public int numberOfUnits;
        public float spawnInterval = 2f;
        public int maxUnits;
        public float timer = 0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (numberOfUnits <= maxUnits && timer >= spawnInterval)
            {
                tunit.name = "TUNIT " + "(" + numberOfUnits + ")";
                Instantiate(tunit, transform.position, Quaternion.identity);
                numberOfUnits++;
                timer = 0f - Time.fixedDeltaTime;
            }

        }
    }

}
