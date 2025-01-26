using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private CanvasGroup menu;
    [SerializeField] private CanvasGroup credits;
    [SerializeField] private Transform bubble;

    private void Awake()
    {
        bubble.localScale = Vector3.zero;
    }

    public void PlayButton()
    {
        SfxManager.Instance.PlaySqueakSfx();
        Invoke("LoadGame", 0.3f);
    }

    public void Credits()
    {
        BubbleTransition(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        BubbleTransition(false);
    }

    private void BubbleTransition(bool credit)
    {
        SfxManager.Instance.PlaySqueakSfx();
        StartCoroutine(Transition(Vector3.zero, new Vector3(1,1,1), .5f, credit));
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    private IEnumerator Transition(Vector3 from, Vector3 to, float speed, bool isCredit)
    {
        float elapsedTime = 0;
        while(elapsedTime < speed)
        {
            float k = elapsedTime / speed;
            bubble.localScale = Vector3.Lerp(from, to, k);
            yield return null;
            elapsedTime += Time.deltaTime;

        }

        bubble.localScale = to;

        if (from == Vector3.zero)
        {
            StartCoroutine(Transition(to, from, speed, isCredit));
        }

        if (isCredit)
        {
            menu.alpha = 0;
            credits.alpha = 1;
        }
        else
        {
            menu.alpha = 1;
            credits.alpha = 0;
        }

       
    }

}
