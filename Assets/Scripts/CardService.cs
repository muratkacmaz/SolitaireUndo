using System.Collections.Generic;
using UnityEngine;

public class CardManager : Service
{
    [SerializeField] private GameObject CardPrefab;
    [SerializeField] private CardData CardData;
    [SerializeField] private List<CardStack> CardStacks;
    [SerializeField] private float cardGap = 0.3f; // Gap between cards in the stack
    
    private const int INITIALCARDCOUNT = 3; // Number of cards to instantiate per stack
    
    private void Start()
    {
        InitializeCardStacks();
    }

    private void InitializeCardStacks()
    {
        foreach (var stack in CardStacks)
        {
            for (var i = 0; i < INITIALCARDCOUNT; i++) 
            {
                var randomCardInfo = CardData.Cards[Random.Range(0, CardData.Cards.Count)];
                var card = Instantiate(CardPrefab, transform);
                var cardComponent = card.GetComponent<Card>();

                // Assign card info from CardData
                cardComponent.SetCardInfo(randomCardInfo.Suit, randomCardInfo.CardValue, randomCardInfo.Image);

                stack.AddCardToStack(cardComponent);
            }
        }
    }
    
}
