using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] PlayerController controller;
    Player player;

    void OnEnable()
    {
        player = LevelManager.Instance.Player;
        LevelManager.Instance.eGameState = EGameState.GamePlay;
        player.SetController(controller);
        controller.SetJoyStickAlpha(0);
    }

    void OnDisable()
    {
        player.SetController(null);
    }

    public override void SetUp()
    {
        base.SetUp();
        UpdateCoin(0);
    }

    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }
}
