using System;
using UnityEngine;

public class BombSpawner : Spawner
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private CustomObjectPool<Bomb> _poolBombs;

    private void Awake()
    {
        _poolBombs = new CustomObjectPool<Bomb>(_bombPrefab);
    }

    private void OnEnable()
    {
        _cubeSpawner.CollisionTransformDetected += SpawnObject;
    }

    private void OnDisable()
    {
        _cubeSpawner.CollisionTransformDetected -= SpawnObject;
    }

    private void SpawnObject(Transform transform)
    {
        TotalSpawnedObjects++;
        ActiveObjectsCount++;

        InvokeObjectSpawned();

        Bomb newBomb = _poolBombs.Get(); 
        newBomb.transform.position = transform.position;

        newBomb.Exploding += ReturnToPool;
    }

    private void ReturnToPool(Bomb bomb)
    {
        ActiveObjectsCount--;

        InvokeReturnedToPool();

        _poolBombs.ReturnToPool(bomb);
        bomb.Exploding -= ReturnToPool;
    }
}