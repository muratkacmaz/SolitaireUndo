
public class CardMove : IMove
{
    private Card _card;
    private CardStack _sourceStack;
    private CardStack _targetStack;

    /// <summary>
    /// Initializes a new instance of the CardMove class.
    /// </summary>
    public CardMove(Card card, CardStack sourceStack, CardStack targetStack)
    {
        _card = card;
        _sourceStack = sourceStack;
        _targetStack = targetStack;
    }
    
    public void Undo()
    {
        _targetStack.RemoveCardFromStack(_card);
        _sourceStack.AddCardToStack(_card, 1.2f);
    }
}

/// <summary>
/// Interface for defining move actions with undo functionality.
///
/// This interface can be improved by adding more methods for different types of moves.
///  Execute, Redo, etc.
/// </summary>
public interface IMove
{
    void Undo();
}