namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    //Make it so they spawn randomly on enemy death, and at the end of each wave
    public class BoosterMaster : MonoBehaviour
    {
        public GameObject pHealth;
        public GameObject pDMG;
        public GameObject pMove;
        public GameObject master;
        public GameObject player;
        public GameObject companion;

        public Transform[] RootPoints;
        private int pointSelector;
        private int boosterTypeSelector;

        public bool hasSpawned;



        void Start()
        {
            master = GameObject.FindGameObjectWithTag("master");
            player = GameObject.FindGameObjectWithTag("Player");
            companion = GameObject.FindGameObjectWithTag("companion");
            GetRandomBooster();
        }

        void FixedUpdate()
        {
            if(GameObject.FindGameObjectWithTag("waveMaster").GetComponent<WaveMaster>().waveLevel > 0)
            {
                if(hasSpawned == true)
                {
                    GetRandomBooster();
                }

                GetRandomBoostTransformPoint();

                if(boosterTypeSelector == 0 && hasSpawned == false)
                {
                    
                    StartCoroutine(DMGBoostCooldown(30));
                }
                else if(boosterTypeSelector == 1 && hasSpawned == false)
                {
                  
                    StartCoroutine(MOVEBoostCooldown(10));
                }
                else if(boosterTypeSelector == 2 && hasSpawned == false)
                {
                    StartCoroutine(HEALTHBoostCooldown(20));
                }

            }

        }

        void GetRandomBoostTransformPoint()
        {
           pointSelector = Random.Range(0, 4);
        }

        void GetRandomBooster()
        {
            boosterTypeSelector = Random.Range(0, 3);

            
        }

        IEnumerator DMGBoostCooldown(float time)
        {
            Instantiate(pDMG, RootPoints[pointSelector].position, Quaternion.identity);
            hasSpawned = true;
            yield return new WaitForSeconds(time);
            hasSpawned = false;
            
        }
        IEnumerator MOVEBoostCooldown(float time)
        {
            Instantiate(pMove, RootPoints[pointSelector].position, Quaternion.identity);
            hasSpawned = true;
            yield return new WaitForSeconds(time);
            hasSpawned = false;

        }

        IEnumerator HEALTHBoostCooldown(float time)
        {
            Instantiate(pHealth, RootPoints[pointSelector].position, Quaternion.identity);
            hasSpawned = true;
            yield return new WaitForSeconds(time);
            hasSpawned = false;

        }
    }

}
