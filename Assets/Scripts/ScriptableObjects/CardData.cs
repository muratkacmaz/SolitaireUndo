using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Card Game/Card Data", order = 1)]
public class CardData : ScriptableObject
{
    public List<CardInfo> Cards = new List<CardInfo>();
}

public enum Suit { Hearts, Diamonds, Clubs, Spades }
public enum CardValue { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }


[System.Serializable]
public class CardInfo
{
    public Sprite Image;
    public Suit Suit;
    public CardValue CardValue;
}