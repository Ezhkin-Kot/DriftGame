using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalScoreScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TotalScore;
    void Start()
    {
        TotalScore.text = DataRepository.Instance.Score.ToString();
    }
    public void FixedUpdate()
    {
        if (DataRepository.Instance.Score == 0)
        {
            TotalScore.text = "0";
        }
    }
}
