namespace AI.Master
{
    using UnityEngine;
    using Apex.AI;
    public class ControllerContext : IAIContext
    {
        public ControllerContext(GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.controller = gameObject.GetComponent<AIController>();
        }

        public GameObject gameObject
        {
            get;
            private set;
        }

        public AIController controller
        {
            get;
            private set;
        }
    }
}