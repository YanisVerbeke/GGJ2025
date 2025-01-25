using UnityEngine;

public class Stain : MonoBehaviour
{
    // valeur entre -1 et 1 uniquement
    public float _currentCleanStatus = -1f;
    [SerializeField] private float yPosUpperLimit;
    [SerializeField] private float yPosLowerLimit;
    private Transform dirtTransform;
    private GameObject cleanEffect;


    private void Awake()
    {

    }

    public void Start()
    {
        dirtTransform = transform.Find("Dirt").transform;
        cleanEffect = transform.Find("CleanEffect").gameObject;
        CleanStain(0);
    }

    public void CleanStain(float cleaningAmount)
    {
        _currentCleanStatus = Mathf.Clamp(_currentCleanStatus + (cleaningAmount * Time.deltaTime), -1f, 1f);
        Vector3 dirtPosition = new Vector3(dirtTransform.position.x, Mathf.Lerp(yPosUpperLimit, yPosLowerLimit,(_currentCleanStatus + 1) / 2), dirtTransform.position.z);
        dirtTransform.position = dirtPosition;
        if(_currentCleanStatus >= 0)
        {
            cleanEffect.SetActive(true);
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

