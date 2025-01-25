using UnityEngine;

public class Duck : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _propulseForce = 20;
    [SerializeField] private float stainForce = 2;
    [SerializeField] private float _maxSpeed;
    //Debug 
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
        if (_rigidbody.linearVelocity.z > _maxSpeed)
        {
            Debug.Log("sfkdhjlhds" + _rigidbody.linearVelocity.z);
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y, _maxSpeed);
        }
        //Debug code, reset la position du canard a l'origine
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
        _rigidbody.AddForce(factorNormalized * _rigidbody.linearVelocity * stainForce, ForceMode.Acceleration);
    }
}
