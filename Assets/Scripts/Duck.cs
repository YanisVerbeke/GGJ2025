using System.Threading;
using UnityEngine;

public class Duck : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField] private float _maxPropulseForce = 20;
    [SerializeField] private float _propulseForce;
    [SerializeField] private float stainForce = 2;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float maxSpeedDifficultyStep;
    [SerializeField] private float stainForceDifficultyStep;
    [SerializeField] private GameObject _baseModel;
    [SerializeField] private GameObject _bonusModel;
    private bool isAlive = true;
    private bool haveStarted = false;
    private bool _startPressed = false;
    // Pas debug en fait, on en a vraiment besoin 
    private Vector3 _basePos;
    private ParticleSystem _bubbles1;
    private ParticleSystem _bubbles2;
    private float _bubblesTimer;
    [SerializeField] private float timeBeforeSpeedLimit;
    private float timer;

    [SerializeField] private float deadzone;
    private Vector3 previousPos;
    private float scoreUpdateTimer = 0.3f;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _bubbles1 = GetComponentsInChildren<ParticleSystem>()[0];
        _bubbles2 = GetComponentsInChildren<ParticleSystem>()[1];
        _basePos = transform.position;
        previousPos = transform.position;
        timer = timeBeforeSpeedLimit;
    }

    private void Start()
    {
        UiManager.Instance.OnUltraGigaMaxiBubblesOfDarknessDoomEnabled += UiManager_OnUltraGigaMaxiBubblesOfDarknessDoomEnabled;

        //Le jeu casse completement sans ce code, on sait pas pourquoi
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

    private void UiManager_OnUltraGigaMaxiBubblesOfDarknessDoomEnabled(object sender, System.EventArgs e)
    {
        if (_bubblesTimer <= 0)
        {
            _bubblesTimer = 3f;
            _bubbles1.Play();
            _bubbles2.Play();
        }
    }

    private void Update()
    {
        if (_bubblesTimer > 0)
        {
            _bubblesTimer -= Time.deltaTime;
        }
        else
        {
            _bubbles1.Stop();
            _bubbles2.Stop();
        }

        //Debug code, reset la position du canard a l'origine
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = _basePos;
        }

        if (Input.GetButtonDown("Jump") && !haveStarted)
        {
            _startPressed = true;
        }
        scoreUpdateTimer -= Time.deltaTime;
        timer -= Time.deltaTime;
        
        if(scoreUpdateTimer <= 0)
        {
            Calculatescore(previousPos.z);
            scoreUpdateTimer = 0.3f;
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody.linearVelocity.z <= 0.4 && haveStarted)
        {
            isAlive = false;
            _rigidbody.linearVelocity = Vector3.zero;
            UiManager.Instance.SetEndScreenVisibility(true);
        }
        if (_startPressed)
        {
            if (Mathf.Abs(_rigidbody.linearVelocity.z) < 0.2f)
            {
                UiManager.Instance.DisplayStartText(false);
                Propulse();
                haveStarted = true;
                _startPressed = false;
                timer = timeBeforeSpeedLimit;
            }
        }

        if (_rigidbody.linearVelocity.z > _maxSpeed && timer <= 0)
        {
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y, _maxSpeed);
        }
        
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
        EffectsManager.Instance.SpawnSparkles(transform.position + Vector3.forward * 2f);
        LevelManager.Instance.AddCombo();
    }

    private void Calculatescore(float previousPos)
    {
        LevelManager.Instance.AddScore((int)Mathf.Floor(transform.position.z - previousPos));
        this.previousPos = transform.position;
    }

    public void DifficultyIncrease()
    {
        _maxSpeed += maxSpeedDifficultyStep;
        stainForce += stainForceDifficultyStep;
    }

    public void StartGame(float propulsionForce)
    {
        if (!haveStarted)
        {
            _startPressed = true;
            _propulseForce = propulsionForce * _maxPropulseForce;
        }
        
    }

    private void OnMouseDown()
    {
        SfxManager.Instance.PlayDuckQuackSfx();
    }
}