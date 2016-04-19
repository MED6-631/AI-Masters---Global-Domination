namespace AI.Master
{
    using System;
    using UnityEngine;
    using Apex.AI;
    using Apex.AI.Components;
    public sealed class ControllerContextProvider : MonoBehaviour, IContextProvider
    {
        private ControllerContext _context;

        private void OnEnable()
        {
            _context = new ControllerContext(this.gameObject);
        }

        private void OnDisable()
        {
            _context = null;
        }

        public IAIContext GetContext(Guid aiId)
        {
            return _context;
        }
    }
}