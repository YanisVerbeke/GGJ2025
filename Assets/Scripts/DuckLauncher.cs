using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DuckLauncher : MonoBehaviour
{
    [SerializeField] GameObject _launcher;
    [SerializeField] UnityEngine.UI.Slider _slider;
    [SerializeField] float cursorMoveSpeed = 1.5f;

    [SerializeField] Duck _duck;

    [SerializeField]
    [Range(0, 1)]
    float _propulseForceNormalized;

    private float _impulse;

    private bool _launched;

    // Update is called once per frame 
    void Update()
    {

        if (!_launched)
        {
            float force = Time.deltaTime * _impulse * cursorMoveSpeed;

            if (_propulseForceNormalized <= 0)
            {
                _impulse = 1;
            }

            if (_propulseForceNormalized >= 1)
            {
                _impulse = -1;
            }

            _propulseForceNormalized += force;
            _slider.value += force;

            if (Input.GetButtonDown("Jump"))
            {
                _launched = true;

                _duck.StartGame(Mathf.Clamp(_propulseForceNormalized, 0.2f, 1f));

                _launcher.SetActive(false);
            }
        }


    }
}








