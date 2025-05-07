
public class CardMove : IMove
{
    private Card _card;
    private CardStack _sourceStack;
    private CardStack _targetStack;

    /// <summary>
    /// Initializes a new instance of the CardMove class.
    /// </summary>
    /// <param name="card">The card to move.</param>
    /// <param name="sourceStack">The source stack of the card.</param>
    /// <param name="targetStack">The target stack of the card.</param>
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