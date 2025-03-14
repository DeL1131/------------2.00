using UnityEngine;
using System;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    private CustomObjectPool<T> _objectPool;

    public event Action ObjectSpawned;
    public event Action ReturnedToPool;

    public int TotalSpawnedObjects { get; protected set; } = 0;
    public int ActiveObjectsCount { get; protected set; } = 0;
    public int CountObjectsCreated { get; protected set; } = 0;

    private void Awake()
    {
        _objectPool = new CustomObjectPool<T>(_prefab);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(UnityEngine.Random.Range(transform.position.x - 5, transform.position.x + 5), transform.position.y, UnityEngine.Random.Range(transform.position.z - 5, transform.position.z + 5));
    }

    protected T SpawnObject()
    {
        TotalSpawnedObjects++;
        ActiveObjectsCount++;
        CountObjectsCreated++;

        T obj = _objectPool.Get();
        obj.transform.position = GetRandomPosition();
        ObjectSpawned?.Invoke();

        return obj;
    }

    protected void ReturnToPool(T obj)
    {
        ActiveObjectsCount--;
        CountObjectsCreated--;

        _objectPool.ReturnToPool(obj);
        ReturnedToPool?.Invoke();
    }
}