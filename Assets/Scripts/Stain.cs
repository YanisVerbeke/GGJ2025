using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stain : MonoBehaviour
{
    // valeur entre -1 et 1 uniquement
    public float _currentCleanStatus = -1f;
    [SerializeField] private float yPosUpperLimit;
    [SerializeField] private float yPosLowerLimit;
    [SerializeField] List<GameObject> dirtModels;
    [SerializeField] List<Material> dirtTextures;
    [SerializeField] private float minStainCleanness;
    [SerializeField] private float maxStainCleanness;
    private Transform dirtTransform;
    private GameObject cleanEffect;
    private bool _effectLaunched = false;
    public bool IsCleaned { get; private set; }



    public void Start()
    {
        _currentCleanStatus = Random.Range(minStainCleanness, maxStainCleanness);
        GameObject dirtModel = dirtModels[Random.Range(0, maxExclusive:dirtModels.Count)];
        dirtTransform = Instantiate(dirtModel, transform).transform;
        MeshRenderer dirtRenderer = dirtTransform.GetComponent<MeshRenderer>();
        int random = Random.Range(0, maxExclusive:dirtTextures.Count);
        dirtRenderer.material = dirtTextures[random];
        cleanEffect = transform.Find("CleanEffect").gameObject;
        CleanStain(0);
        IsCleaned = false;
    }

    private void Update()
    {
        if (Camera.main.transform.position.z > transform.position.z + 10)
        {
            Destroy(gameObject);
        }
    }

    public void CleanStain(float cleaningAmount)
    {
        if (!IsCleaned)
        {
            _currentCleanStatus = Mathf.Clamp(_currentCleanStatus + (cleaningAmount * Time.deltaTime), -1f, 1f);
            Vector3 dirtPosition = new Vector3(dirtTransform.position.x, Mathf.Lerp(yPosUpperLimit, yPosLowerLimit, (_currentCleanStatus + 1) / 2), dirtTransform.position.z);
            dirtTransform.position = dirtPosition;
            if (_currentCleanStatus >= 0)
            {
                cleanEffect.SetActive(true);
                ParticleSystem.MainModule main = cleanEffect.GetComponent<ParticleSystem>().main;
                main.startSize = (_currentCleanStatus + 0.25f) / 1.25f;
            }
            if (_currentCleanStatus >= 0.85f)
            {
                IsCleaned = true;
                dirtTransform.gameObject.SetActive(false);
                if (!_effectLaunched)
                {
                    SfxManager.Instance.PlayStainCleanedSfx();
                    EffectsManager.Instance.SpawnSparkles(transform.position);
                    _effectLaunched = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Duck>() != null)
        {
            other.GetComponent<Duck>().ChangeSpeed(_currentCleanStatus);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Duck>() != null && IsCleaned)
        {
            Debug.Log("Boost!");
            other.GetComponent<Duck>().Spin();
        }
    }
}

