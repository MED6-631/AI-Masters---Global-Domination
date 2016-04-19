namespace AI.Master
{
    using System;
    using UnityEngine;
    using Apex.AI;
    using Apex.AI.Components;

    public sealed class ContextProvider : MonoBehaviour, IContextProvider
    {
        private AIContext _context;

        private void OnEnable()
        {
            // Build a context object for this unit from its GameObject.
            _context = new AIContext(this.gameObject);
        }


        public IAIContext GetContext(Guid aiId)
        {
            return _context;
        }
    }
}