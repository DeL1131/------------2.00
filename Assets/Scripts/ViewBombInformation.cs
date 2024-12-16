using TMPro;
using UnityEngine;

public class ViewBombInformation : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private TextMeshProUGUI _textTotalSpawnObject;
    [SerializeField] private TextMeshProUGUI _textActiveSpawnObject;

    private void OnEnable()
    {
        _bombSpawner.SpawnBomb += ShowInfo;
        _bombSpawner.ReturnBombToPool += ShowInfo;
    }

    private void OnDisable()
    {
        _bombSpawner.SpawnBomb -= ShowInfo;
        _bombSpawner.ReturnBombToPool -= ShowInfo;
    }

    private void ShowInfo()
    {
        _textTotalSpawnObject.text = _bombSpawner.TotalSpawnedObjects.ToString();
        _textActiveSpawnObject.text = _bombSpawner.ActiveObjectsCount.ToString();
    }
}