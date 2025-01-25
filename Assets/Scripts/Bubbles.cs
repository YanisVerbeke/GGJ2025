using UnityEngine;

public class Bubbles : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    float soapQuantityNormalized;

    [SerializeField]
    ParticleSystem _bubblesParticules;
    [SerializeField]
    ParticleSystem _foamParticules;

    [SerializeField]
    int maxBubbles = 0;
    [SerializeField]
    int maxFoam = 0;

    private float _activeTimer = 0f;

    private void Start()
    {
        ParticleSystem.MainModule bubbleMain = _bubblesParticules.main;
        bubbleMain.maxParticles = maxBubbles;
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

        ParticleSystem.EmissionModule bubbleEmission = _bubblesParticules.emission;
        bubbleEmission.rateOverTime = (soapQuantityNormalized * maxBubbles);
        ParticleSystem.EmissionModule foamEmission = _foamParticules.emission;
        foamEmission.rateOverTime = (soapQuantityNormalized * maxFoam);
    }

    public void Play()
    {
        _activeTimer = 0.2f;
        if (!_bubblesParticules.isPlaying)
        {
            _bubblesParticules.Play();
        }
        if (!_foamParticules.isPlaying)
        {
            _foamParticules.Play();
        }
    }

    public void Stop()
    {
        if (_activeTimer <= 0)
        {
            _bubblesParticules.Stop();
            _foamParticules.Stop();
        }
    }
}
