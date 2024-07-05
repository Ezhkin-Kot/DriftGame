using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRepository
{
    private static DataRepository instance;

    public static DataRepository Instance
    {
        get
        {
            instance ??= new DataRepository();
            return instance;
        }
    }

    public const string SceneNameMenu = "MainMenu";
    public const string SceneNameTutorial = "Tutorial";
    public const string SceneNameGarage = "Garage";

    public static List<string> TrackSceneNames = new List<string>()
    {
        "Track_00",
        "Track_01",
        "Track_02"
    };

    public int Score
    {
        get
        {
            return PlayerPrefs.GetInt("Score", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Score", value);
        }
    }

    public int CurrentTrackIndex
    {
        get
        {
            return PlayerPrefs.GetInt("CurrentTrackIndex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("CurrentTrackIndex", value);
        }
    }

    public int MaxOpenedTrackIndex
    {
        get
        {
            return PlayerPrefs.GetInt("MaxOpenedTrackIndex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("MaxOpenedTrackIndex", value);
        }
    }
}
