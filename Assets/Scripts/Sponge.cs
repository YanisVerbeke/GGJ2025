using UnityEngine;

public class Sponge : MonoBehaviour
{
    [SerializeField]
    private float cleanFactor = 2f;
    private Camera mainCamera;
    private float cameraZDistance;
    private Vector3 lastPos;

    private float _soapLevel = 1f;

    private Bloc bloc;
    [SerializeField]
    private float soapLevelStep = 1f;



    private void Start()
    {
        mainCamera = Camera.main;
        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        lastPos = transform.position;
        UiManager.Instance.OnToolChanged += UiManager_OnToolChanged;
    }

    private void UiManager_OnToolChanged(object sender, System.EventArgs e)
    {
        if (UiManager.Instance.CurrentTool == UiManager.Tool.SPONGE)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        lastPos = transform.position;
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
        Vector3 newPos = mainCamera.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(newPos.x, 1, newPos.z);

        if (lastPos != transform.position && bloc != null && _soapLevel > 0)
        {
            CleanStain();
        }

        //Partie debug, a enlever apres
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ReloadSponge(0.6f);
        }

    }

    private void CleanStain()
    {
        float normalizedTimedFactor = Mathf.Clamp(cleanFactor * Time.deltaTime, -1, 1);
        bloc.CleanStain(normalizedTimedFactor);
        _soapLevel -= soapLevelStep * Time.deltaTime;
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

    public void ReloadSponge(float normalizedAmount)
    {
        _soapLevel = Mathf.Clamp(_soapLevel + normalizedAmount, 0, 1);
    }
}
