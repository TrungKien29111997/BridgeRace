using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;
    //public void OnEnable()
    //{
    //    LevelManager.Instance.eGameState = EGameState.Victory;
    //}
    public override void SetUp()
    {
        base.SetUp();
        LevelManager.Instance.eGameState = EGameState.Victory;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void MainMenuButton()
    {
        Close(0);
        LevelManager.Instance.ResetLevel();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
    public void NextButton()
    {
        Close(0);
        LevelManager.Instance.NextLevel();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
