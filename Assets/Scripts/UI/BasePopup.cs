using UnityEngine;

public abstract class BasePopup : MonoBehaviour
{ 
    public abstract void SetPopupData(BasePopupData data);
}

public class BasePopup<TPopupData> :  BasePopup where TPopupData : BasePopupData
{
    public TPopupData PopupData;
    public override void SetPopupData(BasePopupData data)
    {
        PopupData = (TPopupData)data;
    }
}