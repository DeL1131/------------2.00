using System;
using UnityEngine;
using System.Collections;

public class CubeSpawner : Spawner
{
    private const KeyCode SpawnCubeKey = KeyCode.Space;

    [SerializeField] private Cube _cubePrefab;

    private CustomObjectPool<Cube> _poolCubes;
    private Coroutine _coroutineSpawn;

    private float _spawnDelay = 0.1f;
    private bool _isCoroutineActive = false;

    public event Action<Transform> CollisionTransformDetected;

    private void Start()
    {
        _poolCubes = new CustomObjectPool<Cube>(_cubePrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(SpawnCubeKey))
        {
            if (!_isCoroutineActive)
            {
                _isCoroutineActive = true;
                _coroutineSpawn = StartCoroutine(CountDelay());
            }
            else
            {
                _isCoroutineActive = false;
                StopCoroutine(_coroutineSpawn);
            }
        }
    }

    private void SpawnObject()
    {
        TotalSpawnedObjects++;
        ActiveObjectsCount++;

        InvokeObjectSpawned();

        Cube newCube = _poolCubes.Get();
        newCube.OnCollided += ReturnToPool;
        newCube.transform.position = GetRandomPosition();
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(UnityEngine.Random.Range(transform.position.x - 5, transform.position.x + 5), transform.position.y, UnityEngine.Random.Range(transform.position.z - 5, transform.position.z + 5));
    }

    private IEnumerator CountDelay()
    {
        while (_isCoroutineActive)
        {
            SpawnObject();
            WaitForSeconds waitForSeconds = new WaitForSeconds(_spawnDelay);
            yield return waitForSeconds;
        }
    }

    private void ReturnToPool(Cube cube)
    {
        ActiveObjectsCount--;

        InvokeReturnedToPool();
        CollisionTransformDetected?.Invoke(cube.transform);

        cube.OnCollided -= ReturnToPool;
        _poolCubes.ReturnToPool(cube);
    }
}