using UnityEngine;

public class Bloc : MonoBehaviour
{
    // valeur entre -1 et 1 uniquement
    private float _factorNormalized = -1f;
    [SerializeField] private Gradient gradient;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = gradient.Evaluate(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CleanStain(0.006f);
        }
    }

    public void CleanStain(float cleanFactorNormalized)
    {
        _factorNormalized = Mathf.Clamp(_factorNormalized + cleanFactorNormalized, -1f, 1f);
        _meshRenderer.material.color = gradient.Evaluate((_factorNormalized + 1) / 2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Duck>() != null)
        {
            other.GetComponent<Duck>().ChangeSpeed(_factorNormalized);
        }
    }
}
