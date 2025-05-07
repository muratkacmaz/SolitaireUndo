using System.IO;
using UnityEditor;
using UnityEngine;

public class CardPrefabCreator : EditorWindow
{
    private string cardBasePath = "Assets/Prefabs/_cardBase.prefab";
    private string spritesPath = "Assets/2D Cards Game Art Pack/Sprites/Standard 52 Cards/Standard Rounded Cards";

    [MenuItem("Tools/Card Prefab Creator")]
    public static void ShowWindow()
    {
        GetWindow<CardPrefabCreator>("Card Prefab Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Card Prefab Creator", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Card Prefabs"))
        {
            CreateCardPrefabs();
        }
    }

    private void CreateCardPrefabs()
    {
        GameObject cardBasePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(cardBasePath);
        if (cardBasePrefab == null)
        {
            Debug.LogError($"Card base prefab not found at path: {cardBasePath}");
            return;
        }

        string[] spriteFolders = Directory.GetDirectories(spritesPath);
        foreach (string folder in spriteFolders)
        {
            string[] spritePaths = Directory.GetFiles(folder, "*.png");
            foreach (string spritePath in spritePaths)
            {
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
                if (sprite == null) continue;

                GameObject newCard = PrefabUtility.InstantiatePrefab(cardBasePrefab) as GameObject;
                if (newCard == null) continue;

                SpriteRenderer spriteRenderer = newCard.GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = sprite;
                }

                string prefabName = $"{sprite.name}.prefab";
                string savePath = $"Assets/Prefabs/{prefabName}";
                

                PrefabUtility.SaveAsPrefabAsset(newCard, savePath);
                DestroyImmediate(newCard);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Card prefabs created successfully.");
    }
}