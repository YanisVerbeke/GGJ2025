using UnityEngine;

public class Sponge : MonoBehaviour
{
    [SerializeField]
    GameObject sponge;
    private Vector3 _dragOrigin;

    private Camera mainCamera;
    private float cameraZDistance;
    private bool isDragging;
    private Vector3 lastPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        lastPos = transform.position;
    }

    private void Update()
    {
        if(lastPos != transform.position)
        {
            isDragging = true;
        }
        else
        {
            isDragging = false; 
        }

        Debug.Log(isDragging + " - "+ lastPos+" - "+ transform.position);

    }

    private void OnMouseDown()
    {
        if(!isDragging) {
            lastPos = transform.position;
        }
    }

    private void OnMouseUp()
    {
        isDragging=false;
        lastPos = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
        Vector3 newPos = mainCamera.ScreenToWorldPoint(screenPosition);
        transform.position = newPos;    
    }
}
