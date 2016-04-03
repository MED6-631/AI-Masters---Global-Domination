﻿namespace AI.Master
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]

    public sealed class ResourceComponent : MonoBehaviour
    {
        public int minResources = 100;
        public int maxResources = 1000;
        public int currentResources;
        public int maxResourcesPerHarvest = 6;

        private void OnEnable()
        {
            this.currentResources = Random.Range(this.minResources, this.maxResources);
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, Random.Range(0, 360), this.transform.eulerAngles.z);
        }

        private void OnDisable()
        {
            Grid.instance.UpdateCellsBlockedStatus(this.GetComponent<Collider>());
        }

        public void Gather(MinerUnit unit)
        {
            var resources = Random.Range(1, this.maxResourcesPerHarvest);
            this.currentResources -= resources;
            unit.currentCarriedResources += resources;

            if(this.currentResources <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        
    }
}
