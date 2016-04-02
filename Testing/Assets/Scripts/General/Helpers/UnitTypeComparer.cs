namespace AI.Master
{
    using System.Collections.Generic;

    public class UnitTypeComparer : IEqualityComparer<UnitType>
    {
        public bool Equals(UnitType x, UnitType y)
        {
            return x == y;
        }

        public int GetHashCode(UnitType obj)
        {
            return (int)obj;
        }

    }
}

