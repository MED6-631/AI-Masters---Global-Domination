﻿namespace AI.Master
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class Path : List<Vector3>
    {

        public Path(int preallocation)
            : base(preallocation)
        {
        }


        public Vector3 Pop()
        {
            if (this.Count == 0)
            {
                return default(Vector3);
            }

            var item = this[0];
            this.RemoveAt(0);
            return item;
        }
    }
}