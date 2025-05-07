using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    private Stack<Card> _cards = new ();

    [SerializeField] private float cardGap = 0.3f; // Gap between cards in the stack
    
    private void Start()
    {
        GameState.CardMoveStarted += () => {GetComponent<BoxCollider2D>().enabled = true;};
        GameState.CardMoveEnded += () => {GetComponent<BoxCollider2D>().enabled = false;};
    }

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

    public void RemoveCardFromStack(Card card)
    {
        card.SetStack(null);
        var removedCard = _cards.Pop();
        removedCard.transform.SetParent(null); // Detach from stack
        GameState.CardMoveStarted?.Invoke();
    }

    public Card PeekCard()
    {
        return _cards.Count > 0 ? _cards.Peek() : null;
    }

}