namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class WaveSpawner : MonoBehaviour
    {

        public GameObject[] units;

        public int desiredSoldiers;
        public int desiredElites;
        public int desiredBosses;
        private int currentSoldiers;
        private int currentElites;
        private int currentBosses;
        public float spawnInterval = 2f;
        public float timer = 0f;
        public float wave;



        // Update is called once per frame
        void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (currentSoldiers <= desiredSoldiers && timer >= spawnInterval)
            {
                
                Instantiate(units[0], transform.position, Quaternion.identity);
                currentSoldiers++;
                timer = 0f - Time.fixedDeltaTime;
            }

            if(currentElites <= desiredElites && timer >= spawnInterval)
            {
                Instantiate(units[1], transform.position, Quaternion.identity);
                currentElites++;
                timer = 0f - Time.fixedDeltaTime;
            }

            if (currentBosses <= desiredBosses && timer >= spawnInterval)
            {
                Instantiate(units[2], transform.position, Quaternion.identity);
                currentBosses++;
                timer = 0f - Time.fixedDeltaTime;
            }

        }
    }

}
