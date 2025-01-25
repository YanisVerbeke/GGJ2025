using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Duck : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _propulseForce = 20;
    private Vector3 _basePos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _basePos = transform.position;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Mathf.Abs(_rigidbody.linearVelocity.z) < 0.2f)
            {
                Propulse();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = _basePos;
        }
    }

    private void Propulse()
    {
        _rigidbody.AddForce(Vector3.forward * _propulseForce, ForceMode.Impulse);
    }

    public void ChangeSpeed(float factorNormalized)
    {
        _rigidbody.AddForce(factorNormalized * _rigidbody.linearVelocity * 5, ForceMode.Acceleration);
    }
}
