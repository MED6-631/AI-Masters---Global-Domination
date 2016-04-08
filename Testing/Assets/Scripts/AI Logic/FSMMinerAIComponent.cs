namespace AI.Master
{
    using System;
    using UnityEngine;

    public sealed class FSMMinerAIComponent : AIComponentBase<MinerUnit>
    {
        
        private enum MinerState
        {
            Idle,
            Gathering,
            Returning,
            Fleeing,
        }

        [SerializeField]
        private MinerState _currentState = MinerState.Idle;

        private UnitBase _fleeTarget;
        private ResourceComponent _resourceTarget;

        protected override void ExecuteAI()
        {
            if(_currentState == MinerState.Idle)
            {
                HandleIdle();
            }
            else if(_currentState == MinerState.Fleeing)
            {
                HandleFleeing();
            }
            else if(_currentState == MinerState.Gathering)
            {
                HandleGathering();
            }
            else if(_currentState == MinerState.Returning)
            {
                HandleReturning();
            }
        }

        private void HandleIdle()
        {
            var obs = _entity.obs;
            obs.Sort(new GameObjectDistanceSortComparer(this.transform.position));

            var count = obs.Count;
            for (int i = 0; i < count; i++)
            {
                var o = obs[i];

                var otherUnit = o.GetComponent<UnitBase>();
                if(otherUnit != null)
                {
                    if(_entity.IsAllied(otherUnit))
                    {
                        continue;
                    }

                    if((otherUnit.transform.position - _entity.transform.position).sqrMagnitude > (_entity.fleeRadius * _entity.fleeRadius))
                    {
                        continue;
                    }

                    _fleeTarget = otherUnit;
                    _currentState = MinerState.Fleeing;
                    return;
                }

                var resource = o.GetComponent<ResourceComponent>();
                if(resource != null)
                {
                    _resourceTarget = resource;
                    _currentState = MinerState.Gathering;
                    return;
                }
            }

            if(!_entity.isMoving)
            {
                _entity.RandomWander();
            }
        }

        private void HandleGathering()
        {
            if (_resourceTarget == null || _resourceTarget.currentResources <= 0)
            {
                _currentState = MinerState.Idle;
                _resourceTarget = null;
                return;
            }
            var distance = (_entity.transform.position - _resourceTarget.transform.position).sqrMagnitude;
            if (distance > (_entity.attackRadius * _entity.attackRadius))
            {
                if (!_entity.isMoving)
                {
                    _entity.MoveTo(_resourceTarget.transform.position);
                }
                return;
            }

            _entity.Gather(_resourceTarget);
            if (_entity.currentCarriedResources >= _entity.maxCarriableResources)
            {
                _currentState = MinerState.Returning;
            }
        }

        private void HandleReturning()
        {
            var distance = (_entity.transform.position - _entity.mainBase.transform.position).sqrMagnitude;
            if (distance > (_entity.returnHarvestRadius * _entity.returnHarvestRadius))
            {
                if (!_entity.isMoving)
                {
                    _entity.MoveTo(_entity.mainBase.transform.position);
                }

                return;
            }

            _entity.mainBase.currentResources += _entity.currentCarriedResources;
            _entity.currentCarriedResources = 0;
            _currentState = MinerState.Idle;
        }
        private void HandleFleeing()
        {
            var fleeDir = (_entity.transform.position - _fleeTarget.transform.position);
            if (_fleeTarget == null || _fleeTarget.isDead || fleeDir.sqrMagnitude > (_entity.fleeRadius * _entity.fleeRadius))
            {
                _currentState = MinerState.Idle;
                _fleeTarget = null;
                return;
            }

            if (!_entity.isMoving)
            {
                var fleePos = _entity.transform.position + fleeDir.normalized * _entity.fleeRadius;
                _entity.MoveTo(fleePos);
            }
        }
    }
}

