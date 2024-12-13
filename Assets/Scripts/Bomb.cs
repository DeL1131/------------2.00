using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _maxExplosionDelay = 6;
    private float _minExplosionDelay = 0;
    private float _explosionDelay;

    public event Action<Bomb> OnExplosion;

    private void OnEnable()
    {
        _explosionDelay = UnityEngine.Random.Range(0, _maxExplosionDelay);
        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_explosionDelay);
        yield return waitForSeconds;
        OnExplosion?.Invoke(this);
    }
}