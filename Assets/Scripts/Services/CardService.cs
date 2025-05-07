using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CardService : Service
{
    [SerializeField] private List<CardStack> CardStacks;
    [SerializeField] private float cardGap = 0.3f; // Gap between cards in the stack
    
    private const int INITIALCARDCOUNT = 3; // Number of cards to instantiate per stack
    private CardData _cardData;
    private GameObject _cardPrefab;
    
    private async void Start()
    {
        await LoadAssetsAsync();
        InitializeCardStacks();
    }

    private async UniTask LoadAssetsAsync()
    {
        _cardData = await Addressables.LoadAssetAsync<CardData>("CardData");
        if (_cardData == null)
            Debug.LogError($"Failed to load CardData");
    }

    private async void InitializeCardStacks()
    {
        foreach (var stack in CardStacks)
        {
            for (var i = 0; i < INITIALCARDCOUNT; i++) 
            {
                var randomCardInfo = _cardData.Cards[Random.Range(0, _cardData.Cards.Count)];
                var card = await Addressables.InstantiateAsync("Card",transform);
                var cardComponent = card.GetComponent<Card>();

                // Assign card info from CardData
                cardComponent.SetCardInfo(randomCardInfo.Suit, randomCardInfo.CardValue, randomCardInfo.Image);

                stack.AddCardToStack(cardComponent);
            }
        }
    }
    
}
