using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIService : Service
{
    
    //create game view get from addressables
    private void Start()
    {
        var gameViewData = new GameViewData
        {
            GameTitle = "Undo it all",
            MoveCount = 0
        };

        LoadPopup("GameView", gameViewData);
    }
    
    public async UniTask LoadPopup(string popupName, BasePopupData popupData = null)
    {
        var taskCompletionSource = new TaskCompletionSource<GameObject>();
        var asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(popupName);
        asyncOperationHandle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                taskCompletionSource.SetResult(op.Result);
            }
            else
            {
                taskCompletionSource.SetException(new Exception("Failed to load asset"));
            }
        };

        var result = await taskCompletionSource.Task;

        BasePopup popup = Instantiate(result, transform).GetComponent<BasePopup>();
        popup.SetPopupData(popupData);
        popup.transform.localPosition = Vector3.zero;
    }

}