using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {get; private set;}
    [SerializeField] private GameObject _floor1;
    [SerializeField] private GameObject _floor2;
    [SerializeField] private Duck _duck;
    [SerializeField] private GameObject _stainPrefab;
    [SerializeField] private float distanceOfSpawn;
    private int score = 0;
    private float comboMultiplier = 1f;
    [SerializeField] private float comboDuration;
    [SerializeField] private float comboStep;
    [SerializeField] private int difficultyStep;
    public int difficulty;
    private float timer;
    private float _floorOffset = 64.6f;

    private float _nextSpawnPosition;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        timer = comboDuration;
    }

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
        UiManager.Instance.UpdateScoreDistance(score);
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            comboMultiplier = 1f;
            timer = comboDuration;
            Debug.Log("Combo perdu !");
        }

    }

    private void SpawnStain()
    {
        Vector3 position = new Vector3(_duck.transform.position.x + Random.Range(-3f, 3f),0 , _duck.transform.position.z + distanceOfSpawn);
        Instantiate(_stainPrefab, position, Quaternion.identity);
        _nextSpawnPosition = _duck.transform.position.z + Random.Range(10f, 25f);
    }

    public void AddScore(int scoreToAdd)
    {
        score += (int)Mathf.Floor(scoreToAdd * comboMultiplier);
        if(difficulty != score / difficultyStep)
        {
            GameObject.Find("DuckHolder").SendMessage("DifficultyIncrease");
            difficulty++;
        }
    }

    public void AddCombo()
    {
        comboMultiplier += comboStep;
        timer = comboDuration;
        Debug.Log("Combo ! :" + comboMultiplier);
    }

}
