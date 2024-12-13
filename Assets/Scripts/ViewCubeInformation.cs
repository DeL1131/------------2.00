using TMPro;
using UnityEngine;

public class ViewCubeInformation : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private TextMeshProUGUI _textTotalSpawnObject;
    [SerializeField] private TextMeshProUGUI _textActiveSpawnObject;

    private void Update()
    {
        _textTotalSpawnObject.text = _cubeSpawner.TotalSpawnedObjects.ToString();
        _textActiveSpawnObject.text = _cubeSpawner.ActiveObjectsCount.ToString();
    }
}