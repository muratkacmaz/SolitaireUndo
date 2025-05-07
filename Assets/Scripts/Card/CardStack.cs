using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    private Stack<Card> _cards = new ();

    [SerializeField] private float cardGap = 0.3f; // Gap between cards in the stack
    public int CardCount => _cards.Count;
    
    private void Start()
    {
        GameState.CardMoveStarted += () => {GetComponent<BoxCollider2D>().enabled = true;};
        GameState.CardMoveEnded += () => {GetComponent<BoxCollider2D>().enabled = false;};
    }

    public void AddCardToStack(Card card)
    {
        _cards.Push(card);

        // Update card position and parent
        var cardPosition = transform.position;
        cardPosition.y -= (_cards.Count - 1) * cardGap;
        card.transform.position = cardPosition;
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