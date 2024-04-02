using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPreferences
{
    public float MainVolume;
    public float MusicVolume;
    public float SFXVolume;
}
public class SaveGameData
{
    public int LastScore;
    public int HighestScore;
    public int TotalCherriesCollected;
}
public class GameSaver : MonoBehaviour
{
    private const string LastScoreKey = "LastScore";
    private const string HighestScoreKey = "HighestScore";
    private const string TotalCherriesCollectedKey = "CherriesCollected";

    private const string MainVolumeKey = "MainVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";
    public SaveGameData CurrentSave { get; private set; }
    public AudioPreferences AudioPreferences { get; private set; }
    private bool IsLoaded => CurrentSave != null && AudioPreferences != null;
    
    public void SaveGame(SaveGameData saveData)
    {
        CurrentSave = saveData;
        PlayerPrefs.SetInt(LastScoreKey, CurrentSave.LastScore);
        PlayerPrefs.SetInt(HighestScoreKey, CurrentSave.HighestScore);
        PlayerPrefs.SetInt(TotalCherriesCollectedKey, CurrentSave.TotalCherriesCollected);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if(IsLoaded)
        {
            return;
        }

        CurrentSave = new SaveGameData
        {
            LastScore = PlayerPrefs.GetInt(LastScoreKey, 0),
            HighestScore = PlayerPrefs.GetInt(HighestScoreKey, 0),
            TotalCherriesCollected = PlayerPrefs.GetInt(TotalCherriesCollectedKey, 0)
        };

        AudioPreferences = new AudioPreferences
        {
            MainVolume = PlayerPrefs.GetFloat(MainVolumeKey, 1),
            MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1),
            SFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1),
        };
    }

    public void SaveAudioPreferences(AudioPreferences preferences)
    {
        AudioPreferences = preferences;

        PlayerPrefs.SetFloat(MainVolumeKey, AudioPreferences.MainVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, AudioPreferences.MusicVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, AudioPreferences.SFXVolume);

        PlayerPrefs.Save();
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        CurrentSave = null;
        AudioPreferences = null;
        LoadGame();
    }
}
