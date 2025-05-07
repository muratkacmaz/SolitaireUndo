using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPopup : BasePopup<ErrorPopupData>
{
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private Button closeButton;

    public override void SetPopupData(BasePopupData data)
    {
        base.SetPopupData(data);
        if (PopupData == null) return;
        
        errorText.text = PopupData.ErrorMessage;
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        Destroy(gameObject);
    }
}

public class ErrorPopupData : BasePopupData
{
    public string ErrorMessage;
}
