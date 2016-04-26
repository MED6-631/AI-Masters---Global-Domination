namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using System;

    public class FSMEliteAIComponent : AIComponentBase<SoldierUnit>
    {

        private enum EliteState
        {
            Idle,
            Attacking
        }

        [SerializeField]
        private EliteState _currentState = EliteState.Idle;

        private ICanDie _attackTarget;

        protected override void ExecuteAI()
        {
            if (_currentState == EliteState.Idle)
            {
                HandleIdle();
            }
            else if (_currentState == EliteState.Attacking)
            {
                HandleAttacking();
            }
        }

        private void HandleAttacking()
        {
            if (_attackTarget == null || _attackTarget.isDead)
            {
                _attackTarget = null;
                _currentState = EliteState.Idle;
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
                    _currentState = EliteState.Attacking;
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
                    _currentState = EliteState.Attacking;
                    return;
                }
            }

            if (!_entity.isMoving)
            {
                _entity.RandomWander();
            }
        }

    }

}
