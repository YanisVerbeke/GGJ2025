using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }

    [SerializeField] private GameObject _sparklesParticlesPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SpawnSparkles(Vector3 position)
    {
        Instantiate(_sparklesParticlesPrefab, position, Quaternion.Euler(-90, 0, 0));
    }
}
