using System.Collections.Generic;
using UnityEngine;

public class MoveService : Service
{
    private Stack<IMove> moveStack = new ();

    public void RegisterMove(Card card, CardStack sourceStack, CardStack targetStack)
    {
        var move = new CardMove(card, sourceStack, targetStack);
        
        // Add the move to the stack for undo
        moveStack.Push(move);
    }

    public void UndoLastMove()
    {
        if (moveStack.Count > 0)
        {
            var lastMove = moveStack.Pop();
            lastMove.Undo();
        }
        else
        {
            Debug.LogError("No moves to undo.");
        }
    }
}