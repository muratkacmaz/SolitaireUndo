using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// A custom editor window for creating card prefabs from card data.
/// </summary>
public class CardPrefabCreator : EditorWindow
{
    private string prefabSavePath = "Assets/Prefabs/Cards";
    private GameObject cardPrefabTemplate;
    private CardData cardData;

    /// <summary>
    /// Adds a menu item to open the Card Prefab Creator window.
    /// </summary>
    [MenuItem("Tools/Card Prefab Creator")]
    public static void ShowWindow()
    {
        GetWindow<CardPrefabCreator>("Card Prefab Creator");
    }

    /// <summary>
    /// Renders the GUI for the editor window.
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("Card Prefab Creator", EditorStyles.boldLabel);

        cardPrefabTemplate = (GameObject)EditorGUILayout.ObjectField("Card Prefab Template", cardPrefabTemplate, typeof(GameObject), false);
        cardData = (CardData)EditorGUILayout.ObjectField("Card Data", cardData, typeof(CardData), false);

        if (GUILayout.Button("Create Card Prefabs"))
        {
            CreateCardPrefabs();
        }
    }

    /// <summary>
    /// Creates card prefabs using the provided card data and saves them to the specified directory.
    /// </summary>
    private void CreateCardPrefabs()
    {
        if (cardPrefabTemplate == null || cardData == null)
        {
            Debug.LogError("Card Prefab Template or Card Data is missing.");
            return;
        }

        if (!Directory.Exists(prefabSavePath))
        {
            Directory.CreateDirectory(prefabSavePath);
        }

        foreach (var cardInfo in cardData.Cards)
        {
            GameObject cardInstance = Instantiate(cardPrefabTemplate);
            cardInstance.name = $"{cardInfo.Suit}_{cardInfo.CardValue}";

            var cardComponent = cardInstance.GetComponent<Card>();
            if (cardComponent != null)
            {
                cardComponent.SetCardInfo(cardInfo.Suit, cardInfo.CardValue, cardInfo.Image);
            }

            string prefabPath = Path.Combine(prefabSavePath, $"{cardInstance.name}.prefab");
            PrefabUtility.SaveAsPrefabAsset(cardInstance, prefabPath);
            DestroyImmediate(cardInstance);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Card prefabs created and saved to: {prefabSavePath}");
    }
}