using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// Manages card-related operations, including initialization and stack management.
/// </summary>
public class CardService : Service
{
    [SerializeField] private List<CardStack> CardStacks;
    [SerializeField] private float cardGap = 0.3f; // Gap between cards in the stack
    
    private const int INITIALCARDCOUNT = 2; // Number of cards to instantiate per stack
    private CardData _cardData;
    private GameObject _cardPrefab;
    
    private async void Start()
    {
        await LoadAssetsAsync();
        InitializeCardStacks();
    }

    /// <summary>
    /// Loads card data assets asynchronously.
    /// </summary>
    private async UniTask LoadAssetsAsync()
    {
        _cardData = await Addressables.LoadAssetAsync<CardData>("CardData");
        if (_cardData == null)
            Debug.LogError($"Failed to load CardData");
    }

    /// <summary>
    /// Initializes the card stacks with random cards.
    /// </summary>
    private async void InitializeCardStacks()
    {
        foreach (var stack in CardStacks)
        {
            for (var i = 0; i < INITIALCARDCOUNT; i++) 
            {
                var randomCardInfo = _cardData.Cards[Random.Range(0, _cardData.Cards.Count)];
                var card = await Addressables.InstantiateAsync("Card", transform);
                var cardComponent = card.GetComponent<Card>();

                // Assign card info from CardData
                cardComponent.SetCardInfo(randomCardInfo.Suit, randomCardInfo.CardValue, randomCardInfo.Image);

                stack.AddCardToStack(cardComponent);
            }
        }
    }
}