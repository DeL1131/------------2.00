using System;
using UnityEngine;
using System.Collections;

public class CubeSpawner : Spawner<Cube>
{
    private const KeyCode SpawnCubeKey = KeyCode.Space;

    private Coroutine _coroutineSpawn;

    private float _spawnDelay = 0.1f;

    public event Action<Transform> CubeReleased;

    private void Update()
    {
        if (Input.GetKeyDown(SpawnCubeKey))
        {
            if (_coroutineSpawn == null)
            {
                _coroutineSpawn = StartCoroutine(CountDelay());
            }
            else
            {
                StopCoroutine(_coroutineSpawn);
                _coroutineSpawn = null;
            }
        }
    }

    private void HandleSpawn()
    {
        Cube newCube = SpawnObject();
        newCube.OnCollided += HandleCubeReturn;
    }

    private IEnumerator CountDelay()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            HandleSpawn();
            yield return waitForSeconds;
        }
    }

    private void HandleCubeReturn(Cube cube)
    {
        CubeReleased?.Invoke(cube.transform);
        cube.OnCollided -= HandleCubeReturn;
        ReturnToPool(cube);
        cube.ResetState();
    }
}