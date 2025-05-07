using UnityEngine;

public class Card : MonoBehaviour
{
    public CardStack CurrentStack => _currentStack;
    private CardStack _currentStack;
    public Suit Suit { get; private set; }
    public CardValue CardValue { get; private set; }
    public Sprite Image { get; private set; }
    
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        GameState.CardMoveEnded += OnCardMoveEnded;
    }

    private void OnCardMoveEnded()
    { 
        if(CurrentStack == null)
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
}