namespace AI.Master
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]

    public sealed class ResourceComponent : MonoBehaviour
    {
        private const int maxUnitsHarvesting = 6;

        public float gatherRadius = 2.5f;

        public int minResources = 100;
        public int maxResources = 1000;
        public int currentResources;
        public int maxResourcesPerHarvest = 6;

        private readonly Vector3[] _gatherPositions = new Vector3[maxUnitsHarvesting];

        private void OnEnable()
        {
            this.currentResources = Random.Range(this.minResources, this.maxResources);
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, Random.Range(0, 360), this.transform.eulerAngles.z);

            var angle = 360f / maxUnitsHarvesting;
            for (int i = 0; i < maxUnitsHarvesting; i++)
            {
                _gatherPositions[i] = CircleHelpers.GetPointOnCircle(this.transform.position, gatherRadius, angle, i);
            }
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

        public Vector3 GetGatheringPositions(UnitBase unit)
        {
            var unitPos = unit.transform.position;
            var nearest = _gatherPositions[0];
            for (int i = 1; i < _gatherPositions.Length; i++)
            {
                var pos = _gatherPositions[i];
                if((unitPos - pos).sqrMagnitude < (unitPos - nearest).sqrMagnitude)
                {
                    nearest = pos;
                }
            }

            return nearest;
        }
        
    }
}

