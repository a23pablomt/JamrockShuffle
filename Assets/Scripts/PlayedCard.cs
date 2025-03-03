using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayedCard : Creature
{
    public override void Attack(PlayedCard source)
    {
        foreach(Keywords kw in keyword)
        {
            if(kw.ToString() == "Flying" && !source.keyword.Contains(Keywords.SharpSight))
            {

            }
            else if(kw.ToString() == "Overwhelm")
            {
                if(attack > source.health)
                {
                    
                }
            }
            
            if(kw.ToString() == "LifeSteal")
            {
                
            }
        }
        source.TakeDamage();
    }

    public override void Death()
    {
        foreach(Keywords kw in keyword)
        {
            if(kw.ToString() == "LastWhisper")
            {

            }
        }
        Destroy(gameObject);
    }

    public override void Play()
    {
        foreach(Keywords kw in keyword)
        {
            if(kw.ToString() == "QuickDraw")
            {

            }
        }
    }

    public override void TakeDamage()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
