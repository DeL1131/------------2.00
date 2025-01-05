using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _maxExplosionDelay = 6;
    private float _minExplosionDelay = 0;
    private float _explosionDelay;

    public event Action<Bomb> Exploding;

    private void OnEnable()
    {
        _explosionDelay = UnityEngine.Random.Range(_minExplosionDelay, _maxExplosionDelay);
        StartCoroutine(StartExplosionTimer());
    }

    private IEnumerator StartExplosionTimer()
    {       
        yield return new WaitForSeconds(_explosionDelay);
        Exploding?.Invoke(this);
    }
}