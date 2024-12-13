using System.Collections.Generic;
using UnityEngine;

public class CustomObjectPool<T> where T : MonoBehaviour
{
    private readonly List<T> _availableObjects = new List<T>(); 
    private readonly T _prefab; 
    private readonly Transform _parentTransform; 

    public CustomObjectPool(T prefab, Transform parentTransform = null)
    {
        _prefab = prefab;
        _parentTransform = parentTransform;
    }

    public T Get()
    {
        T instance;

        if (_availableObjects.Count > 0)
        {
            instance = _availableObjects[0];
            _availableObjects.RemoveAt(0);
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Object.Instantiate(_prefab, _parentTransform);
        }

        return instance;
    }

    public void ReturnToPool(T instance)
    {
        Rigidbody rigidbody = instance.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.isKinematic = false;
        instance.transform.rotation = Quaternion.identity;

        instance.gameObject.SetActive(false); 
        _availableObjects.Add(instance);     
    }
}