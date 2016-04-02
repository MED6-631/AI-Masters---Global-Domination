﻿namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public abstract class AIComponentBase<T> : MonoBehaviour where T : MonoBehaviour
    {

        public float interval = 0.5f;

        protected T _entity;

        private float _lastExecution;

        protected virtual void Awake()
        {
            _entity = this.GetComponent<T>();
        }

        protected virtual void Update()
        {
            if(_entity == null)
            {
                return;
            }

            var time = Time.time;
            if(time - _lastExecution < this.interval)
            {
                return;
            }

            _lastExecution = time;
            ExecuteAI();
        }

        protected abstract void ExecuteAI();

    }
}

