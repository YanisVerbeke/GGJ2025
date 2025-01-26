using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    public enum Tool { BLANK, SPONGE, BUBBLE };
    public Tool CurrentTool { get; private set; }

    public event EventHandler OnToolChanged;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private GameObject _startText;

    public event EventHandler OnUltraGigaMaxiBubblesOfDarknessDoomEnabled;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentTool = Tool.BLANK;
    }

    private void Start()
    {
        SetEndScreenVisibility(false);
    }

    public void BlankToolButton()
    {
        CurrentTool = Tool.BLANK;
        OnToolChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SpongeToolButton()
    {
        CurrentTool = Tool.SPONGE;
        OnToolChanged?.Invoke(this, EventArgs.Empty);
    }

    public void BubbleToolButton()
    {
        //CurrentTool = Tool.BUBBLE;
        //OnToolChanged?.Invoke(this, EventArgs.Empty);
        OnUltraGigaMaxiBubblesOfDarknessDoomEnabled?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateScoreDistance(int score)
    {
        _scoreText.text = "Distance : " + score;
    }

    public void RestartButton()
    {
        SfxManager.Instance.PlaySqueakSfx();
        Invoke("LoadGame", 0.3f);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void MenuButton()
    {
        SfxManager.Instance.PlaySqueakSfx();
        Invoke("LoadMenu", 0.3f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void SetEndScreenVisibility(bool active)
    {
        _endScreen.SetActive(active);
    }

    public void DisplayStartText(bool active)
    {
        _startText.SetActive(active);
    }

}
