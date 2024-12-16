using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Bomb))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Bomb _bomb;

    private void Awake()
    {
        _bomb = GetComponent<Bomb>();
    }

    private void OnEnable()
    {
        _bomb.OnExplosion += Explode;
    }

    private void OnDisable()
    {
        _bomb.OnExplosion -= Explode;
    }

    private void Explode(Bomb bomb)
    {
        foreach (Rigidbody exploadableObject in GetExplodableObjects())
        {
            exploadableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        ParticleSystem explosionEffect = Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(explosionEffect.gameObject, explosionEffect.main.duration);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> bombs = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                bombs.Add(hit.attachedRigidbody);

        return bombs;
    }
}