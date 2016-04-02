namespace AI.Master
{


    public static class UnitCostManager
    {
        private const int MinerUnitCost = 50;
        private const int FighterUnitCost = 75;
        private const int exploderUnitCost = 120;

        public static int GetCost(UnitType type)
        {
            switch(type)
            {
                case UnitType.Miner:
                {
                        return MinerUnitCost;
                }
                case UnitType.Soldier:
                {
                        return FighterUnitCost;
                }
                case UnitType.Demolisher:
                {
                        return exploderUnitCost;
                }
            }

            return 0;
        }
    
    }
}

