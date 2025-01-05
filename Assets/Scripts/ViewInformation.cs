using TMPro;
using UnityEngine;

public class ViewInformation : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TextMeshProUGUI _textTotalSpawnObject;
    [SerializeField] private TextMeshProUGUI _textActiveSpawnObject;

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

    private void ShowInfo()
    {
        _textTotalSpawnObject.text = _spawner.TotalSpawnedObjects.ToString();
        _textActiveSpawnObject.text = _spawner.ActiveObjectsCount.ToString();
    }
}