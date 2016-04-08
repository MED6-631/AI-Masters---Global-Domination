using System;

namespace AI.Master
{

    public sealed class FSMAIControllerComponent : AIComponentBase<AIController>
    {
        public int desiredMinerCount = 10;
        public int desiredSoldierCount = 10;

        protected override void ExecuteAI()
        {
            var mainBase = _entity.mainBase;
            var resources = mainBase.currentResources;
            if(resources <= 0)
            {
                return;
            }

            if(mainBase.minerCount < desiredMinerCount)
            {
                mainBase.SpawnUnit(UnitType.Miner);
            }
            else if(mainBase.soldierCount <= desiredSoldierCount)
            {
                mainBase.SpawnUnit(UnitType.Soldier);
            }
            
        }
    }
}

