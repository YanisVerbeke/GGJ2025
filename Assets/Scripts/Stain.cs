using UnityEngine;

public class Stain : MonoBehaviour
{
    // valeur entre -1 et 1 uniquement
    private float _currentCleanStatus = -1f;
    [SerializeField] private Gradient _gradient;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = _gradient.Evaluate(0);
    }

    public void CleanStain(float cleaningAmount)
    {
        _currentCleanStatus = Mathf.Clamp(_currentCleanStatus + (cleaningAmount * Time.deltaTime), -1f, 1f);
        _meshRenderer.material.color = _gradient.Evaluate((_currentCleanStatus + 1) / 2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Duck>() != null)
        {
            other.GetComponent<Duck>().ChangeSpeed(_currentCleanStatus);
        }
    }
}
