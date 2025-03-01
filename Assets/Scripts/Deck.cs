using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<Creature> deck = new List<Creature>(); // List of cards in the deck
    List<CardDataB> standardCard = SQLiteDBManager.Instance.GetAllPlayers();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildDeck();
        shuffleDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool buildDeck(){
        // Clear the deck
        deck.Clear();
        // Add cards to the deck
        foreach (var card in standardCard)
        {
            AddCard(card.Name, new string[] {}, card.Attack, card.Defense);
        }

        return true;
    }

    private void AddCard(string v1, string[] strings, int v2, int v3)
    {
        Creature creature = new Creature();
        creature.SetupCard(v1, strings, v2, v3);
        deck.Add(creature);
    }

    public void shuffleDeck(){
        // Shuffle the deck
        System.Random rng = new System.Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Creature value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

}
