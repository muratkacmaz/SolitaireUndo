using System.IO;
using UnityEditor;
using UnityEngine;

public class CardSOCreator : EditorWindow
{
    private string spritesPath = "Assets/2D Cards Game Art Pack/Sprites/Standard 52 Cards/Standard Rounded Cards";
    private string savePath = "Assets/Scripts/ScriptableObjects/CardData.asset";
    
    
    [MenuItem("Tools/Card ScriptableObject Creator")]
    public static void ShowWindow()
    {
        GetWindow<CardSOCreator>("Card ScriptableObject Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Card ScriptableObject Creator", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Card ScriptableObject"))
        {
            CreateCardScriptableObject();
        }
    }
    
    private void CreateCardScriptableObject()
    {
        CardData cardData = CreateInstance<CardData>();

        string[] spriteFolders = Directory.GetDirectories(spritesPath);
        foreach (string folder in spriteFolders)
        {
            string folderName = Path.GetFileName(folder);
            if (!System.Enum.TryParse(folderName, out Suit suit)) continue; // Ignore folders not matching Suit enum

            string[] spritePaths = Directory.GetFiles(folder, "*.png");
            foreach (string spritePath in spritePaths)
            {
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
                if (sprite == null) continue;

                string spriteName = Path.GetFileNameWithoutExtension(spritePath);
                string[] nameParts = spriteName.Split('_');
                if (nameParts.Length < 2) continue;

                string cardValueStr = nameParts[1];
                if (!TryParseCardValue(cardValueStr, out CardValue cardValue)) continue;

                cardData.Cards.Add(new CardInfo
                {
                    Image = sprite,
                    Suit = suit,
                    CardValue = cardValue
                });
            }
        }

        AssetDatabase.CreateAsset(cardData, savePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Card ScriptableObject created at: {savePath}");
    }

    private bool TryParseCardValue(string value, out CardValue cardValue)
    {
        switch (value)
        {
            case "A":
                cardValue = CardValue.Ace;
                return true;
            case "J":
                cardValue = CardValue.Jack;
                return true;
            case "Q":
                cardValue = CardValue.Queen;
                return true;
            case "K":
                cardValue = CardValue.King;
                return true;
            default:
                cardValue = (CardValue)0;
                return int.TryParse(value, out int numericValue) && 
                       System.Enum.IsDefined(typeof(CardValue), numericValue) &&
                       (cardValue = (CardValue)numericValue) != 0;
        }
    }
}
