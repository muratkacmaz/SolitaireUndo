using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CardDragController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer CardHighlightRenderer;
    private Vector3 _offset;
    private Vector3 _originalPosition;
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
        _originalPosition = transform.position;
        _originalStack = _card.CurrentStack;
        
        // Check if the card is the last card in the stack
        if (_originalStack != null && _originalStack.PeekCard() == _card)
        {
            _originalStack.RemoveCardFromStack(_card);
        }
        else
        {
            _isDragging = false;
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
        _isDragging = true;
        
        EnableHighlight();
        SetCardSortingOrder(100);
    }

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

    private bool TrySnapToStack()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (var hit in hits)
        {
            CardStack newStack = hit.collider.GetComponent<CardStack>();
            if (newStack == null) continue;
            newStack.AddCardToStack(_card);
            return true;
        }
        return false;
    }
}