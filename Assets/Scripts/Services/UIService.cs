using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Manages the loading and display of UI popups.
/// </summary>
public class UIService : Service
{
    
    //create game view get from addressables
    private void Start()
    {
        var gameViewData = new GameViewData
        {
            GameTitle = "Undo it all"
        };

        LoadPopup("GameView", gameViewData);
    }
    
    
    /// <summary>
    /// Loads and displays a popup with the specified name and data.
    /// </summary>
    /// <param name="popupName">The name of the popup to load.</param>
    /// <param name="data">The data to pass to the popup.</param>
    public async UniTask LoadPopup(string popupName, BasePopupData popupData = null)
    {
        var taskCompletionSource = new TaskCompletionSource<GameObject>();
        var asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(popupName);
        asyncOperationHandle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
                taskCompletionSource.SetResult(op.Result);
            else
                taskCompletionSource.SetException(new Exception("Failed to load asset"));
        };

        var result = await taskCompletionSource.Task;

        BasePopup popup = Instantiate(result, transform).GetComponent<BasePopup>();
        popup.SetPopupData(popupData);
        popup.transform.localPosition = Vector3.zero;
    }

}