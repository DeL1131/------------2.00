using UnityEngine;
using System;

public abstract class Spawner : MonoBehaviour
{
    public event Action ObjectSpawned;
    public event Action ReturnedToPool;

    public int TotalSpawnedObjects { get; protected set; } = 0;
    public int ActiveObjectsCount { get; protected set; } = 0;

    protected void InvokeObjectSpawned()
    {
        ObjectSpawned?.Invoke();
    }

    protected void InvokeReturnedToPool()
    {
        ReturnedToPool?.Invoke();
    }
}