using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SceneLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(DataRepository.SceneNameMenu);
    }

    public void LoadNextTrack()
    {
        if (DataRepository.Instance.CurrentTrackIndex >= DataRepository.TrackSceneNames.Count)
        {
            return;
        }
        SceneManager.LoadScene(DataRepository.TrackSceneNames[++DataRepository.Instance.CurrentTrackIndex]);
    }

    public void LoadCurrentTrack()
    {
        SceneManager.LoadScene(DataRepository.TrackSceneNames[DataRepository.Instance.CurrentTrackIndex]);
        Debug.Log(DataRepository.Instance.CurrentTrackIndex);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(DataRepository.SceneNameTutorial);
    }

    public void LoadGarage()
    {
        SceneManager.LoadScene(DataRepository.SceneNameGarage);
    }

    public static void ResetPrefs()
    {
        DataRepository.Instance.CurrentTrackIndex = 0;
        DataRepository.Instance.Score = 0;
    }
}
