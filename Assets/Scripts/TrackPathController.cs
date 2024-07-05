using System.Collections.Generic;
using UnityEngine;

public class TrackPathController : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoints;
    private List<TrackSection> sections = new List<TrackSection>();
    private int currentCheckPointIndex;
    public int CurrentCheckPointIndex
    {
        get
        {
            return currentCheckPointIndex;
        }
        set
        {
            if (value < currentCheckPointIndex) return;

            Debug.Log("New checkpoint = " + value);
            currentCheckPointIndex = value;
            if (value >= 0 && value < checkPoints.Length - 1)
            {
                spawnPoint.transform.position = checkPoints[Mathf.Max(0, value - 1)].position;
                spawnPoint.transform.rotation = checkPoints[Mathf.Max(0, value - 1)].rotation;
            }
        }
    }

    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            sections.Add(checkPoints[i].GetComponent<TrackSection>());
        }
        for (int i = 0; i < sections.Count; i++)
        {
            sections[i].Init(this, i);
        }
    }
}
