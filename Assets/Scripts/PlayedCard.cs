using System;
using System.Collections;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayedCard : Creature
{

    ZoneController zoneController;
    GameManager gameManager;

    public override void Attack(PlayedCard source)
    {
        if(source == null || attack == 0)
        {
            return;
        }
        StartCoroutine(SmoothMoveToPosition(new Vector3(10f, this.transform.position.y, this.transform.position.z)));
        StartCoroutine(SmoothMoveToPosition(new Vector3(-10f, this.transform.position.y, this.transform.position.z)));
        foreach(Keywords kw in keyword)
        {
            if(kw.ToString() == "Flying" && !source.keyword.Contains(Keywords.SharpSight))
            {
                if(this.transform.parent.tag == "EnemyField")
                {
                    gameManager.playerHp -= attack;
                }
                else
                {
                    gameManager.enemyHp -= attack;
                }
            }
            else if(kw.ToString() == "Overwhelm")
            {
                if(attack > source.health)
                {
                    if(this.transform.parent.tag == "EnemyField")
                    {
                        gameManager.playerHp -= attack - source.health;
                    }
                    else
                    {
                        gameManager.enemyHp -= attack - source.health;
                    }
                }
            }
            
            if(kw.ToString() == "LifeSteal")
            {
                if(this.transform.parent.tag == "EnemyField")
                {
                    gameManager.enemyHp += attack;
                }
                else
                {
                    gameManager.playerHp += attack;
                }
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
                if(this.transform.parent.tag == "EnemyField")
                {
                    gameManager.playerHp -= attack;
                }
                else
                {
                    gameManager.enemyHp -= attack;
                }
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
        transform.localScale = new Vector3(0.67f, 0.67f, 0.67f);
        SmoothMoveToPosition(transform.position);
        Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = health.ToString();
    }

    public IEnumerator SmoothMoveToPosition(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    }

}
