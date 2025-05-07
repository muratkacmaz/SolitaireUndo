using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CardDragController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer CardHighlightRenderer;
    private Vector3 _offset;
    private CardStack _originalStack;
    private SpriteRenderer _cardRenderer;
    private bool _isDragging;
    private Card _card;

    void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _cardRenderer = GetComponent<SpriteRenderer>();
        _card = GetComponent<Card>();
    }

    void OnMouseDown()
    {
        StartDragging();
    }

    private void StartDragging()
    {
        _originalStack = _card.CurrentStack;
        
        // Check if the card is the last card in the stack
        // I know you can grab more than one card in solitaire
        // but I just implemented the logic for one card
        
        if (_originalStack == null || _originalStack.PeekCard() != _card)
        {
            _isDragging = false;
            return;
        }
        
        _originalStack.RemoveCardFromStack(_card);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
        _isDragging = true;
        
        EnableHighlight();
        SetCardSortingOrder(100);
    }

    // Enable the highlight effect on the card
    private void EnableHighlight()
    {
        CardHighlightRenderer.gameObject.SetActive(true);
        CardHighlightRenderer.sortingOrder = 99;
    }

    private void SetCardSortingOrder(int order)
    {
        if (_cardRenderer != null)
            _cardRenderer.sortingOrder = order;
    }

    void OnMouseDrag()
    {
        if (_isDragging)
            DragCard();
    }

    /// <summary>
    /// Updates the card's position while dragging.
    /// </summary>
    private void DragCard()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0) + _offset;
    }

    void OnMouseUp()
    {
        if (_isDragging)
            StopDragging();
    }

    private void StopDragging()
    {
        _isDragging = false;

        DisableHighlight();

        if (!TrySnapToStack())
            _originalStack.AddCardToStack(_card);
    }

    private void DisableHighlight()
    {
        CardHighlightRenderer.gameObject.SetActive(false);
        CardHighlightRenderer.sortingOrder = 0;
    }

    
    /// <summary>
    /// Attempts to snap the card to a valid stack.
    /// </summary>
    /// <returns>True if the card was successfully snapped to a stack, otherwise false.</returns>
    private bool TrySnapToStack()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (var hit in hits)
        {
            CardStack newStack = hit.collider.GetComponent<CardStack>();
            if (newStack == null) continue;
            
            var moveService = ServiceProvider.Instance.Get<MoveService>();
            moveService.RegisterMove(_card, _originalStack, newStack);
            newStack.AddCardToStack(_card);
            return true;
        }
        return false;
    }
}