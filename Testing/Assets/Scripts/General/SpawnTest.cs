namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class SpawnTest : MonoBehaviour
    {

        public GameObject unit;

        public int numberOfUnits;
        public float spawnInterval = 2f;
        public int maxUnits;
        public float timer = 0f;



        // Update is called once per frame
        void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (numberOfUnits <= maxUnits && timer >= spawnInterval)
            {
                
                Instantiate(unit, transform.position, Quaternion.identity);
                numberOfUnits++;
                timer = 0f - Time.fixedDeltaTime;
            }

        }
    }

}
