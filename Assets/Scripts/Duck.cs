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
    private bool _startPressed = false;
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
            SfxManager.Instance.Bidoof = true;
        }
        else
        {
            _bonusModel.SetActive(false);
            _baseModel.SetActive(true);
            SfxManager.Instance.Bidoof = false;
        }
    }

    private void Update()
    {

        //Debug code, reset la position du canard a l'origine
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = _basePos;
        }

        if (Input.GetButtonDown("Jump") && !haveStarted)
        {
            _startPressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody.linearVelocity.z <= 0.4 && haveStarted)
        {
            isAlive = false;
            _rigidbody.linearVelocity = Vector3.zero;
            UiManager.Instance.SetEndScreenVisibility(true);
            Debug.Log("DEAD!");
        }
        if (_startPressed)
        {
            if (Mathf.Abs(_rigidbody.linearVelocity.z) < 0.2f)
            {
                UiManager.Instance.DisplayStartText(false);
                Propulse();
                haveStarted = true;
                _startPressed = false;
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
        if (isAlive)
        {
            _rigidbody.AddForce(Vector3.forward * _propulseForce, ForceMode.Impulse);
        }
    }

    public void ChangeSpeed(float factorNormalized)
    {
        if (isAlive)
        {
            _rigidbody.AddForce(factorNormalized * _rigidbody.linearVelocity * stainForce, ForceMode.Acceleration);
        }
    }

    public void Spin()
    {
        SfxManager.Instance.PlayDuckSpinSfx();
        _animator.SetTrigger("Spin");
        _maxSpeed += maxSpeedSpinIncrease;
        stainForce += stainForceSpinIncrease;
        EffectsManager.Instance.SpawnSparkles(transform.position + Vector3.forward * 2f);
    }

    private void OnMouseDown()
    {
        SfxManager.Instance.PlayDuckQuackSfx();
    }
}
