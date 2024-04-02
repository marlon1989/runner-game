using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private MainHUD mainHud;
    [SerializeField] private GameSaver gameSaver;
    [SerializeField] private AudioController audioController;

    [Header("UI Elements")]
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button deleteAllDataButton;
    [SerializeField] private TextMeshProUGUI deleteAllDataButtonText;

    private void OnEnable()
    {
        UpdateUI();
        deleteAllDataButton.interactable = true;
        deleteAllDataButtonText.text = "DELETE TEXT";
    }

    private void OnDisable()
    {
        audioController.SaveAudioPreferences(); ;
    }

    private void UpdateUI()
    {
        mainSlider.value = audioController.MainVolume;
        musicSlider.value = audioController.MusicVolume;
        sfxSlider.value = audioController.SFXVolume;
    }

    public void Close()
    {
        mainHud.ShowHudOverlay();
    }

    public void OnMainVolumeChange(float value)
    {
        audioController.MainVolume = value;
    }

    public void OnMusicVolumeChange(float value)
    {
        audioController.MusicVolume = value;
    }

    public void OnSFXVolumeChange(float value)
    {
        audioController.SFXVolume = value;
    }

    public void DeleteAllData()
    {
        gameSaver.DeleteAllData();
        deleteAllDataButton.interactable = false;
        deleteAllDataButtonText.text = "DELETED!";
    }
}
