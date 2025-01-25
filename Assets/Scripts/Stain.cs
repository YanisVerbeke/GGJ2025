using UnityEngine;

public class Stain : MonoBehaviour
{
    // valeur entre -1 et 1 uniquement
    public float _currentCleanStatus = -1f;
    private Transform dirtMesh;


    private void Awake()
    {
        dirtMesh = GetComponentInChildren<Transform>();
    }

    public void CleanStain(float cleaningAmount)
    {
        _currentCleanStatus = Mathf.Clamp(_currentCleanStatus + (cleaningAmount * Time.deltaTime), -1f, 1f);
        Vector3 newPosition = new Vector3(dirtMesh.position.x, Mathf.Lerp(0.18f, 0.06736543f ,(_currentCleanStatus + 1) / 2), dirtMesh.position.z);
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

