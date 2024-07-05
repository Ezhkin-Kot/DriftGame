using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceLoop : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private CarMovementController carMovementController;
    [SerializeField] private UIController _UIController;

    public int Score;

    public static RaceLoop Instance;

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
    }

    private void Start()
    {
        _UIController.FinishHide();
        _UIController.GameOverHide();
        StartRace();
    }

    private void StartRace()
    {
        inputController.IsActive = true;
        carMovementController.IsActive = true;
    }

    public void FinishRace()
    {
        Debug.Log("Race finish");

        CalculateResults();

        inputController.IsActive = false;
        carMovementController.IsActive = false;
        _UIController.FinishShow(Score);
    }

    private void CalculateResults()
    {
        DataRepository.Instance.Score += Score;
        Debug.Log("Total score" + DataRepository.Instance.Score);
    }
}
