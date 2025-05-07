using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public CardStack CurrentStack => _currentStack;
    private CardStack _currentStack;

    public void SetStack(CardStack cardStack)
    {
        _currentStack = cardStack;
    }
}