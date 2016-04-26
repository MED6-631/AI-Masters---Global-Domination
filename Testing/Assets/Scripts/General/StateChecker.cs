namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class StateChecker : MonoBehaviour
    {

        public GameObject player;
        public GameObject companion;
        public GameObject master;


        void Start()
        {
            

            player = GameObject.FindGameObjectWithTag("Player");
            companion = GameObject.FindGameObjectWithTag("companion");
            master = GameObject.FindGameObjectWithTag("master");

        }

        void FixedUpdate()
        {

            
            master.GetComponent<MasterScript>().playerHP = player.GetComponent<PlayerController>().currentHealth;
            master.GetComponent<MasterScript>().companionHP = companion.GetComponent<PathCompanionUnit>().currentHealth;

            if (player.GetComponent<PlayerController>().currentHealth <= 0)
            {
                master.GetComponent<MasterScript>().isPlayerDead = true;
            }

            if(companion.GetComponent<PathCompanionUnit>().currentHealth <= 100)
            {
                master.GetComponent<MasterScript>().isCompanionDead = true;
            }

        }
    }
}

