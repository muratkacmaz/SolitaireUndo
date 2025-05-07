
public class CardMove : IMove
{
    private Card _card;
    private CardStack _sourceStack;
    private CardStack _targetStack;

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

public interface IMove
{
    void Undo();
}