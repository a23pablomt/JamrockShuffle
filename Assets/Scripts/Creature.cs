using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public abstract class Creature : MonoBehaviour
{
    public CardStaple info;
    public int health;
    public int maxHealth;
    public int attack;
    public List<Keywords> keyword;
    public Sprite image;
    public string creatureName;

    private TextMeshProUGUI nameText;
    private Image artworkImage;
    private Transform keywordsParent;
    private TextMeshProUGUI attackText;
    private TextMeshProUGUI healthText;

    protected void Start()
    {
        CacheUIElements();
        UpdateVisuals();
    }

    private void CacheUIElements()
    {
        foreach (Transform child in transform)
        {
            switch (child.name)
            {
                case "CardName":
                    nameText = child.GetComponent<TextMeshProUGUI>();
                    break;

                case "Artwork":
                    artworkImage = child.GetComponent<Image>();
                    break;

                case "Keywords":
                    keywordsParent = child;
                    break;

                case "Stats":
                    foreach (Transform statChild in child.transform)
                    {
                        if (statChild.name == "Attack")
                        {
                            attackText = statChild.GetComponent<TextMeshProUGUI>();
                        }
                        else if (statChild.name == "Health")
                        {
                            healthText = statChild.GetComponent<TextMeshProUGUI>();
                        }
                    }
                    break;
            }
        }
    }

    protected void UpdateVisuals()
    {
        if (nameText != null)
            nameText.text = creatureName;

        if (artworkImage != null)
        {
            if (image != null)
                artworkImage.sprite = image;
        }

        if (attackText != null)
            attackText.text = attack.ToString();

        if (healthText != null)
            healthText.text = health.ToString();

        PopulateKeywords();
    }

    private void PopulateKeywords()
    {
        if (keywordsParent == null) return;
        
        foreach (Transform child in keywordsParent)
            Destroy(child.gameObject); // Clear existing keyword icons

        float spacing = 30f;
        int keywordCount = keyword.Count;
        if (keywordCount == 0) return;

        float totalWidth = (keywordCount - 1) * spacing;
        float startX = -totalWidth / 2;

        for (int i = 0; i < keywordCount; i++)
        {
            Sprite keywordSprite = LoadKeywordSprite(keyword[i].ToString());
            if (keywordSprite == null) continue;

            GameObject keywordPrefab = Resources.Load<GameObject>("Prefabs/KeywordVisual");
            if (keywordPrefab == null) continue;

            GameObject keywordObject = Instantiate(keywordPrefab, keywordsParent);
            keywordObject.GetComponent<Image>().sprite = keywordSprite;

            RectTransform rectTransform = keywordObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(startX + (i * spacing), 0);
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

        attack = cardStaple.Attack;
        health = cardStaple.Defense;
        maxHealth = health;

        UpdateVisuals(); // Instead of calling Start()
    }

    private Sprite LoadKeywordSprite(string keywordName)
    {
        return Resources.Load<Sprite>($"Images/Keywords/{keywordName}");
    }

    public abstract void Play();
    public abstract void Death();
    public abstract IEnumerator Attack(PlayedCard target, string type);
    public abstract void TakeDamage(int damage);
}
