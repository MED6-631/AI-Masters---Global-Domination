namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class StateChecker : MonoBehaviour
    {

        public GameObject player;
        public GameObject companion;
        public GameObject master;
        public GameObject gC;
        public GameObject waveButton;
        private float previousHealth;
        private PathUtilityCompanionUnit pucu;
        private EmoticonCommunicationSystem ecs;


        void Start()
        {
            

            player = GameObject.FindGameObjectWithTag("Player");
            companion = GameObject.FindGameObjectWithTag("companion");
            master = GameObject.FindGameObjectWithTag("master");
            gC = GameObject.FindGameObjectWithTag("GameController");
            pucu = companion.GetComponent<PathUtilityCompanionUnit>();
            ecs = gC.GetComponent<EmoticonCommunicationSystem>();

        }

        void FixedUpdate()
        {

            master.GetComponent<MasterScript>().playerHP = player.GetComponent<PlayerController>().currentHealth;
            if(master.GetComponent<MasterScript>().LevelCheck == 1)
            {
                master.GetComponent<MasterScript>().companionHP = companion.GetComponent<PathCompanionUnit>().currentHealth;
            }
            else if(master.GetComponent<MasterScript>().LevelCheck == 4)
            {
                master.GetComponent<MasterScript>().companionHP = companion.GetComponent<PathUtilityCompanionUnit>().currentHealth;
            }

            master.GetComponent<MasterScript>().waveButton = waveButton;
            if (player.GetComponent<PlayerController>().currentHealth <= 0)
            {
                master.GetComponent<MasterScript>().isPlayerDead = true;
            }
            if(master.GetComponent<MasterScript>().LevelCheck == 1)
            {
                if (master.GetComponent<MasterScript>().companionHP <= 30)
                {
                    master.GetComponent<MasterScript>().isCompanionDead = true;
                }
            }

            if(master.GetComponent<MasterScript>().LevelCheck == 4)
            {
                if (master.GetComponent<MasterScript>().companionHP <= 30)
                {
                    master.GetComponent<MasterScript>().isCompanionDead = true;
                }
            }


            if(pucu.Aggressive)
            {
                if (master.GetComponent<MasterScript>().companionHP <= 400f && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Sad)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Sad;
                }

                if(master.GetComponent<MasterScript>().playerHP <= 40 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Angry)
                {
                    ecs._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                }

                if (master.GetComponent<MasterScript>().dmgBoostTime >= 0 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Happy)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                }

                if (master.GetComponent<MasterScript>().moveBoostTime >= 0 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Happy)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                }

            }
            else if(pucu.Defensive)
            {
                if (master.GetComponent<MasterScript>().companionHP <= 200f && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Angry)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                }

                if (master.GetComponent<MasterScript>().playerHP <= 40 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Sad)
                {
                    ecs._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Sad;
                }

                if (master.GetComponent<MasterScript>().dmgBoostTime >= 0 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Happy)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                }

                if (master.GetComponent<MasterScript>().moveBoostTime >= 0 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Happy)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                }
            }
            else if(pucu.Collector)
            {
                if (master.GetComponent<MasterScript>().companionHP <= 700f && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Sad)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Sad;
                }

                if (master.GetComponent<MasterScript>().playerHP <= 80 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Angry)
                {
                    ecs._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                }

                if (master.GetComponent<MasterScript>().dmgBoostTime >= 0 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Happy)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                }

                if (master.GetComponent<MasterScript>().moveBoostTime >= 0 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Happy)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                }
            }
            else if(pucu.Patrol)
            {
                if (master.GetComponent<MasterScript>().companionHP <= 400f && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Sad)
                {
                    gC.GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Sad;
                }

                if (master.GetComponent<MasterScript>().playerHP <= 40 && ecs._currentCompanionState != EmoticonCommunicationSystem.CompanionEmotionState.Angry)
                {
                    ecs._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                }
            }




        }

        public void AcquireMaster()
        {
            master.GetComponent<MasterScript>().LoadScene(5);
        }


    }
}

