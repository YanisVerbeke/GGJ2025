using UnityEngine;
using UnityEngine.EventSystems;

public class SoapBottle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _isMouseOver = false;
    [SerializeField] private Sponge _sponge;
    private float _reloadAmount = 1f;


    private void Update()
    {
        if (_isMouseOver)
        {
            _sponge.ReloadSponge(_reloadAmount);
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
