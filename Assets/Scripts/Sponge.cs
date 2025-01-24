using UnityEngine;

public class Sponge : MonoBehaviour
{
    private Camera mainCamera;
    private float cameraZDistance;
    private Vector3 lastPos;

    private float _soapLevel = 1f;

    private Bloc bloc;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        lastPos = transform.position;
    }

    private void Update()
    {
        lastPos = transform.position;
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
        Vector3 newPos = mainCamera.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(newPos.x, 1, newPos.z);

        if (lastPos != transform.position)
        {
            if (bloc != null)
            {
                bloc.CleanStain(0.01f);
            }
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bloc>())
        {
            bloc = other.GetComponent<Bloc>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Bloc>())
        {
            bloc = null;
        }
    }
}
