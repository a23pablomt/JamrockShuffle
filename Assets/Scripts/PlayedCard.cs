using System;
using System.Collections;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using Unity.VisualScripting;
using UnityEngine;

public class PlayedCard : Creature
{

    ZoneController zoneController;

    public override void Attack(PlayedCard source)
    {
        if(source == null)
        {
            return;
        }
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
        source.TakeDamage(attack);
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
                zoneController.AddCardSlot();
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        this.health -= damage;
        if(health <= 0)
        {
            Death();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        zoneController = GameObject.FindGameObjectWithTag("CardZone").GetComponent<ZoneController>();
        transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
        SmoothMoveToPosition(transform.position);
        Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SmoothMoveToPosition(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    }

}
