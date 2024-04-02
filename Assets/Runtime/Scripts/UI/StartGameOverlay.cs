using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGameOverlay : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI totalCherriesText;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        highestScoreText.text = $"Highest Score\n{gameMode.CurrentSave.HighestScore}";
        lastScoreText.text = $"Last Score\n{gameMode.CurrentSave.LastScore}";
        totalCherriesText.text = $"{gameMode.CurrentSave.TotalCherriesCollected}";
    }

}
