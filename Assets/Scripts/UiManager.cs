using System;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    public enum Tool { BLANK, SPONGE, BUBBLE };
    public Tool CurrentTool { get; private set; }

    public event EventHandler OnToolChanged;

    [SerializeField] private TextMeshProUGUI _scoreText;

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
        CurrentTool = Tool.BUBBLE;
        OnToolChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateScoreDistance(int score)
    {
        _scoreText.text = "Distance : " + score;
    }

}
