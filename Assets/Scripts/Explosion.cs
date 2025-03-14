using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explode()
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