using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            RaceLoop.Instance.FinishRace();

            if (DataRepository.Instance.CurrentTrackIndex == DataRepository.Instance.MaxOpenedTrackIndex)
            {
                DataRepository.Instance.MaxOpenedTrackIndex++;
            }
            Debug.Log(DataRepository.Instance.CurrentTrackIndex);
        }
    }
}
