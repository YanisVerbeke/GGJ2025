using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject _floor1;
    [SerializeField] private GameObject _floor2;
    [SerializeField] private Duck _duck;
    [SerializeField] private GameObject _stainPrefab;

    private float _floorOffset = 64.6f;

    private float _nextSpawnPosition;

    private void Update()
    {
        if (_duck.transform.position.z > _floor1.transform.position.z + (_floorOffset * 1))
        {
            _floor1.transform.position += new Vector3(0, 0, _floorOffset * 2);
        }
        if (_duck.transform.position.z > _floor2.transform.position.z + (_floorOffset * 1))
        {
            _floor2.transform.position += new Vector3(0, 0, _floorOffset * 2);
        }

        if (_duck.transform.position.z > _nextSpawnPosition)
        {
            SpawnStain();
        }
    }

    private void SpawnStain()
    {
        Vector3 position = new Vector3(_duck.transform.position.x + Random.Range(-3f, 3f), _duck.transform.position.y, _duck.transform.position.z + 20);
        Instantiate(_stainPrefab, position, Quaternion.identity);
        _nextSpawnPosition = _duck.transform.position.z + Random.Range(10f, 25f);
    }

}
