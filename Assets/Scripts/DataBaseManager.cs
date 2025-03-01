using System.Collections.Generic;
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
        dbPath = Path.Combine(Application.streamingAssetsPath, "gameDatabase.db");
        connString = "URI=file:" + dbPath + ";Mode=ReadOnly";
    }

    public List<CardDataB> GetAllPlayers()
    {
        List<CardDataB> players = new List<CardDataB>();

        using (var connection = new SqliteConnection(connString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Name, Score FROM CardDataB";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        players.Add(new CardDataB
                        {
                            Name = reader["Name"].ToString(),
                            Attack = int.Parse(reader["Attack"].ToString()),
                            Defense = int.Parse(reader["Attack"].ToString())
                        });
                    }
                }
            }
        }

        return players;
    }
}

// Simple CardDataB class
public class CardDataB
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
}
