using UnityEngine;

public class TrackSection : MonoBehaviour
{
    private TrackPathController trackPathController;
    private int sectionIndex;

    public void Init(TrackPathController parentController, int index)
    {
        trackPathController = parentController;
        sectionIndex = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            trackPathController.CurrentCheckPointIndex = sectionIndex;
        }
    }
}
