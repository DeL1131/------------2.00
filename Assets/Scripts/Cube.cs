using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public event Action<Cube> OnCollided;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _))
        {
            OnCollided?.Invoke(this);
        }
    }

    public void ResetState()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }
}