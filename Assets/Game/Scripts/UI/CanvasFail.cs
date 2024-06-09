using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;
    public void OnEnable()
    {
        LevelManager.Instance.eGameState = EGameState.Fail;
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

    public void AgainButton()
    {
        Close(0);
        LevelManager.Instance.ResetLevel();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
