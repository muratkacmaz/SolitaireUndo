using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Represents a stack of cards and provides functionality to add, remove, and manage cards.
/// </summary>
public class CardStack : MonoBehaviour
{
    private Stack<Card> _cards = new ();

    [SerializeField] private float cardGap = 0.3f; // Gap between cards in the stack

    private void Start()
    {
        GameState.CardMoveStarted += () => { GetComponent<BoxCollider2D>().enabled = true; };
        GameState.CardMoveEnded += () => { GetComponent<BoxCollider2D>().enabled = false; };
    }

    /// <summary>
    /// Adds a card to the stack and updates its position and sorting order.
    /// </summary>
    /// <param name="card">The card to add.</param>
    /// <param name="moveDuration">The duration of the move animation.</param>
    public void AddCardToStack(Card card, float moveDuration = 0f)
    {
        _cards.Push(card);

        // Update card position and parent
        var cardPosition = transform.position;
        cardPosition.y -= (_cards.Count - 1) * cardGap;
        
        card.transform.DOMove(cardPosition, moveDuration)
            .SetEase(Ease.OutBack);
        card.transform.SetParent(transform);

        // Update sorting order
        var spriteRenderer = card.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = _cards.Count - 1;

        card.SetStack(this);
        GameState.CardMoveEnded?.Invoke();
    }
    
    // Remove the top card from the stack
    public void RemoveCardFromStack(Card card)
    {
        card.SetStack(null);
        var removedCard = _cards.Pop();
        removedCard.transform.SetParent(null); // Detach from stack
        GameState.CardMoveStarted?.Invoke();
    }

    /// <summary>
    /// Peeks at the top card of the stack without removing it.
    /// </summary>
    /// <returns>The top card of the stack, or null if the stack is empty.</returns>
    public Card PeekCard()
    {
        return _cards.Count > 0 ? _cards.Peek() : null;
    }
}