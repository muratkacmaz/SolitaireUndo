using UnityEngine;

/// <summary>
/// Represents a card in the game, including its properties and stack management.
/// </summary>
public class Card : MonoBehaviour
{
    public CardStack CurrentStack => _currentStack;
    public Suit Suit { get; private set; }
    public CardValue CardValue { get; private set; }
    public Sprite Image { get; private set; }

    private BoxCollider2D _boxCollider;
    private CardStack _currentStack;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        GameState.CardMoveEnded += OnCardMoveEnded;
    }

    //Sets the card information at the start
    public void SetCardInfo(Suit suit, CardValue cardValue, Sprite image)
    {
        Suit = suit;
        CardValue = cardValue;
        Image = image;

        // Set the sprite of the card
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = image;
        }
    }
    
    private void OnCardMoveEnded()
    {
        if (CurrentStack == null)
            return;

        _boxCollider.enabled = CurrentStack.PeekCard() == this;
    }

    public void SetStack(CardStack cardStack)
    {
        _currentStack = cardStack;
    }

    private void OnDestroy()
    {
        GameState.CardMoveEnded -= OnCardMoveEnded;
    }
}