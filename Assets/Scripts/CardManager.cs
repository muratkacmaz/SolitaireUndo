using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> CardPrefabs;
    [SerializeField] private List<CardStack> CardStacks;
    [SerializeField] private float cardGap = 0.3f; // Gap between cards in the stack
    
    private const int INITIALCARDCOUNT = 5; // Number of cards to instantiate per stack
    
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
                var randomCardPrefab = CardPrefabs[Random.Range(0, CardPrefabs.Count)];
                var card = Instantiate(randomCardPrefab, transform);
                var cardComponent = card.GetComponent<Card>();
                stack.AddCardToStack(cardComponent);
            }
        }
    }
    
}