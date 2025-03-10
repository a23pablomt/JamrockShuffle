using UnityEngine;

public class CardStaple
{
    public string Name { get; set; }
    public string[] Keywords { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    public CardStaple(string name, string[] abilities, int attack, int defense)
    {
        Name = name;
        Keywords = abilities;
        Attack = attack;
        Defense = defense;
    }
    
}
