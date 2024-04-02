using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private GameObject startGameOverlay;
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject settingsOverlay;


    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI cherryCountText;


    private MainHUDAudioController audioController;


    private void Awake()
    {
        ShowHudOverlay();
        audioController = GetComponent<MainHUDAudioController>();
    }

    private void LateUpdate()
    {
        scoreText.text = $"Score: {gameMode.Score}";
        distanceText.text = $"{Mathf.RoundToInt(player.TravelledDistance)}m";
        cherryCountText.text = $"{gameMode.CherriesPicked}";
    }

    public void StartGame()
    {
        gameMode.StartGame();
    }

    public void ResumeGame()
    {
        gameMode.ResumeGame();
        ShowHudOverlay();
    }
    public void PauseGame()
    {
        ShowPauseOverlay();
        gameMode.PauseGame();
    }

    public void ShowStartGameOverlay()
    {
        startGameOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(false);
    }

    public void ShowHudOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(true);
        settingsOverlay.SetActive(false);
    }

    public void ShowPauseOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(true);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(false);
    }

    public void ShowSettingsOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(true);
    }

    public IEnumerator PlayStartGameCountdown(int countdownSeconds)
    {
        ShowHudOverlay();

        countdownText.gameObject.SetActive(false);

        if(countdownSeconds == 0)
        {
            yield break;
        }

        //tempo desde que a gente começou o jogo
        float timeToStart = Time.time + countdownSeconds;
        yield return null;

        countdownText.gameObject.SetActive(true);

        int previousRemainingTime = countdownSeconds;

        while(Time.time <= timeToStart)
        {
            float remainingTime = timeToStart - Time.time;
            int remaingTimeInt = Mathf.FloorToInt(remainingTime);
            countdownText.text = (remaingTimeInt + 1).ToString();

            if(previousRemainingTime != remaingTimeInt)
            {
                audioController.PlayCountdownAudio();
            }

            float percent = remainingTime - remaingTimeInt; // 0-1, diminuir ao longo do tempo
            //1.9999 - 1 -> 0.9999
            //2 (scale 0 ----> 1) -> 1 (scale 0 -----> 1)
            countdownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
            yield return null;
        }

        audioController.PlayCountdownFinishedAudio();

        countdownText.gameObject.SetActive(false);
    }
}
