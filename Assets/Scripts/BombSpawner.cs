using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeReleased += HandleSpawn;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeReleased -= HandleSpawn;
    }

    private void HandleSpawn(Transform transform)
    {
        Bomb newBomb = SpawnObject(); 
        newBomb.transform.position = transform.position;
        newBomb.Exploded += HandleBombReturn;
    }

    private void HandleBombReturn(Bomb bomb)
    {
        bomb.ResetState();
        bomb.Exploded -= HandleBombReturn;
        ReturnToPool(bomb);
    }
}