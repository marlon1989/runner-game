using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameSaver gameSaver;

    [Header("Player")]
    [SerializeField] PlayerController player;
    [SerializeField] PlayerAnimationController playerAnimationController;

    [Header("UI")]
    [SerializeField] private MainHUD mainHud;

    [SerializeField] private MusicPlayer musicPlayer;

    [SerializeField] private float reloadGameDelay = 3;

    [Header("Gameplay")]
    [SerializeField] private float startPlayerSpeed = 10;
    [SerializeField] private float maxPlayerSpeed = 20;
    [SerializeField] private float timeToMaxSpeedSeconds = 5 * 60;

    [SerializeField]
    [Range(0, 5)]
    private int startGameCountdown = 5;

    [Header("Score")]
    [SerializeField] private float baseScoreMultiplier = 1;

    private float score;
    public SaveGameData CurrentSave => gameSaver.CurrentSave;
    public int Score => Mathf.RoundToInt(score);
    public int CherriesPicked { get; private set; }
    private float startGameTime;
    private bool isGameRunning = false;

    private void Awake()
    {
        gameSaver.LoadGame();
        SetWaitForStartGameState();
    }

    private void Update()
    {
        if(isGameRunning)
        {
            float timePercent = (Time.time - startGameTime) / timeToMaxSpeedSeconds;
            player.ForwardSpeed = Mathf.Lerp(startPlayerSpeed, maxPlayerSpeed, timePercent);
            float extraScoreMultiplier = 1 + timePercent;
            score += baseScoreMultiplier * extraScoreMultiplier * player.ForwardSpeed * Time.deltaTime;
        }
    }

    private void SetWaitForStartGameState()
    {
        player.enabled = false;
        isGameRunning = false;
        mainHud.ShowStartGameOverlay();
        musicPlayer.PlayStartMenuMusic();
    }

    public void OnGameOver()
    {
        isGameRunning = false;
        player.ForwardSpeed = 0;

        gameSaver.SaveGame(new SaveGameData
        {
            HighestScore = score > gameSaver.CurrentSave.HighestScore ? Score : gameSaver.CurrentSave.HighestScore,
            LastScore = Score,
            TotalCherriesCollected = gameSaver.CurrentSave.TotalCherriesCollected + CherriesPicked
        });

        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        //esperar uma frame
        yield return new WaitForSeconds(1);
        musicPlayer.PlayGameOverMusic();
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        //diretivas de compilação
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnCherryPickedUp()
    {
        CherriesPicked++;
    }

    private IEnumerator StartGameCor()
    {
        musicPlayer.PlayMainTrackMusic();
        yield return StartCoroutine(mainHud.PlayStartGameCountdown(startGameCountdown));
        yield return StartCoroutine(playerAnimationController.PlayStartGameAnimation());

        player.enabled = true;
        player.ForwardSpeed = startPlayerSpeed;
        startGameTime = Time.time;
        isGameRunning = true;
        playerAnimationController.PlayStartGameAnimation();
    }
}
