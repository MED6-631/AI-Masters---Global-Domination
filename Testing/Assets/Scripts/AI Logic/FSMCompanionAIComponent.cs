namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using System;

    public class FSMCompanionAIComponent : AIComponentBase<CompanionAISteering>
    {

        private enum CompanionState
        {
            Idle,
            Attacking
        }

        [SerializeField]
        private CompanionState _currentState = CompanionState.Idle;

        private ICanDie _attackTarget;



        protected override void ExecuteAI()
        {
            if (_currentState == CompanionState.Idle)
            {
                HandleIdle();
            }
            else if (_currentState == CompanionState.Attacking)
            {
                HandleAttacking();
            }
        }

        private void HandleAttacking()
        {
            if (_attackTarget == null || _attackTarget.isDead)
            {
                _attackTarget = null;
                _currentState = CompanionState.Idle;
                return;
            }

            if ((_attackTarget.transform.position - _entity.transform.position).sqrMagnitude > (_entity.attackRadius * _entity.attackRadius))
            {
                if (!_entity.isMoving)
                {
                    _entity.MoveTo(_attackTarget.transform.position);
                }

                return;
            }

            _entity.Attack();
        }

        private void HandleIdle()
        {
            var o = _entity.obs;
            o.Sort(new GameObjectDistanceSortComparer(this.transform.position));

            var count = o.Count;
            for (int i = 0; i < count; i++)
            {
                var obs = o[i];

                var otherUnit = obs.GetComponent<UnitBase>();

                if (otherUnit != null)
                {
                    if (_entity.IsAllied(otherUnit))
                    {
                        continue;
                    }

                    if (otherUnit.teamID == this.gameObject.GetComponent<UnitBase>().teamID)
                    {
                        continue;
                    }

                    _attackTarget = otherUnit;
                    _currentState = CompanionState.Attacking;
                    return;
                }

                var mainBase = obs.GetComponent<MainBaseStructure>();
                if (mainBase != null)
                {
                    if (_entity.IsAllied(mainBase))
                    {
                        continue;
                    }

                    _attackTarget = mainBase;
                    _currentState = CompanionState.Attacking;
                    return;
                }
            }

            //if (!_entity.isMoving)
            //{
            //    _entity.RandomWander();
            //}
        }
    }

}
