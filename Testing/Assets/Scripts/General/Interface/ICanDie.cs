﻿namespace AI.Master
{
    using UnityEngine;

    public interface ICanDie
    {
        float currentHealth { get; }

        bool isDead { get; }

        GameObject gameObject { get; }

        Transform transform { get; }
    }
}

