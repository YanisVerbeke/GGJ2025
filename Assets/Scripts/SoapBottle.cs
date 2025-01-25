using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoapBottle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

 
    private bool _isMouseOver = false;
    [SerializeField] Transform _emptyBottle;
    [SerializeField] Transform _fillSoap;
    private Vector3 _baseEmptyBottlePoint;
    private Vector3 _baseFillSoapPoint;
    [SerializeField] private Image _fillSoapImage;
    [SerializeField] private Sponge _sponge;
    [SerializeField] private float _reloadAmount = 1f;

    private bool _isGoingRight = true;
    private bool _isAnimating = false;
    private float _count = 10f;

    private void Start()
    {
        _baseEmptyBottlePoint = new Vector3(_emptyBottle.transform.position.x, _emptyBottle.transform.position.y, _emptyBottle.transform.position.z);
        _baseFillSoapPoint = new Vector3(_fillSoap.transform.position.x, _fillSoap.transform.position.y, _fillSoap.transform.position.z);
    }

    private void Update()
    {

        if (_isMouseOver && !_sponge.GetIsSoapFull())
        {
            _count -= 1f;
            if(_count <= 0)
            {
                _count = 10f;
                _isGoingRight = !_isGoingRight;
                if (!_isAnimating)
                {
                    StartCoroutine(AnimateBottle(_isGoingRight ? 1 : -1));
                }
            }
            _sponge.ReloadSponge(_reloadAmount);

        }
        else
        {
            ResetAnimationPositions();
        }
        _fillSoapImage.fillAmount = 1 - _sponge.GetSoapLevel();

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isMouseOver = false;
    }

    private void ResetAnimationPositions()
    {
        _emptyBottle.position = _baseEmptyBottlePoint;
        _fillSoap.position = _baseFillSoapPoint;
        _isAnimating = false;
    }

    private IEnumerator AnimateBottle(float direction)
    {
        _emptyBottle.position = new Vector3(_emptyBottle.position.x, _emptyBottle.position.y + 2 * direction, _emptyBottle.position.z);
        _fillSoap.position = new Vector3(_fillSoap.position.x + 2 * direction, _fillSoap.position.y, _fillSoap.position.z);
        _isAnimating = true;
        yield return new WaitForSeconds(.1f);
        ResetAnimationPositions();
     }
}
