using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Explosion))]

public class Bomb : MonoBehaviour
{
    private float _maxExplosionDelay = 6;
    private float _minExplosionDelay = 0;
    private float _explosionDelay;

    private Rigidbody _rigidbody;
    private Explosion _explosion;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _explosion = GetComponent<Explosion>();
    }

    private void OnEnable()
    {
        _explosionDelay = UnityEngine.Random.Range(_minExplosionDelay, _maxExplosionDelay);
        StartCoroutine(StartExplosionTimer());
    }

    public void ResetState()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }

    private IEnumerator StartExplosionTimer()
    {       
        yield return new WaitForSeconds(_explosionDelay);
        _explosion.Explode();
        Exploded?.Invoke(this);
    }
}