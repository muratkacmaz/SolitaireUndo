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
            var uiService = ServiceProvider.Instance.Get<UIService>();
            
            var errorData = new ErrorPopupData
            {
                ErrorMessage = "No moves to undo"
            };
            
            uiService.LoadPopup("ErrorPopup", errorData);
        }
    }
}