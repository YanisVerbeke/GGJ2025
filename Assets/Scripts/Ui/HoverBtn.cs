using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 baseScale;
    float scaleSpeed = .15f;
    [SerializeField] float upscale = 1.04f;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Scale(baseScale, transform.localScale * upscale, scaleSpeed));
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Scale(transform.localScale, baseScale, scaleSpeed));
    }

    private IEnumerator Scale(Vector3 from, Vector3 to, float speed)
    {
        float elapsedTime = 0;
        while (elapsedTime < speed)
        {
            float k = elapsedTime / speed;
            transform.localScale = Vector3.Lerp(from, to, k);
            yield return null;
            elapsedTime += Time.deltaTime;

        }

        transform.localScale = to;
    }
}
