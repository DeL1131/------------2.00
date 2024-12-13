using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> OnCollided;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall _))
        OnCollided?.Invoke(this);
    }
}