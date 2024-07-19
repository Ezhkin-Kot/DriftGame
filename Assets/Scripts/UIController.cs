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

    [SerializeField] private TextMeshProUGUI countdownText;
    private RectTransform countdownRect;
    private Color countdownColor;
    private float countdownAnimationStep = 0.04f;

    private void Awake()
    {
        UpdateScoreView(0);

        countdownText.gameObject.SetActive(false);
        countdownRect = countdownText.GetComponent<RectTransform>();
        countdownColor = countdownText.color;
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

    public void CountDown(RaceLoop raceLoop)
    {
        StartCoroutine(CountDownAnimation(raceLoop));
    }

    private IEnumerator CountDownAnimation(RaceLoop raceLoop)
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i >= 0; i--)
        {
            countdownRect.localScale = Vector2.one;
            countdownColor = new Color(countdownColor.r, countdownColor.g, countdownColor.b, 0f);
            countdownText.color = countdownColor;

            if (i > 0)
            {
                countdownText.text = i.ToString();
            }
            else
            {
                countdownText.text = "GO!";
            }

            while (countdownRect.localScale.x < 1.5f)
            {
                countdownRect.localScale = new Vector2(countdownRect.localScale.x + countdownAnimationStep, countdownRect.localScale.y + countdownAnimationStep);
                countdownColor = new Color(countdownColor.r, countdownColor.g, countdownColor.b, countdownColor.a + 2 * countdownAnimationStep);
                countdownText.color = countdownColor;

                yield return new WaitForFixedUpdate();
            }

            while (countdownRect.localScale.x < 2f)
            {
                countdownRect.localScale = new Vector2(countdownRect.localScale.x + countdownAnimationStep, countdownRect.localScale.y + countdownAnimationStep);
                countdownColor = new Color(countdownColor.r, countdownColor.g, countdownColor.b, countdownColor.a - 2 * countdownAnimationStep);
                countdownText.color = countdownColor;

                yield return new WaitForFixedUpdate();
            }
        }
        raceLoop.StartRace();
    }
}
