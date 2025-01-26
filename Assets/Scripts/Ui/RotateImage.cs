using System.Collections;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private float _maxRotate= 20;
    [SerializeField] float _angle;
    [SerializeField] private bool _rightDirection = true;
    [SerializeField] Transform _image;
    private float _count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _count = _maxRotate / 2;
        StartCoroutine(Rotate(_angle));
    }

    private IEnumerator Rotate(float angle)
    {
        while (true)
        {
            _image.Rotate(0, 0, angle * (_rightDirection ? 1: -1));
            _count--;
            if( _count == 0)
            {
                _count = _maxRotate;
                _rightDirection = !_rightDirection;
            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
