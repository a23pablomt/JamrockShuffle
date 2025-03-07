using System.Collections.Generic;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;

public class SQLiteDBManager : MonoBehaviour
{
    private static SQLiteDBManager _instance;
    private string dbPath;
    private string connString;

    public static SQLiteDBManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Keep it alive between scenes
            InitializeDatabase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDatabase()
    {
        dbPath = Path.Combine(Application.streamingAssetsPath, "DataBase");
        connString = "URI=file:" + dbPath + ";Mode=ReadOnly";
    }

    public List<CardDataB> GetAllPlayers()
    {
        Dictionary<string, CardDataB> cards = new Dictionary<string, CardDataB>();

        using (var connection = new SqliteConnection(connString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                // SQL Query to fetch cards and their associated keywords
                command.CommandText = @"
                    SELECT c.id, c.name, c.attack, c.life, 
                           GROUP_CONCAT(k.Name, ',') AS Keywords
                    FROM Card c
                    LEFT JOIN card_keywords ck ON c.Id = ck.cardId
                    LEFT JOIN Keyword k ON ck.keywordId = k.Id
                    GROUP BY c.id, c.name, c.attack, c.life;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string cardName = reader["name"].ToString();
                        if (!cards.ContainsKey(cardName))
                        {
                            cards[cardName] = new CardDataB
                            {
                                Name = cardName,
                                Attack = int.Parse(reader["attack"].ToString()),
                                Defense = int.Parse(reader["life"].ToString()),
                                Keywords = reader["keywords"] != DBNull.Value 
                                           ? reader["keywords"].ToString().Split(',') 
                                           : new string[0] // Handle cases where there are no keywords
                            };
                        }
                    }
                }
            }
        }

        return new List<CardDataB>(cards.Values);
    }

    public CardDataB GetCard(int id){
        CardDataB card = new CardDataB();
        using (var connection = new SqliteConnection(connString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT c.id, c.name, c.attack, c.life, 
                           GROUP_CONCAT(k.Name, ',') AS Keywords
                    FROM Card c
                    LEFT JOIN card_keywords ck ON c.Id = ck.cardId
                    LEFT JOIN Keyword k ON ck.keywordId = k.Id
                    WHERE c.id = @id
                    GROUP BY c.id, c.name, c.attack, c.life;";
                command.Parameters.AddWithValue("@id", id);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        card.Name = reader["name"].ToString();
                        card.Attack = int.Parse(reader["attack"].ToString());
                        card.Defense = int.Parse(reader["life"].ToString());
                        card.Keywords = reader["keywords"] != DBNull.Value 
                                        ? reader["keywords"].ToString().Split(',') 
                                        : new string[0]; // Handle cases where there are no keywords
                    }
                }
            }
        return card;
        }
    }
}

// Simple CardDataB class
public class CardDataB
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public string[] Keywords { get; set; }
}
