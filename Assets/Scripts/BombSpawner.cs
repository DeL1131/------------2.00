using System;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private CustomObjectPool<Bomb> _poolBombs;

    public event Action SpawnBomb;
    public event Action ReturnBombToPool;

    public int TotalSpawnedObjects { get; private set; } = 0;
    public int ActiveObjectsCount { get; private set; } = 0;

    private void Awake()
    {
        _poolBombs = new CustomObjectPool<Bomb>(_bombPrefab);
    }

    private void OnEnable()
    {
        _cubeSpawner.CollisionTransformDetected += Spawn;
    }

    private void OnDisable()
    {
        _cubeSpawner.CollisionTransformDetected -= Spawn;
    }

    private void Spawn(Transform transform)
    {
        TotalSpawnedObjects++;
        ActiveObjectsCount++;

        SpawnBomb?.Invoke();

        Bomb newBomb = _poolBombs.Get(); 
        newBomb.transform.position = transform.position;

        newBomb.OnExplosion += ReturnToPool;
    }

    private void ReturnToPool(Bomb bomb)
    {
        ActiveObjectsCount--;

        ReturnBombToPool?.Invoke();

        _poolBombs.ReturnToPool(bomb);
        bomb.OnExplosion -= ReturnToPool;
    }
}