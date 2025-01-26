using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance { get; private set; }

    private AudioSource _audioSourceShot;
    private AudioSource _audioSourceLoop;
    private AudioSource _audioSourceQuack;

    [SerializeField] private AudioClip _StainCleanedSfx;
    [SerializeField] private AudioClip _DuckSpinSfx;
    [SerializeField] private AudioClip _DuckQuackSfx;
    [SerializeField] private AudioClip _SpongeSfx;
    [SerializeField] private AudioClip _SoapRefillSfx;
    [SerializeField] private AudioClip _SqueakSfx;
    [SerializeField] private AudioClip _honk;
    [SerializeField] private AudioClip _bidoof;

    private float _durationCooldown;
    private bool _isPlayingLoopSound;
    public bool Bidoof { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _audioSourceShot = GetComponents<AudioSource>()[0];
        _audioSourceLoop = GetComponents<AudioSource>()[1];
        _audioSourceQuack = GetComponents<AudioSource>()[2];
    }

    private void Update()
    {
        if (_isPlayingLoopSound)
        {
            if (_durationCooldown > 0f)
            {
                _durationCooldown -= Time.deltaTime;
            }
            else
            {
                _isPlayingLoopSound = false;
                _audioSourceLoop.Stop();
                _audioSourceLoop.clip = null;
            }
        }
    }


    public void PlayStainCleanedSfx()
    {
        _audioSourceShot.PlayOneShot(_StainCleanedSfx);
    }

    public void PlayDuckSpinSfx()
    {
        _audioSourceShot.PlayOneShot(_DuckSpinSfx);
    }

    public void PlayDuckQuackSfx()
    {
        if (Random.Range(0, 50) == 0)
        {
            _audioSourceQuack.pitch = 0.2f;
            if (Bidoof)
            {
                _audioSourceQuack.PlayOneShot(_bidoof);
            }
            else
            {
                _audioSourceQuack.PlayOneShot(_DuckQuackSfx);
            }
        }
        else if (Random.Range(0, 20) == 0)
        {
            _audioSourceQuack.pitch = 1;
            _audioSourceQuack.PlayOneShot(_honk);
        }
        else
        {
            _audioSourceQuack.pitch = Random.Range(0.8f, 1.2f);
            if (Bidoof)
            {
                _audioSourceQuack.PlayOneShot(_bidoof);
            }
            else
            {
                _audioSourceQuack.PlayOneShot(_DuckQuackSfx);
            }
        }
    }

    public void PlaySpongeSfx()
    {
        _durationCooldown = 0.05f;
        if (!_audioSourceLoop.isPlaying && _audioSourceLoop.clip != _SpongeSfx)
        {
            _isPlayingLoopSound = true;
            _audioSourceLoop.clip = _SpongeSfx;
            _audioSourceLoop.loop = true;
            _audioSourceLoop.Play();
        }
    }

    public void PlaySoapRefillSfx()
    {
        _durationCooldown = 0.05f;
        if (!_audioSourceLoop.isPlaying && _audioSourceLoop.clip != _SoapRefillSfx)
        {
            _isPlayingLoopSound = true;
            _audioSourceLoop.clip = _SoapRefillSfx;
            _audioSourceLoop.loop = true;
            _audioSourceLoop.Play();
        }
    }

    public void PlaySqueakSfx()
    {
        _audioSourceShot.PlayOneShot(_SqueakSfx);
    }
}
