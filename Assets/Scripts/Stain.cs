using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    // valeur entre -1 et 1 uniquement
    public float _currentCleanStatus = -1f;
    [SerializeField] private float yPosUpperLimit;
    [SerializeField] private float yPosLowerLimit;
    [SerializeField] List<GameObject> dirtModels;
    private Transform dirtTransform;
    private GameObject cleanEffect;


    public void Start()
    {
        GameObject dirtModel = dirtModels[Random.Range(1, dirtModels.Count)];
        dirtTransform = Instantiate(dirtModel, transform).transform;
        cleanEffect = transform.Find("CleanEffect").gameObject;
        CleanStain(0);
    }

    public void CleanStain(float cleaningAmount)
    {
        if (_currentCleanStatus < 1f)
        {
            _currentCleanStatus = Mathf.Clamp(_currentCleanStatus + (cleaningAmount * Time.deltaTime), -1f, 1f);
            Vector3 dirtPosition = new Vector3(dirtTransform.position.x, Mathf.Lerp(yPosUpperLimit, yPosLowerLimit, (_currentCleanStatus + 1) / 2), dirtTransform.position.z);
            dirtTransform.position = dirtPosition;
            if (_currentCleanStatus >= 0)
            {
                cleanEffect.SetActive(true);
            }
            if (_currentCleanStatus >= 0.9f)
            {
                Debug.Log("MAX");
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
}

