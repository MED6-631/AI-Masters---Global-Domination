﻿namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class HealthPack : MonoBehaviour
    {

        public int Health = 100;
        public int CHealth = 500;

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag.Contains("Player"))
            {
                other.GetComponent<PlayerController>().HealDamage(Health);
                GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().isHealthBoost = true;
                Destroy(this.gameObject);
            }
            else if(other.gameObject.tag.Contains("companion") && GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().LevelCheck == 1)
            {
                other.GetComponent<PathCompanionUnit>().HealDamage(CHealth);
                GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().isHealthBoost = true;
                Destroy(this.gameObject);
            }
            else if(other.gameObject.tag.Contains("companion") && GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().LevelCheck == 4)
            {
                other.GetComponent<PathUtilityCompanionUnit>().HealDamage(CHealth);
                GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().isHealthBoost = true;
                Destroy(this.gameObject);
            }
        }
    }

}
