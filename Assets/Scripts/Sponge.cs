using UnityEngine;

public class Sponge : MonoBehaviour
{
    [SerializeField]
    private float cleaningAmount = 2f;
    private Camera mainCamera;
    private float cameraZDistance;
    private Vector3 lastPos;

    private float _soapLevel = 1f;

    private Stain _stain;
    private float _soapConsumption = 0.2f;

    private Bubbles _bubbles;

    private void Awake()
    {
        _bubbles = GetComponentInChildren<Bubbles>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        lastPos = transform.position;
        //UiManager.Instance.OnToolChanged += UiManager_OnToolChanged;
    }

    /*private void UiManager_OnToolChanged(object sender, System.EventArgs e)
    {
        if (UiManager.Instance.CurrentTool == UiManager.Tool.SPONGE)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }*/

    private void Update()
    {
        lastPos = transform.position;
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
        Vector3 newPos = mainCamera.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(newPos.x, 0, newPos.z);

        if (lastPos != transform.position && _stain != null && _soapLevel > 0)
        {
            CleanStain();
            _bubbles.Play();
        }
        else
        {
            _bubbles.Stop();
        }

        _bubbles.UpdateBubbleAmount(_soapLevel);

        //Partie debug, a enlever apres
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ReloadSponge(0.6f);
        }
    }

    private void CleanStain()
    {
        _stain.CleanStain(cleaningAmount);
        _soapLevel = Mathf.Clamp(_soapLevel - (_soapConsumption * Time.deltaTime), 0, 1);
    }

    public void ReloadSponge(float reloadAmount)
    {
        _soapLevel = Mathf.Clamp(_soapLevel + (reloadAmount * Time.deltaTime), 0, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Stain>())
        {
            _stain = other.GetComponent<Stain>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Stain>())
        {
            _stain = null;
        }
    }
}
