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

    private void Start()
    {
        _bubblesParticules.maxParticles = maxBubbles;
        _foamParticules.maxParticles = maxFoam;
    }

    // Update is called once per frame
    void Update()
    {
        _bubblesParticules.emissionRate = (int) (soapQuantityNormalized * maxBubbles);
        _foamParticules.emissionRate = (int)(soapQuantityNormalized * maxFoam);
    }
}
