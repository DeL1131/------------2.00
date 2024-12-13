using TMPro;
using UnityEngine;

public class ViewBombInformation : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private TextMeshProUGUI _textTotalSpawnObject;
    [SerializeField] private TextMeshProUGUI _textActiveSpawnObject;


    private void Update()
    {
        _textTotalSpawnObject.text = _bombSpawner.TotalSpawnedObjects.ToString();
        _textActiveSpawnObject.text = _bombSpawner.ActiveObjectsCount.ToString();
    }
}