using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spawnables")]
public class Spawnables : ScriptableObject
{
    [field: SerializeField] public List<SpawnRate> spawnRates { get; private set; }

    [Serializable]
    public class SpawnRate
    {
        [field: SerializeField, Range(0f, 1f)] public float rate { get; private set; }
        [field: SerializeField] public GameObject spawnable { get; private set; } // Should have a Collectible component
    }
}
