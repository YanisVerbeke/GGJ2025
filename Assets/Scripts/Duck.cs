using UnityEngine;

public class Duck : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField] private float _propulseForce = 20;
    [SerializeField] private float stainForce = 2;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float maxSpeedSpinIncrease;
    [SerializeField] private float stainForceSpinIncrease;
    [SerializeField] private GameObject _baseModel;
    [SerializeField] private GameObject _bonusModel;
    private bool isAlive = true;
    private bool haveStarted = false;

    //Debug 
    private Vector3 _basePos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _basePos = transform.position;

        if (Random.Range(0, 20) == 0)
        {
            _baseModel.SetActive(false);
            _bonusModel.SetActive(true);
        }
        else
        {
            _bonusModel.SetActive(false);
            _baseModel.SetActive(true);
        }
    }

    private void Update()
    {
        
        //Debug code, reset la position du canard a l'origine
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = _basePos;
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody.linearVelocity.z <= 0.1 && haveStarted)
        {
            isAlive = false;
            Debug.Log("DEAD!");
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (Mathf.Abs(_rigidbody.linearVelocity.z) < 0.2f)
            {
                Propulse();
                haveStarted = true;
            }
        }
        
        if (_rigidbody.linearVelocity.z > _maxSpeed)
        {
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y, _maxSpeed);
        }
    }

    private void Propulse()
    {
        if(isAlive)
        {
            _rigidbody.AddForce(Vector3.forward * _propulseForce, ForceMode.Impulse);
        }
    }

    public void ChangeSpeed(float factorNormalized)
    {
        if(isAlive)
        {
            _rigidbody.AddForce(factorNormalized * _rigidbody.linearVelocity * stainForce, ForceMode.Acceleration);
        }
    }

    public void Spin()
    {
        _animator.SetTrigger("Spin");
        _maxSpeed += maxSpeedSpinIncrease;
        stainForce += stainForceSpinIncrease;
    }
}
