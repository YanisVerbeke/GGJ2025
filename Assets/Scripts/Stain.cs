using UnityEngine;

public class Stain : MonoBehaviour
{
    // valeur entre -1 et 1 uniquement
    public float _currentCleanStatus = -1f;
    [SerializeField] private float yPosUpperLimit;
    [SerializeField] private float yPosLowerLimit;


    private void Awake()
    {

    }

    public void Start()
    {
        CleanStain(0);
    }

    public void CleanStain(float cleaningAmount)
    {
        _currentCleanStatus = Mathf.Clamp(_currentCleanStatus + (cleaningAmount * Time.deltaTime), -1f, 1f);
        Vector3 newPosition = new Vector3(transform.position.x, Mathf.Lerp(yPosUpperLimit, yPosLowerLimit,(_currentCleanStatus + 1) / 2), transform.position.z);
        transform.position = newPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Duck>() != null)
        {
            other.GetComponent<Duck>().ChangeSpeed(_currentCleanStatus);
        }
    }
}

