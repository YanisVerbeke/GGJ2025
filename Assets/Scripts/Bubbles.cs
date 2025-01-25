using UnityEngine;

public class Bubbles : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    float soapQuantityNormalized;

    [SerializeField]
    ParticleSystem _littleBubblesParticules;
    [SerializeField]
    ParticleSystem _bigBubblesParticules;
    [SerializeField]
    ParticleSystem _foamParticules;

    [SerializeField]
    int maxBubbles = 0;
    [SerializeField]
    int maxFoam = 0;

    private float _activeTimer = 0f;

    private void Start()
    {
        ParticleSystem.MainModule littleBubbleMain = _littleBubblesParticules.main;
        ParticleSystem.MainModule bigBubbleMain = _bigBubblesParticules.main;
        littleBubbleMain.maxParticles = maxBubbles;
        bigBubbleMain.maxParticles = maxBubbles;

        ParticleSystem.MainModule foamMain = _foamParticules.main;
        foamMain.maxParticles = maxFoam;
    }

    private void Update()
    {
        _activeTimer -= Time.deltaTime;
    }

    public void UpdateBubbleAmount(float amount)
    {
        soapQuantityNormalized = amount;

        ParticleSystem.EmissionModule littleBubbleEmission = _littleBubblesParticules.emission;
        ParticleSystem.EmissionModule bigBubbleEmission = _bigBubblesParticules.emission;

        littleBubbleEmission.rateOverTime = soapQuantityNormalized * maxBubbles;
        bigBubbleEmission.rateOverTime = soapQuantityNormalized * maxBubbles;

        ParticleSystem.EmissionModule foamEmission = _foamParticules.emission;
        foamEmission.rateOverTime = soapQuantityNormalized * maxFoam;
    }

    public void Play()
    {
        _activeTimer = 0.2f;
        if (!_littleBubblesParticules.isPlaying)
        {
            _littleBubblesParticules.Play();
        }
        if (!_foamParticules.isPlaying)
        {
            _foamParticules.Play();
        }
        if (!_bigBubblesParticules.isPlaying)
        {
            _bigBubblesParticules.Play();
        }
    }

    public void Stop()
    {
        if (_activeTimer <= 0)
        {
            _littleBubblesParticules.Stop();
            _bigBubblesParticules.Stop();
            _foamParticules.Stop();
        }
    }
}
