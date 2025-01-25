using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoapBottle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] Image _image;
    private bool _isMouseOver = false;
    [SerializeField] private Sponge _sponge;
    [SerializeField] private float _reloadAmount = 1f;


    private void Update()
    {
        if (_isMouseOver)
        {
            _sponge.ReloadSponge(_reloadAmount);
            _image.color = Color.white;
        } else
        {
            _image.color = new Color(Mathf.InverseLerp(1,255,226), Mathf.InverseLerp(1, 255, 78), Mathf.InverseLerp(1, 255, 82), 1);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isMouseOver = false;
    }
}
