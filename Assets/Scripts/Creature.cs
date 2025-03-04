using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Creature : MonoBehaviour
{
    public CardStaple info;
    public int health;
    public int maxHealth;
    public int attack;
    public List<Keywords> keyword;
    public Sprite image;
    public string creatureName;

    protected void Start()
    {

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
                        Debug.Log("Failed to load sprite: Images/Terence");
                    }
                    else
                    {
                        imageComponent.sprite = image;
                    }
                }
            }
            else if (child.name == "Keywords")
            {
                float spacing = 40f; // Adjusted to reduce spacing
                int keywordCount = keyword.Count;
                
                if (keywordCount == 0) return; // Exit if there are no keywords

                float totalWidth = (keywordCount - 1) * spacing; // Total space occupied by all keywords
                float startX = -totalWidth / 2; // Center starting position

                int index = 0; // Track position in loop

                foreach (Keywords kw in keyword)
                {
                    var keywordSprite = LoadKeywordSprite(kw.ToString());
                    if (keywordSprite == null)
                        continue;

                    GameObject keywordPrefab = Resources.Load<GameObject>("Prefabs/KeywordVisual");
                    if (keywordPrefab == null)
                        continue;

                    GameObject keywordObject = Instantiate(keywordPrefab, child.transform);
                    if (keywordObject == null)
                        continue;

                    Image imageComponent = keywordObject.GetComponent<Image>();
                    if (imageComponent != null)
                    {
                        imageComponent.sprite = keywordSprite;
                    }

                    // Apply positioning to center-align the keywords
                    RectTransform rectTransform = keywordObject.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        rectTransform.anchoredPosition = new Vector2(startX + (index * spacing), 0);
                    }

                    index++; // Move to the next keyword
                }
            }
            else if (child.name == "Stats")
            {
                foreach (Transform statChild in child.transform)
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

    public void SetupCard(CardStaple cardStaple)
    {
        info = cardStaple;
        creatureName = cardStaple.Name;
        image = Resources.Load<Sprite>($"Images/Artwork/{cardStaple.Name}");
        keyword = new List<Keywords>();

        foreach (string kw in cardStaple.Keywords)
        {
            if (Enum.TryParse(kw, out Keywords parsedKeyword))
            {
            keyword.Add(parsedKeyword);
            }
            else
            {
            Debug.LogWarning($"Keyword '{kw}' could not be parsed.");
            }
        }

        this.attack = cardStaple.Attack;
        this.health = cardStaple.Defense;
        maxHealth = health;

        this.Start();
    }

    private Sprite LoadKeywordSprite(string keywordName)
    {
        return Resources.Load<Sprite>($"Images/Keywords/{keywordName}");
    }
    public abstract void Play();
    public abstract void Death();
    public abstract void Attack(PlayedCard target);
    public abstract void TakeDamage();
}
