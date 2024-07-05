using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private GameObject FinishWindow;
    [SerializeField] private List<TextMeshProUGUI> scoreValuesText;
    [SerializeField] private TextMeshProUGUI score;

    private void Awake()
    {
        UpdateScoreView(0);
    }

    public void UpdateScoreView(int value)
    {
        score.text = value.ToString();
    }

    private void UpdateFinalScoreView(int finalScore)
    {
        foreach (var scoreText in scoreValuesText)
        {
            scoreText.text = finalScore.ToString();
        }
    }

    public void GameOverShow(int finalScore)
    {
        UpdateFinalScoreView(finalScore);

        GameOverWindow.SetActive(true);
    }

    public void GameOverHide()
    {
        GameOverWindow.SetActive(false);
    }

    public void FinishShow(int finalScore)
    {
        UpdateFinalScoreView(finalScore);

        FinishWindow.SetActive(true);
    }

    public void FinishHide()
    {
        FinishWindow.SetActive(false);
    }
}
