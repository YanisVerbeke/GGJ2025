using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void PlayButton()
    {
        SfxManager.Instance.PlaySqueakSfx();
        Invoke("LoadGame", 0.5f);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(0);
    }


}
