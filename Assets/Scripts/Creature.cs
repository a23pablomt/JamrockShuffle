using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Creature : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int attack;
    public List<Keywords> keyword;
    public List<Sprite> keywordIcons;
    public Sprite image;
    public string creatureName;

    void Start()
    {
        // Initialize lists to prevent null reference errors
        keyword = new List<Keywords>();
        keywordIcons = new List<Sprite>();

        // Load the creature data
        SetupCard("Paco", Resources.Load<Sprite>("Images/Terence"), new string[] { "Flying", "QuickDraw" }, 1, 1);

        foreach (Transform child in transform)
        {
            if (child.name == "CardName")
            {
                var textComponent = child.GetComponent<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = creatureName;
                }
            }
            else if (child.name == "Artwork")
        {
            var imageComponent = child.GetComponent<Image>();
            if (imageComponent != null)
            {
                if (image == null)
                {
                    Debug.LogError("Failed to load sprite: Images/Terence");
                }
                else
                {
                    imageComponent.sprite = image;
                }
            }
        }
            else if (child.name == "Keywords")
{
    int count = keyword.Count;
    if (count == 0) return; // No keywords, no need to proceed

    // Adjust base spacing dynamically to keep things tighter
    float baseSpacing = 70f;  // Default spacing for small counts
    float minSpacing = 35f;   // Minimum spacing for large amounts of keywords
    float spacing = Mathf.Lerp(baseSpacing, minSpacing, (count - 1) / 5f); // Smooth reduction

    // Reduce total width slightly to keep them closer to the center
    float totalWidth = (count - 1) * spacing * 0.9f; // Slightly compress to improve centering
    float startX = -totalWidth / 2f;

    for (int i = 0; i < count; i++)
    {
        Keywords kw = keyword[i];
        GameObject keywordVisualPrefab = Resources.Load<GameObject>("Prefabs/KeywordVisual");

        if (keywordVisualPrefab != null) // Ensure the prefab exists
        {
            // Instantiate under the "Keywords" child
            GameObject keywordVisualInstance = Instantiate(keywordVisualPrefab, child.transform);

            // Assign sprite to the UI Image component
            var imageComponent = keywordVisualInstance.GetComponent<Image>();
            if (imageComponent != null && i < keywordIcons.Count)
            {
                imageComponent.sprite = keywordIcons[i];
            }

            // Positioning the keywords more centered
            RectTransform rectTransform = keywordVisualInstance.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(startX + i * spacing, 0);
            }
        }
        else
        {
            Debug.LogError("Failed to load prefab: Prefabs/KeywordVisual");
        }
    }
}

            else if (child.name == "Stats")
            {
                foreach (Transform statChild in child)
                {
                    if (statChild.name == "Attack")
                    {
                        var textComponent = statChild.GetComponent<TextMeshProUGUI>();
                        if (textComponent != null)
                        {
                            textComponent.text = attack.ToString();
                        }
                    }
                    else if (statChild.name == "Health")
                    {
                        var textComponent = statChild.GetComponent<TextMeshProUGUI>();
                        if (textComponent != null)
                        {
                            textComponent.text = health.ToString();
                        }
                    }
                }
            }
        }
    }

    public void SetupCard(string name, Sprite artwork, string[] keywords, int attack, int health)
    {
        creatureName = name;
        image = artwork;

        foreach (string kw in keywords)
        {
            keyword.Add((Keywords)Enum.Parse(typeof(Keywords), kw));
            keywordIcons.Add(LoadKeywordSprite(kw));
        }

        this.attack = attack;
        this.health = health;
        maxHealth = health;
    }

    private Sprite LoadKeywordSprite(string keywordName)
    {
        return Resources.Load<Sprite>($"Images/Keywords/{keywordName}"); // Corrected to use the variable
    }
}

   // public abstract void Play();
   // public abstract void Death();
   // public abstract void Attack();
   // public abstract void TakeDamage();

