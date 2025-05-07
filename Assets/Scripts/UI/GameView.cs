using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : BasePopup<GameViewData>
{
    [SerializeField] Button UndoButton;
    [SerializeField] TextMeshProUGUI GameTitleText;

    public override void SetPopupData(BasePopupData data)
    {
        base.SetPopupData(data);
        if (PopupData == null) return;
        
        GameTitleText.text = PopupData.GameTitle;
        UndoButton.onClick.AddListener(OnUndoButtonClicked);
    }

    private void OnUndoButtonClicked()
    {
        var moveService = ServiceProvider.Instance.Get<MoveService>();
        moveService.UndoLastMove();
    }
}

public class GameViewData : BasePopupData
{
    public string GameTitle;
}