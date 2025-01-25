using UnityEngine;

public class Duck : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField] private float _propulseForce = 20;
    [SerializeField] private float stainForce = 2;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private GameObject _baseModel;
    [SerializeField] private GameObject _bonusModel;

    // Pas debug en fait, on en a vraiment besoin 
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
        if (Input.GetButtonDown("Jump"))
        {
            if (Mathf.Abs(_rigidbody.linearVelocity.z) < 0.2f)
            {
                Propulse();

            }
        }
        if (_rigidbody.linearVelocity.z > _maxSpeed)
        {
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y, _maxSpeed);
        }
        //Debug code, reset la position du canard a l'origine
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = _basePos;
        }
        UiManager.Instance.UpdateScoreDistance((int)(transform.position.z - _basePos.z));
    }

    private void Propulse()
    {
        _rigidbody.AddForce(Vector3.forward * _propulseForce, ForceMode.Impulse);
    }

    public void ChangeSpeed(float factorNormalized)
    {
        _rigidbody.AddForce(factorNormalized * _rigidbody.linearVelocity * stainForce, ForceMode.Acceleration);
    }

    public void Spin()
    {
        _animator.SetTrigger("Spin");
        EffectsManager.Instance.SpawnSparkles(transform.position + Vector3.forward * 2f);
    }
}
