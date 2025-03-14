using TMPro;
using UnityEngine;

public abstract class ViewInformation<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _textTotalSpawnObject;
    [SerializeField] private TextMeshProUGUI _textActiveSpawnObject;
    [SerializeField] private TextMeshProUGUI _textCountObjectsCreated;

    private void OnEnable()
    {
        _spawner.ObjectSpawned += ShowInfo;
        _spawner.ReturnedToPool += ShowInfo;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= ShowInfo;
        _spawner.ReturnedToPool -= ShowInfo;
    }

    protected void ShowInfo()
    {
        _textTotalSpawnObject.text = _spawner.TotalSpawnedObjects.ToString();
        _textActiveSpawnObject.text = _spawner.ActiveObjectsCount.ToString();
        _textCountObjectsCreated.text = _spawner.CountObjectsCreated.ToString();
    }
}