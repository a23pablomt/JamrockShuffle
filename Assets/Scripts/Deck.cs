using System;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck Instance { get; private set; } // Singleton instance

    private List<CardStaple> deck; // List of cards in the deck
    private List<CardStaple> fulldeck; 
    private List<CardDataB> standardCard;

    private void Awake()
    {
        deck = new List<CardStaple>(); // Initialize the deck list

        // Singleton pattern: Ensures only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Ensure SQLiteDBManager is ready
        if (SQLiteDBManager.Instance == null)
        {
            Debug.LogError("SQLiteDBManager.Instance is null! Ensure it exists before Deck.");
            return;
        }

        // Load cards from database
        standardCard = SQLiteDBManager.Instance.GetAllPlayers();
        if (standardCard == null || standardCard.Count == 0)
        {
            Debug.LogError("Failed to load standard cards from database.");
            return;
        }

        buildDeck();
        fulldeck = deck;
        shuffleDeck();
    }

    public bool buildDeck()
    {
        deck.Clear();
        foreach (var card in standardCard)
        {
            AddCard(card.Name, card.Keywords, card.Attack, card.Defense);
        }
        return true;
    }

    private void AddCard(string name, string[] abilities, int attack, int defense)
    {
        CardStaple cardStaple = new CardStaple(name, abilities, attack, defense);
        deck.Add(cardStaple);
    }

    public void shuffleDeck()
    {
        System.Random rng = new System.Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (deck[n], deck[k]) = (deck[k], deck[n]); // Modern swap syntax
        }
    }

    public CardStaple drawCard()
    {
        if (deck.Count > 0)
        {
            CardStaple card = deck[0];
            Debug.Log("Drawn card keywords: " + string.Join(", ", card.Keywords));
            deck.RemoveAt(0);
            return card;
        }
        return null;
    }

    public void ResetDeck()
    {
        deck = fulldeck;
        shuffleDeck();
    }
}