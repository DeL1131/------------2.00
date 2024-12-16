using TMPro;
using UnityEngine;

public class ViewCubeInformation : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private TextMeshProUGUI _textTotalSpawnObject;
    [SerializeField] private TextMeshProUGUI _textActiveSpawnObject;

    private void OnEnable()
    {
        _cubeSpawner.SpawnCube += ShowInfo;
        _cubeSpawner.ReturnCubeToPool += ShowInfo;
    }

    private void OnDisable()
    {
        _cubeSpawner.SpawnCube -= ShowInfo;
        _cubeSpawner.ReturnCubeToPool -= ShowInfo;
    }

    private void ShowInfo()
    {
        _textTotalSpawnObject.text = _cubeSpawner.TotalSpawnedObjects.ToString();
        _textActiveSpawnObject.text = _cubeSpawner.ActiveObjectsCount.ToString();
    }
}