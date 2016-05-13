namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class WaveMaster : MonoBehaviour
    {

        public GameObject[] enemyUnits;
        public bool waveActive = false;

        public GameObject[] spawnPointRoot;

        public int waveLevel = 1;
        public float intermissionLength = 10f;
        public int enemyCount = 0;
        private ArrayList enemies;
        public bool allEnemiesSpawned = false;
        public float difficultyMultiplier;
        private MasterScript master;
        public int enemySoldierAmount;
        public int enemyEliteAmount;
        public int enemyBossAmount;
        private float spawnInterval = 2;
        public GameObject[] sp;

        public enum WaveState
        {
            preStart,
            activeWave,
            intermission
        }

        WaveState state = WaveState.preStart;

        void Start()
        {
            enemies = new ArrayList();
            master = GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>();
            initializeSpawners();

        }

        void FixedUpdate()
        {
            
            switch (state)
            {
                case WaveState.preStart:
                    if(master.startWave)
                    {
                        setNextWave();
                        startNewWave();
                        master.startWave = false;
                    }
                    else
                    {

                    }
                    break;

                case WaveState.activeWave:
                    if(enemyCount == 0 && waveActive && allEnemiesSpawned)
                    {
                        finishWave();
                    }
                    break;


                case WaveState.intermission:
                    break;
            }


            for (int i = 0; i < enemies.Count; i++)
            {
                if ((GameObject)(enemies[i]) == null)
                {
                    enemies.Remove(enemies[i]);
                }
            }
            enemyCount = enemies.Count;

        }

        //void LateUpdate()
        //{
        //    for (int i = 0; i <enemies.Count; i++)
        //    {
        //        if((GameObject)(enemies[i]) == null)
        //        {
        //            enemies.Remove(enemies[i]);
        //        }
        //    }
        //    enemyCount = enemies.Count;
        //}


        void setNextWave()
        {
            difficultyMultiplier = difficultyMultiplier + waveLevel;

            enemySoldierAmount += Mathf.RoundToInt(difficultyMultiplier/3);
            enemyEliteAmount += Mathf.RoundToInt(difficultyMultiplier / 4);
            enemyBossAmount += Mathf.RoundToInt(difficultyMultiplier / 6);

            if(GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().LevelCheck == 1)
            {
                GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().HealDamage(500);
            }
            else if(GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().LevelCheck == 4)
            {
                GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().HealDamage(500);
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().HealDamage(100);


         }

        void startNewWave()
        {
            state = WaveState.activeWave;
            StartCoroutine(StartLevel(1.5f));
            waveLevel++;
        }

        IEnumerator InterMission(float sec)
        {
            yield return new WaitForSeconds(sec);
            setNextWave();
            startNewWave();
        }

        IEnumerator EnemySpawnerRoutine(float spawnIntervall, int eSAmount, int eEAmount, int eBAmount)
        {
            for (int i = 0; i < eSAmount; i++)
            {
                spawnNewSoldierEnemy();
                yield return new WaitForSeconds(spawnIntervall);
            }

            for (int i = 0; i < eEAmount; i++)
            {
                spawnNewEliteEnemy();
                yield return new WaitForSeconds(spawnIntervall);
            }


            allEnemiesSpawned = true;
        }


        void finishWave()
        {
            StartCoroutine("InterMission", intermissionLength);
            state = WaveState.intermission;
            waveActive = false;
        }

        void spawnNewSoldierEnemy()
        {
            int tempI = 0;
            for (int i = 0; i < enemySoldierAmount; i++)
            {
                
                
                GameObject eS = (GameObject)Instantiate(enemyUnits[0], spawnPointRoot[tempI].transform.position, Quaternion.identity);
                enemyCount++;
                enemies.Add(eS);
                tempI++;
                if (tempI > spawnPointRoot.Length -1)
                {
                    tempI = 0;
                }
            }



        }

        void spawnNewEliteEnemy()
        {
            int tempI = 0;
            for (int i = 0; i < enemyEliteAmount; i++)
            {


                GameObject eS = (GameObject)Instantiate(enemyUnits[1], spawnPointRoot[tempI].transform.position, Quaternion.identity);
                enemyCount++;
                enemies.Add(eS);
                tempI++;
                if (tempI > spawnPointRoot.Length - 1)
                {
                    tempI = 0;
                }
            }



        }

        IEnumerator StartLevel(float sec)
        {
            yield return new WaitForSeconds(sec);
            allEnemiesSpawned = false;
            StartCoroutine(EnemySpawnerRoutine(spawnInterval, enemySoldierAmount, enemyEliteAmount, enemyBossAmount));

            waveActive = true;
        }

        void initializeSpawners()
        {
            sp = GameObject.FindGameObjectsWithTag("spawner");

            

            for(int i = 0; i < sp.Length; i++)
            {
                spawnPointRoot[i] = sp[i];
            }

        }


    }
}

