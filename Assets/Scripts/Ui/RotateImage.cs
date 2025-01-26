using System.Collections;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] float _angle;
    private float _direction = -1;
    [SerializeField] Transform _image;
    private float _count = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Rotate(_angle));
    }

    private IEnumerator Rotate(float angle)
    {
        while (true)
        {
            _image.Rotate(0, 0, angle * _direction);
            _count--;
            if( _count == 0)
            {
                _count = 2;
                _direction *= -1;
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
